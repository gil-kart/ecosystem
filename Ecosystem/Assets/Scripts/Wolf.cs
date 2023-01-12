using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Wolf : MonoBehaviour
{

    static public int MAXNUMBEROFWOLVES = 30;
    static public int curNumerOfWolves = 3;

    [SerializeField] private PlayerNavMesh playerNaveMesh;
    [SerializeField] private HungerBar hungerBar;
    public int collisionCount = 0;
    public int trigerCount = 0;
    [SerializeField] float maxHunger = 3;
    private float curHunger;
    private float timePassed = 0f;
    private float timePassedSinceStart = 0f;
    private bool isFull;
    public Wolf offSpring;

    private float sicknessTimer = 0f;
    private float healthTimer = 0f;
    private int originalSpeed;
    Color healthyColor;

    public bool isFemale;
    public bool isPregnent = false;
    private Wolf partner;
    private bool isYoung = false;
    private int speed = 15;
    private float matingDesire = 0f;
    private float likelinessToGetSick = 0.05f;
    private int longevity = 120;
    private float attractivnes;
    private float amuneSystemProbs = 0.7f;
    private bool isSick = false;

    void Start()
    {
        curHunger = maxHunger;
        hungerBar.updateHungerBar(maxHunger, curHunger);
        isFull = true;
        isYoung = false;
        healthyColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        timePassedSinceStart += Time.deltaTime;
        if (timePassedSinceStart > 3)
        {
            curHunger = (float)(curHunger - 0.15);
            hungerBar.updateHungerBar(maxHunger, curHunger);
            timePassedSinceStart = 0;
            if (curHunger <= 0)
            {
                Destroy(this.gameObject);
                curNumerOfWolves--;
            }
                
        }
        _= curHunger >= maxHunger ? isFull = true : isFull = false;

        if (timePassed > 35 && isYoung)  // if the wolf is older then 35 seconds, it becomes an adult 
        {
            isYoung = false;
            transform.localScale *= 2;
            speed *= 2;
        }
        handleSickness();
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
        }
        if (collision.gameObject.CompareTag("ShipTag") && collision.collider.GetType().Name == "BoxCollider" && !isFull)
        {
            Debug.Log("destroying sheep! count: " + trigerCount);
            Player other = collision.gameObject.GetComponent<Player>();
            other.decNumOfSheep();
            trigerCount++;
            Destroy(collision.gameObject);
            curHunger = (float)(curHunger + 0.35);
            hungerBar.updateHungerBar(maxHunger, curHunger);
            playerNaveMesh.updateDestination(new Vector3(Random.Range(460, 750), 3, Random.Range(430, 550)));
            playerNaveMesh.goingToFindFood = false;
        }

        if (collision.gameObject.CompareTag("WolfTag") && collision.collider.GetType().Name == "BoxCollider" && isFemale)
        {
            Wolf other = collision.gameObject.GetComponent<Wolf>();
            if (!isYoung && !isPregnent && isFemale && !other.isFemale && other.getAttractivnes() >= attractivnes - 0.12)
            {
                isPregnent = true;
                partner = other;
                Invoke("spawn", 5.0f); // female wolf will spawn offsprings in 10 seconds from now
            }
        }
    }

    public void spawn()
    {
        int numOfOffsprings = Random.Range(1,   2);
        for (int i = 0; i < numOfOffsprings && curNumerOfWolves < MAXNUMBEROFWOLVES; i++)
        {
            _ = Random.value > 0.5f ? offSpring.isFemale = true : offSpring.isFemale = false;  // 50% for the offspring to be male or female
            Wolf offSpr = Instantiate(offSpring, this.gameObject.transform.position, Quaternion.identity);
            offSpr.isYoung = true;
            GameObject Parent = GameObject.FindGameObjectsWithTag("WolvesTag")[0];
            offSpr.transform.SetParent(Parent.transform);
            offSpr.isPregnent = false;
            offSpr.transform.localScale = offSpr.transform.localScale / 4;
            offSpr.speed /= 2;

            setOffspringsGenes(offSpr);
            curNumerOfWolves++;
            isPregnent = false;

        }
  
    }

    private void setOffspringsGenes(Wolf offSpr)
    {
        float Weight = Random.value;
        offSpr.speed = (int)((1 - Weight) * partner.getSpeed() + Weight * this.getSpeed());

        Weight = Random.value;
        offSpr.longevity = (int)((1 - Weight) * partner.getLongevity() + Weight * this.getLongevity() + Random.Range(-10, 10));

        Weight = Random.value;
        offSpr.likelinessToGetSick = (1 - Weight) * partner.getSicknessLikelihood() + Weight * this.getSicknessLikelihood() + Random.Range(-0.1f, 0.1f);

        Weight = Random.value;
        offSpr.attractivnes = (1 - Weight) * partner.getAttractivnes() + Weight * this.getAttractivnes() + Random.Range(-0.1f, 0.1f);

        Weight = Random.value;
        offSpr.matingDesire = (1 - Weight) * partner.getMatingDesire() + Weight * this.getMatingDesire() + Random.Range(-0.1f, 0.1f);

        Weight = Random.value;
        offSpr.amuneSystemProbs = (1 - Weight) * partner.getAmuneSystemProbs() + Weight * this.getAmuneSystemProbs() + Random.Range(-0.05f, 0.05f);

        offSpr.curHunger = (float)(0.5 * partner.curHunger + 0.5 * this.curHunger);

    }

    private void handleSickness()
    {
        if ((sicknessTimer > 4)) // every four seconds, make a random value and check if its lower then the probabilty to get sick
        {
            float sickProb = Random.value;
            if (sickProb < likelinessToGetSick)
            {   // if we enter, the sheep got sick
                isSick = true;
                originalSpeed = speed;
                healthTimer = 0;

                // slow sheep's speed and change sheep's color to a "sick" color
                playerNaveMesh.updateSpeed((int)(speed / 4));
                GetComponent<Renderer>().material.color = Color.green;
            }
            sicknessTimer = 0;
        }

        if (isSick)
        {
            healthTimer += Time.deltaTime; // count sicknes time
        }

        if (healthTimer > 10) // make sheep healthy after 10 seconds;
        {
            float chanceToDie = Random.value;
            if (chanceToDie > amuneSystemProbs)
            {
                {
                    curNumerOfWolves--;
                }
                Destroy(gameObject);
                return;
            }
            isSick = false;
            speed = originalSpeed;
            playerNaveMesh.updateSpeed(originalSpeed);
            healthTimer = 0;
            GetComponent<Renderer>().material.color = healthyColor;
        }
    }

    private float getAmuneSystemProbs()
    {
        return amuneSystemProbs;
    }

    private float getMatingDesire()
    {
        return matingDesire;
    }

    private float getSicknessLikelihood()
    {
        return likelinessToGetSick;
    }

    private float getLongevity()
    {
        return longevity;
    }

    private float getSpeed()
    {
        return speed;
    }

    private float getAttractivnes()
    {
        return attractivnes;
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