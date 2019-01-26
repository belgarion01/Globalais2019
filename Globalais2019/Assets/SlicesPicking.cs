using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlicesPicking : MonoBehaviour
{

    public GameObject joueur;
    public GameObject myself;

    Image pizzaSprite;
    NeedsManager pizzaManager;

    // Start
    void Start()
    {
        pizzaSprite = this.GetComponent<Image>();
        pizzaManager = joueur.GetComponent<NeedsManager>();

        pizzaManager.mesSlices.Add(this.gameObject);
    }

    // Update
    void Update()
    {
        
    }


    public void TypeOfSlices()
    {
        if (pizzaSprite.sprite.name == "GoodPizza")
        {
            pizzaManager.foodNeed = pizzaManager.foodNeed + 0.1f;

            myself.SetActive(false);
        }

        if (pizzaSprite.sprite.name == "PineapplePizza")
        {
            pizzaManager.foodNeed = pizzaManager.foodNeed - 0.2f;

            myself.SetActive(false);
        }
    }
}
