using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public GameObject myPlayer;
    NeedsManager myPlayerNeeds;

    // Start
    void Start()
    {
        myPlayerNeeds = myPlayer.GetComponent<NeedsManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            myPlayerNeeds.waterNeed = myPlayerNeeds.waterNeed + 0.2f;
            myPlayerNeeds.peeNeed = myPlayerNeeds.peeNeed + 0.1f;
        }
    }
}
