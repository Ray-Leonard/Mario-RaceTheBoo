using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bomb : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private float distance;

    private bool do_once = true;
    [SerializeField] ParticleSystem effect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        agent = GetComponent<NavMeshAgent>();
        distance = 5.0f;
        agent.speed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        NavigationToPlayer();
    }

    private void NavigationToPlayer()
    {
        // start follow
        if(Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            agent.enabled = true;
            agent.SetDestination(player.transform.position);
        }
        // dont follow
        else
        {
            agent.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && do_once)
        {
            do_once = false;
            Explode();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        Explode();
    //    }
    //}

    private void Explode()
    {
        // play explosion particle effect
        ParticleSystem e = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(e.gameObject, 1);

        // deduct life from player
        player.GetComponent<PlayerStatus>().DeductHealth();
        // set invicible time
        player.GetComponent<PlayerStatus>().SetInvicible();

        // destroy game object
        Destroy(gameObject);
    }

}
