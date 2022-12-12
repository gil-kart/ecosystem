using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 


public class Player : MonoBehaviour
{
    private bool jumpKeyWasPressed;
    private bool upKeyWasPressed;
    private bool downKeyWasPressed;
    private bool leftKeyWasPressed;
    private bool rightKeyWasPressed;
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upKeyWasPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downKeyWasPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftKeyWasPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightKeyWasPressed = true;
        }

    }

    private void FixedUpdate()
    {
        if (jumpKeyWasPressed)
        {
            rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        //rigidBodyComponent.velocity = new Vector3(horizontalInput * 3, rigidBodyComponent.velocity.y, verticalInput * 3);
        //rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x + Random.Range(-2, 3), rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z + Random.Range(-2, 3));

        if (upKeyWasPressed)
        {
            rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x + 5, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z);
            upKeyWasPressed = false;
        }
        if (downKeyWasPressed)
        {
            rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x - 5, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z);
            downKeyWasPressed = false;
        }
        if (leftKeyWasPressed)
        {
            rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z + 5);
            leftKeyWasPressed = false;
        }
        if (rightKeyWasPressed)
        {
            rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z - 5);
            rightKeyWasPressed = false; 
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision has happend!");
        if (other.GetComponent<Collider>().CompareTag("FlowerTag"))
        {
            Debug.Log("Destroying by tag!");
            Destroy(other.gameObject);
        }
        

    }
    
}
