using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceBlocksAbility : MonoBehaviour
{

    public GameObject blockPrefab;
    public GameObject hologramPrefab;
    [SerializeField] Transform blockPlacePosition;
    [SerializeField] LayerMask placeAbleLayer;
    [HideInInspector] public GameObject placeBlock;
    [SerializeField] Material holoMat;
    Vector3 placePoint;

    float cooldownTimer = 0;
    const float COOLDOWN_BLOCK_PLACING = 1.2f;

    PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        InstantiateHologram();
    }
    public void InstantiateHologram()
    {
        Destroy(placeBlock);

        placeBlock = Instantiate(hologramPrefab);
        for (int i = 0; i < hologramPrefab.transform.childCount; i++)
        {
            placeBlock.transform.GetChild(i).GetComponent<Renderer>().material = holoMat;
        }
        placeBlock.SetActive(true);
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
            Debug.Log("Ray hit !! ");
            placePoint = hit.point;
         //   Debug.Log(placePoint);
            placeBlock.transform.position = placePoint;
        }

        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
           GameObject objectPlaced = Instantiate(blockPrefab, placePoint, this.transform.parent.rotation);
            objectPlaced.SetActive(true);
            cooldownTimer = COOLDOWN_BLOCK_PLACING;

        } */
    }

    public void Place(InputAction.CallbackContext context)
    {
        Debug.Log("wdafaw");
        GameObject objectPlaced = Instantiate(blockPrefab, placePoint, this.transform.parent.rotation);
        objectPlaced.SetActive(true);
        cooldownTimer = COOLDOWN_BLOCK_PLACING;
    }

    public float getCooldownTimer()
    {
        return cooldownTimer;
    }
}
