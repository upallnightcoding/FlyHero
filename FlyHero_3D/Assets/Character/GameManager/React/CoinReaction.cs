using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoinReaction : Reaction
{
    [SerializeField] private TMP_Text score;

    public CoinReaction() : base(ReactType.BLANK_COIN)
    {

    }

    public override void Animation()
    {
        Debug.Log("Coin Reaction Animation");
    }

    public override void Audio()
    {
        Debug.Log("Coin Reaction Audio");
    }

    public override void UIUpdate()
    {
        Debug.Log("Coin Reaction UIUpdate");
    }

    public override void UpdateGameObject()
    {
        Destroy(gameObject);
    }
}
