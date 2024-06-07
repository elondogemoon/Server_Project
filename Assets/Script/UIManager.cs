using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : NetworkBehaviour
{
    public static UIManager Instance;
    public Button bettingButton;
    public Button dieButton;
    public TextMeshProUGUI chipCount;
    public TextMeshProUGUI bettingedCount;
    public Text notenoughMoney;
    public GameObject winnerText;
    public GameObject loseText;
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
    private void Awake()
    {
        Instance = this;
    }


    public void OnClickBetting()
    {
        // ���� ��ư�� Ŭ���� �� Ĩ ���� 1 ����
        getChip -= 1;
        chipCount.text = getChip.ToString();
        bettingChip += 1;
        Debug.Log(bettingChip);
        if(getChip <= 0)
        {
            bettingButton.enabled = false;
            Debug.Log("�ݾ��� ���ڶ��");
            
        }
    }
    public void OnClickDie()
    {
        bettingButton.enabled = false;
        dieButton.enabled = false;
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
        dieButton.enabled=false;
        bettingButton.enabled=false;
    }

    public void WinnerText()
    {
        
        Debug.Log("�̰� ȣ���");
        winnerText.SetActive(true);
        dieButton.enabled = true;
        bettingButton.enabled = true;

    }

    public void LoserText()
    {
        Debug.Log("���� ȣ���");

        loseText.SetActive(true);
    }
    public void OffUI()
    {
        winnerText.SetActive(false);
    }
}

