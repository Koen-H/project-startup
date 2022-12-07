using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.LightAnchor;

public class PlayerDash : MonoBehaviour
{

    [Header("Dash (Changable)")]
    [SerializeField] float DASH_DURATION = 1;
    [SerializeField] float DASH_COOLDOWN = 4;
    [SerializeField] float DASH_STRENGTH = 1000;

    float dashTimer;
    bool dashReady = true;
    bool isDashing = false;

    [Header("Push (Changable)")]
    [SerializeField] bool INCLUDE_PUSH = true;
    [SerializeField] bool CONSTANT_PUSH = false;
    [SerializeField] bool NO_DASH = false;
    [SerializeField] float force = 500f;
    [SerializeField] bool useExplosionDash;
    [SerializeField] float explosionDashForce = 500f;

    [Header("Setup")]
    [SerializeField] GameObject parent;
    Movement bumpPlayer;

    [SerializeField]PlayerSpawnLogic spawnLogic;

    Vector3 bumpDirection;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing || CONSTANT_PUSH) CheckBounce();

        if (spawnLogic.dead)
        {
            dashTimer = 0;
            isDashing = false;
            dashReady = true;
            spawnLogic.dead = false;
        }



    }

    private void FixedUpdate()
    {
        if (isDashing) Dashing(); 

        if (bumpPlayer != null && INCLUDE_PUSH)
        {
            //Debug.Log("past");
            Rigidbody bumpPlayerRigidBody = bumpPlayer.gameObject.GetComponent<Rigidbody>();

            if (!useExplosionDash)
            {
                //No up bumping allowed
                bumpDirection.y = 0;
                bumpDirection.Normalize();
                bumpDirection *= (Time.deltaTime * force);
                bumpPlayerRigidBody.AddForce(bumpDirection, ForceMode.Impulse);

                
            }
            else if (useExplosionDash)
            {
                bumpPlayerRigidBody.AddExplosionForce(explosionDashForce, transform.position, Vector3.Distance(transform.position, bumpPlayer.transform.position) * 2);
                this.GetComponent<Rigidbody>().AddExplosionForce(explosionDashForce, transform.position, Vector3.Distance(transform.position, bumpPlayer.transform.position) * 2);
            }
            bumpPlayer = null;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (NO_DASH) return;
        if (context.action.triggered && dashReady && !isDashing)
        {
            isDashing = true;      
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
                //Debug.Log("hit");
                bumpPlayer = otherPlayer;
                closestPlayerDistance = hit.distance;
                bumpDirection = hit.point - this.transform.position;
            }
            Debug.DrawRay(position, direction * 2, Color.red);

        }
    }
    
    void Dashing()
    {
        dashReady = false;
        dashTimer += Time.fixedDeltaTime; 
        if (dashTimer > DASH_COOLDOWN)
        {
            isDashing = false;
            dashReady = true;
            dashTimer = 0;
            return;
        }


        if (dashTimer > DASH_DURATION) return;
        transform.parent.GetComponent<Rigidbody>().AddForce(this.transform.forward * DASH_STRENGTH);
    }
}
