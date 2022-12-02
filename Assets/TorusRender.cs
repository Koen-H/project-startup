using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TorusRender : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private float distance = 10f;

    private float test = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //360 points for 360 degrees
        lineRenderer.positionCount = 360 / 4;
        if (test < distance) test += 0.05f;

        //Makes a position in the linerender for every degree
        for (int i = 0; i < 360 ; i += 4)
        {
            
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
            Vector3 position = (direction * test) + transform.position;
            lineRenderer.SetPosition(i/4, position);
        }

        if (test < distance) lineRenderer.widthMultiplier -= .02f;
    }
}
