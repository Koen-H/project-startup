using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TorusRender : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private float distance = 10f;

    private float speed = 0.05f;

    private float size = 0.1f;

    private float height = 3.0f;

    [Tooltip("LineCount is how many points the circle consist from (the closer to 360 the sharper the circle, but more resource intensive)")] 
    [SerializeField] private int lineCount = 90;
    int step;

    public void SetSpeed(float amount) {
        speed = amount;
    }
    public void SetDistance(float amount)
    {
        distance = amount;
    }
    public void SetHeight(float amount)
    {
        height = amount;

    }
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        step = 360 / lineCount;
        lineRenderer.startWidth = height;
    }

    // Update is called once per frame
    void Update()
    {
        //360 points for 360 degrees
        lineRenderer.positionCount = lineCount;
        if (size < distance) size += speed * Time.deltaTime;


        //Makes a position in the linerender for every degree
        for (int i = 0; i < 360 ; i += step)
        {
            
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
            Vector3 position = (direction * size) + transform.position;
            lineRenderer.SetPosition(i/4, position);
        }

        if (lineRenderer.startWidth > 0) lineRenderer.startWidth = height * ((distance - size) / distance);
        else lineRenderer.widthMultiplier = 0;
    }
}
