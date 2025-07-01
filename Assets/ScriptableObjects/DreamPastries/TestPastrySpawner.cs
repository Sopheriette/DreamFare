using UnityEngine;
using System.Collections.Generic;

public class TestPastrySpawner : MonoBehaviour
{
    public List<Ingredient> availableIngredients;

    private void Start()
    {
        var selected = new List<Ingredient>();
        for (int i = 0; i < 3; i++)
        {
            var random = availableIngredients[Random.Range(0, availableIngredients.Count)];
            selected.Add(random);
        }
        DreamPastry newPastry = DreamPastry.Create(selected);
        Debug.Log("Created Pastry: "+newPastry.GetPastryName());

        foreach (var tag in newPastry.dreamTags)
        {
            Debug.Log("Tag: "+tag.name);
        }
    }
}
