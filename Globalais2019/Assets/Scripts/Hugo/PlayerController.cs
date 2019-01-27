using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private GameManager gManager;

    public float speed;

    private enum Direction { Haut, Droite, Bas, Gauche };
    private Direction currDirection;

    public enum Action { Nothing, isPlaying, isHiding, isDrinking, isEating, isPhoning, isPissing };
    public Action currAction = Action.Nothing;

    public bool hided = false;

    public Vector3 lastPosition;
    public Transform chairPosition;
    public Transform bedPosition;

    public GameObject eatingPanel;

    private bool TAP = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currAction != Action.isPlaying) {
            GetComponent<SpriteRenderer>().sortingOrder = SortingLayer.GetLayerValueFromName("Player");
        }
        switch (currAction) {
            case Action.Nothing:
                Move();
                anim.SetInteger("Action", 0);
                break;
            case Action.isPlaying:
                anim.SetInteger("Action", 1);
                anim.SetBool("TAP", TAP);
                break;
            case Action.isHiding:
                anim.SetInteger("Action", 2);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currAction = Action.Nothing;
                }
                break;
            case Action.isDrinking:
                anim.SetInteger("Action", 3);
                gManager.Drinking();
                if (Input.GetKeyDown(KeyCode.A)) {
                    currAction = Action.Nothing;
                }
                break;
            case Action.isEating:
                anim.SetInteger("Action", 4);
                //gManager.Eating();
                eatingPanel.SetActive(true);
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currAction = Action.Nothing;
                    eatingPanel.SetActive(false);
                }

                break;
            case Action.isPhoning:
                break;
            case Action.isPissing:
                anim.SetInteger("Action", 6);
                gManager.Pissing();
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currAction = Action.Nothing;
                }
                break;
        }
        hided = currAction == Action.isHiding ? true : false;
    }

    void Move() {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector2 moveDir;
        if (vAxis < 0)
        {
            currDirection = Direction.Bas;
            anim.SetInteger("Direction", 2);
            //moveDir = new Vector2(0, -1);
        }
        else if (vAxis > 0)
        {
            currDirection = Direction.Haut;
            anim.SetInteger("Direction", 0);
            //moveDir = new Vector2(0, 1);
        }
        else if (hAxis < 0)
        {
            currDirection = Direction.Gauche;
            anim.SetInteger("Direction", 3);
            //moveDir = new Vector2(0, -1);
        }
        else if (hAxis > 0)
        {
            currDirection = Direction.Droite;
            anim.SetInteger("Direction", 1);
            //moveDir = new Vector2(0, -1);
        }

        moveDir = new Vector2(hAxis, vAxis).normalized;
        rb2d.MovePosition((Vector2)transform.position + moveDir * speed * Time.deltaTime);
        if (moveDir.magnitude > 0)
        {
            anim.SetBool("isMoving", true);
        }
        else {
            anim.SetBool("isMoving", false);
        }
    }

    public void FPlaying() {
        StartCoroutine(Playing());
    }
    IEnumerator Playing() {
        lastPosition = transform.position;
        transform.position = chairPosition.position;
        currAction = Action.isPlaying;
        bool active = true;
        while (active) {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Argent");
                gManager.GetMoney();
                TAP = !TAP;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                active = false;
            }
            yield return null;
        }
        transform.position = lastPosition;
        currAction = Action.Nothing;
    }

    public void FHiding() {
        StartCoroutine(Hiding());
    }
    IEnumerator Hiding()
    {
        currAction = Action.isHiding;
        GetComponent<SpriteRenderer>().enabled = false;
        lastPosition = transform.position;
        transform.position = bedPosition.position;
        bool active = true;
        while (active)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                active = false;
            }
            yield return null;
        }
        transform.position = lastPosition;
        GetComponent<SpriteRenderer>().enabled = true;
        currAction = Action.Nothing;
    }
}
