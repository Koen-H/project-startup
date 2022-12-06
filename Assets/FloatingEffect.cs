using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float speed = 1.0f; // The speed of the floating effect
    public float amplitude = 0.1f; // The amplitude of the floating effect
    public float rotationSpeed = 10.0f; // The speed of the rotation effect

    // The original position of the GameObject
    Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the GameObject
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position of the GameObject
        float x = originalPosition.x + amplitude * Mathf.Sin(speed * Time.time);
        float y = originalPosition.y + amplitude * Mathf.Cos(speed * Time.time);
        float z = originalPosition.z;
        Vector3 newPosition = new Vector3(x, y, z);

        // Set the position of the GameObject
        transform.position = newPosition;

        // Calculate the new rotation of the GameObject
        float newRotation = rotationSpeed * Time.time;
        Quaternion rotation = Quaternion.Euler(0, newRotation, 0);

        // Set the rotation of the GameObject
        transform.rotation = rotation;
    }
}