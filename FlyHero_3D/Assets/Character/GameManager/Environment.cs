using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FlyHero/Environment", fileName = "PointsSystem")]
public class Environment : ScriptableObject
{
    public GameObject[] benches;

    public GameObject[] plants;

    public GameObject[] storeFronts;

    public GameObject[] hedges;

    public GameObject sidewalk;
    public GameObject centerRoad;

    [Header("Coins")]
    public GameObject goldCoin;
    public GameObject greenCoin;
    public GameObject redCoin;
    public GameObject whiteCoin;
    public GameObject blueCoin;

    // Get Coins
    public GameObject GetGoldCoin() => goldCoin;

    public GameObject GetCoin() {
        GameObject coin = null;

        int whichCoin = Random.Range(0, 4);

        switch (whichCoin)
        {
            case 0:
                coin = greenCoin;
                break;
            case 1:
                coin = redCoin;
                break;
            case 2:
                coin = whiteCoin;
                break;
            case 3:
                coin = blueCoin;
                break;
        }

        return(coin);
    }

    public GameObject PickPlants() {
        return(plants[GetRandom(plants.Length)]);
    }

    public GameObject PickHedges() {
        return(hedges[GetRandom(hedges.Length)]);
    }

    public GameObject PickStoreFront() {
        return(storeFronts[GetRandom(storeFronts.Length)]);
    }

    public GameObject PickBench() {
        return(benches[GetRandom(benches.Length)]);
    }

    public int GetRandom(int max) {
        return(Random.Range(0, max));
    }
}
