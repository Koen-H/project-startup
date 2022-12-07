using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class ManageUI : MonoBehaviour
{

    [SerializeField] Sprite pictureBlock;
    [SerializeField] Sprite pictureBarrel;
    [SerializeField] Image imagePlayerOne;
    [SerializeField] Image imagePlayerTwo;

    PlayerInventory PlayerOneInventory;
    PlayerInventory PlayerTwoInventory;

    public void AddPlayer(Transform player)
    {
        if (PlayerOneInventory == null)
        {
            PlayerOneInventory = player.GetComponentInChildren<PlayerInventory>();
            PlayerOneInventory.OnSwitchItem += PlayerOneInventory_OnSwitchItem;
        }
        else if (PlayerTwoInventory == null)
        {
            PlayerTwoInventory = player.GetComponentInChildren<PlayerInventory>();
            PlayerTwoInventory.OnSwitchItem += PlayerTwoInventory_OnSwitchItem;

        }
        else Debug.LogError("TOO MANY PLAYERS!!!!");
    }

    private void PlayerTwoInventory_OnSwitchItem(object sender, System.EventArgs e)
    {
        switch (PlayerOneInventory.GetCurrentIndex()) 
        {
            case 0:
                imagePlayerOne.sprite = pictureBlock;
                break;
            case 1:
                imagePlayerOne.sprite = pictureBarrel;
                break;
        }
    }

    private void PlayerOneInventory_OnSwitchItem(object sender, System.EventArgs e)
    {
        switch (PlayerOneInventory.GetCurrentIndex())
        {
            case 0:
                imagePlayerOne.sprite = pictureBlock;
                break;
            case 1:
                imagePlayerOne.sprite = pictureBarrel;
                break;
        }
    }
}
