using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;


public class Player : MonoBehaviour
{
    static public int MAXNUMBEROFSHEEP = 28;
    static public int curNumerOfSheep = 11;
    public Player offSpring;
    private float timePassed = 0f;
    private float sicknessTimer = 0f;
    private float healthTimer = 0f;
    private float timePassedSinceStart = 0f;
    public bool isFemale;
    public bool isPregnent = false;
    private Player partner;
    private bool isSick = false;
    private int originalSpeed;

    private int speed = 15;
    private float  matingDesire = 0f;
    private float  likelinessToGetSick = 0.05f;
    private int longevity = 120;
    private float attractivnes;
    private float amuneSystemProbs = 0.7f;



    Color healthyColor;
    [SerializeField] float maxHunger = 3;
    [SerializeField] RandomObjectSpawner spawner;
    private float curHunger;
    [SerializeField] private HungerBar hungerBar;
    [SerializeField] private PlayerNavMesh playerNaveMesh;
    public int numberOfPregnencys = 0;
    private bool isYoung = false;

    void Start()
    {
        timePassed = 0f;
        curHunger = maxHunger;
        hungerBar.updateHungerBar(maxHunger, curHunger);
        healthyColor = GetComponent<Renderer>().material.color;

        numberOfPregnencys = 0;
        speed = Random.Range(11, 18);
        playerNaveMesh.updateSpeed(speed);
        likelinessToGetSick = Random.Range(0.001f, 0.04f);
        longevity = Random.Range(140, 170);
        attractivnes = Random.value;
        matingDesire = Random.Range(0.0f, 0.2f);
        amuneSystemProbs = Random.Range(0.75f, 0.94f);
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        timePassedSinceStart += Time.deltaTime;
        sicknessTimer += Time.deltaTime;

        if (timePassedSinceStart > 3)  // update hunger bar every three seconds
        {
            curHunger = (float)(curHunger - 0.08);  
            hungerBar.updateHungerBar(maxHunger, curHunger);
            timePassedSinceStart = 0;
        }
        
        if (timePassed > 13 && isYoung)  // if the sheep is older then 30 seconds, it becomes an adult sheep
        {
            isYoung = false;
            transform.localScale *= 2;
        }

        if (timePassed > longevity || curHunger <= 0) // if the sheep is  older then its longevity or if sheep starves, it dies, it dies
        {
            decNumOfSheep();
            Destroy(this.gameObject);
        }

        handleSickness();
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

            float eatingProb = Random.value;
            
            if (Vector3.Distance(this.gameObject.transform.position, collision.gameObject.transform.position) < 14 
                && eatingProb > matingDesire) // consume food if its in a smaller distance then 7
            {
                Flower flower = collision.gameObject.GetComponent<Flower>();
                flower.setToDestroyed();
                playerNaveMesh.goingToFindFood = false;
                playerNaveMesh.destination = new Vector3(Random.Range(460, 650), 3, Random.Range(430, 550));  // set new random destination
                curHunger += 0.3f;  // update hunger bar
                hungerBar.updateHungerBar(maxHunger, curHunger);
                spawner.flowerCount -= 1;
            }
        }

        if (collision.gameObject.CompareTag("WolfTag"))  // if the sheep entered a wolf's radius, it tries to escape by setting a new destination and increasing speed
        {
            try
            {
                Vector3 oppositeWolfDirection = collision.gameObject.transform.position;
                playerNaveMesh.goingToFindFood = false;
                Vector3 newDest = new Vector3(collision.gameObject.transform.position.x + oppositeWolfDirection.x * 300, 3, 300 * collision.gameObject.transform.position.z + oppositeWolfDirection.z);
                playerNaveMesh.updateDestination(newDest);
                playerNaveMesh.updateSpeed(speed * 3);
                Invoke("returnToNormalSpeed", 7.0f);
            }
            catch
            {

            }
            
        }
        if (collision.gameObject.CompareTag("ShipTag"))
        {
            Player other = collision.gameObject.GetComponent<Player>();
            if (!isYoung && !isPregnent && isFemale && !other.isFemale && numberOfPregnencys < 4 && other.getAttractivnes() >= attractivnes - 0.12)
            {
               // numberOfPregnencys++;
                isPregnent = true;
                partner = other;
                Invoke("spawn", 4.0f); // sheep will spawn offsprings in 10 seconds from now
            }
        }
    } 
    public void decNumOfSheep()
    {
        curNumerOfSheep--;
    }

    public void returnToNormalSpeed()
    {
        playerNaveMesh.updateSpeed(speed);
    }
    public void spawn()
    {
        int numOfOffsprings = Random.Range(1, 4);
        for(int i=0; i< numOfOffsprings && curNumerOfSheep < MAXNUMBEROFSHEEP; i++)
        {
            _ = Random.value > 0.5f ? offSpring.isFemale = true : offSpring.isFemale = false;  // 50% for the offspring to be male or female
            Player offSpr = Instantiate(offSpring, this.gameObject.transform.position, Quaternion.identity);
            offSpr.transform.localScale /= 2;
            offSpr.isYoung = true;
            offSpr.isPregnent = false;
            offSpr.numberOfPregnencys = 0;
            GameObject Parent = GameObject.FindGameObjectsWithTag("SheepsTag")[0];
            offSpr.transform.SetParent(Parent.transform);

            setOffspringGenes(offSpr);
            
            isPregnent = false;
            curNumerOfSheep++;
        }
    }

    private void setOffspringGenes(Player offSpr)
    {
        // determine the offspring's genes by using a weighted average of parents' genes
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

    }

    private float getAmuneSystemProbs()
    {
        return amuneSystemProbs;
    }

    public static explicit operator Player(GameObject v)
    {
        global::System.Type type = v.GetType();
        if (type == typeof(Player)) 
            return (Player)v;   
        return null;
        throw new System.NotImplementedException();
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
                decNumOfSheep();
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

    public void setIsFemale(bool isFemaleVal) { isFemale = isFemaleVal; }
    public void setIsYoung(bool isYoungVal) { isYoung = isYoungVal; }
    // -- getters --
    public float getAttractivnes()
    {
        return attractivnes;
    }
    public float getMatingDesire()
    {
        return matingDesire;
    }
    public float getSicknessLikelihood()
    {
        return likelinessToGetSick;
    }
    public int getLongevity()
    {
        return longevity;
    }
    public int getSpeed()
    {
        return speed;
    }
}
