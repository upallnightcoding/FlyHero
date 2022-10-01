using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuilder : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject road;
    [SerializeField] private int nForwardDepth;
    [SerializeField] private int nRearDepth;
    [SerializeField] private GameObject blankCoin;

    private float lastPosition;
    private float forwardDepth;
    private float rearDepth;
    private float roadSize;

    private Queue<GameObject> deleteQueue;

    // Start is called before the first frame update
    void Start()
    {
        deleteQueue = new Queue<GameObject>();

        roadSize = road.GetComponent<BoxCollider>().bounds.size.z;

        lastPosition = roadSize;

        rearDepth = roadSize * nRearDepth;
        forwardDepth = roadSize * nForwardDepth;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.z + forwardDepth) > lastPosition)
        {
            lastPosition += roadSize;

            GameObject parent = CreateRoad(lastPosition);

            CreateCoins(parent, lastPosition);

            DeleteGameObjects();
        }
    }

    private GameObject CreateRoad(float lastPosition)
    {
        GameObject go = Instantiate(road);

        float x = 0.0f;
        float y = 0.0f;
        float z = lastPosition;

        go.transform.position = new Vector3(x, y, z);

        deleteQueue.Enqueue(go);

        return(go);
    }

    private void CreateCoins(GameObject parent, float lastPosition)
    {
        float x = Random.Range(-20.0f, 20.0f);
        float y = Random.Range(0.0f, 15.0f);
        float z = lastPosition;

        GameObject go = Instantiate(blankCoin, new Vector3(x, y, z), Quaternion.identity, parent.transform);

        deleteQueue.Enqueue(go);
    }

    private void DeleteGameObjects()
    {
        while((deleteQueue.Count > 0) && (deleteQueue.Peek().transform.position.z < (player.transform.position.z - rearDepth)))
        {
            Destroy(deleteQueue.Dequeue());
        }
    }
}
