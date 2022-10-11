using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CollectableObject 
{
    public void Collect();
    public CoinType GetCoinType();
}
