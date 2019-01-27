using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChaiseJouer : MonoBehaviour
{
    private PlayerController pController;
    public GameObject chair;
    public GameObject table;

    public GameObject JouerPanelEntrer;
    public GameObject JouerPanelSortir;

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
            JouerPanelEntrer.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {               
                chair.GetComponent<BoxCollider2D>().isTrigger = true;
                table.GetComponent<BoxCollider2D>().isTrigger = true;
                pController.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Meuble";
                pController.FPlaying();
            }
        }
        if (pController.currAction != PlayerController.Action.isPlaying)
        {
            pController.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            chair.GetComponent<BoxCollider2D>().isTrigger = false;
            table.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else {
            JouerPanelEntrer.SetActive(false);
        }
        if (pController.currAction == PlayerController.Action.isPlaying)
        {
            JouerPanelSortir.SetActive(true);
        }
        else {
            JouerPanelSortir.SetActive(false);
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
