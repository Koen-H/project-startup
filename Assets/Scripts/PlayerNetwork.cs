using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode; 
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] float playerSpeed = 1;


    private NetworkVariable<PlayerStats> randomNumber = new NetworkVariable<PlayerStats>(
        new PlayerStats
        {
            alive = true,
            health = 100,
            ammoAmount = 20
        }, NetworkVariableReadPermission.Everyone , NetworkVariableWritePermission.Owner);


    public struct PlayerStats : INetworkSerializable
    {
        public bool alive;
        public int health;
        public int ammoAmount;
        public FixedString32Bytes name; 

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref alive);
            serializer.SerializeValue(ref health);
            serializer.SerializeValue(ref ammoAmount);
            serializer.SerializeValue(ref name);
        }
    }
    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (PlayerStats previousValue, PlayerStats newVale) =>
        {
            Debug.Log(OwnerClientId + " : RandomNumber Id was" + newVale.alive + " ; " + newVale.health + " ; " + newVale.ammoAmount + " ; " + newVale.name);
        };
    }
    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T)) {
            randomNumber.Value = new PlayerStats
            {
                health = Random.Range(0, 100),
                alive = true,
                ammoAmount = 10,
                name = "CarBRuhMOnetne"
            };
  
        }


        
        Vector2 direction = new Vector2(0, 0);

        direction.x += Input.GetAxis("Horizontal");
        direction.y += Input.GetAxis("Vertical");

        direction.Normalize();

        transform.position += new Vector3(direction.x, 0, direction.y) * playerSpeed * Time.deltaTime;

        

    }
}
