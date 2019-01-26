using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaGameDetector : MonoBehaviour
{

    public GameObject PizzaOnCanvas;
    public PizzaRandomizer pizzaRandomizer;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PizzaOnCanvas.SetActive(false);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            PizzaOnCanvas.SetActive(true);
        }

        if(collision.tag == "Player" && Input.GetKeyDown(KeyCode.Space) && pizzaRandomizer.canRandomize == true)
        {
            Debug.Log("Entered");
            pizzaRandomizer.RandomizePizza();
            pizzaRandomizer.canRandomize = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PizzaOnCanvas.SetActive(false);
        }
    }



}
