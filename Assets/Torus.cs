using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torus : MonoBehaviour
{

    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 360; i += 4)
        {
            Quaternion angle = Quaternion.AngleAxis(i, Vector3.up);
            Instantiate(prefab, this.transform.position, angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
