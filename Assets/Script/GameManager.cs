using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UIElements;
using Unity.VisualScripting;
public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;

    public Cards cardManager;
    public Player player;
    public int count = 10;

    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ShuffleDeck(Transform cardSpawnPos)
    {
        List<GameObject> deck = cardManager.cards;
        for(int i = 0; i < deck.Count; i++)
        {
            int RandomIndex = Random.Range(i,deck.Count);
            GameObject Temp = deck[i];
            deck[i] = deck[RandomIndex];
            deck[RandomIndex] = Temp;
            Debug.Log(deck[RandomIndex]);
        }
        SetGameCard(deck, cardSpawnPos);
    }
    [Client]
    public void SetGameCard(List<GameObject> Deck, Transform cardSpawnPos)
    {
        
        Debug.Log(player);
        var cardObj = Deck[Deck.Count - count];
        if(cardObj == null || cardSpawnPos == null)
        {
            return;
        }

        // GameObject gObj = GameObject.Find()

        // player.showCard.transform
        GameObject showCard = Instantiate(cardObj, cardSpawnPos);
        
        count--;
        Debug.Log(player.name);
        
    }
    [ClientRpc]
    public void JudgeWinner(List<GameObject> Deck)
    {
        foreach(GameObject deck in Deck)
        {
            if(deck != null)
            {
                
            }
        }


    }
}
