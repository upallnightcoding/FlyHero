using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        CollectableObject collectable =
            collider.GetComponent<CollectableObject>();

        if (collectable != null)
        {
            collectable.Collect();

            collider.gameObject.SetActive(false);
        }
    }
}
