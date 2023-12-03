using UnityEngine;

public class SpawnpointList : MonoBehaviour
{
    [HideInInspector]
    public Transform[] spawnpoints = new Transform[] {};

    public Transform[] GetList()
    {
        return spawnpoints;
    }
}
