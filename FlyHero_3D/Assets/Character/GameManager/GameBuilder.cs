using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuilder : MonoBehaviour
{
    [SerializeField] private GameCache gameCache;
    [SerializeField] private Environment environment;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject centerRoad;
    [SerializeField] private GameObject sidewalk;
    [SerializeField] private GameObject benchPreFab;

    [SerializeField] private int nForwardDepth;
    [SerializeField] private int nRearDepth;

    private float lastPosition;
    private float forwardDepth;
    private float rearDepth;
    private float roadSize;

    private Queue<GameObject> deleteQueue;

    // Start is called before the first frame update
    void Start()
    {
        deleteQueue = new Queue<GameObject>();

        roadSize = 5.0f;

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

    private GameObject CreateRoad(float position)
    {
        GameObject center   = CreateRoad(centerRoad, 0.0f, position);
        GameObject left     = CreateRoad(centerRoad, -10.0f, position);
        GameObject right    = CreateRoad(centerRoad, 10.0f, position);

        GameObject leftSideWalk     = CreateSideWalk(sidewalk, -17.5f, position);
        GameObject rightSideWalk    = CreateSideWalk(sidewalk, 17.5f, position);

        GetStoreFront(-22.5f, position);
        GetStoreFront(22.5f, position);

        return(center);
    }

    private GameObject GetStoreFront(float x, float position) {
        GameObject storeFront = environment.PickStoreFront();

        GameObject go = Instantiate(storeFront, new Vector3(x, 0.0f, position), storeFront.transform.rotation);

        deleteQueue.Enqueue(go);

        return(go);
    }

    private GameObject CreateSideWalk(GameObject sideWalkPreFab, float x, float position) {
        Transform slot = null;
        GameObject bench = null;
        GameObject hedge = null;

        GameObject sideWalk = Instantiate(sideWalkPreFab, new Vector3(x, 0.0f, position), Quaternion.identity);

        int which = environment.GetRandom(4);

        switch(which) {
            case 0:
                slot = sideWalk.transform.GetChild(1);
                bench = Instantiate(environment.PickBench());
                bench.transform.localPosition = slot.transform.position;
                break;
            case 1:
                slot = sideWalk.transform.GetChild(2);
                bench = Instantiate(environment.PickBench());
                bench.transform.localPosition = slot.transform.position;
                break;
            case 2:
                slot = sideWalk.transform.GetChild(2);
                hedge = Instantiate(environment.PickHedges());
                hedge.transform.localPosition = slot.transform.position;
                break;
            case 3:
                slot = sideWalk.transform.GetChild(3);
                hedge = Instantiate(environment.PickHedges());
                hedge.transform.localPosition = slot.transform.position;
                slot = sideWalk.transform.GetChild(4);
                hedge = Instantiate(environment.PickHedges());
                hedge.transform.localPosition = slot.transform.position;
                break;
        }

        deleteQueue.Enqueue(sideWalk);

        return(sideWalk);
    }

    private GameObject CreateRoad(GameObject preFab, float x, float z)
    {
        GameObject road = Instantiate(preFab, new Vector3(x, 0.0f, z), Quaternion.identity);

        deleteQueue.Enqueue(road);

        return(road);
    }

    private void CreateCoins(GameObject parent, float lastPosition)
    {
        Vector3 position = gameCache.GetLaneLevelPos(lastPosition);

        GameObject coin = 
            (Random.Range(0, 9) == 0) ? environment.GetCoin() : environment.GetGoldCoin();

        GameObject go = 
            Instantiate(coin, position, Quaternion.identity, parent.transform);

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
