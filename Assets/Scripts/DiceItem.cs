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

    private IEnumerator ItemDiceDelay()
    {
        yield return new WaitForSeconds(1.5f);
        afterDiceDelay = true;
    }
}
