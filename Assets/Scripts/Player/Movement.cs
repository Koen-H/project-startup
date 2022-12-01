using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;

public class Movement : MonoBehaviour
{
    //  [Space(10)]
    [Header("Changable")]
    float speed = 50f;
    [SerializeField] float rotationSensitivity = 1f;
    [SerializeField] float STANDARD_SPEED = 25f;
    [SerializeField] float JUMP_FORCE = 30f;
    [SerializeField] float EXTRA_GRAVITY = 30f;
    [SerializeField] float FLIPPED_CONTROLLS_DURATION = 6; 

    [SerializeField] GameObject cameraObj;

    bool flippedControlls;
    int flippedControllsValue = 1;
    float flippedTime; 

    Quaternion to = Quaternion.identity;

    Vector3 movement = Vector3.zero;
    float jumpmovement = 0;
    Vector2 movement2d = Vector2.zero;
    Vector2 look2d = Vector2.zero;
    bool jumped = false;

    float time = 0;


    float jumpTimer = 0f;
    float jumpMaxTime = .2f;
  //  [Space(10)]
    [Header("Setup")]
    public Rigidbody rigidBody;
    [SerializeField] LayerMask groundLayer;

    bool grounded;
    Vector2 moving;




    private void Start()
    {
        SetStandardSpeed();


    }
    public void Move(InputAction.CallbackContext context)
    {
        //if (grounded) movement2d = context.ReadValue<Vector2>();

        Debug.Log("Moving");
        //  movement2d = context.ReadValue<Vector2>();
        if (grounded)  moving  = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        

       if (context.started)
        {
            jumped = true;
        }
       if (context.performed)
        {
            jumped = true;
        }
       if (context.canceled)
        {
            jumped = false;
        }
        Debug.Log("jumped");

    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    float angle = 0;
    // Update is called once per frame
    void Update()
    {
        movement2d = Vector2.zero;
        movement2d = moving;
     //   movement2d.x += Input.GetAxis("Horizontal");
     //  movement2d.y += Input.GetAxis("Vertical");

        movement2d.Normalize();
     //   Debug.Log(movement2d);
        Vector3 playerPosition = this.transform.position;
        Ray ray = new Ray(new Vector3(playerPosition.x, playerPosition.y + 0.1f, playerPosition.z), Vector3.down);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 0.3f);

        grounded = Physics.Raycast(new Vector3(playerPosition.x, playerPosition.y + 0.1f, playerPosition.z), Vector3.down, 0.3f, groundLayer);
        if (jumped && grounded)
        {
            movement.y = JUMP_FORCE;
          //  jumpmovement = JUMP_FORCE;
            jumpTimer = jumpMaxTime;
        }
        if (jumped)
        {
            if (jumpTimer > 0)
            {
                movement.y = JUMP_FORCE / 10;
                jumpTimer -= Time.deltaTime;
                Debug.Log("Jumping");
            }
        }
     
        //  transform.Rotate(0, look2d.x * rotationSensitivity, 0);

        //  Rotation();


       // Debug.Log(movement2d);
        //Rotate player
        //Quaternion to = Quaternion.Euler(movement2d.x, 0, movement2d.y);

        Vector3 movement3dAngle = new Vector3(movement2d.x, 0, movement2d.y);
        Quaternion from = transform.rotation;
        if (movement2d != Vector2.zero)
        {
            // float angle = Mathf.Asin(movement2d.y);
            // float angle = Mathf.Atan(movement2d.y / movement2d.x);
            //  float angle = Vector3.Angle(Vector3.forward, movement3dAngle);
            // Vector3 test = new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
            // if (test == Vector3.zero) test = Vector3.forward;
            //    angle = Vector3.Angle(Vector3.forward, movement3dAngle);
            //  angle = Vector3.Angle(Vector3.forward, movement3dAngle);
            angle = Vector3.Angle(Vector3.forward, movement3dAngle);
         //   Debug.Log(angle);

          //  float angleQuat = Quaternion.Angle(Quaternion.identity, from);
            if (movement2d.x < 0) angle *= -1f;

            // to = Quaternion.AngleAxis(angle, Vector3.up);
            //   angle = angle / Vector3.Distance(from.eulerAngles, to.eulerAngles);
            to = Quaternion.AngleAxis(angle, transform.up);
          //  Debug.Log(to);

            angle = Mathf.Clamp(angle, 0, 2);
            Quaternion middlePoint = Quaternion.AngleAxis(angle, transform.up);
         //   to = 
            //     Debug.Log(angle);
            //  angle *= Mathf.Rad2Deg;
            // to.Normalize();
        }

     //   time += Time.deltaTime / Vector3.Distance(from.eulerAngles, to.eulerAngles);
    //    if (time > 1) time = 0;
        Quaternion rotation = Quaternion.Slerp(from, to, 0.05f);
        float dAngle = (Quaternion.Angle(from, to));

       // Debug.Log(dAngle);
        float fromAngle = Quaternion.Angle(Quaternion.identity, from);
        float toAngle = Quaternion.Angle(Quaternion.identity, to);

        // if (from.y < to.y) dAngle *= -1f;
        // if (from.y * to.y < 0) dAngle *= -1f; 
        //Debug.Log(dAngle);
        // Debug.Log(fromAngle);
        // Debug.Log(toAngle);
        // if (dAngle >= 179 && (from.y * to.y < 0)) dAngle *= -1f;
        //    if (from.y * to.y < 0) dAngle *= -1f;
        //  if (from.y - to.y > 0) dAngle *= -1f;

        //     if (from.y - to.y > 0 && from.y * to.y < 0) dAngle *= -1f;

        //if (fromAngle > toAngle && (from.y * to.y < 0)) dAngle *= -1f;
        //   float dAngleClamped = Mathf.Clamp(dAngle, -2, 2);

        //    Quaternion change = Quaternion.AngleAxis(dAngleClamped, transform.up);


        //   Debug.Log(rotation);
        Debug.Log(Mathf.Abs(transform.rotation.y - rotation.eulerAngles.y));
        if (Mathf.Abs(transform.rotation.eulerAngles.y - rotation.eulerAngles.y) < .1f) transform.rotation = to;
        else transform.rotation = rotation;
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
        movements = transform.rotation * movements;
        
        movements.Normalize();
        if (size > 0)
        {
          //  Debug.Log(movements);
         //   Debug.Log(direction);
         //   Debug.Log(size);
        }
        

        Vector3 vec3 = new Vector3(-movement2d.x, 0, movement2d.y);
      //  Vector3 vec3 = Vector3.forward;
        vec3 = transform.rotation * vec3;
      //  Debug.Log(vec3);
        vec3.Normalize();
        movement2d = new Vector2(vec3.x, vec3.z);
        //  float sizeforward = Vector3.Dot(Vector3.forward, movements);

        // movement2d = transform.forward;


        //movements.Normalize();
        if (size < 0) size = 0;
        movement2d *= size;
      //   Debug.Log(movement2d);
        // Mathf.Abs(movement2d);
      //  movement2d *= -0.05f;

      //  movement2d = Vector2.zero;

    }

    public void FlipControlls()
    {
        if (flippedControlls) return;
        flippedControlls = true;
        flippedControllsValue = -1; 

    }
    void FlippedTimer()
    {
        flippedTime += Time.deltaTime; 
        if(flippedTime > FLIPPED_CONTROLLS_DURATION)
        {
            flippedTime = 0;
            flippedControlls = false;
            flippedControllsValue = 1; 
        }
    }

    private void FixedUpdate()
    {
        movement = new Vector3(movement2d.x * flippedControllsValue, movement.y, movement2d.y * flippedControllsValue);

        ExtraGravity(EXTRA_GRAVITY);

   //     Debug.Log(movement);
        rigidBody.AddRelativeForce(movement * Time.deltaTime * speed, ForceMode.Impulse);
        if(flippedControlls) FlippedTimer();

        movement = Vector3.zero;
        movement2d = Vector2.zero;
        // movement2d = Vector2.zero;
        jumpmovement = 0;
       // jumped = false;



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
        // look2d = context.ReadValue<Vector2>();

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
