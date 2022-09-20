using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;

public class PgNode : Node
{
    private readonly Vector2 DEFAULT_NODE_POS = Vector2.zero;
    private readonly Vector2 DEFAULT_NODE_SIZE = new Vector2(150, 200);

    private string guid;

    public PgNode(string title)
    {
        this.title = title;
        this.guid = Guid.NewGuid().ToString();
    }

    public void CreateSinglePort(string portName, Direction direction)
    {
        CreatePort(portName, direction, Port.Capacity.Single);
    }

    public void CreateMultiPort(string portName, Direction direction)
    {
        CreatePort(portName, direction, Port.Capacity.Multi);
    }

    private void CreatePort(string portName, Direction direction, Port.Capacity capacity)
    {
        Port port = InstantiatePort(
            Orientation.Horizontal, 
            direction, 
            capacity, 
            typeof(float)
        );

        port.portName = portName;

        switch(direction)
        {
            case Direction.Input:
                inputContainer.Add(port);
                break;
            case Direction.Output:
                outputContainer.Add(port);
                break;
        }

        RefreshExpandedState();
        RefreshPorts();
        SetPosition(new Rect(DEFAULT_NODE_POS, DEFAULT_NODE_SIZE));
    }
}

