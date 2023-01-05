using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed = 300.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * 4;
        float vertical = Input.GetAxis("Vertical") * 4;
        float yAxis = Input.GetAxis("Mouse ScrollWheel") * 50;
        // Calculate the movement vector
        Vector3 movement = new Vector3(horizontal, yAxis, vertical);

        // Move the camera
        transform.position += movement * speed * Time.deltaTime;

    }
}
