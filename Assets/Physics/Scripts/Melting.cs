using UnityEngine;

public class Melting : MonoBehaviour
{
    [SerializeField]
    private float MeltingRate = 0.01f;

    void FixedUpdate()
    {
        Vector3 temp = transform.localScale;

        if (temp.x > 1)
        {
            temp.x -= MeltingRate;
            temp.z -= MeltingRate;
        }

        transform.localScale = temp;
    }
}
