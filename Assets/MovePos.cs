using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MovePos : MonoBehaviour
{


    [SerializeField] bool Player1 = true;
    Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player1)
        {
            if (Input.GetKey(KeyCode.T))
            {
                movement.z += .1f;
            }
            if (Input.GetKey(KeyCode.G))
            {
                movement.z -= .1f;
            }
            if (Input.GetKey(KeyCode.F))
            {
                movement.x -= .1f;
            }
            if (Input.GetKey(KeyCode.H))
            {
                movement.x += .1f;
            }
        }
        else if (!Player1)
        {
            if (Input.GetKey(KeyCode.P))
            {
                movement.z += .1f;
            }
            if (Input.GetKey(KeyCode.Semicolon))
            {
                movement.z -= .1f;
            }
            if (Input.GetKey(KeyCode.L))
            {
                movement.x -= .1f;
            }
            if (Input.GetKey(KeyCode.Quote))
            {
                movement.x += .1f;
            }
        }

      //  movement.Normalize();

        transform.position += movement;

        movement= Vector3.zero;
    }
}
