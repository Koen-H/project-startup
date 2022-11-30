using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class EffectMushroom : MonoBehaviour
{

    [SerializeField]
    //By how much does the object grow
    float growthStrength = 3;
    //How fast does the object grow
    [SerializeField]
    float growthSpeed = 2;
    [SerializeField]
    //For how long does this effect stay?
    float growthDuration = 8;

    float startTime;
    float t = 1;
    float scale = 0;

    bool growing, isFullyGrown, shrinking = false;

    Vector3 targetScale, originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = Vector3.one + originalScale * (growthStrength * 0.9f);
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
            float closestPlayerDistance = float.MaxValue;

            for (int i = 0; i < 360; i += 4)
            {
                float angle = i * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
                Vector3 position = this.gameObject.transform.position;

                Physics.Raycast(position, direction, out RaycastHit hit, 2);
                if (hit.collider != null && hit.distance < closestPlayerDistance && hit.collider.gameObject.layer == 6) ApplyEffect(hit.collider.gameObject);

                Debug.DrawRay(position, direction * 2, Color.red);

            }
        }
        if (shrinking) Shrink();

    }

    private void ApplyEffect(GameObject obj)
    {
        if (obj.GetComponent<EffectFlatten>() != null)
        {
            obj.GetComponent<EffectFlatten>().flattenDuration = 4;
            Debug.Log("Already flattened, extended duration!");
        }
        else if (obj.GetComponent<EffectMushroom>() != null)
        {
            Debug.Log("Not shrinking, is using mushroom");
        }
        else obj.AddComponent<EffectFlatten>();
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
        if(transform.localScale.x > targetScale.x)
        {
            isFullyGrown = true;
            growing = false;
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
