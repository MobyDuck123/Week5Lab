using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float shootingDuration = 30f;

    private bool canShoot = false;
    private float shootingTimer = 0f;

    // Reference to the spawn point transform
    public Transform spawnPoint;

    // Called when the player collides with a trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the "PowerUp" tag
        if (other.CompareTag("PowerUp"))
        {
            Debug.Log("Power Up hit");
            // Activate shooting behavior
            canShoot = true;

            // Reset the shooting timer
            shootingTimer = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if shooting is allowed
        if (canShoot)
        {
            // Check if the shooting duration has not elapsed
            if (shootingTimer < shootingDuration)
            {
                // Shoot projectile if the spacebar is pressed
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ShootProjectile();
                }

                // Increment the shooting timer using Time.deltaTime
                shootingTimer += Time.deltaTime;
            }
            else
            {
                // Deactivate shooting behavior when the duration elapses
                canShoot = false;
            }
        }
    }

    // Shoot projectile towards the enemy
    void ShootProjectile()
    {
        // Check if the spawn point is assigned
        if (spawnPoint != null)
        {
            // Instantiate a projectile at the spawn point's position
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

            // Find the enemy with the "Enemy" tag
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

            // Check if an enemy is found
            if (enemy != null)
            {
                // Use LookAt to make the projectile face the target
                projectile.transform.LookAt(enemy.transform);

                // Calculate the direction to the target
                Vector3 direction = (enemy.transform.position - projectile.transform.position).normalized;

                // Use ForceMode.Impulse to apply force to the projectile
                projectile.GetComponent<Rigidbody>().AddForce(direction * projectileSpeed, ForceMode.Impulse);

                
            }
            else
            {
                // Handle the case when there's no enemy
                Destroy(projectile);
            }
        }
        else
        {
            Debug.LogError("Spawn point is not assigned!");
        }
    }
}