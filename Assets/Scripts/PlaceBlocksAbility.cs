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

    float cooldownTimer = 0;
    const float COOLDOWN_BLOCK_PLACING = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
       placeBlock = Instantiate(hologramPrefab);
    }

    // Update is called once per frame
    void Update()
    {

        if (cooldownTimer > 0)
        {
            Debug.Log(cooldownTimer);
            cooldownTimer -= Time.deltaTime;
            placeBlock.transform.position = new Vector3(100, 100, 100);
            return;
        }

        if (Physics.Raycast(blockPlacePosition.position, Vector3.down, out RaycastHit hit, float.MaxValue,placeAbleLayer))
        {
            placePoint = hit.point;
         //   Debug.Log(placePoint);
            placeBlock.transform.position = placePoint;
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
           GameObject objectPlaced = Instantiate(blockPrefab, placePoint, this.transform.parent.rotation);
            cooldownTimer = COOLDOWN_BLOCK_PLACING;

        }
    }

   public float getCooldownTimer()
    {
        return cooldownTimer;
    }
}
