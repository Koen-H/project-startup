using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<PlayerData> players = new List<PlayerData>(); 

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("GameManager is null");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    public void AddPlayer(PlayerData newPlayer)
    {
        players.Add(newPlayer);
    }
}
