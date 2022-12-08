using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManageUI : MonoBehaviour
{

    [SerializeField] Sprite pictureBlock;
    [SerializeField] Sprite pictureBarrel;
    [SerializeField] Image imagePlayerOne;
    [SerializeField] Image imagePlayerTwo;

    [SerializeField] GameObject UI;

    PlayerInventory PlayerOneInventory;
    PlayerInventory PlayerTwoInventory;

    Color normalColor = new Color(255, 255, 255, 255);
    [SerializeField] Color darknedColor = new Color(96, 96, 96, 255);

    [SerializeField] TextMeshProUGUI cooldownP1;
    [SerializeField] TextMeshProUGUI cooldownP2;

    bool twoPlayersReady = false;


    public void AddPlayer(Transform player)
    {
        Debug.Log("Added player");
        if (PlayerOneInventory == null)
        {
            PlayerOneInventory = player.GetComponentInChildren<PlayerInventory>();
            PlayerOneInventory.OnSwitchItem += PlayerOneInventory_OnSwitchItem;
        }
        else if (PlayerTwoInventory == null)
        {
            PlayerTwoInventory = player.GetComponentInChildren<PlayerInventory>();
            PlayerTwoInventory.OnSwitchItem += PlayerTwoInventory_OnSwitchItem;
            twoPlayersReady = true;

        }
        else Debug.LogError("TOO MANY PLAYERS!!!!");
    }

    private void PlayerTwoInventory_OnSwitchItem(object sender, System.EventArgs e)
    {
        Debug.Log("Switches Two");
        switch (PlayerTwoInventory.GetCurrentIndex()) 
        {
            case 0:
                imagePlayerTwo.sprite = pictureBarrel;
                break;
            case 1:
                imagePlayerTwo.sprite = pictureBlock;
                break;
        }
    }

    private void PlayerOneInventory_OnSwitchItem(object sender, System.EventArgs e)
    {
        Debug.Log("Switches One");
        switch (PlayerOneInventory.GetCurrentIndex())
        {
            case 0:
                imagePlayerOne.sprite = pictureBarrel;
                break;
            case 1:
                imagePlayerOne.sprite = pictureBlock;
                break;
        }
    }

    private void Update()
    {
        if (twoPlayersReady)
        {
            float p1CooldownTime = PlayerOneInventory.place.getCooldownTimer();
            float p2CooldownTime = PlayerTwoInventory.place.getCooldownTimer();
            if (p1CooldownTime > 0)
            {
                cooldownP1.text = p1CooldownTime.ToString("F1");
                imagePlayerOne.color = darknedColor;
            }
            else
            {
                cooldownP1.text = "";
                imagePlayerOne.color = normalColor;
            }

            if (p2CooldownTime > 0)
            {
                cooldownP2.text = p2CooldownTime.ToString("F1");
                imagePlayerTwo.color = darknedColor;
            }
            else
            {
                cooldownP2.text = "";
                imagePlayerTwo.color = normalColor;
            }
        }
    }

    public void EnabeUI()
    {
        UI.SetActive(true);
    }
    public void DisableUI()
    {
        UI.SetActive(false);
    }
}
