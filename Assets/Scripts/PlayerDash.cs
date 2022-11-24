using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.LightAnchor;

public class PlayerDash : MonoBehaviour
{
    const float DASH_DURATION = 1;
    const float DASH_COOLDOWN = 4;
    const float DASH_STRENGTH = 1000;
    bool dashReady = true;
    bool isDashing = false;


    [SerializeField] const bool INCLUDE_PUSH = true;

    [SerializeField] GameObject parent;
    [SerializeField] float force = 500f;
    Movement bumpPlayer;

    Vector3 bumpDirection;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) CheckBounce();
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

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.action.triggered && dashReady)
        {
            StartCoroutine(Dashing());
        }

    }

    void CheckBounce()
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
                Debug.Log("hit");
                bumpPlayer = otherPlayer;
                closestPlayerDistance = hit.distance;
                bumpDirection = hit.point - this.transform.position;
            }
            Debug.DrawRay(position, direction * 2, Color.red);

        }
    }

    private IEnumerator Dashing()
    {
        dashReady = false;
        transform.parent.GetComponent<Rigidbody>().AddForce(this.transform.forward * DASH_STRENGTH);
        Debug.Log("DASH");
        isDashing = true;
        yield return new WaitForSeconds(DASH_DURATION);
        isDashing = false;
        yield return new WaitForSeconds(DASH_COOLDOWN);
        dashReady = true;
    }
}
