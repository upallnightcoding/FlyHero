using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float destroyTime;

    private Rigidbody rb;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3(0.0f, 0.0f, bulletSpeed);
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(velocity);
    }
}