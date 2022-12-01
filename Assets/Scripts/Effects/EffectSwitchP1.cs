using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSwitchP1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   GameObject SelectRandomPlayer()
    {
        GameObject randomPlayer = null;
        while (randomPlayer == null || randomPlayer == this.gameObject)
        {
            randomPlayer = GameManager.Instance.players[Random.Range(0, GameManager.Instance.players.Count)].gameObject;
                
        }

        return randomPlayer;
    }
}
