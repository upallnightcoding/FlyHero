using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reaction : MonoBehaviour
{
    public ReactType Type { set; get; } = ReactType.UNKNOWN;

    public Reaction(ReactType type)
    {
        Type = type;
    }

    public void Execute()
    {
        Audio();
        Animation();
        UIUpdate();
        UpdateGameObject();
    }

    public abstract void UpdateGameObject();
    public abstract void Audio();
    public abstract void UIUpdate();
    public abstract void Animation();
}
