using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomQuacker : MonoBehaviour
{
    float timeLeft;
    float timer;
    AudioManager audioManager;


    private void Start()
    {
        SetRandomTime();
        audioManager = AudioManager.Instance;
    }

    private void FixedUpdate()
    {
        timer = Time.fixedDeltaTime;

        if (timer > timeLeft)
        {
            audioManager.Play("Quack1");
            SetRandomTime();
            timer = 0;
        }
    }


    void SetRandomTime()
    {
        timeLeft = Random.Range(1, 4);
    }
}