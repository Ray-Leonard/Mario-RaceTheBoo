using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    [SerializeField] Text ResultText;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Boo boo;
    [SerializeField] GameObject marioFlag;
    [SerializeField] GameObject booFlag;
    [SerializeField] GameObject Star;

    private bool isGameOver = false;
    private PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStatus>();
        GameOverPanel.SetActive(false);
        marioFlag.SetActive(false);
        booFlag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        
        if (isGameOver)
        {
            Time.timeScale = 0;
            if (Input.GetKey(KeyCode.R))
            {
                Debug.Log("Restart");
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
                isGameOver = false;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!boo.isInRace)
        {
            return;
        }

        if(other.tag == "Boo")
        {
            booFlag.SetActive(true);
            GameOverPanel.SetActive(true);
            ResultText.text = "Boo wins";
            isGameOver = true;
        }

        if(other.tag == "Player")
        {
            marioFlag.SetActive(true);
            GameOverPanel.SetActive(true);
            ResultText.text = "Mario wins";
            Star.SetActive(true) ;
            isGameOver = true;
        }
    }

    private void CheckDeath()
    {
        if(playerStatus.GetHealth() <= 0)
        {
            isGameOver = true;
            GameOverPanel.SetActive(true);
            ResultText.text = "Mario Dies";
        }
    }

}
