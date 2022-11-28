using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaEnemy : EnemyController
{
    bool die; 
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
        if ((other.transform.position - transform.position).y > 0 && other.GetComponent<Rigidbody>().velocity.y > 1) {
           die = true;
           Destroy(gameObject);
            }
    }
}
