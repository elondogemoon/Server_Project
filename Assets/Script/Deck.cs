using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List <GameObject> cards;

    public void ShuffleDeck()
    {
        List<GameObject> deck = cards;
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(i, deck.Count);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
            Debug.Log(deck[randomIndex]);
        }
    }

    public GameObject DrawCard()
    {
        return cards[cards.Count - GameManager.Instance.count];
    }
}
