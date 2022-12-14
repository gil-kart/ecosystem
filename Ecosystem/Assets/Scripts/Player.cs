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
    private float timePassedSinceStart = 0f;
    public bool isFemale;
    public bool isPregnent;
    [SerializeField] float maxHunger = 3;
    [SerializeField] RandomObjectSpawner spawner;
    private float curHunger;
    [SerializeField] private HungerBar hungerBar;
    [SerializeField] private PlayerNavMesh playerNaveMesh;

    // Start is called before the first frame update
    void Start()
    {
        
        curHunger = maxHunger;
        rigidBodyComponent = GetComponent<Rigidbody>();
        hungerBar.updateHungerBar(maxHunger, curHunger);
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
        timePassedSinceStart += Time.deltaTime;
        if (timePassedSinceStart > 3)
        {
            curHunger = (float)(curHunger - 0.05);
            hungerBar.updateHungerBar(maxHunger, curHunger);
            timePassedSinceStart = 0;
        }

    }

    private void FixedUpdate()
    {
        if (curHunger <= 0)
        {
            Destroy(this.gameObject);
        }

        if (jumpKeyWasPressed)
        {
            rigidBodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
        //rigidBodyComponent.velocity = new Vector3(horizontalInput * 3, rigidBodyComponent.velocity.y, verticalInput * 3);

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

       // if (Random.value > 0.5f)
         //   rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x + Random.Range(-5, 6), rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z);
        //else
       //     rigidBodyComponent.velocity = new Vector3(rigidBodyComponent.velocity.x, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z + Random.Range(-5, 6));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FlowerTag"))
        {

            playerNaveMesh.foodLocation = collision.gameObject.transform.position;
            playerNaveMesh.goingToFindFood = true;
            
            if (Vector3.Distance(this.gameObject.transform.position, collision.gameObject.transform.position) < 7)
            {
                Destroy(collision.gameObject);
                playerNaveMesh.goingToFindFood = false;
                playerNaveMesh.destination = new Vector3(Random.Range(460, 750), 3, Random.Range(400, 640));

            }
                
            curHunger += 0.4f;
            spawner.flowerCount--;
        }
            
        if (collision.gameObject.CompareTag("WolfTag"))
        {
            if ((Vector3.Distance(this.gameObject.transform.position, collision.gameObject.transform.position)) < 25)
            {
                Debug.Log("wolf ahead!");
                //Destroy(collision.gameObject);
            }
                

        }
        if (collision.gameObject.CompareTag("ShipTag") && !isPregnent && this.isFemale)
        {
            try
            {
                Player other = collision.gameObject.GetComponent<Player>(); 
                if (!other.isFemale)
                {

                    isPregnent = true;
                    Invoke("spawn", 3.0f);
                    Debug.Log("mating is successful!");
                    timePassed = 0;
                }
            }
            catch
            {
                Debug.Log("mating failed!");
            }
            
        }
    }

    public void spawn()
    {
        _ = Random.value > 0.5f ? offSpring.isFemale = true : offSpring.isFemale = false;
        Player offSpr = Instantiate(offSpring, this.gameObject.transform.position, Quaternion.identity);
        GameObject Parent = GameObject.FindGameObjectsWithTag("SheepsTag")[0];
        offSpr.transform.SetParent(Parent.transform);
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
