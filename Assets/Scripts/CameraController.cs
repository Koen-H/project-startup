using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Rigidbody playerRidgidBody;
    [SerializeField] Camera PlayerCamera;

    float FOV = 60;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerSpeed = Mathf.Sqrt(Mathf.Pow(playerRidgidBody.velocity.x,2) + Mathf.Pow(playerRidgidBody.velocity.z,2));
        PlayerCamera.fieldOfView = FOV + playerSpeed * 3; 
       // transform.localRotation = Quaternion.Euler(playerRidgidBody.velocity.magnitude, transform.rotation.y, transform.rotation.z); 
    }
}
