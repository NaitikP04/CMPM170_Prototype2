using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpBoost = 10f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.rigidbody;
            if (playerRb != null)
            {
                // Get the player's incoming velocity
                Vector3 incomingVelocity = playerRb.linearVelocity;

                // Determine the boost direction based on the jump pad's orientation
                Vector3 boostDirection = Vector3.zero;

                // Check the orientation of the jump pad
                Vector3 upDirection = transform.up;

                if (upDirection == Vector3.up)
                {
                    // Jump pad is horizontal; boost upwards
                    boostDirection = Vector3.up * jumpBoost;
                }
                else if (upDirection == Vector3.right)
                {
                    // Jump pad is vertical (facing right); boost left if coming from the left
                    if (incomingVelocity.x < 0)
                    {
                        boostDirection = Vector3.left * jumpBoost;
                    }
                    else
                    {
                        boostDirection = Vector3.right * jumpBoost;
                    }
                }
                else if (upDirection == Vector3.left)
                {
                    // Jump pad is vertical (facing left); boost right if coming from the right
                    if (incomingVelocity.x > 0)
                    {
                        boostDirection = Vector3.right * jumpBoost;
                    }
                    else
                    {
                        boostDirection = Vector3.left * jumpBoost;
                    }
                }
                else if (upDirection == Vector3.forward || upDirection == Vector3.back)
                {
                    // For vertical planes facing forward/backward, boost accordingly
                    if (incomingVelocity.z < 0)
                    {
                        boostDirection = Vector3.back * jumpBoost; // Boost backward
                    }
                    else
                    {
                        boostDirection = Vector3.forward * jumpBoost; // Boost forward
                    }
                }

                // Apply the force
                playerRb.AddForce(boostDirection, ForceMode.Impulse);
            }
        }
    }
}
