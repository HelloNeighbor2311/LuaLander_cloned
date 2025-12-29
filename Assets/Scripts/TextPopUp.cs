using System.Collections;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    private float timer = 1.2f;


    private void Update()
    {
       timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SimplePool2.Despawn(gameObject);
            timer = 1.2f;
        }
    }
}
