using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] myObjects;
    private float timePassed = 0f;

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 4f)
        {
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-97, 132), -1, Random.Range(0, 158));
            Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity).transform.Rotate(-90f, 0, 0);
            timePassed = 0f;
        }
        
    }
}
