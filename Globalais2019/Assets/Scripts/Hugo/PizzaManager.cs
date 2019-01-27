using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaManager : MonoBehaviour
{
    public Image[] PizzasImage;
    public SinglePizza[] Pizzas;

    public List<int> placeAnanas;

    void Start()
    {
        ResetPizzas();
        RollingAnanas();
        PlacingAnanas();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            GeneratePizzas();
        }
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10f;
        mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("UI"));
        if (hit)
        {
            if (hit.collider.CompareTag("Pizza") && Input.GetMouseButtonDown(0))
            {
                EatPizza(hit.collider.gameObject);
            }
        }
    }

    public void GeneratePizzas() {
        ResetPizzas();
        RollingAnanas();
        PlacingAnanas();
    }

    public void ResetPizzas() {
        foreach (SinglePizza piz in Pizzas) {
            piz.ananas = false;
        }
        foreach (Image pizM in PizzasImage) {
            pizM.sprite = pizM.GetComponent<SinglePizza>().Normal;
            pizM.enabled = true;
            pizM.GetComponent<PolygonCollider2D>().enabled = true;
        }
        placeAnanas.Clear();
    }

    void RollingAnanas() {
        int rand = Random.Range(2, 5);
        int randAnanas = 0;
        for (int i = 0; i <= rand; i++) {
            randAnanas = Random.Range(0, 8);
            while (placeAnanas.Contains(randAnanas)) {
                randAnanas = Random.Range(0, 8);
            }
            placeAnanas.Add(randAnanas);
        }
    }

    void PlacingAnanas() {
        for (int i = 0; i <= placeAnanas.Count-1; i++) {
            PizzasImage[placeAnanas[i]].sprite = PizzasImage[placeAnanas[i]].GetComponent<SinglePizza>().Ananas;
            PizzasImage[placeAnanas[i]].GetComponent<SinglePizza>().ananas = true;
        }
    }

    void EatPizza(GameObject pizza) {
        pizza.GetComponent<PolygonCollider2D>().enabled = false;
        pizza.GetComponent<Image>().enabled = false;
        if (pizza.GetComponent<SinglePizza>().ananas)
        {
            FindObjectOfType<GameManager>().EatingAnanas();
        }
        else {
            FindObjectOfType<GameManager>().EatingPizza();
        }
    }
}
