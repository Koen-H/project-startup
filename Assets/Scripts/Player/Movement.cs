using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

    [SerializeField] GameObject cameraObj;


    Quaternion to = Quaternion.identity;

    Vector3 movement = Vector3.zero;
    Vector2 movement2d = Vector2.zero;
    Vector2 look2d = Vector2.zero;
    bool jumped = false;

    float time = 0;
  //  [Space(10)]
    [Header("Setup")]
    public Rigidbody rigidBody;
    [SerializeField] LayerMask groundLayer;


    public void Move(InputAction.CallbackContext context)
    {
        movement2d = context.ReadValue<Vector2>();
        
    }
    public void Jump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;

    }


    float angle = 0;
    // Update is called once per frame
    void Update()
    {
        movement2d.x += Input.GetAxis("Horizontal");
        movement2d.y += Input.GetAxis("Vertical");

        movement2d.Normalize();

        Vector3 playerPosition = this.transform.position;
        Ray ray = new Ray(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 0.3f);

        if (jumped && Physics.Raycast(new Vector3(playerPosition.x, playerPosition.y - 0.9f, playerPosition.z), Vector3.down, 0.3f, groundLayer))
        {
            movement.y = JUMP_FORCE;
        }

        //  transform.Rotate(0, look2d.x * rotationSensitivity, 0);

        //  Rotation();


       // Debug.Log(movement2d);
        //Rotate player
        //Quaternion to = Quaternion.Euler(movement2d.x, 0, movement2d.y);

        Vector3 movement3dAngle = new Vector3(movement2d.x, 0, movement2d.y);
        Quaternion from = transform.rotation;
        if (Input.anyKey)
        {
            // float angle = Mathf.Asin(movement2d.y);
            // float angle = Mathf.Atan(movement2d.y / movement2d.x);
            //  float angle = Vector3.Angle(Vector3.forward, movement3dAngle);
           // Vector3 test = new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
           // if (test == Vector3.zero) test = Vector3.forward;
        //    angle = Vector3.Angle(Vector3.forward, movement3dAngle);
            angle = Vector3.Angle(Vector3.forward, movement3dAngle);
            if (movement2d.x < 0) angle *= -1f;

           // to = Quaternion.AngleAxis(angle, Vector3.up);
         //   angle = angle / Vector3.Distance(from.eulerAngles, to.eulerAngles);
            to = Quaternion.AngleAxis(angle, transform.up);

       //     Debug.Log(angle);
            //  angle *= Mathf.Rad2Deg;
           // to.Normalize();
        }

        time += Time.deltaTime / Vector3.Distance(from.eulerAngles, to.eulerAngles);
        if (time > 1) time = 0;
        Quaternion rotation = Quaternion.Slerp(from, to, time);

     //   Debug.Log(rotation);
             transform.rotation = rotation;
      //  transform.rotation = to;
        //Movement based on rotation
        Vector3 velocity = rigidBody.velocity;
        Vector3 movements = new Vector3(movement2d.x, 0, movement2d.y);


        //Project movement unto wanted rotation


        
        //Vector3 rotationDirectionVector = new Vector3(Mathf.Sin(transform.rotation.eulerAngles.x), 0, Mathf.Cos(transform.rotation.eulerAngles.y) * Mathf.Cos(transform.rotation.eulerAngles.x));
        float elevation = Mathf.Deg2Rad * transform.rotation.eulerAngles.x;
        float heading = Mathf.Deg2Rad * transform.rotation.eulerAngles.y;

        Vector3 direction = new Vector3(Mathf.Cos(elevation) * Mathf.Sin(heading), Mathf.Sin(elevation), Mathf.Cos(elevation) * Mathf.Cos(heading));

        float size = Vector3.Dot(movements, direction);
        // float size = Vector3.Dot(movements, direction);
        movements = rotation * movements;
        
        movements.Normalize();
        if (size > 0)
        {
          //  Debug.Log(movements);
         //   Debug.Log(direction);
            Debug.Log(size);
        }
        

        Vector3 vec3 = new Vector3(-movement2d.x, 0, movement2d.y);
      //  Vector3 vec3 = Vector3.forward;
        vec3 = rotation * vec3;
        Debug.Log(vec3);
        vec3.Normalize();
        movement2d = new Vector2(vec3.x, vec3.z);
        //  float sizeforward = Vector3.Dot(Vector3.forward, movements);

        // movement2d = transform.forward;


        //movements.Normalize();
        // Debug.Log(movement2d);
        if (size < 0) size = 0;
        movement2d *= size;
       // Mathf.Abs(movement2d);
        

     //   movement2d = Vector2.zero;

    }

    private void FixedUpdate()
    {
     //   Debug.Log(movement2d);
        movement = new Vector3(movement2d.x, movement.y, movement2d.y);

   //     Debug.Log(movement);
        rigidBody.AddRelativeForce(movement * Time.deltaTime * speed, ForceMode.Impulse);

        movement = Vector3.zero;
        movement2d = Vector2.zero;

        
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
     ///   look2d = context.ReadValue<Vector2>();

    }
    void Rotation()
    {
    //    transform.Rotate(0, Input.GetAxisRaw("Mouse X") * rotationSensitivity, 0);
    }


}
