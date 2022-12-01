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

    [SerializeField]
    private bool TOTAL_RANDOM = false;
    [SerializeField]
    private GameObject diceItem;



    // Start is called before the first frame update
    void Start()
    {
        if(diceItem == null || diceItem)
        {
            diceItem = randItem();
        }

    }
    
    GameObject randItem()
    {
        return diceItems[Random.Range(0, diceItems.Count)];
    }
    
    
    // Update is called once per frame
    void Update()
    {
        float closestPlayerDistance = float.MaxValue;

        for (int i = 0; i < 360; i += 4)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Vector3 position = this.gameObject.transform.position;

            Physics.Raycast(position, direction, out RaycastHit hit, transform.localScale.x * 0.7f);
            if (hit.collider != null && hit.distance < closestPlayerDistance && hit.collider.gameObject.layer == 6)
            {
                ShootItem();
                break;
            }
            Debug.DrawRay(position, direction * transform.localScale.x * 0.7f, Color.red);
        }
    }

    void ShootItem()
    {
        float rand = Random.Range(0f, 360f);
        float randTwo = Random.Range(-60f, -80f);
        //direction.rot  
        GameObject item = Instantiate(diceItem, shootFrom.transform.position, Quaternion.identity);
        shootFrom.transform.Rotate(Vector3.up, rand);
        shootFrom.transform.Rotate(Vector3.right, randTwo);
        item.GetComponent<Rigidbody>().AddForce(shootFrom.transform.forward * SHOOTSTRENGTH);
        item.GetComponent<DiceItem>().afterDiceDelay = false;
        Destroy(this.gameObject);

    }
}
