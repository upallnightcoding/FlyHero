using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class PreFabPgNode : PgNode
{
    public PreFabPgNode() : base("PreFab")
    {
        CreateSinglePort("Input", Direction.Input);
    }
}
