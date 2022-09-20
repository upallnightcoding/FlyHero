using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class UnionPgNode : PgNode
{
    public UnionPgNode() : base("Union")
    {
        CreateSinglePort("Input", Direction.Input);

        CreateSinglePort("Left", Direction.Output);
        CreateSinglePort("Right", Direction.Output);
    }
}
