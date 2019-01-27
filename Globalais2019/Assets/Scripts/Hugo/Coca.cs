using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coca : MonoBehaviour
{
    private PlayerController pController;

    public float radius;
    public Vector2 size;

    public GameObject BoirePanelEntrer;
    public GameObject BoirePanelSortir;

    public Vector2 offset;

    public bool gizmos = false;

    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Physics2D.OverlapBox((Vector2)transform.position + offset, size, 0f, 1 << LayerMask.NameToLayer("Player")) && pController.currAction != PlayerController.Action.isPlaying)
        {
            BoirePanelEntrer.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().PlaySound("Gloup");
                pController.currAction = PlayerController.Action.isDrinking;
            }
        }
        else {
            BoirePanelEntrer.SetActive(false);
        }
        if (pController.currAction == PlayerController.Action.isDrinking)
        {
            BoirePanelSortir.SetActive(true);
        }
        else {
            BoirePanelSortir.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (gizmos)
        {
            Gizmos.DrawCube((Vector2)transform.position + offset, size);
        }
    }
}
