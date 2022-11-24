using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBlockPlacing : MonoBehaviour
{
    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject hologramPrefab;
    [SerializeField] Camera topDownCamera;
    [SerializeField] LayerMask placeAbleLayer;
    GameObject placeBlock;

    Vector3 placePoint;

    bool canPlace = false;

    // Start is called before the first frame update
    void Start()
    {
        placeBlock = Instantiate(hologramPrefab);

        if (!canPlace) topDownCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            canPlace = !canPlace;
            topDownCamera.gameObject.SetActive(canPlace);
        }

        if (!canPlace) return;


        Ray blockpos = topDownCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(blockpos, out RaycastHit hit, float.MaxValue, placeAbleLayer))
        {
            placePoint = hit.point;
            //   Debug.Log(placePoint);
            placeBlock.transform.position = placePoint;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject objectPlaced = Instantiate(blockPrefab, placePoint, Quaternion.identity);

        }
    }
}
