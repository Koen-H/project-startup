using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.LightAnchor;

public class PushAbility : MonoBehaviour
{

    [Header("Changable")]
    [SerializeField] float force = 500f;

    [Header("Setup")]
    //Write automatic getter
    [SerializeField] GameObject parent;
    Movement bumpPlayer;

    Vector3 bumpDirection;

    public void Push(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            float closestPlayerDistance = float.MaxValue;

            for (int i = 0; i < 360; i += 4)
            {
                float angle = i * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
                Vector3 position = this.gameObject.transform.position;

                Physics.Raycast(position, direction, out RaycastHit hit, 2);


                if (hit.collider != null && hit.collider.gameObject != parent && hit.distance < closestPlayerDistance && hit.collider.gameObject.TryGetComponent<Movement>(out Movement otherPlayer))
                {
                    bumpPlayer = otherPlayer;
                    closestPlayerDistance = hit.distance;
                    bumpDirection = hit.point - this.transform.position;
                }
                Debug.DrawRay(position, direction * 2, Color.red);

            }
        }
    }


    private void Update()
    {

        //if ()
        //{

        //    for (int i = 0; i < 360; i += 4)
        //    {
        //        float angle = i * Mathf.Deg2Rad;
        //        Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        //        Vector3 position = this.gameObject.transform.position;

        //        Physics.Raycast(position, direction, out RaycastHit hit, 2);


        //        if (hit.collider != null && hit.collider.gameObject != parent && hit.distance < closestPlayerDistance && hit.collider.gameObject.TryGetComponent<Movement>(out Movement otherPlayer))
        //        {
        //            bumpPlayer = otherPlayer;
        //            closestPlayerDistance = hit.distance;
        //            bumpDirection = hit.point - this.transform.position;
        //        }
        //        Debug.DrawRay(position, direction * 2, Color.red);

        //    }
        //}



    }

    private void FixedUpdate()
    {


        if (bumpPlayer != null)
        {
            Debug.Log("past");
            Rigidbody bumpPlayerRigidBody = bumpPlayer.gameObject.GetComponent<Rigidbody>();

            //No up bumping allowed
            bumpDirection.y = 0;
            bumpDirection.Normalize();
            Debug.Log(bumpDirection);
            Debug.Log(Time.deltaTime * force);
            bumpDirection *= (Time.deltaTime * force);
            Debug.Log(bumpDirection);
            bumpPlayerRigidBody.AddForce(bumpDirection, ForceMode.Impulse);

            bumpPlayer = null;
        }
    }
}
