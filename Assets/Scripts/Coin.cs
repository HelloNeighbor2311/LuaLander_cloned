using UnityEngine;

public class Coin : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
