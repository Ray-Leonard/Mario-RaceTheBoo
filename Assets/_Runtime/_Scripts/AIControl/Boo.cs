using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boo : MonoBehaviour
{

    [SerializeField] GameObject TalkPanelObj;

    private bool isStart;
    public bool isInRace = false;

    private NavMeshAgent agent;

    [SerializeField] Transform pathPointsParent;
    private List<Vector3> destinations = new List<Vector3>();
    int nextDestination = 0;
    // Start is called before the first frame update
    void Start()
    {
        TalkPanelObj.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;

        foreach(Transform t in pathPointsParent)
        {
            destinations.Add(t.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BeginRace();
    }

    private void BeginRace()
    {
        if (isStart)
        {
            agent.SetDestination(destinations[nextDestination]);

            if(nextDestination == destinations.Count)
            {
                agent.enabled = false;
                isStart = false;
                return;
            }

            if(Vector3.Distance(transform.position, destinations[nextDestination]) < 3)
            {
                nextDestination++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isStart)
        {
            TalkPanelObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TalkPanelObj.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !isStart)
        {
            if (Input.GetKey(KeyCode.E))
            {
                isStart = true;
                agent.enabled = true;
                isInRace = true;
                TalkPanelObj.SetActive(false);
            }
        }
    }
}
