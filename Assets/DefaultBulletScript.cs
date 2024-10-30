using UnityEngine;

public class DefaultBulletScript : MonoBehaviour
{
    public GameObject platformPrefab; // The prefab to spawn when hitting the specified tag
    public string targetTag; // The tag to check for

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        Debug.Log("Bullet");
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

            // Spawn the platform and align it with the normal
            SpawnPlatform(impactPoint, normal);
            Destroy(gameObject); // Destroy the bullet after it hits
        }
    }

    void SpawnPlatform(Vector3 position, Vector3 normal)
    {
        // Instantiate the platform prefab at the impact point
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity);

        // Align the platform to the normal of the surface
        platform.transform.up = normal; // Align the up direction of the platform with the normal
    }
}
