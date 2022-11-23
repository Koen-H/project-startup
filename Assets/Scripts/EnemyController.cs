using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float enemySpeed;

    bool rechedTarget; 
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - target.position).magnitude < 1.5) rechedTarget = true;
        else rechedTarget = false;

    }
    private void FixedUpdate()
    {
        if (target != null && !rechedTarget) Movements();
    }

    public virtual void Movements()
    {
        transform.LookAt(new Vector3(target.position.x, 0, target.position.z));
        transform.position += transform.forward * enemySpeed; 
    }

    public virtual void EnemyAI()
    {

    }
}
