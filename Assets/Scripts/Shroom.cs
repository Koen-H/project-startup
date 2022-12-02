using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : DiceItem
{
    void Update()
    {
        if (afterDiceDelay)
        {
            for (int i = 0; i < 360; i += 4)
            {
                float angle = i * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
                Vector3 position = this.gameObject.transform.position;

                Physics.Raycast(position, direction, out RaycastHit hit, transform.localScale.x * 0.7f);
                if (hit.collider != null && hit.collider.gameObject.layer == 6)
                {
                    ApplyEffect(hit.collider.gameObject);
                    break;
                }

                Debug.DrawRay(position, direction * transform.localScale.x * 0.7f, Color.red);
            }
        }
    }

    private void ApplyEffect(GameObject obj)
    {
        if (obj.GetComponent<EffectMushroom>() != null)
        {
            obj.GetComponent<EffectMushroom>().growthDuration = 8;
            Debug.Log("Already using mushroom, extended duration!");
        }
        else obj.AddComponent<EffectMushroom>();
        Destroy(this.gameObject);
    }
}
