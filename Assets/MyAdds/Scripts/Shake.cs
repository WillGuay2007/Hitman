using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float Duration = 1.0f;
    public float Amplitude = 1.0f;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

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
