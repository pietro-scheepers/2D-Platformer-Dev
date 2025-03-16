using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text valueText;
    public float fillAmount;
    private float maxHealth;

    private void Awake()
    {
        maxHealth = playerHealth.StartingHealth;
        fillAmount = healthBar.fillAmount;
        healthBar.fillAmount = Map(maxHealth, 0, maxHealth, 0, 1);
    }

    private void Update()
    {
        HandleBar();
    }
    private void HandleBar(){
        if (fillAmount!=healthBar.fillAmount){}
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0]+ ": " + playerHealth.currentHealth;
            healthBar.fillAmount = fillAmount;
        }
    }
    public float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
