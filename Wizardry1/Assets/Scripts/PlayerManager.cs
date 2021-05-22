using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public GameObject Card1;
    public GameObject Card2;
    public GameObject PlayerArea;
    public GameObject EnemyArea;
    public GameObject DropZone;

    List<GameObject> cards = new List<GameObject>();

    public override void OnStartClient()
    {
        base.OnStartClient();

        PlayerArea = GameObject.Find("PlayerArea");
        EnemyArea = GameObject.Find("EnemyArea");
        DropZone = GameObject.Find("DropZone");
    }

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        cards.Add(Card1);
        cards.Add(Card2);
    }

    [Command]
    public void CmdDealCards()
    {
        GameObject card = Instantiate(cards[Random.Range(0,cards.Count)], new Vector2(0, 0), Quaternion.identity);
        NetworkServer.Spawn(card, connectionToClient);
        RpcShowCard(card, "Dealt");
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    [Command]
    void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played");
    }

    [ClientRpc]
    private void RpcShowCard(GameObject card, string type)
    {
        if(type == "Dealt")
        {
            if (hasAuthority)
            {
                card.transform.SetParent(PlayerArea.transform, false);
                card.GetComponent<CardFlipper>().ShowFront();
            }
            else
            {
                card.transform.SetParent(EnemyArea.transform, false);
                card.GetComponent<CardFlipper>().ShowBack();
            }
        }
        else if(type == "Played")
        {
            card.transform.SetParent(DropZone.transform,false);
            card.GetComponent<CardFlipper>().ShowFront();      
        }
    }
}
