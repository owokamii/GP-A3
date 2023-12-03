using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class NoteSpawner : EditorWindow
{
    private const string _helpText1 = "Cannot find 'SpawnpointList' component on any GameObject in the scene!";
    private const string _helpText2 = "Cannot find 'NoteList' component on any GameObject in the scene!";
    private const string _helpText3 = "Cannot find 'BeatList' component on any GameObject in the scene!";

    int pos;
    float beat;
    float songPositionInBeats;
    
    private static Rect _helpRect1 = new Rect(0f, 0f, 400f, 100f);
    private static Rect _helpRect2 = new Rect(0f, 200f, 400f, 100f);
    private static Rect _helpRect3 = new Rect(0f, 400f, 400f, 100f);
    private static Rect _listRect1 = new Rect(0, 0, 500f, 500f);
    private static Rect _listRect2 = new Rect(0, 150, 500f, 500f);
    private static Rect _listRect3 = new Rect(0, 435, 500f, 500f);

    private bool _isActive;
    private bool eachSpawn = false;
    private bool everyUpdate = false;

    SerializedObject spawnpoints = null;
    SerializedObject notes = null;
    SerializedObject beats = null;
    ReorderableList spList = null;
    ReorderableList nList = null;
    ReorderableList bList = null;
   
    SpawnpointList spawnpointList;
    NoteList noteList;
    BeatList beatList;
    Conductor conductor;

    [MenuItem("Editor Tool/Note Spawner")]
    public static void OpenSimulatorWindow()
    {
        GetWindow<NoteSpawner>("Note Spawner");
    }

    private void OnEnable()
    {
        spawnpointList = FindObjectOfType<SpawnpointList>();
        if (spawnpointList)
        {
            spawnpoints = new SerializedObject(spawnpointList);
            spList = new ReorderableList(spawnpoints, spawnpoints.FindProperty("spawnpoints"), true, true, true, true);

            spList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Spawnpoints");
            spList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 2f;
                rect.height = EditorGUIUtility.singleLineHeight;
                GUIContent objectLabel = new GUIContent($"Spawnpoint {index}");
                //the index will help numerate the serialized fields
                EditorGUI.PropertyField(rect, spList.serializedProperty.GetArrayElementAtIndex(index), objectLabel);
            };
        }

        noteList = FindObjectOfType<NoteList>();
        if (noteList)
        {
            notes = new SerializedObject(noteList);
            nList = new ReorderableList(notes, notes.FindProperty("notes"), true, true, true, true);

            nList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Note Types");
            nList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 2f;
                rect.height = EditorGUIUtility.singleLineHeight;
                GUIContent objectLabel = new GUIContent($"Note {index}");
                //the index will help numerate the serialized fields
                EditorGUI.PropertyField(rect, nList.serializedProperty.GetArrayElementAtIndex(index), objectLabel);
            };
        }

        beatList = FindObjectOfType<BeatList>();
        if (beatList)
        {
            beats = new SerializedObject(beatList);
            bList = new ReorderableList(beats, beats.FindProperty("beats"), true, true, true, true);

            bList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Beats");
            bList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += 2f;
                rect.height = EditorGUIUtility.singleLineHeight;
                GUIContent objectLabel = new GUIContent($"Beats {index}");
                //the index will help numerate the serialized fields
                EditorGUI.PropertyField(rect, bList.serializedProperty.GetArrayElementAtIndex(index), objectLabel);
            };
        }
    }

    private void OnInspectorUpdate()
    {
        conductor = FindObjectOfType<Conductor>();
        songPositionInBeats = conductor.songPositionInBeats;

        //for constant checking
        if (everyUpdate)
        {
            Debug.Log("Beat " + pos + ": " + beatList.beats[pos] + " & Song Position In Beats: " + Mathf.Round(songPositionInBeats));
        }

        Repaint();

        if (beatList.beats[pos] == Mathf.Round(songPositionInBeats))
        {
            if (pos < beatList.beats.Length - 1)
            {
                //for checking each spawn
                if(eachSpawn)
                {
                    Debug.Log("Beat " + pos + ": " + beatList.beats[pos] + " & Song Position In Beats: " + Mathf.Round(songPositionInBeats));
                }    
                SpawnNote();
                pos++;
            }
        }

        beat = Mathf.Round(songPositionInBeats);
    }

    private void OnGUI()
    {
        //spawnpoints
        if (spawnpoints == null)
        {
            EditorGUI.HelpBox(_helpRect1, _helpText1, MessageType.Warning);
            return;
        }
        else if (spawnpoints != null)
        {
            spawnpoints.Update();
            spList.DoList(_listRect1);
            spawnpoints.ApplyModifiedProperties();
        }
        GUILayout.Space(spList.GetHeight() + 110f);  

        //notes
        if(notes == null)
        {
            EditorGUI.HelpBox(_helpRect2, _helpText2, MessageType.Warning);
            return;
        }
        else if(notes != null)
        {
            notes.Update();
            nList.DoList(_listRect2);
            notes.ApplyModifiedProperties();
        }

        //beats
        if (beats == null)
        {
            EditorGUI.HelpBox(_helpRect3, _helpText3, MessageType.Warning);
            return;
        }
        else if (beats != null)
        {
            beats.Update();
            bList.DoList(_listRect3);
            beats.ApplyModifiedProperties();
        }

        GUILayout.Space(5f);

        pos = EditorGUILayout.IntField("Current Beat in Beats List", pos);
        GUILayout.Space(5f);

        beat = EditorGUILayout.FloatField("Song Position In Beats", beat);
        GUILayout.Space(5f);

        //button to spawn note
        if (GUILayout.Button("Spawn Note"))
        {
            SpawnNote();
        }
        GUILayout.Space(5f);

        //button to reset beats list
        if (GUILayout.Button("Reset Beats"))
        {
            pos = 0;
        }
        GUILayout.Space(5f);

        //button to check beat each spawn
        if (GUILayout.Button("Check Beat Each Spawn"))
        {
            if(!eachSpawn)
                eachSpawn = true;
            else if(eachSpawn)
                eachSpawn = false;
        }
        GUILayout.Space(5f);

        //button to check beat every update
        if (GUILayout.Button("Check Beat Every Update"))
        {
            if (!everyUpdate)
                everyUpdate = true;
            else if (everyUpdate)
                everyUpdate = false;
        }
        GUILayout.Space(5f);

        EditorGUILayout.LabelField("Beatmapper", EditorStyles.boldLabel);
    }

    private void SpawnNote()
    {
        int randSpawnpoint = Random.Range(0, spawnpointList.spawnpoints.Length);
        Transform spawnTransform = spawnpointList.spawnpoints[randSpawnpoint];
        int randNote = Random.Range(0, noteList.notes.Length);
        GameObject spawnNote = noteList.notes[randNote];
        //noteToSpawn = spawnNote;
        Instantiate(spawnNote, spawnTransform.position, spawnTransform.rotation);
    }
}

/*
if(noteToSpawn == null)
        {
            Debug.LogError("Error: Please assign an object to be spawned.");
            return;
        }
if(objectBaseName == string.Empty)
        {
            Debug.LogError("Error: Please enter a base name for the object.");
            return;
}*/