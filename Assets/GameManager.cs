using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<PlayerData> players = new List<PlayerData>();

    [SerializeField] GameObject preGameCamera;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameReady();
        }
    }



    /// <summary>
    /// Once everyone's ready, start the cinematic where the road to bread is shown.
    /// It should automatically trigger the countdown when it's ready.
    /// </summary>
    private void GameReady()
    {
        if (preGameCamera != null) preGameCamera.GetComponent<PreGameCamera>().StartAnim();
        else Debug.LogError("There was no pre-game camera found!");

        //preGameCamera.GetComponent<Animation>()["test"].speed
    }

    public void AddPlayer(PlayerData newPlayer)
    {
        newPlayer.name = "player " + (players.Count + 1);
        players.Add(newPlayer);

        //players.disableplayermovement or something like that
    }

    public void StartGame()
    {
        
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
        //players.enableplayermovement or something like that
        yield return new WaitForSeconds(1);//Remove GO after one second
    }   
}
