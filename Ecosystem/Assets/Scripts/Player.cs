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
    private int numberOfPregnencys = 0;
    private bool isYoung = true;

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
        if (timePassed > 10 && isYoung)
        {
            isYoung = false;
            transform.localScale *= 2;
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
                Flower f = collision.gameObject.GetComponent<Flower>();
                f.setToDestroyed();
                //Destroy(collision.gameObject);
                playerNaveMesh.goingToFindFood = false;
                playerNaveMesh.destination = new Vector3(Random.Range(460, 750), 3, Random.Range(400, 640));
                curHunger += 0.4f;
                spawner.flowerCount--;
            }

        }

        if (collision.gameObject.CompareTag("WolfTag"))
        {
            try
            {
                Debug.Log("runing from wolf!!");
                Vector3 oppositeWolfDirection = transform.position - collision.gameObject.transform.position;
                playerNaveMesh.goingToFindFood = false;
                Vector3 newDest = new Vector3(collision.gameObject.transform.position.x + oppositeWolfDirection.x + 600, 3, 600 + collision.gameObject.transform.position.z + oppositeWolfDirection.z);
                playerNaveMesh.updateDestination(newDest);
                playerNaveMesh.updateSpeed(30);
            }
            catch
            {

            }
        }
        if (collision.gameObject.CompareTag("ShipTag"))
        {
            Player other = collision.gameObject.GetComponent<Player>();

            if ((!isPregnent && this.isFemale && !other.isFemale) || (!isFemale && other.isFemale && other.isPregnent))
            {
                try
                {
                    if (!other.isFemale && numberOfPregnencys < 4)
                    {
                        numberOfPregnencys++;
                        isPregnent = true;
                        Invoke("spawn", 5.0f);
                    }
                    /*else if(other.numberOfPregnencys < 4)
                    {
                        other.isPregnent = true;
                        other.Invoke("spawn", 5.0f);
                    }*/
                }
                catch
                {
                    Debug.Log("mating failed!");
                }
            }
        }
    } 



    public void spawn()
    {
        _ = Random.value > 0.5f ? offSpring.isFemale = true : offSpring.isFemale = false;
        Player offSpr = Instantiate(offSpring, this.gameObject.transform.position, Quaternion.identity);
        offSpr.transform.localScale /= 2;
        GameObject Parent = GameObject.FindGameObjectsWithTag("SheepsTag")[0];
        offSpr.transform.SetParent(Parent.transform);
        isPregnent = false;
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
