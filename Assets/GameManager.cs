using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<PlayerData> players = new List<PlayerData>();
    public bool disablePlayerInput = true;


    [SerializeField] GameObject preGameCamera;

    [SerializeField] PlayerData winner = null;

    [SerializeField] GameObject globalCanvas;
    [SerializeField] TextMeshProUGUI topText;
    [SerializeField] TextMeshProUGUI centerText;

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
        topText.text = "";
        centerText.text = "Press any button to ready up!";
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

        topText.text = "Be the first to reach the golden bread!";
        centerText.text = "";
        
        //preGameCamera.GetComponent<Animation>()["test"].speed
    }

    public void AddPlayer(PlayerData newPlayer)
    {
        newPlayer.name = "player " + (players.Count + 1);
        players.Add(newPlayer);
        if (disablePlayerInput) TogglePlayerInput(false);
    }

    public void StartGame()
    {

        StartCoroutine(StartCountDown());
    }

    public void Winner(GameObject playerWon)
    {
        if (winner != null) return;
        winner = playerWon.GetComponent<PlayerData>();

        topText.text = $"{winner.GetName()} won!";

        Debug.Log($"{winner.GetName()} won!");
        foreach (PlayerData player in GameManager.Instance.players)//Look at the winner
        {
            CinemachineFreeLook cam = player.gameObject.GetComponentInChildren<CinemachineFreeLook>();
            cam.Follow = playerWon.transform;
            cam.LookAt = playerWon.transform.Find("PointLookAt").transform;
        }
    }

    public void TogglePlayerInput(bool allowInput)
    {
        foreach (PlayerData player in GameManager.Instance.players)//Look at the winner
        {
            player.gameObject.GetComponentInChildren<Movement>().allowInput = allowInput;
        }
    }


    private IEnumerator StartCountDown()
    {
        Debug.Log("Starting countdown");
        centerText.text = "3";
        yield return new WaitForSeconds(1);//3
        centerText.text = "2";
        yield return new WaitForSeconds(1);//2
        centerText.text = "1";
        yield return new WaitForSeconds(1);//1
        TogglePlayerInput(true);
        if (disablePlayerInput) TogglePlayerInput(true);
        centerText.text = "GO!";
        yield return new WaitForSeconds(1);//Remove GO after one second
        centerText.text = "";
    }   
}
