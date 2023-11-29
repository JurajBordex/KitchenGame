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
    [SerializeField] TextMeshProUGUI recipeDifficulty1;

    [Header("Page 2")]
    [SerializeField] TextMeshProUGUI recipeName2;
    [SerializeField] TextMeshProUGUI recipeDescription2;
    [SerializeField] TextMeshProUGUI recipeServings2;
    [SerializeField] TextMeshProUGUI recipeWeight2;
    [SerializeField] TextMeshProUGUI recipeDifficulty2;

    [Header("Visiuals")]
    [SerializeField] Image recipeImage1;
    [SerializeField] Image recipeImage2;

    [Header("Cached Items")]
    GameSession gameSession;
    [SerializeField] Recipes[] recipesList;
    [SerializeField] TextMeshProUGUI ingredientTextPrefab;
    [SerializeField] GameObject ingredientList1;
    [SerializeField] GameObject ingredientList2;
    TextMeshProUGUI newText;

    [Header("SerializeField for Testing")]
    [SerializeField] int recipeIndex = 0;

    private SFX sfx;
    

    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFX>();

        gameSession = FindObjectOfType<GameSession>();
        UpdateRecipePage1();
        UpdateRecipePage2();
    }

    void UpdateRecipePage1()
    {
            recipeName1.text = recipesList[recipeIndex].GetRecipeName();

            recipeDescription1.text = recipesList[recipeIndex].GetRecipeDescription();

            recipeDifficulty1.text = recipesList[recipeIndex].GetRecipeDifficulty().ToString();

            recipeWeight1.text = recipesList[recipeIndex].GetRecipeWeight().ToString();

            recipeImage1.sprite = recipesList[recipeIndex].GetRecipeSprite();

        for (int i = 0; i< recipesList[recipeIndex].GetIngredients().Length; i++)
        {
            newText = Instantiate(ingredientTextPrefab);
            AssignParent(ingredientList1.transform);
            newText.text = recipesList[recipeIndex].GetIngredients()[i].ToString();
        }
            
       
    }

    void UpdateRecipePage2()
    {
        int recipeIndex2 = recipeIndex + 1;

        recipeName2.text = recipesList[recipeIndex2].GetRecipeName();

        recipeDescription2.text = recipesList[recipeIndex2].GetRecipeDescription();

        recipeDifficulty2.text = recipesList[recipeIndex2].GetRecipeDifficulty().ToString();

        recipeWeight2.text = recipesList[recipeIndex2].GetRecipeWeight().ToString();

        recipeImage2.sprite = recipesList[recipeIndex2].GetRecipeSprite();

        for (int i = 0; i < recipesList[recipeIndex2].GetIngredients().Length; i++)
        {
            newText = Instantiate(ingredientTextPrefab);
            AssignParent(ingredientList2.transform);
            newText.text = recipesList[recipeIndex2].GetIngredients()[i].ToString();
            
        }
    }

    void AssignParent(Transform newParent)
    {
        newText.transform.SetParent(newParent);
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
            PlayPageFlipSFX();
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
            PlayPageFlipSFX();
            recipeIndex -= 2;
            ResetIngredients();
            RefreshRecipeBookPages();
        }
        else
        {
            return;
        }
        
    }
    private void PlayPageFlipSFX()
    {
        sfx.PlayPageFlip();
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
