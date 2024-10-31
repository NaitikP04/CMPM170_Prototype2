using UnityEngine;

public class DefaultBulletScript : MonoBehaviour
{
    /*public GameObject normalPlatformPrefab; // The prefab to spawn when hitting the specified tag
    public GameObject bouncyPlatformPrefab;
    public GameObject jumpPadPlatformPrefab;
    public GameObject Player;*/
    public GameObject spawnPlatform;
    public string targetTag; // The tag to check for

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        //Debug.Log("Bullet");
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the specified tag
        if (other.CompareTag(targetTag))
        {
            // Get the impact point and the normal of the surface
            Vector3 impactPoint = other.ClosestPointOnBounds(transform.position);
            Vector3 normal = Vector3.up; // Default normal

            // If the other collider is a BoxCollider, you can calculate the normal from its mesh
            if (other is BoxCollider)
            {
                BoxCollider boxCollider = other as BoxCollider;
                normal = boxCollider.transform.up; // Adjust this based on your mesh's normal
            }

            // Access the equipped platform type from the PlayerController script on the Player
            /*PlayerController playerController = Player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                SpawnPlatform(impactPoint, normal, playerController.equippedPlatformType);
            }*/
            SpawnPlatform(impactPoint, normal, spawnPlatform);
            Destroy(gameObject); // Destroy the bullet after it hits
        }
    }

    void SpawnPlatform(Vector3 position, Vector3 normal, GameObject platformType)
    {
        /*GameObject platformPrefab;

        // Select the appropriate prefab based on the equipped platform type
        switch (platformType)
        {
            case PlayerController.PlatformType.Bouncy:
                platformPrefab = bouncyPlatformPrefab;
                break;
            case PlayerController.PlatformType.jumpPad:
                platformPrefab = jumpPadPlatformPrefab;
                break;
            default:
                platformPrefab = normalPlatformPrefab;
                break;
        }*/

        //GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity);
        GameObject platform = Instantiate(platformType, position, Quaternion.identity);

        platform.transform.up = normal;
    }
}
