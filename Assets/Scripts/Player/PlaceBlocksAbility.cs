using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceBlocksAbility : MonoBehaviour
{

    [Header("Changable")]
    [SerializeField] float COOLDOWN_BLOCK_PLACING = 1.2f;

    [Header("Setup")]
    public GameObject blockPrefab;
    public GameObject hologramPrefab;
    [SerializeField] Transform blockPlacePosition;
    [SerializeField] LayerMask placeAbleLayer;
    public GameObject placeBlock;
    [SerializeField] Material holoMat;
    Vector3 placePoint;

    float cooldownTimer = 0;

    PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateHologram();
    }
    public void InstantiateHologram()
    {
        Destroy(placeBlock);
        placeBlock = Instantiate(hologramPrefab, placePoint + blockPrefab.transform.position, this.transform.parent.rotation * blockPrefab.transform.rotation);
       // placeBlock.transform.rotation = this.transform.parent.rotation;
          if (placeBlock.transform.TryGetComponent<Renderer>(out Renderer rend)) rend.material = holoMat;
        if (placeBlock.transform.TryGetComponent<Rigidbody>(out Rigidbody rig))
        {
            Destroy(rig);
        }
        if (placeBlock.TryGetComponent<Collider>(out Collider coll)) Destroy(coll);
        placeBlock.layer = 0;

        placeBlock.transform.GetComponent<Renderer>().material = holoMat;

        for (int i = 0; i < hologramPrefab.transform.childCount; i++)
        {
            placeBlock.transform.GetChild(i).GetComponent<Renderer>().material = holoMat;
            placeBlock.layer = 0;
        }
        placeBlock.transform.position = placePoint + blockPrefab.transform.position;
        placeBlock.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        placeBlock.transform.rotation = this.transform.parent.rotation * blockPrefab.transform.rotation;

        if (cooldownTimer > 0)
        {
            // Debug.Log(cooldownTimer);
            cooldownTimer -= Time.deltaTime;
            placeBlock.transform.position = new Vector3(100, 100, 100);
            return;
        }

        if (Physics.Raycast(blockPlacePosition.position, Vector3.down, out RaycastHit hit, float.MaxValue,placeAbleLayer))
        {
          //  Debug.Log("Ray hit !! ");
            placePoint = hit.point;
         //   Debug.Log(placePoint);
            placeBlock.transform.position = placePoint + blockPrefab.transform.position;
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
        if (cooldownTimer <= 0)
        {
            // Debug.Log("wdafaw");
            GameObject objectPlaced = Instantiate(blockPrefab, placePoint + blockPrefab.transform.position, this.transform.parent.rotation * blockPrefab.transform.rotation);
            objectPlaced.GetComponent<Lifetime>().enabled = true;
            objectPlaced.SetActive(true);
            objectPlaced.layer = 3;
            cooldownTimer = COOLDOWN_BLOCK_PLACING;
        }
    }

    public float getCooldownTimer()
    {
        return cooldownTimer;
    }
}
