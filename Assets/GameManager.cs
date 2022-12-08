using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<PlayerData> players = new List<PlayerData>();
    public bool disablePlayerInput = true;


    [SerializeField] GameObject preGameCamera;
    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject waitingForPlayersIMG;

    [SerializeField] PlayerData winner = null;

    [SerializeField] GameObject globalCanvas;
    [SerializeField] TextMeshProUGUI topText;
    [SerializeField] TextMeshProUGUI centerText;

    AudioSource backgroundMusic;
    [SerializeField] AudioClip raceCountdown1;
    [SerializeField] AudioClip raceCountdown2;
    [SerializeField] AudioClip raceCountdown3;
    [SerializeField] AudioClip raceStart;
    [SerializeField] AudioClip raceLoop;
    [SerializeField] AudioClip raceWin;




   // AudioManager audioManager;

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
        backgroundMusic = this.GetComponent<AudioSource>();
    }
    private void Start()
    {
        topText.text = $"Currently {players.Count} players detected. Game auto-starts at 2!";
        centerText.text = "Press any button to ready up and join the game. ";
        //audioManager = AudioManager.Instance;
        
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
        GameUI.SetActive(true);
        waitingForPlayersIMG.SetActive(false);
        topText.text = "Be the first to reach the golden bread!";
        centerText.text = "";
        
        //preGameCamera.GetComponent<Animation>()["test"].speed
    }

    public void AddPlayer(PlayerData newPlayer)
    {
        newPlayer.name = "player " + (players.Count + 1);
        newPlayer.SetName(newPlayer.name);
        players.Add(newPlayer);
        topText.text = $"Currently {players.Count} players detected. Game auto-starts at 2!";
        if (disablePlayerInput) TogglePlayerInput(false);
        if (players.Count == 2) GameReady();//For now, it's a 1v1 so we start the game once 2 players are connected.
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
        StartCoroutine(WinnerCountDown());

    }

    public void TogglePlayerInput(bool allowInput)
    {
        foreach (PlayerData player in GameManager.Instance.players)//Look at the winner
        {
            player.gameObject.GetComponentInChildren<Movement>().allowInput = allowInput;
            player.gameObject.GetComponent<randomQuack>().enabled = allowInput;
            player.gameObject.GetComponentInChildren<PlaceBlocksAbility>().enabled = allowInput;
        }
    }


    private IEnumerator StartCountDown()
    {
        Debug.Log("Starting countdown");
        centerText.text = "3";
        backgroundMusic.clip = raceCountdown3;
        backgroundMusic.Play();
        yield return new WaitForSeconds(1);//3
        centerText.text = "2";
        backgroundMusic.clip = raceCountdown2;
        backgroundMusic.Play();
        yield return new WaitForSeconds(1);//2
        centerText.text = "1";
        backgroundMusic.clip = raceCountdown1;
        backgroundMusic.Play();
        yield return new WaitForSeconds(1);//1
        TogglePlayerInput(true);
        if (disablePlayerInput) TogglePlayerInput(true);
        centerText.text = "GO!";
        backgroundMusic.clip = raceStart;
        backgroundMusic.Play();
        yield return new WaitForSeconds(1.5f);//Remove GO after one second
        backgroundMusic.clip = raceLoop;
        backgroundMusic.loop = true;
        backgroundMusic.Play();
        centerText.text = "";
        topText.text = "";
    }

    private IEnumerator WinnerCountDown()
    {
        backgroundMusic.clip = raceWin;
        backgroundMusic.Play();
        backgroundMusic.loop = false;
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("StartMenuScene");

       
    }
}
