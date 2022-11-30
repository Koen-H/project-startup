using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;


public class StartTimer : MonoBehaviour
{
    public static StartTimer Instance { get; private set; }

    public event EventHandler OnGameStart;

    [SerializeField] TextMeshProUGUI text;
   
    private bool activated = false;

    private bool started = false;

    private float cooldown = 3f;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Already a StartTimer, removing: " + this.name);
            Destroy(this);
            return;
        }

        Instance = this;
    }
    public void Update()
    {
        if (started) return;

        if (Input.GetKeyDown(KeyCode.T)) activated = true;

        if (!activated) return;

        cooldown -= Time.deltaTime;
        text.text= cooldown.ToString();

        Debug.Log(cooldown);
        if (cooldown < 0)
        {
            text.text = "";
            cooldown = 0;
            started = true;
            OnGameStart?.Invoke(this, EventArgs.Empty);
            Debug.Log("hnjk");
        }


    }


}
