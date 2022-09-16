using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class React : MonoBehaviour
{
    public ReactType Type { set; get; } = ReactType.UNKNOWN;

    public React(ReactType type)
    {
        Type = type;
    }

    public void Execute()
    {
        Audio();
        Animation();
        UIUpdate();
    }

    public abstract void Audio();
    public abstract void UIUpdate();
    public abstract void Animation();
}

public enum ReactType
{
    UNKNOWN,
    BLANK_COIN
}
