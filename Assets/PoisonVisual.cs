using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoisonVisual : MonoBehaviour
{

    [SerializeField] Transform PoisonCanvas;

    private void Start()
    {
        if (PoisonCanvas == null) Debug.LogError("NO POISONCANVAS");
        DeactivatePoison();
    }
    public void ActivatePoison()
    {
        Debug.Log("test");
        PoisonCanvas.gameObject.SetActive(true);
    }

    public void DeactivatePoison()
    {
        PoisonCanvas.gameObject.SetActive(false);
    }
}
