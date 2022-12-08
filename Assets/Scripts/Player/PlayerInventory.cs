using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlaceBlocksAbility))]
public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> playerStorage;
    int currentBlockIndex = 0;
    public PlaceBlocksAbility place;



    public event EventHandler OnSwitchItem;

    // Start is called before the first frame update
    void Start()
    {
        place = GetComponent<PlaceBlocksAbility>();
    }


    public void SwitchNextItem(InputAction.CallbackContext context)
    {

        if (context.canceled)
        {
            NextItem();
            OnSwitchItem?.Invoke(this, EventArgs.Empty);
        }
    }
    public void SwitchPreviousItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PreviousItem();
            OnSwitchItem?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetCurrentIndex() { 
        return currentBlockIndex;
    }

    void NextItem()
    {
        Debug.Log("Next");
        currentBlockIndex++;
        if (currentBlockIndex >= playerStorage.Count) currentBlockIndex = 0;
        BlockPlacing();

    }

    void PreviousItem() {
        Debug.Log("Prev");
        currentBlockIndex--;
        if (currentBlockIndex < 0) currentBlockIndex = playerStorage.Count - 1;
        Debug.Log(currentBlockIndex);
        BlockPlacing();
    }

    void BlockPlacing() {
        place.hologramPrefab = playerStorage[currentBlockIndex];
        place.blockPrefab = playerStorage[currentBlockIndex];
        place.InstantiateHologram();
    }
}
