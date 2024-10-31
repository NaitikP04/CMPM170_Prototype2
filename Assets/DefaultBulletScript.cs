using UnityEngine;

public class DefaultBulletScript : MonoBehaviour
{
    public GameObject spawnPlatform;
    public string targetTag; // The tag to check for
    public float bounceMultiplier = 1.0f;

    private Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            SpawnPlatform(impactPoint, normal, spawnPlatform);
            Destroy(gameObject); // Destroy the bullet after it hits
        }
        else if (other.CompareTag("NormalPlatform"))
        {
            // Calculate the bounce direction
            Vector3 bounceDirection = Vector3.Reflect(rb.linearVelocity, other.transform.up);
            rb.linearVelocity = bounceDirection * bounceMultiplier; // Apply the bounce effect
        }

    }

    void SpawnPlatform(Vector3 position, Vector3 normal, GameObject platformType)
    {
        GameObject platform = Instantiate(platformType, position, Quaternion.identity);

        platform.transform.up = normal;
    }
}
