using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class EffectFlatten : MonoBehaviour
{


    //How fast does the object grow
    [SerializeField]
    float GROWTH_SPEED = 2;

    bool growBack = false;

    float startTime;
    float t = 1;
    float scale = 0;


    [SerializeField]
    float FLATTEN_STRENGTH = 0.2f;
    public float flattenDuration = 4f;

    Vector3 targetScale, originalScale;

    void Start()
    {
        growBack = false;
        originalScale = transform.localScale;
        targetScale = new Vector3(transform.localScale.x, originalScale.y * FLATTEN_STRENGTH, transform.localScale.z);
        this.transform.localScale = targetScale;
        StartCoroutine(FlattenCountdown());
    }

    void Update()
    {
        t = Time.time - startTime;
        if (growBack) transform.localScale = new Vector3(originalScale.x, Grow() + targetScale.y, originalScale.z);

        if (transform.localScale.y > originalScale.y * 0.9f)
        {
            transform.localScale = originalScale;
            Debug.Log(originalScale);
            Destroy(this);
        }
    }

    private IEnumerator FlattenCountdown()
    {
        while (flattenDuration > 0)
        {
            flattenDuration--;
            yield return new WaitForSeconds(1f);
        }
        startTime = Time.time;
        growBack = true;
    }

    private float Grow()
    {
        //this.transform.localScale += Vector3.one * Time.deltaTime * growthSpeed;
        //if (this.transform.localScale.x > targetScale.x)
        //{
        //    growing = false;
        //    isFullyGrown = true;
        //    this.transform.localScale = targetScale;
        //}

        float a = originalScale.y - FLATTEN_STRENGTH;
        float d = GROWTH_SPEED;
        float k = d / (3 * Mathf.PI);


        if (t > 0 && t < (Mathf.PI * k))
        {
            scale = a / 6 * Mathf.Cos(t / k + Mathf.PI) + a / 6;
        }
        else if (t > (Mathf.PI * k) && t < 2 * (Mathf.PI * k))
        {
            scale = a / 6 * Mathf.Cos(t / k + Mathf.PI * 2) + 3 * (a / 6);
        }
        else if (t > 2 * (Mathf.PI * k) && t < 3 * (Mathf.PI * k))
        {
            scale = a / 6 * Mathf.Cos(t / k + Mathf.PI) + 5 * (a / 6);
        }

        //Debug.Log("tme is : " + t + "scale is : " + scale);
        return scale;
    }
}
