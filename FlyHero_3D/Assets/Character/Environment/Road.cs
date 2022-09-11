using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject road;
    [SerializeField] private float depth;

    private float lastPosition;

    private float roadSize;
    

    // Start is called before the first frame update
    void Start()
    {
        roadSize = road.GetComponent<BoxCollider>().bounds.size.z;

        lastPosition = roadSize;

        depth = roadSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z + depth > lastPosition)
        {
            GameObject go = Instantiate(road);

            lastPosition += roadSize;

            float x = 0.0f;
            float y = 2.15f;
            float z = lastPosition;
            go.transform.position = new Vector3(x, y, z);
        }
    }
}
