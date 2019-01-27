using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private PlayerController pController;

    public Vector2 size;
    public Vector2 offset;

    public GameObject HidPanelEntrer;
    public GameObject HidPanelSortir;

    public bool gizmos = false;

    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Physics2D.OverlapBox((Vector2)transform.position + offset, size, 0f, 1 << LayerMask.NameToLayer("Player")) && pController.currAction != PlayerController.Action.isPlaying)
        {
            if (pController.currAction != PlayerController.Action.isHiding)
            {
                HidPanelEntrer.SetActive(true);
            }
            else {
                HidPanelEntrer.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().PlaySound("Ronflement");
                GetComponent<BoxCollider2D>().isTrigger = true;
                pController.FHiding();
            }
        }
        else {
            HidPanelEntrer.SetActive(false);
        }
        if (pController.currAction != PlayerController.Action.isHiding) {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (pController.currAction == PlayerController.Action.isHiding) {
            HidPanelSortir.SetActive(true);
        }
        else{
            HidPanelSortir.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (gizmos)
        {
            Gizmos.DrawCube((Vector2)transform.position+offset, new Vector3(size.x, size.y, 1f));
        }
    }
}
