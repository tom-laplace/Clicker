using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public float Health, MaxHealth, Width, Height;

    [SerializeField] private RectTransform healthBar;
    // Start is called before the first frame update
    public void SetMaxHealth(float maxHealth)
    {
        MaxHealth = maxHealth;
    }

    public void setHealth(float health)
    {
        Health = health;
        healthBar.sizeDelta = new Vector2(Health / MaxHealth * Width, Height);
    }
}
