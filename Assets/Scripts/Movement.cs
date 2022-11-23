using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    Vector3 movement = Vector3.zero;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] LayerMask groundLayer;



    // Update is called once per frame
    void Update()
    {
        movement.x += Input.GetAxis("Horizontal");
        movement.z += Input.GetAxis("Vertical");

        movement.Normalize();

        Vector3 playerPosition = this.transform.position;
        Ray ray = new Ray(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 0.3f);

        if (Input.GetKeyDown(KeyCode.LeftShift) && Physics.Raycast(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down, 0.3f, groundLayer))
        {
            movement.y = 10f;
        }

        
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(movement * Time.deltaTime * speed, ForceMode.Impulse);

        movement = Vector3.zero;
    }
}
