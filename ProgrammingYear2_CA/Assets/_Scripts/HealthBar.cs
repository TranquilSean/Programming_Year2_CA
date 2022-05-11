using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public float maxHealth;
    public float health;

    public GameManager gm;
    private void Start()
    {

        health = maxHealth;
        SetHealth(health);
    }

    private void Update()
    {
        slider.maxValue = maxHealth;
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void EditHealth(float damage)
    {
        damage = Mathf.RoundToInt(damage);
        health -= (int)damage;
        SetHealth(health);
    }
}