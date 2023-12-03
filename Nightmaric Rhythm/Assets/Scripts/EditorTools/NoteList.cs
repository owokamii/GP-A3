using UnityEngine;

public class NoteList : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] notes = new GameObject[] { };

    public GameObject[] GetList()
    {
        return notes;
    }
}
