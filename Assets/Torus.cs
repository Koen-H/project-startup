using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TorusRender))]
public class Torus : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _distance = 10f;
    [SerializeField] private float _size = 3f;
    private TorusRender _render;
    public GameObject torusPartPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _render = GetComponent<TorusRender>();
        _render.SetSpeed(_speed);
        _render.SetDistance(_distance);
        _render.SetHeight(_size);
        for (int i = 0; i < 360; i += 4)
        {
            Quaternion angle = Quaternion.AngleAxis(i, Vector3.up);
            GameObject torusPart = Instantiate(torusPartPrefab, this.transform.position, angle);

            torusPart.GetComponent<TorusTest>().SetSpeed(_speed);
            torusPart.transform.localScale = new Vector3(_size, _size, _size);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
