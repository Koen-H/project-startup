using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnLogic : MonoBehaviour
{
    public CheckPoint currentCheckPoint;
    [SerializeField] GameObject playerMesh;
    [SerializeField] GameObject parent;

    [SerializeField] public bool dead = false; 
    // Start is called before the first frame update
    void Start()
    {
        currentCheckPoint = GameObject.FindObjectOfType<CheckPoint>();
    }

    public void DespawnPlayer()
    {
      //  playerMesh.SetActive(false);
        Movement playerMovementScript = parent.GetComponent<Movement>();
        playerMovementScript.enabled = true;
        playerMovementScript.rigidBody.velocity = Vector3.zero;
        SpawnPlayer();
        //StopCoroutine(playerDash.Dashing()); 
        dead = true;

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
