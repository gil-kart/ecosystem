using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 


public class Player : MonoBehaviour
{
    
    




















    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rigidBodyComponent;


    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (jumpKeyWasPressed)
        {
            rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        //rigidBodyComponent.velocity = new Vector3(horizontalInput * 3, rigidBodyComponent.velocity.y, verticalInput * 3);
        rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x + Random.Range(-2, 3), rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z + Random.Range(-2, 3));
    }
}
