using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateCraftingList : MonoBehaviour
{
    public GameObject RecepieSlot;
    void Start()
    {
        var recipeList = Resources.LoadAll<CraftingRecipes>("Crafting");
        for (int i = 0; i < recipeList.Length; i++)
        {
            GameObject recipeSlot = Instantiate(RecepieSlot, transform, false);
            recipeSlot.GetComponent<RectTransform>().localPosition += new Vector3(0, -(90 * i)-30, 0);
            recipeSlot.GetComponentInChildren<SpawnIngredients>().Recipie = recipeList[i];
            recipeSlot.GetComponent<SetImageItemCount>().setVars(1, recipeList[i].Output.Icon);
            recipeSlot.GetComponent<CraftingSlot>().recipe = recipeList[i];
        }
    }
}
