using CGL.Controller;
using CGL.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    MouseRotate mr;

    [SerializeField]
    KinematicCharacterController KCC;

    [SerializeField]
    Gun gun;

    [SerializeField]
    WinScreen winScreen;

    public void OnDeath()
    {
        animator?.SetTrigger("Dead");
        KCC.Speed = 0;
        mr.enable = false;
        gun.CanFire = false;
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(5.0f);
        winScreen.OnGameOver();
    }
}
