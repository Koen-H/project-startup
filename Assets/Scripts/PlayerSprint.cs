using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    PlayerNetwork playerNetwork;

    const int MAX_SPRINT_AMOUNT = 100;
    float sprintAmount = 100f;

    const float STANDARD_SPEED = 5f;
    const float SPRINT_MODIFIER = 2f;
    const float REGEN_SPRINT_AMOUNT = 25f;
    const float DRAIN_SPRINT_AMOUNT = 100f;


    private void Start()
    {
        playerNetwork = this.gameObject.GetComponent<PlayerNetwork>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNetwork == null)
        {
            Debug.LogError("NO PLAYERNETWORK ON: " + gameObject.name);
            return;
        }

        playerNetwork.SetPlayerSpeed(STANDARD_SPEED);
        if (sprintAmount > 0 && Input.GetKey(KeyCode.Space))
        {
            sprintAmount -= DRAIN_SPRINT_AMOUNT * Time.deltaTime;
            playerNetwork.SetPlayerSpeed(STANDARD_SPEED * SPRINT_MODIFIER);

        } else if (sprintAmount < MAX_SPRINT_AMOUNT)
        {
            sprintAmount += REGEN_SPRINT_AMOUNT * Time.deltaTime;
        }

        Debug.Log("SprintAmount: " + sprintAmount);
    }
}
