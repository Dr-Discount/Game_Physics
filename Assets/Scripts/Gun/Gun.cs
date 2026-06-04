using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject Muzzle;

    [SerializeField]
    GameObject Ammo;

    [SerializeField]
    float MaxFireRate;

    float fireate;

    [SerializeField]
    AudioSource audio;

    public bool CanFire = true;

    void Start()
    {
        fireate = 0;   
    }

    void Update()
    {
        if (!(fireate <= 0) )
            fireate -= Time.deltaTime;

        if (Mouse.current.leftButton.wasPressedThisFrame && fireate <= 0 && CanFire)
            Fire();
    }

    private void Fire()
    {
        audio.Play();
        Instantiate(Ammo, Muzzle.transform.position, Muzzle.transform.rotation);
        fireate = MaxFireRate;
    }
}
