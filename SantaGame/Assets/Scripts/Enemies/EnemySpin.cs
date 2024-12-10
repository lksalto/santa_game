using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpin : MonoBehaviour
{
    public float spinSpeed = 360f; // Speed of rotation in degrees per second
    public float lifeSpan = 10f;


    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }
    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
    }
}
