using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpawnManager sm;
    public GameObject powerupIndicator;
    public bool hasPowerup;
    public float speed = 5.0f;
    float powerupStrength = 15.0f;
    Rigidbody playerRb;
    GameObject focalPoint;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward
            * speed * forwardInput);
        powerupIndicator.transform.position
            = transform.position
            + new Vector3(0, -0.5f, 0);
        if (transform.position.y < -10)
        {
            sm.isGameOver = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("POWERUP"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
            powerupIndicator.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ENEMY")
            && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject
                .GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =
                (collision.gameObject.transform.position
                 - transform.position);

            Debug.Log("Collided with "
                + collision.gameObject.name
                + " with powerup set to "
                + hasPowerup);
            enemyRb.AddForce(awayFromPlayer * powerupStrength,
                ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(7f);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}
