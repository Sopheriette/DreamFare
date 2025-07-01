using System.Collections.Generic;
using UnityEngine;

public class IngredientInventory : MonoBehaviour
{
    public static IngredientInventory Instance;

    [System.Serializable]
    public class IngredientStock
    {
        public Ingredient ingredient;
        public int currentAmount = 10;
        public int maxAmount = 10;
    }

    public List<IngredientStock> ingredientStocks = new List<IngredientStock>();

    private Dictionary<Ingredient, IngredientStock> stock;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        stock = new Dictionary<Ingredient, IngredientStock>();
        foreach (var inventory in ingredientStocks)
        {
            stock[inventory.ingredient] = inventory;
        }
    }

    public bool HasIngredient(Ingredient ingredient)
    {
        return stock.ContainsKey(ingredient) && stock[ingredient].currentAmount > 0;
    }

    public bool UseIngredient(Ingredient ingredient)
    {
        if (!HasIngredient(ingredient)) return false;

        stock[ingredient].currentAmount--;
        return true;
    }

    public int GetAmount(Ingredient ingredient)
    {
        return stock.ContainsKey(ingredient) ? stock[ingredient].currentAmount : 0;
    }

    public int GetMaxAmount(Ingredient ingredient)
    {
        return stock.ContainsKey(ingredient) ? stock[ingredient].maxAmount : 0;
    }
    public void RestockIngredient(Ingredient ingredient)
    {
        if (stock.ContainsKey(ingredient))
        {
            stock[ingredient].currentAmount = stock[ingredient].maxAmount;
        }
    }
}