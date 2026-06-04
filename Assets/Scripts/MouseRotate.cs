using Unity.Cinemachine;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask groundLayer;
    public bool enable = true;
    // Update is called once per frame
    public void Look()
    {
        if (enabled)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
            {
                Vector3 targetPosition = hit.point;

                Vector3 direction = targetPosition - transform.position;
                direction.y = 0f;

                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
                }
            }
        }
    }
}
