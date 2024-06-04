using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UIElements;
public class GameManager : NetworkBehaviour
{
    public List <Cards> Deck;
    public List <Player> Player;

    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    [Command]
    public void InitializeDeck()
    {
        Deck.Clear();
        for(int i = 1;i<10;i++)
        {
            Deck.Add(new Cards(i));
        }
        ShuffleDeck();
    }
    public void SetGameCard()
    {
        
    }
    [Command]
    public void ShuffleDeck()
    {
        for(int i = 0; i < Deck.Count; i++)
        {
            
        }
    }
}
