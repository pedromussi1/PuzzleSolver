using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloScript : MonoBehaviour
{

    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }
}