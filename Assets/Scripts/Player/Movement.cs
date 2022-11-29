using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //  [Space(10)]
    [Header("Changable")]
    float speed = 50f;
    [SerializeField] float rotationSensitivity = 1f;
    [SerializeField] float STANDARD_SPEED = 25f;
    [SerializeField] float JUMP_FORCE = 30f;
    [SerializeField] float EXTRA_GRAVITY = 30f;





    Vector3 movement = Vector3.zero;
    Vector2 movement2d = Vector2.zero;
    Vector2 look2d = Vector2.zero;
    bool jumped = false;
    //  [Space(10)]
    [Header("Setup")]
    public Rigidbody rigidBody;
    [SerializeField] LayerMask groundLayer;

    bool grounded;



    private void Start()
    {
        SetStandardSpeed();


    }
    public void Move(InputAction.CallbackContext context)
    {
        movement2d = context.ReadValue<Vector2>();

    }
    public void Jump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;

    }

    // Update is called once per frame
    void Update()
    {
        //movement2d.x += Input.GetAxis("Horizontal");
        //movement2d.y += Input.GetAxis("Vertical");

        movement2d.Normalize();

        Vector3 playerPosition = this.transform.position;
        Ray ray = new Ray(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 0.3f);

        grounded = Physics.Raycast(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down, 0.3f, groundLayer);
        if (jumped && grounded)
        {
            movement.y = JUMP_FORCE;
        }

        transform.Rotate(0, look2d.x * rotationSensitivity, 0);

        Rotation();



    }


    private void FixedUpdate()
    {
        movement = new Vector3(movement2d.x, movement.y, movement2d.y);

        ExtraGravity(EXTRA_GRAVITY);

        rigidBody.AddRelativeForce(movement * Time.deltaTime * speed, ForceMode.Impulse);

        movement = Vector3.zero;
        //movement2d = Vector2.zero;
    }

    public void SlowDownSpeed(float factor)
    {
        speed /= factor;

        Debug.Log("Speed is : " + speed + " factor of : " + factor); 
    }

    public void SetPlayerSpeed(float value)
    {
        speed = value;
    }

    public void SetStandardSpeed()
    {
        speed = STANDARD_SPEED;
    }

    public float GetStandardSpeed()
    {
        return STANDARD_SPEED;
    }

    public void Look(InputAction.CallbackContext context)
    {
  
        //transform.Rotate(0, context.ReadValue<Vector2>().x * rotationSensitivity, 0);
        look2d = context.ReadValue<Vector2>();

    }
    void Rotation()
    {
    //    transform.Rotate(0, Input.GetAxisRaw("Mouse X") * rotationSensitivity, 0);
    }

    void ExtraGravity(float _gravity)
    {
        if (!grounded)
        {
            movement.y -= _gravity; 
        }
    }


}
