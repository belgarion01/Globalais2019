using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlicesPicking : MonoBehaviour
{

    public GameObject joueur;

    Image pizzaSprite;
    NeedsManager myPlayer;

    // Start
    void Start()
    {
        pizzaSprite = this.GetComponent<Image>();

    }

    // Update
    void Update()
    {
        
    }


    public void TypeOfSlices()
    {

        if(pizzaSprite.sprite.name == "GoodPizza")
        {


            this.enabled = false;
        }

        if (pizzaSprite.sprite.name == "PineapplePizza")
        {


            this.enabled = false;
        }

    }
}
