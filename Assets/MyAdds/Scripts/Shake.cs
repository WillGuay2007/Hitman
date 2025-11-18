using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float Duration = 1.0f;
    public float Amplitude = 1.0f;

    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < Duration) { 
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / Duration);
            transform.localPosition = startPosition + Random.insideUnitSphere * strength * Amplitude;
            yield return null;
        }

        transform.localPosition = startPosition;

    }
}
