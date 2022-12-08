using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] LayerMask waterLayer;
    public GameObject placeBlock;
    [SerializeField] Material holoMat;
    Vector3 placePoint;
    [SerializeField] List<AudioClip> placeSfx = new List<AudioClip>();

    AudioSource placeSource;

    float cooldownTimer = 0;

    PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateHologram();
        placeSource = this.AddComponent<AudioSource>();
        placeSource.loop = false;
    }
    public void InstantiateHologram()
    {
        DestroyImmediate(placeBlock);
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
            placeBlock.transform.GetChild(i).GetComponentInChildren<Renderer>().material = holoMat;
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
        RaycastHit hit;
        RaycastHit hitWater;
        RaycastHit hitGround;
        Physics.Raycast(blockPlacePosition.position, Vector3.down, out hitWater, float.MaxValue, waterLayer);
        Physics.Raycast(blockPlacePosition.position, Vector3.down, out hitGround, float.MaxValue, placeAbleLayer);
        hit = (hitWater.distance < hitGround.distance) ? hitWater : hitGround;
        if (hit.collider != null)
        {
            //if(hitLayer == placeAbleLayer || hitLayer == waterLayer)
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
            objectPlaced.GetComponent<AudioSource>().Play();
            placeSource.clip = placeSfx[Random.Range(0, placeSfx.Count)];
            placeSource.Play();
            if (objectPlaced.GetComponent<BarrelMovement>())
            {
                objectPlaced.GetComponent<BarrelMovement>().isHologram = false;
            }
        }
    }

    public float getCooldownTimer()
    {
        return cooldownTimer;
    }
}
