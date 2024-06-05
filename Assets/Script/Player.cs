using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public int Chip;
    public Cards cards;
    [SerializeField]
    public Transform showCard;

    public override void OnStartClient()
    {
        GameManager.Instance.player = this;
        GameManager.Instance.ShuffleDeck(showCard);
        GameManager.Instance.SetGameCard(GameManager.Instance.cardManager.cards, showCard, this);
    }

    private void Update()
    {
    }

    private void Awake()
    {
        var playerSetCard = GetComponentInChildren<Cards>();
        cards = playerSetCard.GetComponent<Cards>();
    }
}
