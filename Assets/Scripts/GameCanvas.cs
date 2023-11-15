using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] Image imagetoChange;
    [SerializeField] Sprite newSprite;
    // Start is called before the first frame update
    void Start()
    {
        imagetoChange.sprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
