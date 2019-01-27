using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenetrePipi : MonoBehaviour
{
    private PlayerController pController;
    public Vector2 size;
    public Vector2 offset;

    private Animator anim;
    private bool open = false;
    private bool inRange;

    public GameObject PipiEntrer;
    public GameObject PipiSortir;

    public bool gizmos = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        pController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Physics2D.OverlapBox((Vector2)transform.position + offset, size, 0f, 1 << LayerMask.NameToLayer("Player")) && pController.currAction != PlayerController.Action.isPlaying)
        {
            PipiEntrer.SetActive(true);
            Debug.Log("eee");
            inRange = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                pController.currAction = PlayerController.Action.isPissing;
                open = true;
            }
        }
        else {
            PipiEntrer.SetActive(false);
        }
        if (pController.currAction != PlayerController.Action.isPissing)
        {
            open = false;
            PipiSortir.SetActive(false);
        }
        else {
            PipiSortir.SetActive(true);
        }
        anim.SetBool("Open", open);
    }

    private void OnDrawGizmos()
    {
        if (gizmos)
        {
            Gizmos.DrawCube((Vector2)transform.position + offset, new Vector3(size.x, size.y, 1f));
        }
    }
}
