using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnField : MonoBehaviour
{

    [SerializeField] List<GameObject> obstacleObjects;
    [SerializeField] LayerMask ground;
    [SerializeField] MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.enabled = false;

        int obstacleIndex = Random.Range(0, obstacleObjects.Count);
        float angle = Random.Range(0, 360);
        float mag = Random.Range(0, transform.localScale.x);

        // Debug.Log("Fhxdzgjkdsc : " + obstacleIndex);

        GameObject newObstacleObject = Instantiate(obstacleObjects[obstacleIndex]);
        newObstacleObject.transform.position = transform.position + Vector3.forward * mag / 2;
        newObstacleObject.transform.RotateAround(transform.position, Vector3.up, angle);
        // newObstacleObject.layer = ground;

        //Debug.DrawRay(newObstacleObject.transform.position, Vector3.down, Color.red, float.MaxValue);

        //Debug.Log("Fcjk shit : "  + transform.position);

        if (Physics.Raycast(newObstacleObject.transform.position + Vector3.up * 5, Vector3.down, out RaycastHit hit, float.MaxValue, ground))
        {
            newObstacleObject.transform.position = new Vector3(newObstacleObject.transform.position.x, hit.point.y, newObstacleObject.transform.position.z);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
