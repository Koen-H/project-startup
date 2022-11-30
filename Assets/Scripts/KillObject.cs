using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KillObject : MonoBehaviour
{
    float y = 0; 


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
            Debug.Log("PLayer should Despwan");
            other.gameObject.GetComponentInChildren<PlayerSpawnLogic>().DespawnPlayer();
        }
    }
}
