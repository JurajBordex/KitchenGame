using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Recipe", fileName ="New Recipe")]
public class Recipes : ScriptableObject
{
    [Header("Recipe Information")]
    [SerializeField] string recipeName = "Enter Recipe Name here";
    [TextArea(2,4)] [SerializeField] string recipeDescription = "Enter Recipe Description here";
    [SerializeField] string recipeServings = "Enter how many servings here";
    [SerializeField] float recipeWeight = 0.90f;
    [SerializeField] string recipeDuration = "Enter Recipe preperation Time here in minutes";

    [Header("Ingredients")]
    [SerializeField] string[] ingredient;

    [Header("Visiuals")]
    [SerializeField] Sprite recipeSprite;

    public string GetRecipeName()
    {
        return recipeName;
    }

    public string GetRecipeDescription()
    {
        return recipeDescription;
    }

    public string GetRecipeServings()
    {
        return recipeServings;
    }

    public float GetRecipeWeight()
    {
        return recipeWeight;
    }

    public string GetRecipeDuration() 
    {
        return recipeDescription;
    }

    public string GetIngredient(int index)
    {
        return ingredient[index];
    }

    public Sprite GetRecipeSprite()
    {
        return recipeSprite;
    }
}
