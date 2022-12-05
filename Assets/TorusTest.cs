using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusTest : MonoBehaviour
{

    private float _speed = 5f;
    private float _size = 3f;
    private float MAX_DISTANCE = 10f;
    private float _distance = 10f;
    [SerializeField] private float forceAmount = 5000f;
    public void SetSpeed(float amount)
    {
        _speed = amount;
    }

    public void SetSize(float amount)
    {
        _size = amount;
    }
    public void SetDistance(float amount)
    {
        MAX_DISTANCE = amount;
        _distance = amount; 
    }

    // Start is called before the first frame update
    Vector3 direction;
    void Start()
    {
        float angle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 dPos = direction * _speed * Time.deltaTime;
        transform.position += dPos;
        _distance -= dPos.magnitude;
        if (_distance <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            float percentageSize = _distance / MAX_DISTANCE;
            float newSize = _size * percentageSize;
            transform.localScale = new Vector3(newSize, newSize, newSize);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 positionTorus = this.transform.position;
            Vector3 positionPlayer = other.transform.position;

            Vector3 dPos = positionPlayer - positionTorus;
            dPos += Vector3.up;
            Debug.Log("efafa");
            other.GetComponent<Rigidbody>().AddForce(dPos * forceAmount * Time.deltaTime, ForceMode.Impulse);

        }
    }

}
