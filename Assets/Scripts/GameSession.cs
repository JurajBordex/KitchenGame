using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] Recipes[] recipes;

    [Header("Cached Items")]
    [SerializeField] GameObject orderSpritePrefab;
    [SerializeField] GameObject poster;
    GameObject newSprite;
    

    [Header("Testing")]
    [SerializeField] int currentRecipeIndex;
    [SerializeField] List<Recipes> recipesList;

    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] float gameTime;
    private float timeCounter;

    private SFX sfx;
    void Start()
    {
        timeCounter = gameTime;
        timeText.text = gameTime.ToString("F0");

        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFX>();

        GenerateRecipesList();
        GenerateRandomRecipe();

        StartCoroutine(WaitToEndGame(gameTime));
    }
    private void Update()
    {
        timeCounter -= Time.deltaTime;

        if(timeCounter + 1 > int.Parse(timeText.text)) //if changed by 1 sec change the text
        {
            timeText.text = timeCounter.ToString("F0");

        }
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
        newSprite = Instantiate(orderSpritePrefab);
        newSprite.GetComponent<SpriteRenderer>().sprite = recipes[currentRecipeIndex].GetRecipeSprite();
        AssignSpriteParent(poster.transform);
    }

    void AssignSpriteParent(Transform newPosition)
    {
        newSprite.transform.SetParent(newPosition);
    }

    public void SubrtactRecipeMenu(Recipes completedRecipe)
    {
        if (recipesList.Count > 1)
        {

            recipesList.Remove(recipesList[currentRecipeIndex]);
            GenerateRandomRecipe();

            recipesList.Remove(completedRecipe);

        }
        else
        {
            LevelCompleted();
            return;
            // LevelCompleted()
            // LoadNextScene()
        }
        
    }
    //the reason we will shorten the list is to keep track of what has been made and what is left to do
    //to complete the level, once the elements reach 0 then we advance to next level

    private void LevelCompleted()
    {
        StopCoroutine(WaitToEndGame(gameTime)); //stops the time coroutine

        sfx.PlayLevelFinished();
        //win window
        //load next level
    }

    private void LevelFailed()
    {
        sfx.PlayLevelFailed();
        //failed window
        //restart level
    }

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

    //The passed recipe index will be passed when meal is put on the counter, each recipe(meal) will have a recipeIndex(what recipe it is)
    public void RecipeCompletedTracker(List<Vector3> passedIngredientTypeWeightState)
    {
        bool ingredientsMatch = false;
        Recipes passedRecipe = null;

        for(int recipeIndex = 0; recipeIndex < recipesList.Count; recipeIndex++) //goes through each recipe in recipe list
        {
            //COmparing if all the ingredients are same in the chosen recipe
            for (int i = 0; i < recipesList[recipeIndex].ingredientsTypeWeightState.Count; i++)
            {
                if (ingredientsMatch)
                {
                    RecipeCompleted(passedRecipe);
                    return;

                }
                //SIMPLIFIED if(recipesList[recipeIndex].ingredientsTypeWeightState.Contains(passedIngredientTypeWeightState[i])) //if the chosen vector 3 list contains the ingredient the loop continues until all of ingredients have been looped through 
                if (recipesList[recipeIndex].ingredientsTypeWeightState.Contains(passedIngredientTypeWeightState[i]) && passedIngredientTypeWeightState.Contains(passedIngredientTypeWeightState[i])) //if the chosen vector 3 list contains the ingredient the loop continues until all of ingredients have been looped through , check for the other way as well
                {
                    ingredientsMatch = true;
                    passedRecipe = recipesList[recipeIndex]; //sets the recipe to passedRecipe
                }
                else
                {
                    sfx.PlayWrongBell();
                    ingredientsMatch = false;
                    passedRecipe = null;
                    break;
                }
                //if the script got here it will loop the second for loop again
            }
        }
    }

    public void RecipeCompleted(Recipes completedRecipe)
    {
        //CODE WHEN COMPLETED AS DELETING OBJS AND RESETING THE ASSIGNED VARIABLES

            sfx.PlayBell();
            SubrtactRecipeMenu(completedRecipe);
            GenerateRandomRecipe();
    }
    
    IEnumerator WaitToEndGame(float gameTime)
    {
        yield return new WaitForSeconds(gameTime); //counts down time
        LevelFailed();
    }
}
