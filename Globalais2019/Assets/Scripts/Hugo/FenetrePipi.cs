using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenetrePipi : MonoBehaviour
{
    private PlayerController pController;
    public Vector2 size;

    public bool gizmos = false;

    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Physics2D.OverlapBox(transform.position, size, 0f, 1 << LayerMask.NameToLayer("Player")) && pController.currAction != PlayerController.Action.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pController.currAction = PlayerController.Action.isPissing;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (gizmos)
        {
            Gizmos.DrawCube(transform.position, new Vector3(size.x, size.y, 1f));
        }
    }
}
