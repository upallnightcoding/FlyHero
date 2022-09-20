using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class StartPgNode : PgNode
{
    public StartPgNode() : base("Start")
    {
        CreateSinglePort("Beginning", Direction.Output);
    }
}
