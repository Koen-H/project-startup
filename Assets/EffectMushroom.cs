using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EffectMushroom : MonoBehaviour
{

    [SerializeField]
    //By how much does the object grow
    float growthStrength = 10;
    //How fast does the object grow
    [SerializeField]
    float growthSpeed = 10;
    [SerializeField]
    //For how long does this effect stay?
    float growthDuration = 10;

    float startTime;
    float t = 1;
    float scale = 0;

    bool growing, isFullyGrown, shrinking = false;

    Vector3 targetScale, originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * growthStrength;
        startTime = Time.time;

        //If this effect is already active, extend the duration on the other component, if not. Start the coroutine!
        growing = true;
        StartCoroutine(GrowCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        t =  Time.time - startTime;
        if (growing) transform.localScale = Vector3.one * Grow() + originalScale; 

        if (isFullyGrown)
        {
            //Fully grown stuff like stamping on people
        }
        if (shrinking) Shrink();

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

        float a = growthStrength;
        float d = growthSpeed;
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

    private IEnumerator GrowCountdown()
    {
        while(growthDuration > 0)
        {
            growthDuration--;
            yield return new WaitForSeconds(1f);
        }
        growing = false;
        shrinking = true;
    }

    private void Shrink()
    {
        this.transform.localScale -= Vector3.one * Time.deltaTime * growthSpeed;
        if (this.transform.localScale.x < originalScale.x)
        {
            shrinking = false;
            isFullyGrown = false;
            this.transform.localScale = originalScale;
            Destroy(this);
        }
    }

}
