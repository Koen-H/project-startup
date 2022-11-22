using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PushAbility : MonoBehaviour
{



    private void Update()
    {

        for (int i = 0; i < 360; i += 4)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Vector3 position = this.gameObject.transform.position + new Vector3(0, 0.5f, 0);

            Physics.Raycast(position, direction, out RaycastHit hit, 2);

            Debug.DrawRay(position, direction * 2, Color.red);

        }
    }
}
