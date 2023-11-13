using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] TextMeshProUGUI recipeName;
    [SerializeField] TextMeshProUGUI recipeDescription;
    [SerializeField] TextMeshProUGUI recipeServings;
    [SerializeField] TextMeshProUGUI recipeWeight;
    [SerializeField] TextMeshProUGUI recipeDuration;
    [SerializeField] TextMeshProUGUI[] ingredient;

    [Header("Visiuals")]
    [SerializeField] Image recipeImage;

    [Header("Cached Items")]
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        UpdateRecipePage();
    }

    void UpdateRecipePage()
    {
        var currentRecipeIndex = gameSession.RecallCurrentRecipe();
        recipeName.text = currentRecipeIndex.GetRecipeName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
