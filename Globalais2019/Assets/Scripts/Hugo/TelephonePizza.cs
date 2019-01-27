using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephonePizza : MonoBehaviour
{
    private PizzaManager pManager;
    private GameManager gManager;

    public GameObject TelephonePanel;

    public int pizzaPrice = 15;

    public Vector2 size;
    public Vector2 offset;

    public bool gizmos = false;

    void Start()
    {
        pManager = FindObjectOfType<PizzaManager>();
        gManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Physics2D.OverlapBox((Vector2)transform.position + offset, size, 0f, 1 << LayerMask.NameToLayer("Player")))
        {
            TelephonePanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (gManager.moneyCount >= pizzaPrice)
                {
                    pManager.GeneratePizzas();
                    gManager.moneyCount -= pizzaPrice;
                }
            }
        }
        else {
            TelephonePanel.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (gizmos)
        {
            Gizmos.DrawCube((Vector2)transform.position + offset, new Vector3(size.x, size.y, 1f));
        }
    }
}
