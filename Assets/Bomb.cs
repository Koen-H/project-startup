using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Bomb : DiceItem
{
    [SerializeField] float EXPLOSION_RADIUS = 4;

    [SerializeField] Material flickerMat;
    [SerializeField] Material defaultMat;

    Vector3 origin;
    Vector3 direction;

    [SerializeField] float explosionForce = 500f;
    [SerializeField] bool explode = false;

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
        
       if (explode) Explode();
        
    }

    void Explode()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, EXPLOSION_RADIUS, transform.forward * 0.1f, 1f);
        Debug.Log(hits.Length);
        foreach(RaycastHit hit in hits)
        {
            if(hit.rigidbody == null) continue;
            hit.rigidbody.AddExplosionForce(explosionForce,this.transform.position, EXPLOSION_RADIUS);
        }
        explode = false;
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin);
        Gizmos.DrawWireSphere(origin, EXPLOSION_RADIUS);
    }

    //Could be improved, but fine for this prototype...
    private IEnumerator ExplosionCount()
    {
        yield return new WaitForSeconds(1.0f);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(0.20f);
        yield return new WaitForSeconds(0.15f);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(0.05f);
        Explode();
    }
}
