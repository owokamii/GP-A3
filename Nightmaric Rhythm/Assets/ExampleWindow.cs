using UnityEngine;
using UnityEditor;

public class ExampleWindow : EditorWindow
{
    [MenuItem("Window/Example")]
    public static void ShowWindow()
    {
        GetWindow<ExampleWindow>("Example");
    }

    void OnGUI()
    {
        GUILayout.Label("This is an Editor Tool", EditorStyles.boldLabel);

        AudioSource audioSource;

        if (GUILayout.Button("Press Me"))
        {
            Debug.Log("Button was pressed");
        }
    }
}
