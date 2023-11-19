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
    [SerializeField] int recipeDifficulty = 2;
    

    [Header("Ingredients")]
    [SerializeField] string[] ingredient;
    [SerializeField] float[] ingredientWeight;

    public List<Vector3> ingredientsTypeWeightState; //I FEEL FANCY TODAY .... AND TOMORROW

    [Header("Visiuals")]
    [SerializeField] Sprite recipeSprite;

    public Vector3 GetIngredientsInfo(int i)
    {
        return ingredientsTypeWeightState[i];
    }

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

    public int GetRecipeDifficulty() 
    {
        return recipeDifficulty;
    }

    public string[] GetIngredients()
    {
        return ingredient;
    }

    public float[] GetIngredientsWeights()
    {
        return ingredientWeight;
    }

    public Sprite GetRecipeSprite()
    {
        return recipeSprite;
    }
}
