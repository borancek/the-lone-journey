using UnityEngine;
public class SpawnIngredients : MonoBehaviour
{
    public GameObject IngredientPrefab;
    public CraftingRecipes Recipie;
    // Start is called before the first frame update
    void Start()
    {
        var ingredients = Recipie.getIngredients();
        for (int i = 0; i < ingredients.Count; i++)
        {
            GameObject UIElement = Instantiate(IngredientPrefab, transform, false);
            UIElement.GetComponent<RectTransform>().localPosition += new Vector3(130 + (80*i), 0, 0);
            UIElement.GetComponent<SetImageItemCount>().setVars(ingredients[i].Item2, ingredients[i].Item1.Icon);
        }
    }
}
