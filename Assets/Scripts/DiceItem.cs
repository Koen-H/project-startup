using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceItem : MonoBehaviour
{
    public bool afterDiceDelay = false;

    private void Start()
    {
        StartCoroutine(ItemDiceDelay());
    }

    public IEnumerator ItemDiceDelay()
    {
        yield return new WaitForSeconds(1.5f);
        afterDiceDelay = true;
    }

    // The force with which the object will bounce away from the trigger
    public float bounceForce = 10.0f;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<KillObject>())
        {

            // Find the nearest player
            GameObject nearestPlayer = FindNearestPlayer();

            // Calculate the direction from the object to the nearest player
            Vector3 direction = nearestPlayer.transform.position - transform.position;




            // Normalize the direction vector
            direction = direction.normalized;
            direction += Vector3.up * 2;

            // Add force in the direction of the player to bounce the object away from the trigger
            GetComponent<Rigidbody>().AddForce(direction * bounceForce, ForceMode.Impulse);
        }

    }

    // Finds the nearest player game object
    public GameObject FindNearestPlayer()
    {
        // Find all objects with the "Player" tag
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Set the initial minimum distance to the maximum float value
        float minDistance = float.MaxValue;

        // Set the initial nearest player to null
        GameObject nearestPlayer = null;

        // Loop through all players
        foreach (GameObject player in players)
        {
            // Calculate the distance from the object to the player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // If the distance is smaller than the current minimum distance, set the nearest player
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPlayer = player;
            }
        }

        // Return the nearest player
        return nearestPlayer;
    }
}
