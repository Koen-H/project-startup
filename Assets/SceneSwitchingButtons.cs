using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchingButtons : MonoBehaviour
{ 

    public void ShopButton()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void QuitButton() { 
        Application.Quit();
    }

    public void InventoryButton()
    {
        SceneManager.LoadScene("InventoryScene");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("ThomasScene");
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
