using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/CraftingRecipe")]

public class CraftingRecipes : ScriptableObject
{
    public string Name;
    public Item Output;
    public Item[] Input;
    public int[] Ratios;


    public CraftingRecipes(string _Name, Item _Output, Item[] _Input, int[] _Ratios)
    {
        Name = _Name;
        Output = _Output;
        Input = _Input;
        Ratios = _Ratios;
    }

    public List<Tuple<Item, int>> getIngredients()
    {
        List<Tuple<Item, int>> Ingredients = new List<Tuple<Item, int>>();
        for (int i = 0; i < Input.Length; i++)
        {
            if (i >= Ratios.Length)
            {
                Ingredients.Add(new Tuple<Item, int>(Input[i], 1));
            }
            else
            {
                Ingredients.Add(new Tuple<Item, int>(Input[i], Ratios[i]));
            }
        }
        return Ingredients;
    }
}





