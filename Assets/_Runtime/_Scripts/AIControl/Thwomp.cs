using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp : MonoBehaviour
{

    private GameObject player;
    private float distance;
    public bool isTriggered = false;
    private bool do_once = true;

    [SerializeField] private float fallTime = 0.1f;
    [SerializeField] private float fallSpeed = 100;
    [SerializeField] private float backSpeed = 5f;
    public float timer;
    public float delayTime = 0.5f;
    public float delayTimer = 0;

    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        distance = 5.0f;
        originalPos = transform.position;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        Movement();
    }

    // look at player when in range
    private void LookAtPlayer()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            Vector3 forwardUp = player.transform.position - transform.position;
            forwardUp.y = 0;
            Quaternion rotation = Quaternion.LookRotation(forwardUp);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }
    }

    // fall down on player and restore position (visual)
    private void Movement()
    {
        //if (isTriggered && timer <= 0)
        //{
        //    timer = fallTime;
        //    return;
        //}
        if(isTriggered && delayTimer <= delayTime)
        {
            delayTimer += Time.deltaTime;
            return;
        }

        if(timer >= 0 && isTriggered)
        {
            timer -= Time.deltaTime;
            transform.position -= new Vector3(0, fallSpeed*Time.deltaTime, 0);
            return;

        }
        else
        {
            timer = fallTime;
            isTriggered = false;
        }
        
        
        if(!isTriggered)
        {
            delayTimer = 0;
            if(transform.position.y <= originalPos.y)
                transform.position += new Vector3(0, backSpeed * Time.deltaTime, 0);
            return;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && do_once)
        {
            player.GetComponent<PlayerStatus>().DeductHealth();
            player.GetComponent<PlayerStatus>().SetInvicible();
            player.transform.localScale = new Vector3(player.transform.localScale.x, 0.3f, player.transform.localScale.z);
            do_once = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !do_once)
        {
            
            do_once = true;
        }
    }


}
