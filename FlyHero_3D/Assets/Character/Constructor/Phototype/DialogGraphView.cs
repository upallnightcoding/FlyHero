using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using System.Linq;

public class DialogGraphView : GraphView
{
    private readonly Vector2 defaultNodeSize = new Vector2(150, 200);

    public DialogGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddElement(GenerateEntryPointNode());
    }

    private Port GeneratePort(DialogNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    private DialogNode GenerateEntryPointNode()
    {
        var node = new DialogNode
        {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            DialogText = "ENTRYPOINT",
            EntryPoint = true
        };

        node.SetPosition(new Rect(100, 200, 100, 150));

        var generatePort = GeneratePort(node, Direction.Output);
        generatePort.portName = "Next";
        node.outputContainer.Add(generatePort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        return (node);
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

    public void CreateNode(string nodeName)
    {
        AddElement(CreateDialogueNode(nodeName));
    }

    public DialogNode CreateDialogueNode(string nodeName)
    {
        var dialogueNode = new DialogNode
        {
            title = nodeName,
            GUID = Guid.NewGuid().ToString(),
            DialogText = nodeName
        };

        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

        var button = new Button(() => { AddChoicePort(dialogueNode); });
        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);

        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return (dialogueNode);
    }

    public void AddChoicePort(DialogNode dialogNode, string overriddenPortName = "")
    {
        var generatedPort = GeneratePort(dialogNode, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = dialogNode.outputContainer.Query("connector").ToList().Count;
        generatedPort.portName = $"Choice {outputPortCount}";

        var choicePortName = string.IsNullOrEmpty(overriddenPortName)
            ? $"Choice {outputPortCount + 1}" : overriddenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = choicePortName
        };

        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);

        var deleteButton = new Button(() => RemovePort(dialogNode, generatedPort))
        {
            text = "X"
        };
        generatedPort.contentContainer.Add(deleteButton);

        dialogNode.outputContainer.Add(generatedPort);
        dialogNode.RefreshPorts();
        dialogNode.RefreshExpandedState();
    }

    private void RemovePort(DialogNode dialogNode, Port generatedPort)
    {
        var targetEdge = edges.ToList()
            .Where(x => x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);

        if (!targetEdge.Any()) return;
        var edge = targetEdge.First();
        edge.input.Disconnect(edge);
        RemoveElement(targetEdge.First());

        dialogNode.outputContainer.Remove(generatedPort);
        dialogNode.RefreshPorts();
        dialogNode.RefreshExpandedState();
    }
}

