using UnityEngine;
using System.Collections;

public static class CanvasGroupExtensions
{
    public static IEnumerator Fade(this CanvasGroup group, float targetAlpha, float duration, bool changeRaycast)
    {
        // Only modify blocksRaycasts if requested
        if (changeRaycast)
        {
            group.blocksRaycasts = targetAlpha > 0.5f;
            yield return null; // Allow 1 frame for potential rebuild
        }

        float startAlpha = group.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            group.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        group.alpha = targetAlpha;
    }

    public static Coroutine FadeIn(this CanvasGroup group, MonoBehaviour runner, float duration = 0.3f)
    {
        return runner.StartCoroutine(Fade(group, 1f, duration, true));
    }

    public static Coroutine FadeOut(this CanvasGroup group, MonoBehaviour runner, float duration = 0.3f)
    {
        return runner.StartCoroutine(Fade(group, 0f, duration, true));
    }
}