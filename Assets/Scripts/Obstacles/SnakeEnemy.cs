using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : DiceItem
{
    [SerializeField] Animator animator; 
    private void OnTriggerEnter(Collider other)
    {

        if (!afterDiceDelay) return;
        //6 is the player layer
        if (other.gameObject.layer == 6)
        {
            other.gameObject.GetComponentInChildren<Movement>().FlipControlls();

            animator.SetBool("attack", true);

        }
        else animator.SetBool("attack", false);
    }

    private void FixedUpdate()
    {
        transform.LookAt(FindNearestPlayer().transform);
    }
}
