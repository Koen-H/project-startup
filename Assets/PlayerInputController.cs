using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;
    [SerializeField]
    private PlayerInputManager playerInputManager;

    private GameManager gameManager;

    private ManageUI manage;

    private void Awake()
    {
        playerInputManager = this.GetComponent<PlayerInputManager>();
    }
    private void Start()
    {
        gameManager = GameManager.Instance;

        manage = GameObject.FindObjectOfType<ManageUI>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }
    private void onDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {

        Debug.Log("Added");
        players.Add(player);

        Transform playerParent = player.transform;

        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);
     //   Debug.LogError(layerToAdd);

        playerParent.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layerToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        gameManager.AddPlayer(playerParent.GetComponentInChildren<PlayerData>());
        
        if(startingPoints.Count > 1) playerParent.position = startingPoints[players.Count - 1].position;
        else if (startingPoints.Count == 1) playerParent.position = startingPoints[0].position;

        Debug.Log(manage);
        manage.AddPlayer(playerParent);

        /*playerParent.GetComponentInChildren<InputHandler>().look = player.actions.FindAction("Look");
        Debug.Log(playerParent.GetComponentInChildren<InputHandler>());
        Debug.Log(player.actions.FindAction("Look"));
        Debug.Log(playerParent.GetComponentInChildren<InputHandler>().look);*/

    }

}
