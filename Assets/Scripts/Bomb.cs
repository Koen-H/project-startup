using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Bomb : DiceItem
{
    [SerializeField] float EXPLOSION_RADIUS = 4;

    [SerializeField] Material flickerMat;
    [SerializeField] Material defaultMat;
    [SerializeField] MeshRenderer meshRenderer;

    Vector3 origin;
    Vector3 direction;

    [SerializeField] float explosionForce = 500f;
    [SerializeField] bool doExplode = false;
    bool exploded = false;

    [SerializeField] SkinnedMeshRenderer bomb;

    float expand = 0;
    float explode = 0;
    float gravity = 0;
    float t;

    private void Start()
    {
        StartCoroutine(DestroyBarrel());
        StartCoroutine(ItemDiceDelay());
    }

    void Update()
    {
        //if (afterDiceDelay)
        //{
        //    for (int i = 0; i < 360; i += 4)
        //    {
        //        float angle = i * Mathf.Deg2Rad;
        //        Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        //        Vector3 position = this.gameObject.transform.position;

        //        Physics.Raycast(position, direction, out RaycastHit hit, transform.localScale.x * 0.7f);
        //        if (hit.collider != null && hit.collider.gameObject.layer == 6)
        //        {
        //            break;
        //        }

        //        Debug.DrawRay(position, direction * transform.localScale.x * 0.7f, Color.red);
        //    }
        //}
        origin = transform.position;
        
       if (doExplode && !exploded) Explode();
        
    }
    private void FixedUpdate() // Dont fuck with it unless you wanna fix it 
    {
        if (afterDiceDelay)
        {
            t += Time.deltaTime;

            float vlaue = 10 * Mathf.Cos(t * 20) * Mathf.Sin(t * 16) * Mathf.Cos(t * 30 + 2);

            float expandFactor = Mathf.Lerp(0, 100, t / 2);
            expand = expandFactor + vlaue;
            if (t > 2) expand = 1;
            float explodeFactor = Mathf.Lerp(0, 100, t - 2);
            if (t > 2.1f) doExplode = true; // The float is the time when it explodes

            explode = explodeFactor;

            float gravityFactor = Mathf.Lerp(0, 100, (t - 2.1f) / 10);
            gravityFactor *= gravityFactor;

            gravity = gravityFactor;

            if (t > 3.1f) Destroy(this.gameObject);

            bomb.SetBlendShapeWeight(0, expand);
            bomb.SetBlendShapeWeight(1, explode);
            bomb.SetBlendShapeWeight(2, gravity);
        }

    }

    void Explode()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, EXPLOSION_RADIUS, transform.forward * 0.1f, 1f);
        //Debug.Log(hits.Length);
        foreach(RaycastHit hit in hits)
        {
            if(hit.rigidbody == null || hit.rigidbody == this.gameObject.GetComponent<Rigidbody>()) continue;
            float force = explosionForce * hit.rigidbody.mass;
            //Debug.Log(force);
            hit.rigidbody.AddExplosionForce(force, this.transform.position, EXPLOSION_RADIUS);
        }
        doExplode = false;
        exploded = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin);
        Gizmos.DrawWireSphere(origin, EXPLOSION_RADIUS);
    }

    //Could be improved, but fine for this prototype...
    private IEnumerator DestroyBarrel()
    {
        yield return new WaitForSeconds(8.0f);
        Destroy(this.gameObject);
    }
}
