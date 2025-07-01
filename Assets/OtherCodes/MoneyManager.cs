using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public TextMeshProUGUI emberText;
    private float embers = 100.00f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddEmbers(float amount)
    {
        embers += amount;
        UpdateUI();
    }

    public void SubtractEmbers(float amount)
    {
        embers -= amount;
        if (embers < 0) embers = 0;
        UpdateUI();
    }

    public float GetEmbers()
    {
        return embers;
    }

    void UpdateUI()
    {
        emberText.text = $"{embers:0.00}";
    }
}