using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PgView : GraphView
{
    public PgView()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        AddElement(new StartPgNode());

        this.StretchToParentSize();
    }

    public void CreateOrPgNode() => AddElement(new OrPgNode());
    public void CreatePreFabPgNode() => AddElement(new PreFabPgNode());
    public void CreateUnionPgNode() => AddElement(new UnionPgNode());

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach(port => {
            if (startPort != port && startPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }


}
