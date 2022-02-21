using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int coin = 0;
    [SerializeField] private int currHealth = 4;
    private int maxHealth = 4;

    [SerializeField] GameObject coinParent;

    public float invicibleTime = 3;
    public float invicibleTimer = 0;
    private bool isInvicible;

    [SerializeField] AudioSource CoinSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        CheckInvicible();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Coin"))
        {
            coin++;
            Destroy(other.gameObject);
            CoinSound.Play();
        }

        if (other.tag.Equals("RedCoin"))
        {
            currHealth = (currHealth + 1) > maxHealth ? currHealth : (currHealth + 1);
            Destroy(other.gameObject);
            CoinSound.Play();
        }
    }

    private void CheckInvicible()
    {
        if (invicibleTimer > 0)
        {
            isInvicible = true;
            invicibleTimer -= Time.deltaTime;
        }
        else
        {
            isInvicible = false;
            // restore size (if hit by thwomp)
            transform.localScale = Vector3.one;
        }
    }

    public int GetCoinCount()
    {
        return coin;
    }

    public int GetMaxCoin()
    {
        return coinParent.transform.childCount;
    }

    public int GetHealth()
    {
        return currHealth;
    }

    public void AddHealth()
    {
        currHealth++;
    }

    public void DeductHealth()
    {
        if(!isInvicible)
            currHealth--;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetInvicible()
    {
        invicibleTimer = invicibleTime;
    }
}
