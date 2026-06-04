using CGL.Actor;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageOverTime : MonoBehaviour
{
    Health health;

    [SerializeField, Range(0.1f, 10)]
    float Timer = 1;
    
    [SerializeField, Range(0.1f, 10)]
    float damage = 1;

    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = Timer;
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            health.TakeDamage(damage);
            timer = Timer;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
