using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject myObject;
    public Wolf wolfPrefab;
    public Player sheepPrefab;
    private float timePassed = 0f;
    public int flowerCount = 0;
    Flower flower = new Flower();
    void Update()
    {

        timePassed += Time.deltaTime;
        
        if (((timePassed > 1f) || Input.GetKeyDown(KeyCode.W)) && flower.getFlowerCount() < 100) 
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(460, 650), 3, Random.Range(430, 550));
            GameObject flowerOffSpr = Instantiate(myObject, randomSpawnPosition, Quaternion.AngleAxis(-90, Vector3.right));
            GameObject Parent = GameObject.FindGameObjectsWithTag("FlowersTag")[0];
            flowerOffSpr.transform.SetParent(Parent.transform);
            timePassed = 0f;
            Flower curflower = flowerOffSpr.GetComponent<Flower>();
            curflower.incFlowerCount();
            flowerCount = curflower.getFlowerCount();
        }
    }

    public void addWolvesToScene(int wolfNumber)
    {
        for(int i=0; i<wolfNumber; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(460, 650), 3, Random.Range(430, 550));
            Wolf curWolf = Instantiate(wolfPrefab, randomSpawnPosition, Quaternion.AngleAxis(-90, Vector3.right));
            _ = Random.value > 0.5f ? curWolf.isFemale = true : curWolf.isFemale = false;  // 50% for the offspring to be male or female
            GameObject Parent = GameObject.FindGameObjectsWithTag("WolvesTag")[0];
            curWolf.transform.SetParent(Parent.transform);
            curWolf.isPregnent = false;
            curWolf.transform.localScale = curWolf.transform.localScale / 2;
        }
        Wolf.curNumerOfWolves = wolfNumber;
    }

    public void addSheepToScene(int sheepNumber)
    {
        for (int i = 0; i < sheepNumber; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(460, 650), 3, Random.Range(430, 550));

            Player curSheep = Instantiate(sheepPrefab, randomSpawnPosition, Quaternion.AngleAxis(-90, Vector3.right));
            if (Random.value > 0.5f)    // 50% to be male or female
                curSheep.setIsFemale(true);  
            else
                curSheep.setIsFemale(false);  
            
            curSheep.setIsYoung(false);
            curSheep.isPregnent = false;
            curSheep.numberOfPregnencys = 0;
            GameObject Parent = GameObject.FindGameObjectsWithTag("SheepsTag")[0];
            curSheep.transform.SetParent(Parent.transform);
        }
        Player.curNumerOfSheep = sheepNumber;
    }
}
