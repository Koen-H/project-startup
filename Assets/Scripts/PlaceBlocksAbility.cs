using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBlocksAbility : MonoBehaviour
{

    [SerializeField] GameObject blockPrefab;
    [SerializeField] GameObject hologramPrefab;
    [SerializeField] Transform blockPlacePosition;
    [SerializeField] LayerMask placeAbleLayer;
    GameObject placeBlock;

    Vector3 placePoint;

    // Start is called before the first frame update
    void Start()
    {
       placeBlock = Instantiate(hologramPrefab);
    }

    // Update is called once per frame
    void Update()
    {


        if (Physics.Raycast(blockPlacePosition.position, Vector3.down, out RaycastHit hit, float.MaxValue,placeAbleLayer))
        {
            placePoint = hit.point;
         //   Debug.Log(placePoint);
            placeBlock.transform.position = placePoint;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           GameObject objectPlaced = Instantiate(blockPrefab, placePoint, this.transform.parent.rotation);

        }
    }
}
