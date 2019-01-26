using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coca : MonoBehaviour
{
    private PlayerController pController;

    public float radius;

    public bool gizmos = false;

    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, radius, 1 << LayerMask.NameToLayer("Player")) && pController.currAction != PlayerController.Action.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pController.currAction = PlayerController.Action.isDrinking;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (gizmos)
        {
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
