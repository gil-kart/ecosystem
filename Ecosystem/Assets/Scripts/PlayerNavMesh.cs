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
        destination = new Vector3(Random.Range(460, 750), 3, Random.Range(400, 640));

    }
    private void Update()
    {
        if (!goingToFindFood && timePassed > 4f)
        {
            agent.destination = destination;
            destination = new Vector3(Random.Range(460, 750), 3, Random.Range(400, 640));
            timePassed = 0f;
        }
        else if (goingToFindFood)
        {
            agent.destination = foodLocation;
        }
        else if (!goingToFindFood || (Vector3.Distance(agent.destination, destination) < 8))
        {
            agent.destination = destination;
        }

        timePassed += Time.deltaTime;
    }
}
