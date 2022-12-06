using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameCamera : MonoBehaviour
{
    Dictionary<GameObject, CinemachineFreeLook> playerCamera = new Dictionary<GameObject, CinemachineFreeLook>();
    [SerializeField] GameObject lookAt;

    public void StartAnim()
    {
        
        foreach (PlayerData player in GameManager.Instance.players)
        {
            CinemachineFreeLook cam = player.gameObject.GetComponentInChildren<CinemachineFreeLook>();
            playerCamera[player.gameObject] = cam;
            cam.Follow = this.gameObject.transform;
            cam.LookAt = lookAt.transform;
        }
        GetComponent<Animation>().Play();

    }

    public void Finished()
    {
        GetComponent<Animation>().Stop();

        foreach (KeyValuePair<GameObject, CinemachineFreeLook> entry in playerCamera)
        {
            entry.Value.Follow = entry.Key.transform;
            entry.Value.LookAt = entry.Key.transform.Find("PointLookAt").transform;
        }
        GameManager.Instance.StartGame();
    }
}
