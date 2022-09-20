using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphSaveUtility 
{
    private DialogGraphView targetGraphView;
    private DialogueContainer containerCache;

    private List<Edge> Edges => targetGraphView.edges.ToList();
    private List<DialogNode> Nodes => targetGraphView.nodes.ToList().Cast<DialogNode>().ToList();
    
    public static GraphSaveUtility GetInstance(DialogGraphView graphView)
    {
        return new GraphSaveUtility
        {
            targetGraphView = graphView
        };
    }

    public void SaveGraph(string fileName)
    {
        if (!Edges.Any()) return;

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();
        for (var i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogNode;
            var inputNode = connectedPorts[i].input.node as DialogNode;

            dialogueContainer.NodeLinks.Add(new NodeLinkData
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = connectedPorts[i].output.portName,
                TargetNodeGuid = inputNode.GUID
            }); 
        }

        foreach (var dialogueNode in Nodes.Where(node => !node.EntryPoint))
        {
            dialogueContainer.DialogueNodeData.Add(new DialogueNodeData
            {
                Guid = dialogueNode.GUID,
                DialogueText = dialogueNode.DialogText,
                Position = dialogueNode.GetPosition().position
            }) ; 
        }

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName)
    {
        containerCache = Resources.Load<DialogueContainer>(fileName);

        if (containerCache == null)
        {
            EditorUtility.DisplayDialog("File not FOund", "Target dialogue graph file does not exists!", "Ok");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ConnectNodes()
    {
        throw new System.NotImplementedException();
    }

    private void CreateNodes()
    {
        foreach(var nodeData in containerCache.DialogueNodeData)
        {
            var tempNode = targetGraphView.CreateDialogueNode(nodeData.DialogueText);
            tempNode.GUID = nodeData.Guid;
            targetGraphView.AddElement(tempNode);

            var nodePorts = containerCache.NodeLinks.Where(x=>x.BaseNodeGuid == nodeData.Guid).ToList();
            nodePorts.ForEach(x => targetGraphView.AddChoicePort(tempNode, x.PortName));
        }
    }

    private void ClearGraph()
    {
        Nodes.Find(x => x.EntryPoint).GUID = containerCache.NodeLinks[0].BaseNodeGuid;

        foreach(var node in Nodes)
        {
            if (node.EntryPoint) continue;

            Edges.Where(x => x.input.node == node).ToList()
                .ForEach(edge => targetGraphView.RemoveElement(edge));

            targetGraphView.RemoveElement(node);
        }
    }
}
