using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompTrigger : MonoBehaviour
{

    private Thwomp thwomp;
    // Start is called before the first frame update
    void Start()
    {
        thwomp = transform.parent.gameObject.GetComponent<Thwomp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            thwomp.isTriggered = true;
        }
    }
}
