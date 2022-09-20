using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class OrPgNode : PgNode
{
    public OrPgNode() : base("Or")
    {
        CreateSinglePort("Input", Direction.Input);

        CreateSinglePort("Left", Direction.Output);
        CreateSinglePort("Right", Direction.Output);
    }
}
