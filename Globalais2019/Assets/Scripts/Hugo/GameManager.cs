using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float moneyCount;
    public float moneyGain;

    private float soifLevel;
    public float soifDiminution;
    public float soifGain;

    private float mangerLevel;
    public float mangerDiminution;
    public float mangerGain;

    private float pipiLevel;
    public float pipiDiminution;
    public float pipiGain;

    public TextMeshProUGUI Qui;
    public TextMeshProUGUI Quoi;
    public TextMeshProUGUI Reponse1;
    public TextMeshProUGUI Reponse2;
    public GameObject panel;

    public TextMeshProUGUI Qui2;
    public TextMeshProUGUI Quoi2;
    public TextMeshProUGUI Reponse3;
    public TextMeshProUGUI Reponse4;
    public GameObject panel2;

    private enum Lieu { Supermarche, Resto, Footing };
    private Lieu lieuPropose;
    private Lieu lieuPropose2;
    private Lieu lieuDitMaman;
    private Lieu lieuDitManon;
    private bool maman;
    private string Ou;
    private bool buttonPushed;
    private bool buttonPushedRight;
    private bool phase1 = true;
    //private bool flagLieu = false;
    private bool flagPhoneDebut = false;
    private int randQuoi;

    public float phoneTimer;
    private float currPhoneTimer;

    public float colocTimer;
    private float currColocTimer;
    private bool isColocing = false;

    private PlayerController pController;
    private bool encoreUnAutreFlag = true;

    void Start()
    {
        pController = FindObjectOfType<PlayerController>();
        currPhoneTimer = phoneTimer;
        currColocTimer = colocTimer;
        WhereWereYou();
        //StartCoroutine(Phoning());  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Phoning());
        }
        if (currPhoneTimer >= 0&&pController.currAction!=PlayerController.Action.isPhoning&&!isColocing)
        {
            currPhoneTimer -= Time.deltaTime;
        }
        else if (pController.currAction != PlayerController.Action.isPhoning&&currPhoneTimer<0)
        {
            //pController.currAction = PlayerController.Action.isPhoning;
            ResetPhoneTimer();
            StartCoroutine(Phoning());
        }

        if (currColocTimer >= 0 && pController.currAction != PlayerController.Action.isPhoning && !isColocing)
        {
            currColocTimer -= Time.deltaTime;
        }
        else if (currColocTimer < 0) {
            StartCoroutine(ColocTime());
        }
    }

    IEnumerator Phoning() {
        pController.currAction = PlayerController.Action.isPhoning;
        //if (flagPhoneDebut)
        //{
            panel.SetActive(true);
            int randQui = Random.Range(1, 3);
            switch (randQui)
            {
                case 1:
                    Qui.text = "Maman";
                    maman = true;
                    break;
                case 2:
                    Qui.text = "Manon";
                    maman = false;
                    break;
            }
            randQuoi = Random.Range(1, 4);
            switch (randQuoi)
            {
                case 1:
                    Ou = "Supermarché";
                    lieuPropose = Lieu.Supermarche;
                    break;
                case 2:
                    Ou = "Restaurant";
                    lieuPropose = Lieu.Resto;
                    break;
                case 3:
                    Ou = "Footing";
                    lieuPropose = Lieu.Footing;
                    break;
            }
            Quoi.text = "C'était bien le " + Ou + " ?";
            Reponse1.text = "Oui, c'était super !";
            Reponse2.text = "Quel " + Ou + " ?";
            while (!buttonPushed)
            {
                Debug.Log("Attente d'input");
                yield return null;
            }
            if (maman)
            {
                if ((buttonPushedRight == false && lieuPropose != lieuDitMaman) || (buttonPushedRight = true && lieuPropose == lieuDitMaman) && phase1)
                {
                    Debug.Log("GameOver");
                    Debug.Log(buttonPushedRight);
                    Debug.Log(lieuDitMaman);
                    Debug.Log(lieuPropose);
                }
            }
            else
            {
                if ((buttonPushedRight == false && lieuPropose != lieuDitManon) || (buttonPushedRight = true && lieuPropose == lieuDitManon) && phase1)
                {
                    Debug.Log("GameOver");
                    Debug.Log(buttonPushedRight);
                    Debug.Log(lieuDitManon);
                    Debug.Log(lieuPropose);
                }
            }
            phase1 = false;
            Quoi.text = "Vient me voir !";
            randQuoi = Random.Range(1, 4);
            int check = randQuoi;
            switch (randQuoi)
            {
                case 1:
                    Ou = "Supermarché";
                    lieuPropose = Lieu.Supermarche;
                    break;
                case 2:
                    Ou = "Restaurant";
                    lieuPropose = Lieu.Resto;
                    break;
                case 3:
                    Ou = "Footing";
                    lieuPropose = Lieu.Footing;
                    break;
            }
            Reponse1.text = "Désolé, je suis occupé au " + Ou;
            randQuoi = Random.Range(1, 4);
            while (randQuoi == check)
            {
                randQuoi = Random.Range(1, 4);
            }
            switch (randQuoi)
            {
                case 1:
                    Ou = "Supermarché";
                    lieuPropose2 = Lieu.Supermarche;
                    break;
                case 2:
                    Ou = "Restaurant";
                    lieuPropose2 = Lieu.Resto;
                    break;
                case 3:
                    Ou = "Footing";
                    lieuPropose2 = Lieu.Footing;
                    break;
            }
            Reponse2.text = "Désolé, je suis occupé au " + Ou;
            yield return null;
       // }
        /*else {
            Debug.Log("Maman");
            maman = true;
            Qui.text = "Maman";
            panel.SetActive(true);
            phase1 = false;
            Quoi.text = "Vient me voir !";
            randQuoi = Random.Range(1, 4);
            int check = randQuoi;
            switch (randQuoi)
            {
                case 1:
                    Ou = "Supermarché";
                    lieuPropose = Lieu.Supermarche;
                    break;
                case 2:
                    Ou = "Restaurant";
                    lieuPropose = Lieu.Resto;
                    break;
                case 3:
                    Ou = "Footing";
                    lieuPropose = Lieu.Footing;
                    break;
            }
            Reponse1.text = "Désolé, je suis occupé au " + Ou;
            randQuoi = Random.Range(1, 4);
            while (randQuoi == check)
            {
                randQuoi = Random.Range(1, 4);
            }
            switch (randQuoi)
            {
                case 1:
                    Ou = "Supermarché";
                    lieuPropose2 = Lieu.Supermarche;
                    break;
                case 2:
                    Ou = "Restaurant";
                    lieuPropose2 = Lieu.Resto;
                    break;
                case 3:
                    Ou = "Footing";
                    lieuPropose2 = Lieu.Footing;
                    break;
            }
            Reponse2.text = "Désolé, je suis occupé au " + Ou;
            flagPhoneDebut = true;
            //yield return null;
        }*/

    }


    public void R1() {
        if (phase1)
        {
            buttonPushedRight = false;
            buttonPushed = true;           
        }
        else {
            if (maman)
            {
                lieuDitMaman = lieuPropose;
            }
            else
            {
                lieuDitManon = lieuPropose;
            }

            panel.SetActive(false);
            Debug.Log("eeeh");
            phase1 = true;
            buttonPushed = false;
            pController.currAction = PlayerController.Action.Nothing;
            encoreUnAutreFlag = false;

        }
    }

    public void R2() {
        if (phase1)
        {
            buttonPushedRight = true;
            buttonPushed = true;         
        }
        else {
            if (maman)
            {
                lieuDitMaman = lieuPropose2;
            }
            else {
                lieuDitManon = lieuPropose2;
            }
            panel.SetActive(false);
            phase1 = true;
            buttonPushed = false;
            pController.currAction = PlayerController.Action.Nothing;
            encoreUnAutreFlag = false;
        }
    }

    public void R3() {
        lieuDitMaman = lieuPropose;
        lieuDitManon = lieuPropose;
        panel2.SetActive(false);
        pController.currAction = PlayerController.Action.Nothing;
    }

    public void R4() {
        lieuDitMaman = lieuPropose2;
        lieuDitManon = lieuPropose2;
        panel2.SetActive(false);
        pController.currAction = PlayerController.Action.Nothing;
    }

    IEnumerator ColocTime() {
        isColocing = true;
        yield return new WaitForSeconds(2f);
        isColocing = false;
        ResetColocTimer();
    }

    public void GetMoney() {
        moneyCount += moneyGain;
    }

    public void Drinking() {
        soifLevel += soifGain;
    }

    public void Pissing() {
        pipiLevel -= pipiDiminution;
    }

    public void Eating() {
        mangerLevel += mangerGain;
    }

    void ResetPhoneTimer() {
        currPhoneTimer = Random.Range(7f, 10f);
    }

    void ResetColocTimer() {
        currColocTimer = Random.Range(6f, 10f);
    }

    void WhereWereYou() {
        panel2.SetActive(true);
        pController.currAction = PlayerController.Action.isPhoning;
        Qui2.text = "Toi";
        Quoi2.text = "Où étais-tu censé allé aujourd'hui ?";
        randQuoi = Random.Range(1, 4);
        int check = randQuoi;
        switch (randQuoi)
        {
            case 1:
                Ou = "Supermarché";
                lieuPropose = Lieu.Supermarche;
                break;
            case 2:
                Ou = "Restaurant";
                lieuPropose = Lieu.Resto;
                break;
            case 3:
                Ou = "Footing";
                lieuPropose = Lieu.Footing;
                break;
        }
        Reponse3.text = "J'ai dit que j'étais au " + Ou;
        randQuoi = Random.Range(1, 4);
        while (randQuoi == check)
        {
            randQuoi = Random.Range(1, 4);
        }
        switch (randQuoi)
        {
            case 1:
                Ou = "Supermarché";
                lieuPropose2 = Lieu.Supermarche;
                break;
            case 2:
                Ou = "Restaurant";
                lieuPropose2 = Lieu.Resto;
                break;
            case 3:
                Ou = "Footing";
                lieuPropose2 = Lieu.Footing;
                break;
        }
        Reponse4.text = "J'ai dit que j'étais au " + Ou;
    }
}
