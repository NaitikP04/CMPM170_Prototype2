using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab to shoot
    public float projectileSpeed = 10f;  // Speed of the projectile
    public Camera mainCamera;            // Reference to the camera

    void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform a raycast to find where the mouse is pointing in the 3D world
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 shootDirection = (hit.point - transform.position).normalized;

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            // Set the projectile's velocity
            rb.linearVelocity = shootDirection * projectileSpeed;
        }
    }
}
