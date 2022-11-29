using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        //6 is the player layer
        if (other.gameObject.layer == 6)
        {

            other.gameObject.GetComponentInChildren<PlayerSpawnLogic>().currentCheckPoint = this.gameObject.GetComponent<CheckPoint>(); //Setting Checkpoint
        }
    }
}
