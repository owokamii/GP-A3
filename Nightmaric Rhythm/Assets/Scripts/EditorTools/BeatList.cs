using UnityEngine;

public class BeatList : MonoBehaviour
{
    [HideInInspector]
    public float[] beats = new float[] { };

    public float[] GetList()
    {
        return beats;
    }
}
