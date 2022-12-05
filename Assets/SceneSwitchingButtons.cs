using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchingButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    }
}
