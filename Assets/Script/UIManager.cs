using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : NetworkBehaviour
{
    public Button bettingButton;
    public Button dieButton;
    public TextMeshProUGUI chipCount;
    public TextMeshProUGUI bettingedCount;
    public Text notenoughMoney;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI loseText;
    public int getChip = 26;
    public int bettingChip;
    public bool isDie = false;
    public bool isBetting = false;
    // Start is called before the first frame update
    void Start()
    {
        
        chipCount.text = getChip.ToString();

        
        //bettingButton.onClick.AddListener(OnClickBetting);
    }

  

    public void OnClickBetting()
    {
        // 베팅 버튼이 클릭될 때 칩 수를 1 감소
        getChip -= 1;
        chipCount.text = getChip.ToString();
        bettingChip += 1;
        Debug.Log(bettingChip);
        if(getChip <= 0)
        {
            bettingButton.enabled = false;
            Debug.Log("금액이 모자라요");
            
        }
    }
    public void OnClickDie()
    {
        bettingButton.enabled = false;
        getChip -= 5;
        chipCount.text = getChip.ToString();
        bettingChip += 5;
        isDie = true;
        if (getChip <= 0||getChip<5)
        {
            dieButton.enabled = false;
        }
    }
    public void OnCliickBetting()
    {
        bettingedCount.text = bettingChip.ToString();
        isBetting = true;
    }
    [ClientRpc]
    public void WinnerText()
    {
        winnerText.enabled = true;
    }
    [ClientRpc]
    public void LoserText()
    {
        loseText.enabled = true;
    }
}
