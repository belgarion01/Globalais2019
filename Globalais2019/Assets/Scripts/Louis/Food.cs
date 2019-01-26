using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
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
            myPlayerNeeds.foodNeed = myPlayerNeeds.foodNeed + 0.2f;
        }
    }
}
