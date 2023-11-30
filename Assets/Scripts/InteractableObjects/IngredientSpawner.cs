using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
	//Ints
	//Floats
	//Bools

	//Strings
	//Components
	private SFX sfx;
	private GameManager gameManager;
	//GameObjects
	[SerializeField] private GameObject ingredientPrefab;
    //Vectors
    private void Start()
    {
		sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFX>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

	}
	public void SpawnIngredient()
    {
		//Spawns the ingredient on mouse positio
		Instantiate(ingredientPrefab, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Quaternion.identity);
    }
	private void OnMouseDown()
	{
		if (!gameManager.isDragging)
		{
            PlayPickingSFX();
			SpawnIngredient();
		}
	}
    private void PlayPickingSFX()
    {
        if(ingredientPrefab.GetComponent<Ingredient>() != null)
        {
            Ingredient ingredientScript = ingredientPrefab.GetComponent<Ingredient>();

            if (ingredientScript.type >= 0 && ingredientScript.type <= 5) //vegetable
            {
                int randomInt = Random.Range(0, 2); //changin random int to randomize sound
                if (randomInt == 0)
                {
                    sfx.PlayPickingVegetable1();
                }
                else
                {
                    sfx.PlayPickingVegetable2();
                }
            }
            else if (ingredientScript.type >= 9 && ingredientScript.type <= 11) //fish
            {
                sfx.PlayPickingMeat();

            }
            else if (ingredientScript.type == 8)
            {
                sfx.PlayPickingBread();

            }
            else
            {
                sfx.PlayPickingVegetable1();
            }
        }
        else //means it is instrument
        {
            sfx.PlaySettingInstrument();
        }
        
    }

}//END OF CLASS 
