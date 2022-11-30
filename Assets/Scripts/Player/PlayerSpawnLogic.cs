using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnLogic : MonoBehaviour
{
    public CheckPoint currentCheckPoint;
    [SerializeField] GameObject playerMesh;
    GameObject parent;
    [SerializeField] float SPAWN_PROTECTION_DURATION = 4;
    bool spawnProtection = false;
    float spawnProtectionTimer;
    float hologramAlphaValue;
    [SerializeField] float flickerFrequency = 10;
    [SerializeField] Material flickerMat;
    [SerializeField] Material defaultMat;
    [SerializeField] MeshRenderer meshRenderer;



    [SerializeField] public bool dead = false; 
    // Start is called before the first frame update
    void Start()
    {
        currentCheckPoint = GameObject.FindObjectOfType<CheckPoint>();
        parent = transform.parent.gameObject;
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

        

        spawnProtection = true;
        meshRenderer.material = flickerMat;



        // Rest any health or ability attributes
    }

    public void SpawnPlayer()
    {
        parent.transform.SetPositionAndRotation(currentCheckPoint.transform.position, Quaternion.AngleAxis(0,Vector3.up));
        parent.GetComponent<Movement>().enabled = true;
        playerMesh.SetActive(true);
    }

    void SpawnProtection()
    {
        spawnProtectionTimer += Time.deltaTime;
        hologramAlphaValue = 0.4f * Mathf.Cos(spawnProtectionTimer * flickerFrequency) + 0.6f;
        flickerMat.SetFloat("_Alpha", hologramAlphaValue);
       
        if(spawnProtectionTimer > SPAWN_PROTECTION_DURATION)
        {
            spawnProtectionTimer = 0;
            flickerMat.SetFloat("_Alpha", 1);
            spawnProtection = false;
            meshRenderer.material = defaultMat; 

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SpawnPlayer();
        if(spawnProtection) SpawnProtection();
    }
}
