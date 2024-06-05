using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    public Player player;
    public Cards cardManager;
    public int count = 10;
    public Dictionary<Player, int> playerCurrentCard = new Dictionary<Player, int>();

    private void Awake()
    {
        Instance = this;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    public void ShuffleDeck(Transform cardSpawnPos)
    {
        List<GameObject> deck = cardManager.cards;
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(i, deck.Count);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
            Debug.Log(deck[randomIndex]);
        }
        // ShuffleDeck은 카드를 셔플하는 역할만 합니다.
    }

    [Client]
    public void SetGameCard(List<GameObject> deck, Transform cardSpawnPos, Player player)
    {
        Debug.Log(player);
        if (count <= 0)
        {
            return;
        }

        var cardObj = deck[deck.Count - count];
        if (cardObj == null || cardSpawnPos == null)
        {
            return;
        }

        GameObject showCard = Instantiate(cardObj, cardSpawnPos);
        Cards cardComponent = showCard.GetComponent<Cards>();
        if (cardComponent != null)
        {
            playerCurrentCard[player] = cardComponent.value;
        }
        count--;
        Debug.Log(player.name);
    }

    [ClientRpc]
    public void JudgeWinner()
    {
        Player winner = null;
        int highestValue = -1;

        foreach (KeyValuePair<Player, int> entry in playerCurrentCard)
        {
            if (entry.Value > highestValue)
            {
                highestValue = entry.Value;
                winner = entry.Key;
            }
        }

        if (winner != null)
        {
            Debug.Log("Winner is: " + winner.name + " with card value: " + highestValue);
            
        }
        else
        {
            Debug.Log("No winner could be determined.");
        }
    }
}
