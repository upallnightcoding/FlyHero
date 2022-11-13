using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;
using UnityEngine.UIElements;

public class PgGraphView : GraphView
{
   private readonly Vector2 defaultNodeSize = new Vector2(150, 200);

   public PgGraphView() 
   {
      styleSheets.Add(Resources.Load<StyleSheet>("PgGraph"));
      SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

      this.AddManipulator(new ContentDragger());
      this.AddManipulator(new SelectionDragger());
      this.AddManipulator(new RectangleSelector());

      var grid = new GridBackground();
      Insert(0, grid);
      grid.StretchToParentSize();

      AddElement(GenerateEntryPointNode());
   }

   public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
   {
      var compatiblePorts = new List<Port>();
      ports.ForEach((port) => {
         if (startPort != port && startPort.node != port.node)
            compatiblePorts.Add(port);
      });

      return(compatiblePorts);
   }

   private Port GeneratePort(PgNode node, Direction direction, Port.Capacity capacity = Port.Capacity.Single)
   {
      return (node.InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(float)));
   }

   private PgNode GenerateEntryPointNode() 
   {
      var node = new PgNode
      {
         title = "START",
         GUID = Guid.NewGuid().ToString(),
         entryPoint = true
      };

      var generatePort = GeneratePort(node, Direction.Output);
      generatePort.portName = "Next";
      node.outputContainer.Add(generatePort);

      node.RefreshExpandedState();
      node.RefreshPorts();

      node.SetPosition(new Rect(100, 200, 100, 150));

      return(node);
   }

   public void CreateNode(string nodeName) 
   {
      AddElement(CreatePgNode(nodeName));
   }

   public PgNode CreatePgNode(string nodeName)
   {
      var pgNode = new PgNode
      {
         title = nodeName,
         GUID = Guid.NewGuid().ToString()
      };

      var inputPort = GeneratePort(pgNode, Direction.Input, Port.Capacity.Multi);
      inputPort.portName = "Input";
      pgNode.inputContainer.Add(inputPort);

      var button = new Button(() => { AddChoicePort(pgNode);});
      button.text = "New Choice";
      pgNode.titleContainer.Add(button);

      pgNode.RefreshExpandedState();
      pgNode.RefreshPorts();
      pgNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

      return(pgNode);
   }

   private void AddChoicePort(PgNode pgNode) 
   {
      var generatedPort = GeneratePort(pgNode, Direction.Output);

      var outputPortCount = pgNode.outputContainer.Query("connector").ToList().Count;
      generatedPort.portName = $"Choice {outputPortCount}";

      pgNode.outputContainer.Add(generatedPort);
      pgNode.RefreshExpandedState();
      pgNode.RefreshPorts();
   }
}
