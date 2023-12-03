using UnityEngine;

public class HeartTween : MonoBehaviour
{
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 1f).setEaseOutExpo().setLoopClamp();
    }
}
