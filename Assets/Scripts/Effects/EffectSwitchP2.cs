using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectSwitchP2 : MonoBehaviour
{
    // Start is called before the first frame update
    bool doAnimate = true;

    [SerializeField] Material teleportMat;
    Material[] teleportMats;
    [SerializeField] Material[] defaultMat;
    [SerializeField] SkinnedMeshRenderer meshRenderer;

    public void Start()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        defaultMat = meshRenderer.materials;
        teleportMat = Resources.Load<Material>("Materials/TeleportMat");

        teleportMats = meshRenderer.materials;
        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            Debug.Log("loop");
            teleportMats[i] = teleportMat;
        }
        StartCoroutine(Animate());
    }

    public void Stop()
    {
        //Set animation back to normal and then destroy
        doAnimate = false;
        meshRenderer.materials = defaultMat;
        Destroy(this);
    }



    private IEnumerator Animate()
    {
        while (doAnimate)
        {
            meshRenderer.materials = teleportMats;
            yield return new WaitForSeconds(0.5f);
            meshRenderer.materials = defaultMat;
            yield return new WaitForSeconds(0.5f);
        }
        
        
    }
}
