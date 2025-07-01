using UnityEngine;
using UnityEngine.UI;

public class IngredientSlotUI : MonoBehaviour
{
    public Ingredient ingredient;
    public Slider quantitySlider;
    public Button button;
    public Button restockButton;

    private void Start()
    {
        UpdateVisuals();

        if (button != null)
            button.onClick.AddListener(AttemptUseIngredient);

        if (restockButton != null)
        {
            restockButton.onClick.AddListener(RestockIngredient);
            restockButton.gameObject.SetActive(false); // Hide at start
        }
    }

    void AttemptUseIngredient()
    {
        if (IngredientInventory.Instance.UseIngredient(ingredient))
        {
            UpdateVisuals();
        }
        else
        {
            Debug.LogWarning($"🚫 Not enough {ingredient.name} left!");
        }
    }

    public void UpdateVisuals()
    {
        int current = IngredientInventory.Instance.GetAmount(ingredient);
        int max = IngredientInventory.Instance.GetMaxAmount(ingredient);

        if (quantitySlider != null)
        {
            quantitySlider.maxValue = max;
            quantitySlider.value = current;
        }

        if (button != null)
            button.interactable = current > 0;

        if (restockButton != null)
            restockButton.gameObject.SetActive(current <= 0);
    }

    void RestockIngredient()
    {
        const float restockCost = 30.0f;

        if (MoneyManager.Instance.GetEmbers() >= restockCost)
        {
            MoneyManager.Instance.SubtractEmbers(restockCost);
            IngredientInventory.Instance.RestockIngredient(ingredient);
            UpdateVisuals();
        }
        else
        {
            Debug.LogWarning("💸 Not enough embers to restock!");
        }
    }
}