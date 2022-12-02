using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectSwitchP2 : MonoBehaviour
{
    // Start is called before the first frame update
    bool doAnimate = true;
    MeshRenderer meshRenderer;
    Material defaultMat;
    Material teleportMat;

    public void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        defaultMat = meshRenderer.material;
        teleportMat = Resources.Load<Material>("Materials/TeleportMat");
        StartCoroutine(Animate());
    }

    public void Stop()
    {
        //Set animation back to normal and then destroy
        doAnimate = false;
        meshRenderer.material = defaultMat;
        Destroy(this);
    }



    private IEnumerator Animate()
    {
        while (doAnimate)
        {
            meshRenderer.material = teleportMat;
            yield return new WaitForSeconds(0.5f);
            meshRenderer.material = defaultMat;
            yield return new WaitForSeconds(0.5f);
        }
        
        
    }
}
