using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIZZA : MonoBehaviour
{
    private PlayerController pController;

    public Vector2 size;
    public Vector2 offset;


    public GameObject PizzaPanelEntrer;
    public GameObject PizzaPanelSortir;

    public bool gizmos = false;

    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Physics2D.OverlapBox((Vector2)transform.position + offset, size, 0f, 1 << LayerMask.NameToLayer("Player")) && pController.currAction != PlayerController.Action.isEating)
        {
            PizzaPanelEntrer.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && pController.currAction != PlayerController.Action.isPhoning)
            {
                pController.currAction = PlayerController.Action.isEating;
            }
        }
        else {
            PizzaPanelEntrer.SetActive(false);
        }

        if (pController.currAction == PlayerController.Action.isEating)
        {
            PizzaPanelSortir.SetActive(true);
        }
        else {
            PizzaPanelSortir.SetActive(false);
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
