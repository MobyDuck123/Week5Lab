using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;

    public PowerUpType currentPowerUpType = PowerUpType.None;
    public GameObject bulletPrefab;

    private bool hasPowerUp = false;
    private float powerupStrength = 15.0f;

    private GameObject tempBullet;
    private Coroutine powerUpCountDown;
    private GameObject FocalPoint;

    public GameObject powerUpIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        FocalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(FocalPoint.transform.forward * speed * forwardInput);

        if (currentPowerUpType == PowerUpType.Bullet && Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullets();
        }

        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            currentPowerUpType = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);

            if (powerUpCountDown != null)
            {
                StopCoroutine(powerUpCountDown);
            }
            powerUpCountDown = StartCoroutine(PowerUpCountDownRoutine());
            powerUpIndicator.gameObject.SetActive(true);
        }


        if (other.CompareTag("PowerUp2"))
        {
            hasPowerUp = true;
            currentPowerUpType = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);

            if (powerUpCountDown != null)
            {
                StopCoroutine(powerUpCountDown);
            }
            powerUpCountDown = StartCoroutine(PowerUpCountDownRoutine2());
        }
    }



    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        currentPowerUpType = PowerUpType.None;
        powerUpIndicator.gameObject.SetActive(false);
    }

    IEnumerator PowerUpCountDownRoutine2()
    {
        yield return new WaitForSeconds(5);
        hasPowerUp = false;
        currentPowerUpType = PowerUpType.None;
    }

    private void ShootBullets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tempBullet = Instantiate(bulletPrefab, transform.position + Vector3.up, Quaternion.identity);
            tempBullet.GetComponent<Bullet>().Shoot(enemy.transform);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)  
        { 
         Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("Player Collided With" + collision.gameObject.name + "with powerup set to" + hasPowerUp);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }


    }

}