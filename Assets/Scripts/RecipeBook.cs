using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    [Header("Page 1")]
    [SerializeField] TextMeshProUGUI recipeName1;
    [SerializeField] TextMeshProUGUI recipeDescription1;
    [SerializeField] TextMeshProUGUI recipeServings1;
    [SerializeField] TextMeshProUGUI recipeWeight1;
    [SerializeField] TextMeshProUGUI recipeDuration1;
    [SerializeField] TextMeshProUGUI ingredientText1;

    [Header("Page 2")]
    [SerializeField] TextMeshProUGUI recipeName2;
    [SerializeField] TextMeshProUGUI recipeDescription2;
    [SerializeField] TextMeshProUGUI recipeServings2;
    [SerializeField] TextMeshProUGUI recipeWeight2;
    [SerializeField] TextMeshProUGUI recipeDuration2;
    [SerializeField] TextMeshProUGUI ingredientText2;


    [Header("Visiuals")]
    [SerializeField] Image recipeImage1;
    [SerializeField] Image recipeImage2;

    [Header("Cached Items")]
    GameSession gameSession;
    [SerializeField] Recipes[] recipesList;
    [SerializeField] TextMeshProUGUI ingredientTextPrefab;
    [SerializeField] GameObject ingredientList1;
    [SerializeField] GameObject ingredientList2;

    [Header("SerializeField for Testing")]
    [SerializeField] int recipeIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        UpdateRecipePage1();
        UpdateRecipePage2();
    }

    void UpdateRecipePage1()
    {
            recipeName1.text = recipesList[recipeIndex].GetRecipeName();

            recipeDescription1.text = recipesList[recipeIndex].GetRecipeDescription();

            recipeDuration1.text = recipesList[recipeIndex].GetRecipeDuration();

            recipeWeight1.text = recipesList[recipeIndex].GetRecipeWeight().ToString();

            recipeImage1.sprite = recipesList[recipeIndex].GetRecipeSprite();

        for (int i = 0; i< recipesList[recipeIndex].GetIngredients().Length; i++)
        {
            var newIngredient = Instantiate(ingredientTextPrefab);
            newIngredient.transform.parent = ingredientList1.transform;
            newIngredient.text = recipesList[recipeIndex].GetIngredients()[i].ToString();
        }
            
       
    }

    void UpdateRecipePage2()
    {
        int recipeIndex2 = recipeIndex + 1;

        recipeName2.text = recipesList[recipeIndex2].GetRecipeName();

        recipeDescription2.text = recipesList[recipeIndex2].GetRecipeDescription();

        recipeDuration2.text = recipesList[recipeIndex2].GetRecipeDuration();

        recipeWeight2.text = recipesList[recipeIndex2].GetRecipeWeight().ToString();

        recipeImage2.sprite = recipesList[recipeIndex2].GetRecipeSprite();

        for (int i = 0; i < recipesList[recipeIndex2].GetIngredients().Length; i++)
        {
            var newIngredient = Instantiate(ingredientTextPrefab);
            newIngredient.transform.parent = ingredientList2.transform;
            newIngredient.text = recipesList[recipeIndex2].GetIngredients()[i].ToString();
        }
    }

    public void RefreshRecipeBookPages()
    {
        
        UpdateRecipePage1();
        UpdateRecipePage2();
    }

    public void RightButtonSelected()
    {
        if(recipeIndex + 2 <  recipesList.Length)
        {
            recipeIndex += 2;
            ResetIngredients();
            RefreshRecipeBookPages();
        }
        else
        {
            return;
        }
        
    }

    public void LefttButtonSelected()
    {
        if (recipeIndex > 0)
        {
            recipeIndex -= 2;
            ResetIngredients();
            RefreshRecipeBookPages();
        }
        else
        {
            return;
        }
        
    }

    void ResetIngredients()
    {
        var itemsToDestroy = GameObject.FindGameObjectsWithTag("IngredientText");
        for(int i = 0;i < itemsToDestroy.Length;i++)
        {
            Destroy(itemsToDestroy[i]);
        }
        
    }
}
