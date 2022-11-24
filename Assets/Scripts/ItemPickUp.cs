using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
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
        if (other.gameObject.layer == 6)
        {
            Debug.Log("Item Should be picked up ");

            GameObject newItem = Instantiate(this.gameObject, (other.transform.position - other.transform.forward*2), Quaternion.identity); 

            this.gameObject.SetActive(false);
            other.gameObject.GetComponentInChildren<PlayerInventory>().playerStorage.Add(this.gameObject);
            PlaceBlocksAbility placeBlocksAbility = other.gameObject.GetComponentInChildren<PlaceBlocksAbility>();
            placeBlocksAbility.hologramPrefab = this.gameObject;
            placeBlocksAbility.blockPrefab = this.gameObject;
            placeBlocksAbility.InstantiateHologram();
            this.gameObject.GetComponent<ItemPickUp>().enabled = false;
            this.gameObject.layer = 3;
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
