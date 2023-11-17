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
	private GameManager gameManager;
	//GameObjects
	[SerializeField] private GameObject ingredientPrefab;
    //Vectors
    private void Start()
    {
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
			SpawnIngredient();
		}
	}


}//END OF CLASS 
