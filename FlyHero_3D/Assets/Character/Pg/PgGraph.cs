using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class PgGraph : EditorWindow
{
    private PgGraphView graphView;

    [MenuItem("Graph/Pg Graph")]
    public static void OpenPgGraphWindow()
    {
        var window = GetWindow<PgGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void OnEnable() 
    {
        ConstructGraphView();
        GenerateToolbar();
    }

    private void ConstructGraphView() 
    {
        graphView = new PgGraphView
        {
            name = "Procedural Generator"
        };

        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
    }

    private void GenerateToolbar() 
    {
        var toolbar = new Toolbar();

        var nodeCreateButton = new Button(() => { graphView.CreateNode("Pg Node"); });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }

    private void OnDisable() 
    {
        rootVisualElement.Remove(graphView);
    }
    

}
