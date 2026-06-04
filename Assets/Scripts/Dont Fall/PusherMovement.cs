using UnityEngine;

public class PusherMovement : MonoBehaviour
{
    public int points = 5;
    
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 8 * Time.deltaTime);
    }
}
