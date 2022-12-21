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
    private GameObject nextSheep;
    [SerializeField] private PlayerNavMesh playerNaveMesh;
    public int collisionCount = 0;
    public int trigerCount = 0;

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ShipTag"))
        {
            collisionCount++;
            playerNaveMesh.updateDestination(collision.gameObject.transform.position);
            playerNaveMesh.goingToFindFood = true;
            nextSheep = collision.gameObject;
            
        }
        if (collision.gameObject.CompareTag("ShipTag") && collision.collider.GetType().Name == "BoxCollider")
        {
            Debug.Log("destroying sheep! count: " + trigerCount);
            trigerCount++;
            Destroy(collision.gameObject);
            playerNaveMesh.updateDestination(new Vector3(Random.Range(460, 750), 3, Random.Range(430, 550)));
            playerNaveMesh.goingToFindFood = false;
        }


    }
}




/*  // code user interface 
   /*  if (Input.GetKeyDown(KeyCode.UpArrow))
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

        if ((Vector3.Distance(this.gameObject.transform.position, nextSheep.transform.position)) < 4)
        {
            playerNaveMesh.agent.destination = new Vector3(Random.Range(460, 750), 3, Random.Range(400, 640));
        }*/