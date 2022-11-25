using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float minVibrate;
    [SerializeField] float maxVibrate;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Gamepad.all.Count; i++) Debug.Log(Gamepad.all[i].name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            //if (Gamepad.all[0].leftStick.left.isPressed)
            //{
            //    this.transform.position += Vector3.left * Time.deltaTime * 4;
            //}
            //if (Gamepad.all[0].leftStick.right.isPressed)
            //{
            //    this.transform.position += Vector3.right * Time.deltaTime * 4;
            //}
            Vector3 joyStickDirection = new Vector3(Gamepad.all[0].leftStick.ReadValue().x, 0, Gamepad.all[0].leftStick.ReadValue().y);
            this.transform.position += joyStickDirection * movementSpeed;
        }
    }
}
