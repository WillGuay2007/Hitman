using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Transform Camera;
    private bool IsShaking;
    private float ShakeProgress;
    private float ShakeTime;

    public void Shake(float amplitude)
    {
        if (IsShaking) return;
        IsShaking = true;
    }

}
