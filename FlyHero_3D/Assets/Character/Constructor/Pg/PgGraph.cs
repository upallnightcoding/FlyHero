using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PgGraph : EditorWindow
{
    private PgView graphView;

    [MenuItem("Graph/Procedure Generation")]
    public static void OpenPgGraphWindow()
    {
        var window = GetWindow<PgGraph>();
        window.titleContent = new GUIContent("Pg - Procedure Generation");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolBar();
    }

    private void OnDisable()
    {
        DeConstructGraphView();
    }

    private void ConstructGraphView()
    {
        graphView = new PgView();

        rootVisualElement.Add(graphView);
    }

    private void DeConstructGraphView()
    {
        rootVisualElement.Remove(graphView);
    }

    private void GenerateToolBar()
    {
        var toolBar = new Toolbar();

        toolBar.Add(new Button(() => TestNodeTraverse()) { text = "Test" });

        toolBar.Add(new Label("Create Nodes -> "));

        toolBar.Add(new Button(() => CreateOrNode()) { text = "Or" });
        toolBar.Add(new Button(() => CreateUnionNode()) { text = "Union" });
        toolBar.Add(new Button(() => CreatePreFabNode()) { text = "PreFab" });

        rootVisualElement.Add(toolBar);
    }

    private void CreatePreFabNode() => graphView.CreatePreFabPgNode();
    private void CreateOrNode() => graphView.CreateOrPgNode();
    private void CreateUnionNode() => graphView.CreateUnionPgNode();

    private void TestNodeTraverse() => graphView.TestNodeTraverse();
}
