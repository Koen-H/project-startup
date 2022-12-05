using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TorusRender))]
public class Torus : DiceItem
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private float _size = 3f;
    private TorusRender _render;
    public GameObject torusPartPrefab;

    public GameObject playerPickedUp;

    // Update is called once per frame
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

                Physics.Raycast(position, direction, out RaycastHit hit, 0.8f);
                if (hit.collider != null && hit.distance < closestPlayerDistance && hit.collider.gameObject.layer == 6)
                {
                    playerPickedUp = hit.collider.gameObject;
                    DoBongo();
                    break;
                }

                Debug.DrawRay(position, direction * 0.4f, Color.red);

            }
        }
    }

    void DoBongo()
    {
        _render = GetComponent<TorusRender>();
        _render.SetSpeed(_speed);
        _render.SetDistance(_distance);
        _render.SetHeight(_size);
        _render.start = true;
        for (int i = 0; i < 360; i += 4)
        {
            Quaternion angle = Quaternion.AngleAxis(i, Vector3.up);
            GameObject torusPart = Instantiate(torusPartPrefab, this.transform.position, angle);

            torusPart.GetComponent<TorusTest>().SetSpeed(_speed);
            torusPart.GetComponent<TorusTest>().ExcludePlayer(playerPickedUp);
            torusPart.transform.localScale = new Vector3(_size, _size, _size);
        }
       // Destroy(gameObject);
    }
}
