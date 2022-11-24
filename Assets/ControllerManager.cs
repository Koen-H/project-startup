using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    /// <summary>
    /// Each gamepad is related to a player int
    /// </summary>
    Dictionary<int, Gamepad> gamepadPlayers;
    [SerializeField] GameObject playerController = null;

    // Start is called before the first frame update
    //void Start()
    //{
    //    gamepadPlayers = new Dictionary<int, Gamepad>();
    //    Debug.Log($"Currently {Gamepad.all.Count} gamepads found!");
    //    for (int i = 0; i < Gamepad.all.Count; i++)
    //    {
    //        Gamepad gamepad = Gamepad.all[i];
    //        Debug.Log($"Gamepad found:{gamepad.name}");
    //        gamepadPlayers.Add(i, gamepad);
    //        GameObject playerObj = Instantiate(playerController);
    //        playerObj.GetComponent<InputController>().setGamepad(gamepad);

    //    }
    //    InputSystem.onDeviceChange += (device, change) =>
    //    {
    //        switch (change)
    //        {
    //            case InputDeviceChange.Added:
    //                // New Device
    //                //TODO: Assign the device to a player
    //                gamepadPlayers.Add(gamepadPlayers.Count, (Gamepad)device);
    //                Debug.Log("New Controller plugged in!");
    //                break;
    //            case InputDeviceChange.Disconnected:
    //                // Device got unplugged.
    //                Debug.Log("Controller got unplugged!");
    //                break;
    //            case InputDeviceChange.Reconnected:
    //                // Plugged back in.
    //                //TODO: Re-assing the device to the player.
    //                Debug.Log(device.GetType());
    //                ((Gamepad)device).SetMotorSpeeds(0.2f, 0.2f);
    //                ((Gamepad)device).ResumeHaptics();
    //                Debug.Log("Controller plugged back in!");
    //                break;
    //            default:
    //                // See InputDeviceChange reference for other event types.
    //                break;
    //        }
    //    };
    //}

    // Update is called once per frame
    void Update()
    {
    }
    void Start()
    {

    }
}
