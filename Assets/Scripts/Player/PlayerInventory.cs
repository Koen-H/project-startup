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


    public void Switch(InputAction.CallbackContext context)
    {
        NextItem();
    }



    void NextItem()
    {
        currentBlockIndex++;
        if (currentBlockIndex > playerStorage.Count + 1) currentBlockIndex = 0;
        place.hologramPrefab = playerStorage[currentBlockIndex];
        place.blockPrefab = playerStorage[currentBlockIndex];
        place.InstantiateHologram();

    }
}
