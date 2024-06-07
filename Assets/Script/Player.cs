using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.Diagnostics;
public class Player : NetworkBehaviour
{
    public Animator Animator;
    public int Chip;
    public Deck deck;
    [SerializeField]
    public Transform showCardPos;
    [SerializeField]
    public UIManager UIManager;
    [SerializeField]
    GameObject playerCamera;
    public override void OnStartClient()
    {
        if (isLocalPlayer)
        {
            GameManager.Instance.PlayerConnection(connectionToClient);
            GameManager.Instance.localPlayer = this;
            playerCamera.SetActive(true);

            deck.ShuffleDeck();
            GameObject card = deck.DrawCard();

            InstantiateAndAddDic(card.name, connectionToClient);
        }
    }

    [Command]
    private void InstantiateAndAddDic(string cardName, NetworkConnectionToClient conn)
    {
        GameObject prefab = CardPrefabReference.Instance.GetPrefab(cardName);
        GameObject cardObject = Instantiate(prefab);


        Transform setTransform = conn.identity.GetComponent<Player>().showCardPos;
        cardObject.transform.position = setTransform.position;
        cardObject.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0) + conn.identity.transform.rotation.eulerAngles);


        NetworkServer.Spawn(cardObject);
        int value = cardObject.GetComponent<Card>().value;
        GameManager.Instance.AddCardToDic(conn, value);

        NetworkIdentity ni = cardObject.GetComponent<NetworkIdentity>();
        //리스트로 SpawnedCard를 GameManager에 만들고 저장
        GameManager.Instance.SpawnedCard.Add(ni);

        //지울땐 GameManager에서 NetworkServer.Destroy()를 foreach돌려서 지우기
    }

    [ClientRpc]
    public void SetWinOrLose(bool isWin)
    {
        if(isLocalPlayer) 
        {
            Debug.Log(netId);
            if (isWin)
            {
                WinnerUiActive();
            }
            else
            {
                Animator.SetTrigger("IsLostMoney");
                LoserUiActive();
                //WinnerUiActiveFalse(localPlayer);
            }
        }
    }

    public void WinnerUiActive()
    {
        if (UIManager != null)
        {
            UIManager.WinnerText();
        }
    }
    public void LoserUiActive()
    {
        if (UIManager != null)
        {
            UIManager.LoserText();
        }
    }
/*
    public void WinnerUiActiveFalse(Player player)
    {
        player.UIManager.OffUI();
    }*/


    private void Awake()
    {
       // UIManager = this.GetComponent<UIManager>();
        UIManager =FindObjectOfType<UIManager>().GetComponent<UIManager>();
        Debug.Log(UIManager);
    }
}
