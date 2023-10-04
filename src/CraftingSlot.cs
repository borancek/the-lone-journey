using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSlot : MonoBehaviour
{
    public CraftingRecipes recipe;
    public void onItemButton()
    {
        int itemCount = 0;
        var item = recipe.Output;
        var b = recipe.getIngredients();
        foreach (var a in b) {
            if (Player.instance.hasItem(a.Item1, a.Item2))
            {
                itemCount++;
            }
            else
            {
                return;
            }
        }

        if (itemCount == b.Count)
        {
            foreach (var a in b)
            {
                Player.instance.removeItem(a.Item1, a.Item2);
            }
        }
        Player.instance.addItem(item, 1);
    }

    public void onHoverEnter()
    {
        /*var rect = new Rect();
        rect.position = Input.mousePosition;
        GUI.Label(rect, "");*/
    }
}
