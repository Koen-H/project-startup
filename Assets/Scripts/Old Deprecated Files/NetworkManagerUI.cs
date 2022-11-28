using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode; 
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private UnityTransport networkTransport; 
    [SerializeField] private Button serverButton; 
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TMP_InputField ipInput;





    private void Start()
    {

    }

    private void Awake()
    {
        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });

        ipInput.onEndEdit.AddListener((string ip) =>
        {
            Debug.Log("Ip input was : " + ip);
            networkTransport.ConnectionData.Address = ip; 
        });


    }

}
