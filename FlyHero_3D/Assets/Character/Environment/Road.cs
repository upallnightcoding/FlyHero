using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject road;
    [SerializeField] private int nForwardDepth;
    [SerializeField] private GameObject blankCoin;

    private float lastPosition;
    private float forwardDepth;
    private float roadSize;

    // Start is called before the first frame update
    void Start()
    {
        roadSize = road.GetComponent<BoxCollider>().bounds.size.z;

        lastPosition = roadSize;

        forwardDepth = roadSize * nForwardDepth;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.z + forwardDepth) > lastPosition)
        {
            lastPosition += roadSize;

            GameObject go = Instantiate(road);

            float x = 0.0f;
            float y = 2.15f;
            float z = lastPosition;
            go.transform.position = new Vector3(x, y, z);

            x = 0.0f;
            y = 4.0f;
            z = lastPosition;
            GameObject coin = Instantiate(blankCoin, new Vector3(x, y, z), Quaternion.identity, go.transform); 
        }
    }
}
