using UnityEngine;
using System;

public class Coin : MonoBehaviour, CollectableObject
{
    public static event Action<CoinType> OnCoinCollection = null;

    public virtual CoinType GetCoinType() => CoinType.NONE;

    public void Collect() => OnCoinCollection?.Invoke(GetCoinType());
}
