using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlaceBlocksAbility))]
public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> playerStorage;
    int currentBlockIndex = 0;
    PlaceBlocksAbility place;

    // Start is called before the first frame update
    void Start()
    {
        place = GetComponent<PlaceBlocksAbility>();
    }


    public void SwitchNextItem(InputAction.CallbackContext context)
    {
        NextItem();
    }
    public void SwitchPreviousItem(InputAction.CallbackContext context)
    {
        PreviousItem();
    }



    void NextItem()
    {
        currentBlockIndex++;
        if (currentBlockIndex > playerStorage.Count + 1) currentBlockIndex = 0;
        BlockPlacing();

    }

    void PreviousItem() {
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
