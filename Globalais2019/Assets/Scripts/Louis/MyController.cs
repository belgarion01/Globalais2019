using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyController : MonoBehaviour
{
    //SerializeField
    [SerializeField]
    float moveX = 0f;
    [SerializeField]
    float moveY = 0f;
    [SerializeField]
    int playerVelocity = 5;


    //Private
    Rigidbody2D myRigidbody;
    Transform myCharacter;



    // Start
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCharacter = GetComponent<Transform>();
    }

    // Update
    void Update()
    {
        //Déplacement
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        myRigidbody.velocity = new Vector2(playerVelocity * moveX, playerVelocity * moveY);
    }


}
