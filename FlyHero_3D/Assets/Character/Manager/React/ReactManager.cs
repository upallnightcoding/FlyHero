using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactManager 
{
    public static event Action<ReactType> reactEvent;

    private Dictionary<ReactType, React> reactDict;

    public ReactManager()
    {
        reactDict = new Dictionary<ReactType, React>();
    }

    public void Add(React react)
    {
        reactDict.Add(react.Type, react);
    }
}
