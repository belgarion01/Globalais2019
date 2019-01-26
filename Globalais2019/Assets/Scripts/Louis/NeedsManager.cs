using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsManager : MonoBehaviour
{

    public GameObject waterSlider;
    public GameObject foodSlider;

    Slider waterBar;
    Slider foodBar;

    public float waterNeed = 1f;
    public float foodNeed = 1f;
    public float peeNeed = 0f;

    [Range(1,10), SerializeField]
    private float level = 1;

    private float levelCopy = 1;

    float waterRate;
    float foodRate;

    // Start
    void Start()
    {
        waterBar = waterSlider.GetComponent<Slider>();
        foodBar = foodSlider.GetComponent<Slider>();

        waterRate = 10 / level;
        foodRate = 5 / level;
        InvokeRepeating("Thirst", waterRate, waterRate);
        InvokeRepeating("Hunger", foodRate, foodRate);
    }

    // Update
    void Update()
    {
        waterBar.value = waterNeed;
        foodBar.value = foodNeed;

        // Mise à jour du niveau dans l'InvokeRepeating
        if (level != levelCopy)
        {
            levelCopy = level;
            UpdateLevel();
        }


        // Plafond eau
        if (waterNeed > 1)
        {
            waterNeed = 1;
        }
        // Manque d'eau
        if (waterNeed <= 0)
        {
            Debug.Log("You are dry - You died");
            CancelInvoke("Thirst");
        }


        // Plafond nourriture
        if (foodNeed > 1)
        {
            foodNeed = 1;
        }
        // Manque de nourriture
        if (foodNeed <= 0)
        {
            Debug.Log("You are out of pizzas !");
            CancelInvoke("Hunger");
        }


        // Plafond pisse (GameOver)
        if (peeNeed > 1)
        {
            Debug.Log("Tu t'es pissé dessus : PERDU !");
            peeNeed = 1;
        }

        // Vessie vide
        if (peeNeed <= 0)
        {
            CancelInvoke("PeeEmptier");
            peeNeed = 0;
        }


    }


    public void Thirst()
    {
        if (waterNeed > 0f && waterNeed <= 1f)
        {
            waterNeed = waterNeed - 0.2f;
        }
    }

    public void Hunger()
    {
        if (foodNeed > 0f && foodNeed <= 1f)
        {
            foodNeed = foodNeed - 0.05f;
        }
    }

    public void UpdateLevel()
    {
        CancelInvoke();
        waterRate = 10 / level;
        foodRate = 5 / level;
        InvokeRepeating("Thirst", waterRate, waterRate);
        InvokeRepeating("Hunger", foodRate, foodRate);
    }

    public void LevelIncrease()
    {
        level++;
    }

    public void PeeEmptier()
    {
        peeNeed = peeNeed - 0.01f;
    }



}
