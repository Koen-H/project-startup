using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class LuckyDice : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> diceItems = new List<GameObject>();
    [SerializeField]
    private GameObject shootFrom;
    Vector3 direction = Vector3.forward;
    [SerializeField]
    private float SHOOTSTRENGTH = 1;


    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        ShootItem();
    }

    void ShootItem()
    {


        float rand = Random.Range(0f, 360f);
        float randTwo = Random.Range(-60f, -80f);
        //direction.rot  

        GameObject item = Instantiate(diceItems[0], shootFrom.transform.position, Quaternion.identity);
        shootFrom.transform.Rotate(Vector3.up, rand);
        shootFrom.transform.Rotate(Vector3.right, randTwo);
        item.GetComponent<Rigidbody>().AddForce(shootFrom.transform.forward * SHOOTSTRENGTH);
        shootFrom.transform.Rotate(Vector3.right, -randTwo);
        shootFrom.transform.Rotate(Vector3.up, -rand);
    }
}
