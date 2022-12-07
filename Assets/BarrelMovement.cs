using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMovement : MonoBehaviour
{


    [SerializeField] private float MOVE_SPEED = 1000;
    private Rigidbody rb;
    private Vector3 frwrd;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        frwrd = transform.forward *-1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Quaternion rot = transform.rotation;

        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);


        Vector3 moveDir = MOVE_SPEED * (rot * frwrd);
        moveDir *= -1;

        rb.AddRelativeForce(moveDir);

       
    }



    private void Update()
    {
        float closestPlayerDistance = float.MaxValue;

        for (int i = 0; i < 360; i += 4)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Vector3 position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y -0.5f, this.gameObject.transform.position.z);

            Physics.Raycast(position, direction, out RaycastHit hit, 2);
            if (hit.collider != null && hit.collider.gameObject.layer == 6) ApplyEffect(hit.collider.gameObject);

            Debug.DrawRay(position, direction * 2, Color.red);

        }
    }


    private void ApplyEffect(GameObject obj)
    {
        if (obj.GetComponent<EffectFlatten>() != null)
        {
            obj.GetComponent<EffectFlatten>().flattenDuration = 4;
            Debug.Log("Already flattened, extended duration!");
        }
        else if (obj.GetComponent<EffectMushroom>() != null)
        {
            Debug.Log("Not shrinking, is using mushroom");
        }
        else obj.AddComponent<EffectFlatten>();
    }
}
