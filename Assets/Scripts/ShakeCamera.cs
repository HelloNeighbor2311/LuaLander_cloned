using Unity.Cinemachine;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera instance; 

    private CinemachineCamera cinemachineCamera;
    private float shakeTimerTotal = 1f;
    private float shakeTimer = 0f;
    private float startShakeIntensity;
    private void Awake()
    {
        instance = this;
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void ShakeMyCamera(float shakeIntensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.AmplitudeGain = shakeIntensity;

        startShakeIntensity = shakeIntensity;
        shakeTimer = time;
        shakeTimerTotal = time;

    }
    void Update()
    {
        if (shakeTimer > 0f) { 
            shakeTimer -= Time.deltaTime;   
            if(shakeTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.AmplitudeGain = Mathf.Lerp(startShakeIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));
            }
        }
    }
}
