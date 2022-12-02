using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwap : DiceItem
{

    void Update()
    {
        if (afterDiceDelay)
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
                    ApplyEffect(hit.collider.gameObject);
                    break;
                }

                Debug.DrawRay(position, direction * 0.4f, Color.red);

            }
        }
    }

    private void ApplyEffect(GameObject obj)
    {
        obj.AddComponent<EffectSwitchP1>();
        Destroy(gameObject);
    }
}
