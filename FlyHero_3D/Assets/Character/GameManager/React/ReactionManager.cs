using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionManager 
{
    public event Action<ReactType> reactEvent;

    private Dictionary<ReactType, Reaction> reactDict;

    public ReactionManager()
    {
        reactDict = new Dictionary<ReactType, Reaction>();
    }

    public void Add(Reaction react)
    {
        reactDict.Add(react.Type, react);
    }

    public void InvokeAddPoints(int points)
    {

    }
}

public enum ReactType
{
    UNKNOWN,
    BLANK_COIN
}
