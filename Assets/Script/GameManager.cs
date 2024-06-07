using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Reflection;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    public Player localPlayer;
    public Deck cardManager;
    [SyncVar]
    public int count = 10;
    private int drawCount = 0;
    public Dictionary<NetworkConnectionToClient, int> playerCurrentCard = new Dictionary<NetworkConnectionToClient, int>();
    public List<NetworkConnectionToClient> AllPlayers = new List<NetworkConnectionToClient>();
    public List<NetworkIdentity> SpawnedCard = new List<NetworkIdentity>();
    private void Awake()
    {
        Instance = this;
    }

    [Command(requiresAuthority = false)]
    public void PlayerConnection(NetworkConnectionToClient playerConn)
    {
        Debug.Log(playerConn);
        AllPlayers.Add(playerConn);
        playerCurrentCard.Add(playerConn, 0);
    }

    [Server]
    public void AddCardToDic(NetworkConnectionToClient playerConn, int value)
    {
        playerCurrentCard[playerConn] = value;
        drawCount += 1;

        // 클라이언트에서 서버로 명령을 보냅니다.
        WaitforBetting();
    }

    [Server]
    public void WaitforBetting()
    {
        Debug.Log("drawCount  " + drawCount);
        if(drawCount == 2)
        {
            Debug.Log("waitforbet");
            count--;
            Invoke(nameof(WaitAndCheckResult), 5);
        }
    }
    [Server]
    public void WaitAndCheckResult()
    {
        NetworkConnectionToClient winner = null;
        int highestValue = -1;

        foreach (KeyValuePair<NetworkConnectionToClient, int> entry in playerCurrentCard)
        {
            Debug.Log($"player{entry.Key.identity.netId},{entry.Value}");
            if (entry.Value > highestValue)
            {
                highestValue = entry.Value;
                winner = entry.Key;
            }
        }
        if (winner != null)
        {
            Debug.Log(winner);


            foreach (NetworkConnectionToClient conn in AllPlayers)
            {
                if (conn == winner)
                {
                    conn.identity.GetComponent<Player>().SetWinOrLose(true);
                }
                else if (conn != winner)
                {
                    conn.identity.GetComponent<Player>().SetWinOrLose(false);
                }
            }
        }
    }
    public void DestroyUsedCard()
    {
        //foreach (var item in playerCurrentCard)
        //{
            
        //}
    }
}
