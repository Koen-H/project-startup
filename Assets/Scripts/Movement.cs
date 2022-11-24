using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    Vector3 movement = Vector3.zero;
    Vector2 movement2d = Vector2.zero;
    bool jumped = false;
    public Rigidbody rigidBody;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool Player1 = true;
    const float STANDARD_SPEED = 25f;

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

                if (movement.y == 0) movement.Normalize();

        Vector3 playerPosition = this.transform.position;
        Ray ray = new Ray(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 0.3f);

        if (jumped && Physics.Raycast(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down, 0.3f, groundLayer))
        {
            movement.y = 30f;
        }

        
    }

    private void FixedUpdate()
    {
        movement = new Vector3(movement2d.x, movement.y, movement2d.y); 

        rigidBody.AddForce(movement * Time.deltaTime * speed, ForceMode.Impulse);

        movement = Vector3.zero;
        //movement2d = Vector2.zero;
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


}
