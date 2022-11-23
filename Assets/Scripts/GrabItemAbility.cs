using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.LightAnchor;

public class GrabItemAbility : MonoBehaviour
{

    //Write automatic getter
    [SerializeField] GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
    }
    
        
    private void Update()
    {
        float closestPlayerDistance = float.MaxValue;
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {

            for (int i = 0; i < 360; i += 4)
            {
                float angle = i * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
                Vector3 position = this.gameObject.transform.position;

                Physics.Raycast(position, direction, out RaycastHit hit, 2);


                if (hit.collider != null && hit.collider.gameObject != parent && hit.collider.gameObject.TryGetComponent<PickupableItem>(out PickupableItem otherPlayer))
                {
                    Debug.Log("Picked up Item");
                    Destroy(otherPlayer.gameObject);
                }
                Debug.DrawRay(position, direction * 2, Color.red);

            }
        }



    }
}
