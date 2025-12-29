using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartPosition;
    [SerializeField] private Transform cameraStartPosition;
    [SerializeField] private float zoomedOutOrthographicSize;



    public int getLevelNumber()
    {
        return levelNumber;
    }
    public Vector3 getLanderStartPosition()
    {
        return landerStartPosition.transform.position;
    }
    public Transform getCameraStartPosition()
    {
        return cameraStartPosition;
    }
    public float getZoomedOutOrthographicSize()
    {
        return zoomedOutOrthographicSize;
    }
}
