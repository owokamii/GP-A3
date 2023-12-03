using UnityEngine;

public class BedTween : MonoBehaviour
{
    private Vector3 originalScale;
    private float bounceScaleFactor = 1.5f;
    private float animationDuration = 0.2f;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void AnimateBedBounce()
    {
        LeanTween.scale(gameObject, originalScale * bounceScaleFactor, animationDuration / 2).setEase(LeanTweenType.easeOutQuad).setOnComplete(AnimateBedShrink);
    }

    public void AnimateBedShrink()
    {
        LeanTween.scale(gameObject, originalScale, animationDuration / 2).setEase(LeanTweenType.easeInQuad);
    }
}
