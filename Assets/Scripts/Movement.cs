using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    Vector3 movement = Vector3.zero;
    public Rigidbody rigidBody;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool Player1 = true;


    // Update is called once per frame
    void Update()
    {

        if (Player1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                movement.z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement.z -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movement.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movement.x += 1;
            }
        }
        else if (!Player1)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movement.z += 1;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movement.z -= 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x -= 1;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.x += 1;
            }
        }
                //    movement.x += Input.GetAxis("Horizontal");
                //    movement.z += Input.GetAxis("Vertical");

                if (movement.y == 0) movement.Normalize();

        Vector3 playerPosition = this.transform.position;
        Ray ray = new Ray(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 0.3f);

        if (Input.GetKeyDown(KeyCode.LeftShift) && Physics.Raycast(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down, 0.3f, groundLayer))
        {
            movement.y = 25f;
        }

        
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(movement * Time.deltaTime * speed, ForceMode.Impulse);

        movement = Vector3.zero;
    }
}
