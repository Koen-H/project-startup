using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioObjectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnObj());
        this.GetComponent<AudioSource>().Play();
    }

    private IEnumerator DespawnObj()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);

    }
}
