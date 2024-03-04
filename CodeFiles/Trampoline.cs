using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{

    
    public float bounceForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the object entering the trampoline is the player
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            

            if (playerRigidbody != null)
            {
                // Apply upward force to make the player jump
                playerRigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }
        }
    }
}

