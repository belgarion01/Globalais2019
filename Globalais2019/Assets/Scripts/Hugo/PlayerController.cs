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

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currAction) {
            case Action.Nothing:
                Move();
                break;
            case Action.isPlaying:
               
                break;
            case Action.isHiding:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currAction = Action.Nothing;
                }
                break;
            case Action.isDrinking:
                gManager.Drinking();
                if (Input.GetKeyDown(KeyCode.A)) {
                    currAction = Action.Nothing;
                }
                break;
            case Action.isEating:
                gManager.Eating();
                if (Input.GetKeyDown(KeyCode.A))
                {
                    currAction = Action.Nothing;
                }
                break;
            case Action.isPhoning:
                break;
            case Action.isPissing:
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
            //moveDir = new Vector2(0, -1);
        }
        else if (vAxis > 0)
        {
            currDirection = Direction.Haut;
            //moveDir = new Vector2(0, 1);
        }
        else if (hAxis < 0)
        {
            currDirection = Direction.Gauche;
            //moveDir = new Vector2(0, -1);
        }
        else if (hAxis > 0)
        {
            currDirection = Direction.Droite;
            //moveDir = new Vector2(0, -1);
        }

        moveDir = new Vector2(hAxis, vAxis).normalized;
        rb2d.MovePosition((Vector2)transform.position + moveDir * speed * Time.deltaTime);
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
                gManager.GetMoney();
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
        lastPosition = transform.position;
        transform.position = bedPosition.position;
        currAction = Action.isHiding;
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
        currAction = Action.Nothing;
    }
}
