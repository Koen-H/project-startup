using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnLogic : MonoBehaviour
{
    public GameObject currentCheckPoint;
    [SerializeField] GameObject playerMesh;
    [SerializeField] GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DespawnPlayer()
    {
        playerMesh.SetActive(false);
        Movement playerMovementScript = parent.GetComponent<Movement>();
        playerMovementScript.enabled = false;
        playerMovementScript.rigidBody.velocity = Vector3.zero;

        // Rest any health or ability attributes
    }

    public void SpawnPlayer()
    {
        parent.transform.SetPositionAndRotation(currentCheckPoint.transform.position, Quaternion.AngleAxis(0,Vector3.up));
        parent.GetComponent<Movement>().enabled = true;
        playerMesh.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SpawnPlayer(); 
    }
}
