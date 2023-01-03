using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject myObject;
    private float timePassed = 0f;
    public int flowerCount = 0;
    void Update()
    {

        timePassed += Time.deltaTime;
        if (((timePassed > 1.5f) || Input.GetKeyDown(KeyCode.W)) && flowerCount < 20) 
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(460, 650), 3, Random.Range(430, 550));
            GameObject flowerOffSpr = Instantiate(myObject, randomSpawnPosition, Quaternion.AngleAxis(-90, Vector3.right));
            GameObject Parent = GameObject.FindGameObjectsWithTag("FlowersTag")[0];
            flowerOffSpr.transform.SetParent(Parent.transform);
            timePassed = 0f;
            flowerCount++;
        }
    }
}
