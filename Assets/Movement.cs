using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    Vector3 movement = Vector3.zero;
    [SerializeField] Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x += Input.GetAxis("Horizontal");
        movement.z += Input.GetAxis("Vertical");

        movement.Normalize();
        if (Input.GetKeyDown(KeyCode.LeftShift))
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
