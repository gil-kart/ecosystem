using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject myObject;
    private float timePassed = 0f;
    public int flowerCount = 0;
    // Update is called once per frame
    void Update()
    {

        timePassed += Time.deltaTime;
        if (((timePassed > 4f) || Input.GetKeyDown(KeyCode.W)) && flowerCount < 16) 
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(460, 750), 3, Random.Range(400, 640));
            GameObject flowerOffSpr = Instantiate(myObject, randomSpawnPosition, Quaternion.AngleAxis(-90, Vector3.right));
            GameObject Parent = GameObject.FindGameObjectsWithTag("FlowersTag")[0];
            flowerOffSpr.transform.SetParent(Parent.transform);
            timePassed = 0f;
            flowerCount++;
        }
    }
}
