using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject myObject;
    private float timePassed = 0f;

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if ((timePassed > 4f) || Input.GetKeyDown(KeyCode.W)) // 15 is the maximal number of food instances available in each simulation!
        {
           // int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(460, 750), 3, Random.Range(400, 640));
            Instantiate(myObject, randomSpawnPosition, Quaternion.AngleAxis(-90, Vector3.right));
            timePassed = 0f;
        }
    }
}
