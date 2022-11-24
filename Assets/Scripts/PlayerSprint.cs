using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{

    const int MAX_SPRINT_AMOUNT = 1;
    float sprintAmount = 100f;
    const float SPRINT_MODIFIER = 2f;
    const float REGEN_SPRINT_AMOUNT = 25f;
    const float DRAIN_SPRINT_AMOUNT = 100f;


    Movement movement;
    private void Start()
    {
        movement = this.gameObject.GetComponentInParent<Movement>();
    }

    // Update is called once per frame
    void Update()
    { 

        movement.SetStandardSpeed();
        if (sprintAmount > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            sprintAmount -= DRAIN_SPRINT_AMOUNT * Time.deltaTime;
            movement.SetPlayerSpeed(movement.GetStandardSpeed() * SPRINT_MODIFIER);

        } else if (sprintAmount < MAX_SPRINT_AMOUNT)
        {
            sprintAmount += REGEN_SPRINT_AMOUNT * Time.deltaTime;
        }

        Debug.Log("SprintAmount: " + sprintAmount);
    }
}
