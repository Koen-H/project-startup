using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    int playerNumber = 0;
    string playerName = "PlayerName";
    GameObject playerGameObject;
    Canvas playerCanvas;

    private void Awake()
    {
        playerGameObject = this.gameObject;
        this.playerCanvas = GetComponentInChildren<Canvas>();
    }

    public void SetName(string newName)
    {
        playerName = newName;
    }

    public string GetName()
    {
        return playerName;
    }
}
