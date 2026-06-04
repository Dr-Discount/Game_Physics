using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        RollerPlayer roller = other.GetComponent<RollerPlayer>();

        if (roller != null)
        {
            gameManager.GameOver();
        }

    }
}
