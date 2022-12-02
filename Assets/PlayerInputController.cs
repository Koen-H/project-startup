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

    private void Awake()
    {
        playerInputManager = this.GetComponent<PlayerInputManager>();
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
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
        players.Add(player);

        Transform playerParent = player.transform;
        playerParent.position = startingPoints[players.Count - 1].position;

        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        playerParent.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layerToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        gameManager.AddPlayer(playerParent.GetComponentInChildren<PlayerData>());
        /*playerParent.GetComponentInChildren<InputHandler>().look = player.actions.FindAction("Look");
        Debug.Log(playerParent.GetComponentInChildren<InputHandler>());
        Debug.Log(player.actions.FindAction("Look"));
        Debug.Log(playerParent.GetComponentInChildren<InputHandler>().look);*/

    }

}
