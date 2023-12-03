using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1);
    }
}
