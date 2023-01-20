using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    public bool goingToFindFood = false;
    [SerializeField] private Transform movePositionTransform;
    public NavMeshAgent agent;
    public Vector3 destination;
    public Vector3 foodLocation;
    private float timePassed = 0f;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = new Vector3(Random.Range(460, 650), 3, Random.Range(430, 550));
        goingToFindFood = false;

    }
    private void Update()
    {
        if (!goingToFindFood && timePassed > 4f)
        {
            //agent.destination = new Vector3(Random.Range(460, 650), 3, Random.Range(430, 550)); 
            Vector3 dir = new Vector3(Random.Range(-200, 200), 3, Random.Range(-200, 200));
            agent.destination = gameObject.transform.position + dir;

            timePassed = 0f;
        }
        else if (goingToFindFood)
        {
            agent.destination = foodLocation;
        }

        if(timePassed > 7f)
        {
            //agent.destination = new Vector3(Random.Range(460, 650), 3, Random.Range(430, 550));
            Vector3 dir = new Vector3(Random.Range(-200, 200), 3, Random.Range(-200, 200));
            agent.destination = gameObject.transform.position + dir;
            timePassed = 0f;
            goingToFindFood = false;
            agent.speed = 17;
        }

        timePassed += Time.deltaTime;
    }

    public void updateSpeed(int speed)
    {
        agent.speed = speed;
        timePassed = 0f;
    }

    public void updateDestination(Vector3 dest) 
    {
        agent.destination = dest;
    }
}
