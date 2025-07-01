using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IngredientButton : MonoBehaviour
{
    public Ingredient ingredientData;
    public GameObject draggableIngredientPrefab;
    public Transform dragSpawnParent;

    public void OnClickSpawnDraggable()
    {
        GameObject newDrag = Instantiate(draggableIngredientPrefab, dragSpawnParent);
        newDrag.transform.position = Mouse.current.position.ReadValue();

        DraggableIngredient dragScript = newDrag.GetComponent<DraggableIngredient>();
        dragScript.Setup(ingredientData);
    }
}