using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChaiseJouer : MonoBehaviour
{
    private PlayerController pController;
    public GameObject chair;
    public GameObject table;

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
                
                chair.GetComponent<BoxCollider2D>().isTrigger = true;
                table.GetComponent<BoxCollider2D>().isTrigger = true;
                pController.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Meuble";
                pController.FPlaying();
            }
        }
        if (pController.currAction != PlayerController.Action.isPlaying) {
            pController.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            chair.GetComponent<BoxCollider2D>().isTrigger = false;
            table.GetComponent<BoxCollider2D>().isTrigger = false;
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
