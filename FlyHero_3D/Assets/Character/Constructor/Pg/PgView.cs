using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class PgView : GraphView
{
    private StartPgNode startPgNode = null;

    public PgView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("PgStyleSheet"));

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        SetupBackGround();

        startPgNode = new StartPgNode();
        AddElement(startPgNode);

        this.StretchToParentSize();
    }

    public void CreateOrPgNode() => AddElement(new OrPgNode());
    public void CreatePreFabPgNode() => AddElement(new PreFabPgNode());
    public void CreateUnionPgNode() => AddElement(new UnionPgNode());

    public void TestNodeTraverse()
    {
        TestNodeTraverse(startPgNode);
    }

    public void TestNodeTraverse(PgNode node)
    {
        foreach (VisualElement element in node.ListOutputPorts())
        {
            TestNodeTraverse(element as PgNode);
        }
    }

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

    private void SetupBackGround()
    {
        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
    }
}
