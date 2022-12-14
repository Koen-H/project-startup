using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bread : MonoBehaviour
{
    GameManager gameManager;
    
    void Start()
    {
        gameManager = GameManager.Instance;
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

            Physics.Raycast(position, direction, out RaycastHit hit, 0.4f);
            if (hit.collider != null && hit.distance < closestPlayerDistance && hit.collider.gameObject.layer == 6)
            {
                gameManager.Winner(hit.collider.gameObject);
                break;
            }

            Debug.DrawRay(position, direction * 0.4f, Color.red);

        }
    }


}
