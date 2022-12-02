using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EffectSwitchP1 : MonoBehaviour
{
    GameObject randomPlayer;
    List<PlayerData> players = GameManager.Instance.players;

    void Start()
    {
        if (players.Count < 2)
        {
            Destroy(this);
            return;
        }
        foreach (PlayerData player in players)
        {
            //Add player teleport animation
            player.gameObject.AddComponent<EffectSwitchP2>();
        }
        players.Remove(GetComponent<PlayerData>());
        randomPlayer = SelectRandomPlayer();
        players.Remove(randomPlayer.GetComponent<PlayerData>());

        StartCoroutine(TeleportCountdown());
    }

    /// <summary>
    /// To cause some intension, a animation will be displayed on all players and every 3 seconds it will remove a animation on one player, causing some tension.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TeleportCountdown()
    {
        yield return new WaitForSeconds(3f);
        while (players.Count > 0)//Remove the animation of one player.
        {
            GameObject p = players[Random.Range(0, players.Count)].gameObject;
            p.GetComponent<EffectSwitchP2>().Stop();
            yield return new WaitForSeconds(3f);

        }
        //Teleport
        Vector3 p1Position = this.transform.position;
        this.transform.position = randomPlayer.transform.position;
        randomPlayer.transform.position = p1Position;
        Destroy(GetComponent<EffectSwitchP2>());
        Destroy(randomPlayer.GetComponent<EffectSwitchP2>());
        Destroy(this);
    }

    GameObject SelectRandomPlayer()
    {
        GameObject randomPlayer = null;
        Debug.Log(players.Count);
        while (randomPlayer == null || randomPlayer == this.gameObject)
        {
            randomPlayer = players[Random.Range(0, players.Count)].gameObject;
                
        }

        return randomPlayer;
    }
}
