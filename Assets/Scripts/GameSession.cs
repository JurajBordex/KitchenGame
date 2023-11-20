using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] Recipes[] recipes;
    [SerializeField] int numOfRecipesInLevel = 4;

    [Header("Cached Items")]
    [SerializeField] GameObject orderSpritePrefab;
    [SerializeField] GameObject poster;
    

    [Header("Testing")]
    [SerializeField] int currentRecipeIndex;
    [SerializeField] int randomRecipeIndex;
    [SerializeField] List<Recipes> recipesList;


    void Start()
    {
        GenerateRecipesList();
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

    private void GenerateOrderSprite()
    {
        var orderSprite = Instantiate(orderSpritePrefab);
        orderSprite.GetComponent<SpriteRenderer>().sprite = recipes[currentRecipeIndex].GetRecipeSprite();
        orderSprite.transform.parent = poster.transform;
    }

    public void SubrtactRecipeMenu()
    {
        if (recipesList.Count > 1)
        {
            recipesList.Remove(recipesList[randomRecipeIndex]);
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
        randomRecipeIndex = Random.Range(0, recipesList.Count);
    }

    public Recipes RecallCurrentRecipe()
    {
        GenerateRandomRecipe();
        return recipesList[randomRecipeIndex];
        //more code to add
        //instantiate Banner/ Music/ Animation / Game trackers
    }

    //The passed recipe index will be passed when meal is put on the counter, each recipe(meal) will have a recipeIndex(what recipe it is)
    public void RecipeCompletedTracker(List<Vector3> passedIngredientTypeWeightState)
    {
        bool ingredientsMatch = false;

        for(int recipeIndex = 0; recipeIndex < recipesList.Count; recipeIndex++) //goes through each recipe in recipe list
        {
            for (int i = 0; i < recipesList[recipeIndex].ingredientsTypeWeightState.Count; i++) //goes through every vector3 of previously chosen List
            {
                //SIMPLIFIED if(recipesList[recipeIndex].ingredientsTypeWeightState.Contains(passedIngredientTypeWeightState[i])) //if the chosen vector 3 list contains the ingredient the loop continues until all of ingredients have been looped through 
                if (recipesList[recipeIndex].ingredientsTypeWeightState.Contains(passedIngredientTypeWeightState[i]) && passedIngredientTypeWeightState.Contains(passedIngredientTypeWeightState[i])) //if the chosen vector 3 list contains the ingredient the loop continues until all of ingredients have been looped through , check for the other way as well
                {
                    Debug.Log("CONTAINS THIS ELEMENT INGREDIENT " + i);
                }
                else
                {
                    Debug.Log("DOES NOT CONTAIN THIS ELEMENT INGREDIENT OR WRONG WEIGHT OF INGREDIENT " + i);
                    Debug.Log("ESCAPE THESE BLOODY LOOPS PLEASE");
                }
                //if the script got here it will loop the second for loop again
            }
        }
    }

    public void RecipeCompleted()
    {
        //CODE WHEN COMPLETED AS DELETING OBJS AND RESETING THE ASSIGNED VARIABLES
            SubrtactRecipeMenu();
            GenerateRandomRecipe();
    }
}
