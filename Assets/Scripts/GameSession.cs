using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] Recipes[] recipes;
    

    [Header("Testing")]
    [SerializeField] int currentRecipeIndex;
    [SerializeField] List<Recipes> recipesList;

    void Start()
    {
        GenerateRecipesList();
        GenerateRandomRecipe();
    }
    // note: we have an array that is manually filled from inspector, and a list that is generated
    // automatically though code
    // the reason is; the list will be shortened and therefore can't be used in other game elements
    // the array will be consistent throught the level, and will be used for the Recipe book and checklist
    private void GenerateRecipesList()
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            recipesList.Add(recipes[i]);
        }
    }

    public void SubrtactRecipeMenu()
    {
        if (recipesList.Count > 1)
        {
            recipesList.Remove(recipesList[currentRecipeIndex]);
        }
        else
        {
            return;
            // will add code here later
            // LevelCompleted()
            // LoadNextScene()
        }
        
    }
    //the reason we will shorten the list is to keep track of what has been made and what is left to do
    //to complete the level, once the elements reach 0 then we advance to next level

    public void GenerateRandomRecipe()
    {
        currentRecipeIndex = Random.Range(0, recipesList.Count);
    }

    public Recipes RecallCurrentRecipe()
    {
        return recipesList[currentRecipeIndex];
        //more code to add
        //instantiate Banner/ Music/ Animation / Game trackers
    }

    //THe passed recipe index will be passed when meal is put on the counter, each recipe(meal) will have a recipeIndex(what recipe it is)
    public void RecipeCompletedTracker(int passedRecipeIndex)
    {
        for(int i = 0; i < recipes[passedRecipeIndex].ingredientsTypeWeightState.Count; i++)
        {
            //DO
        }
    }

    public void RecipeCompleted()
    {
            SubrtactRecipeMenu();
            GenerateRandomRecipe();
    }
}
