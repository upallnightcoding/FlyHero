using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coin Trigger ...");
    }
}
