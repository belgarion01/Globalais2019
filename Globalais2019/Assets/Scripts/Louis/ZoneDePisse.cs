using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDePisse : MonoBehaviour
{

    public GameObject myPlayer;
    NeedsManager myNeeds;

    void Start()
    {
        myNeeds = myPlayer.GetComponent<NeedsManager>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Ca marche");
            myNeeds.InvokeRepeating("PeeEmptier", 0.1f, 0.1f);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            myNeeds.CancelInvoke("PeeEmptier");
        }

    }
}
