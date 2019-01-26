using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChaiseJouer : MonoBehaviour
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
        if (Physics2D.OverlapCircle(transform.position, radius, 1<<LayerMask.NameToLayer("Player")) && pController.currAction != PlayerController.Action.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                /*pController.lastPosition = pController.gameObject.transform.position;
                pController.currAction = PlayerController.Action.isPlaying;*/
                pController.FPlaying();
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
