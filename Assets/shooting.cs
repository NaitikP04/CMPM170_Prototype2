using UnityEngine;
using static PlayerController;

public class ShootingController : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab to shoot
    public float projectileSpeed = 10f;  // Speed of the projectile
    public Camera mainCamera;            // Reference to the camera

    public GameObject normalPlatformPrefab; // The prefab to spawn when hitting the specified tag
    public GameObject jumpPadPlatformPrefab;
    public GameObject chosenPlatform;
    public PlatformType platformName;

    public PlayerController playerController;

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
            platformName = playerController.equippedPlatformType;
            ChoosePlatform(platformName);
            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            DefaultBulletScript projectileScript = projectile.GetComponent<DefaultBulletScript>();
            projectileScript.spawnPlatform = chosenPlatform;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            // Set the projectile's velocity
            rb.linearVelocity = shootDirection * projectileSpeed;
        }
    }

    void ChoosePlatform(PlatformType Name)
    {
        switch (Name)
        {
            case PlayerController.PlatformType.jumpPad:
                chosenPlatform = jumpPadPlatformPrefab;
                break;
            default:
                chosenPlatform = normalPlatformPrefab;
                break;
        }
    }
}
