using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float time = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    private void FixedUpdate()
    {
        time -= Time.deltaTime;
        if (time < 0 ) 
            GameObject.Destroy(gameObject);
    }
}
