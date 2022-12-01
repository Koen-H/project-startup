using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float closestPlayerDistance = float.MaxValue;

        for (int i = 0; i < 360; i += 4)
        {
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Vector3 position = this.gameObject.transform.position;

            Physics.Raycast(position, direction, out RaycastHit hit, 2);
            if (hit.collider != null && hit.distance < closestPlayerDistance && hit.collider.gameObject.layer == 6) ApplyEffect(hit.collider.gameObject);

            Debug.DrawRay(position, direction * 2, Color.red);

        }
    }

    private void ApplyEffect(GameObject obj)
    {
       obj.AddComponent<EffectFlatten>();
    }
}
