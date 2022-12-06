using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMovement : MonoBehaviour
{


    [SerializeField] private float MOVE_SPEED = 1000;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Quaternion rot = transform.rotation;

        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);


        Vector3 moveDir = MOVE_SPEED * (rot * Vector3.forward);
        moveDir *= -1;

        rb.AddRelativeForce(moveDir);

       
    }
}
