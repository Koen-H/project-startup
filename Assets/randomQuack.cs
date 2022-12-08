using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class randomQuack : MonoBehaviour
{

    [SerializeField] float minRandTime = 6;
    [SerializeField] float maxRandTime = 18;

    AudioSource randomQuacker;
    [SerializeField] List<AudioClip> quacks;
    // Start is called before the first frame update
    void Start()
    {
        randomQuacker = this.AddComponent<AudioSource>();
        randomQuacker.loop = false;

        StartCoroutine(PlayRandomQauck());
    }

    private IEnumerator PlayRandomQauck()
    {
        float waitSeconds = Random.Range(minRandTime, maxRandTime);
        yield return new WaitForSeconds(waitSeconds);
        randomQuacker.clip = quacks[Random.Range(0, quacks.Count)];
        randomQuacker.Play();
        StartCoroutine(PlayRandomQauck());

    }
}
