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
    public bool isPregnent = false;
    [SerializeField] float maxHunger = 3;
    [SerializeField] RandomObjectSpawner spawner;
    private float curHunger;
    [SerializeField] private HungerBar hungerBar;
    [SerializeField] private PlayerNavMesh playerNaveMesh;
    public int numberOfPregnencys = 0;
    private bool isYoung = false;

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
        timePassed += Time.deltaTime;
        timePassedSinceStart += Time.deltaTime;
        if (timePassedSinceStart > 3)
        {
            curHunger = (float)(curHunger - 0.05);
            hungerBar.updateHungerBar(maxHunger, curHunger);
            timePassedSinceStart = 0;
        }
        if (timePassed > 30 && isYoung)
        {
            isYoung = false;
            transform.localScale *= 2;
        }
        if (timePassed > 120)
        {
            Destroy(this.gameObject);
        }

        if (curHunger <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FlowerTag"))
        {

            playerNaveMesh.foodLocation = collision.gameObject.transform.position;
            playerNaveMesh.goingToFindFood = true;

            if (Vector3.Distance(this.gameObject.transform.position, collision.gameObject.transform.position) < 7)
            {
                Flower flower = collision.gameObject.GetComponent<Flower>();
                flower.setToDestroyed();
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
                Vector3 oppositeWolfDirection = transform.position - collision.gameObject.transform.position;
                playerNaveMesh.goingToFindFood = false;
                Vector3 newDest = new Vector3(collision.gameObject.transform.position.x + oppositeWolfDirection.x + 600, 3, 600 + collision.gameObject.transform.position.z + oppositeWolfDirection.z);
                playerNaveMesh.updateDestination(newDest);
                playerNaveMesh.updateSpeed(35);
            }
            catch
            {

            }
        }
        if (collision.gameObject.CompareTag("ShipTag"))
        {
            Player other = collision.gameObject.GetComponent<Player>();
            if ((!isPregnent && isFemale && !other.isFemale))
            {
                if (!other.isFemale && numberOfPregnencys < 4)
                {
                    numberOfPregnencys++;
                    isPregnent = true;
                    Invoke("spawn", 5.0f);
                }
            }
        }
    } 



    public void spawn()
    {
        _ = Random.value > 0.5f ? offSpring.isFemale = true : offSpring.isFemale = false;
        Player offSpr = Instantiate(offSpring, this.gameObject.transform.position, Quaternion.identity);
        offSpr.transform.localScale /= 2;
        offSpr.isYoung = true;
        offSpr.offSpring = offSpring;
        offSpr.isPregnent = false;
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
