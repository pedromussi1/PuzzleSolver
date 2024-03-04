using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMusic : MonoBehaviour
{
    private static bool isMusicInitialized = false;

    void Awake()
    {
        // Check if an instance of the background music already exists
        if (!isMusicInitialized)
        {
            // If not, mark this instance as the initialized one and prevent it from being destroyed
            isMusicInitialized = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
}
