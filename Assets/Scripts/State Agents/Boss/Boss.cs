using CGL.UI;
using UnityEngine;

public class BossStateAgent : MonoBehaviour
{
    [SerializeField]
    public WinScreen win;

    private EnemyStateAgent agent;
    private void Awake()
    {
        GameObject screen = GameObject.FindWithTag("GameController");
        win = screen.GetComponent<WinScreen>();

        GameObject health = GameObject.FindWithTag("Health_Bar");
        health.GetComponent<Canvas>().enabled = true;

        TryGetComponent<EnemyStateAgent>(out agent);
        GameObject slider = GameObject.FindWithTag("Slider");
        agent.bar = slider.GetComponent<SliderUI>();
    }
}
