using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl_Coin : MonoBehaviour
{
    private RectTransform fillImage;
    private RectTransform fullImage;
    private Text amountText;

    // need reference to player status
    [SerializeField] private PlayerStatus player;

    // for setting the width of fillImage
    private float fullWidth;
    private float currWidth = 0;

    [SerializeField] private Transform CoinsParent;
    private int totalCoinCount;
    private int currCoinCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        fillImage = transform.Find("Bar").Find("FillImage").GetComponent<RectTransform>();
        fullImage = transform.Find("Bar").GetComponent<RectTransform>();
        amountText = transform.Find("Bar").Find("Text").GetComponent<Text>();

        fullWidth = fullImage.rect.width;
        totalCoinCount = CoinsParent.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        // get the current coin count from player
        currCoinCount = player.GetCoinCount();
        // calculate the fill width
        currWidth = ((float)currCoinCount / totalCoinCount) * fullWidth;
        // change the UIs
        fillImage.sizeDelta = new Vector2(currWidth, fillImage.rect.height);
        amountText.text = currCoinCount.ToString("D2") + "/" + totalCoinCount.ToString("D2");
    }
}
