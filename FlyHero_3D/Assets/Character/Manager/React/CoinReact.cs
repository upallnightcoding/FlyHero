using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoinReact : React
{
    [SerializeField] private TMP_Text score;

    public CoinReact() : base(ReactType.BLANK_COIN)
    {

    }

    public override void Animation()
    {
        Debug.Log("Coin React Animation");
    }

    public override void Audio()
    {
        Debug.Log("Coin React Audio");
    }

    public override void UIUpdate()
    {
        Debug.Log("Coin React UIUpdate");
    }
}
