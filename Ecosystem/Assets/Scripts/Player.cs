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
    public Player offSpring;
    private float timePassed = 0f;
    public bool isFemale;

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

        if (Input.GetKeyDown(KeyCode.F))
        {
            upKeyWasPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            downKeyWasPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            leftKeyWasPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rightKeyWasPressed = true;
        }
        timePassed += Time.deltaTime;

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
        if (collision.gameObject.CompareTag("FlowerTag"))
            Destroy(collision.gameObject);
        if (collision.gameObject.CompareTag("WolfTag"))
        {
            Destroy(this);
        }
        if (collision.gameObject.CompareTag("ShipTag") && timePassed > 10f && this.isFemale)
        {
            try
            {
                Player other = collision.gameObject.GetComponent<Player>(); 
                if (!other.isFemale)
                {
                    _ = Random.value  > 0.5f ? offSpring.isFemale = true: offSpring.isFemale = false;
                    Instantiate(offSpring, collision.gameObject.transform.position, Quaternion.identity);
                    timePassed = 0;
                }
            }
            catch
            {
                Debug.Log("mating failed!");
            }
            
        }
    }

    public static explicit operator Player(GameObject v)
    {
        global::System.Type type = v.GetType();
        if (type == typeof(Player)) 
            return (Player)v;   
        return null;
        throw new System.NotImplementedException();
    }
}
