using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<PlayerData> players = new List<PlayerData>(); 

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
    private void Start()
    {

    }

    public void AddPlayer(PlayerData newPlayer)
    {
        newPlayer.name = "player " + (players.Count + 1);
        players.Add(newPlayer);
    }

    public void StartGame()
    {
        Time.timeScale = 0f;
        StartCoroutine(StartCountDown());
    }

    public void Winner(GameObject playerWon)
    {
        Debug.Log($"{playerWon.GetComponent<PlayerData>().name} won!");
    }


    private IEnumerator StartCountDown()
    {
        Debug.Log("Starting countdown");
        Debug.Log("3");
        yield return new WaitForSeconds(1);//3
        Debug.Log("2");
        yield return new WaitForSeconds(1);//2
        Debug.Log("1");
        yield return new WaitForSeconds(1);//1
        Debug.Log("Go!");
        Time.timeScale = 1f;//Go!
        yield return new WaitForSeconds(1);//1
    }   
}
