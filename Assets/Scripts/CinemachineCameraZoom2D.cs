using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
    public static CinemachineCameraZoom2D Instance { get; private set; }

    public const float NORMAL_ORTHOGRAPHIC_SIZE = 10f;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float targetOrthoGraphicSize = 10f;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        float zoomSpeed = 2f;

        // làm mượt camera khi zoom in
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrthoGraphicSize, zoomSpeed * Time.deltaTime);
    }

    public void setTargetOrthographicSize(float targetOrthoGraphicSize)
    {
        this.targetOrthoGraphicSize = targetOrthoGraphicSize; 
    }

    public void setNormalOrthographicSize()
    {
        setTargetOrthographicSize(NORMAL_ORTHOGRAPHIC_SIZE);
    }
}
