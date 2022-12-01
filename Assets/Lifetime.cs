using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{

    [SerializeField] float LIFETIME_TIMER = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LIFETIME_TIMER < 0)
        {
            Destroy(this.gameObject);
        }

        LIFETIME_TIMER -= Time.deltaTime;
        Debug.Log(LIFETIME_TIMER);
    }
}
