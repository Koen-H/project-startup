using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    int playerNumber = 0;
    string playerName = "PlayerName";
    GameObject playerGameObject;

    private void Awake()
    {
        playerGameObject = this.gameObject;
    }
}