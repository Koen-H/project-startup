using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusTest : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction;
    void Start()
    {
        float angle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += direction * Time.deltaTime;
    }
}
