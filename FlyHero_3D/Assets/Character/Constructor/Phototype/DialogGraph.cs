using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class DialogGraph : EditorWindow
{
    private DialogGraphView graphView;
    private string fileName = "New Narrative";

    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolBar();
    }

    private void ConstructGraphView()
    {
        graphView = new DialogGraphView
        {
            name = "Dialogue Graph"
        };

        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }

    private void GenerateToolBar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });
        toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });

        var nodeCreateButton = new Button(() => { graphView.CreateNode("Dialog Node"); });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            EditorUtility.DisplayDialog("Invalid FileName!", "Please enter a valid file name.", "OK");
            return;
        }

        var saveUtility = GraphSaveUtility.GetInstance(graphView);

        if (save)
        {
            saveUtility.SaveGraph(fileName);    
        } 
        else
        {
            saveUtility.LoadGraph(fileName); 
        }
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }
}
