using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixingAreaDropZone : MonoBehaviour, IDropHandler
{
    public List<Ingredient> currentIngredients = new List<Ingredient>();
    public MixingManager mixManager;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableIngredient drag = eventData.pointerDrag?.GetComponent<DraggableIngredient>();
        if (drag != null && drag.ingredientData != null)
        {
            bool added = mixManager.TryAddIngredient(drag.ingredientData);
            if (added)
            {
                Debug.Log("🥣 Added ingredient: " + drag.ingredientData.ingredientName);
                Destroy(drag.gameObject); // 👈 destroy dragged ingredient after use
            }
            else
            {
                Debug.Log("❌ Bowl is full! Cannot add more ingredients.");
            }
        }
    }
}