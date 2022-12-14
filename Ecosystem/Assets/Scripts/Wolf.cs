using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    private bool upKeyWasPressed;
    private bool downKeyWasPressed;
    private bool leftKeyWasPressed;
    private bool rightKeyWasPressed;
    private Rigidbody rigidBodyComponent;
    [SerializeField] private PlayerNavMesh playerNaveMesh;

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x, rigidBodyComponent.velocity.y + 5, rigidBodyComponent.velocity.z);
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ShipTag"))
        {
            Debug.Log("detected sheep!");
            playerNaveMesh.foodLocation = collision.gameObject.transform.position;
            playerNaveMesh.goingToFindFood = true;
            playerNaveMesh.agent.speed += 25;
            if ((Vector3.Distance(this.gameObject.transform.position, collision.gameObject.transform.position)) < 21)
            {
                Debug.Log("destroying sheep!");
                Destroy(collision.gameObject);
                playerNaveMesh.foodLocation = new Vector3(Random.Range(460, 750), 3, Random.Range(400, 640));
                playerNaveMesh.goingToFindFood = false;
                playerNaveMesh.agent.speed -= 25;
            }
            
        }
            
    }
}
