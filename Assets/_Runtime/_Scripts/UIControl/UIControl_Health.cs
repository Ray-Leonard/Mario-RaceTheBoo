using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl_Health : MonoBehaviour
{
    [SerializeField] private GameObject healthParent;

    private List<GameObject> hearts = new List<GameObject>();

    [SerializeField] private PlayerStatus player;

    // Start is called before the first frame update
    void Start()
    {
        // get hearts and store them in array
        for(int i = 0; i < healthParent.transform.childCount; ++i)
        {
            hearts.Add(healthParent.transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // set the remaining health active to true, and other health to false
        for(int i = 1; i <= player.GetMaxHealth(); ++i)
        {
            if(i <= player.GetHealth())
            {
                hearts[i-1].SetActive(true);
            }
            else
            {
                hearts[i-1].SetActive(false);
            }
        }
    }
}
