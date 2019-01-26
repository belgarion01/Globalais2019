using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float moneyCount;
    public float moneyGain;

    private float soifLevel = 1f;
    public float soifDiminution;
    public float soifGain;

    private float mangerLevel = 1f;
    public float mangerDiminution;
    public float mangerGain;

    private float pipiLevel = 0f;
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
    private bool ColocBAM = false;
    public Light coloclight;
    public Animator PorteColoc;
    public Animator Lit;

    private PlayerController pController;

    public enum TypeGameOver { Faim, Boire, Pipi, MamanFausseReponse, ManonFausseReponse, Coloc, MamanTimer, ManonTimer };
    public bool gameover = false;
    public TextMeshProUGUI GameOverTexte;
    public GameObject GameOverPanel;

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
        if (!gameover)
        {
            Lit.SetBool("Actif", pController.hided);
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Phoning());
            }
            if (currPhoneTimer >= 0 && pController.currAction != PlayerController.Action.isPhoning && !isColocing)
            {
                currPhoneTimer -= Time.deltaTime;
            }
            else if (pController.currAction != PlayerController.Action.isPhoning && currPhoneTimer < 0)
            {
                //pController.currAction = PlayerController.Action.isPhoning;
                ResetPhoneTimer();
                StartCoroutine(Phoning());
            }

            if (currColocTimer >= 0 && pController.currAction != PlayerController.Action.isPhoning && !isColocing)
            {
                currColocTimer -= Time.deltaTime;
            }
            else if (currColocTimer < 0)
            {
                Debug.Log("ColocTIME");
                StartCoroutine(ColocTime());
            }

            if (!pController.hided && ColocBAM)
            {
                GameOver(TypeGameOver.Coloc);
            }

            if (mangerLevel <= 0) {
                GameOver(TypeGameOver.Faim);
            }
            if (soifLevel <= 0)
            {
                GameOver(TypeGameOver.Boire);
            }
            if (pipiLevel >= 1)
            {
                GameOver(TypeGameOver.Boire);
            }
        }
    }

    IEnumerator Phoning() {
        pController.currAction = PlayerController.Action.isPhoning;
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
                if ((buttonPushedRight == false && lieuPropose != lieuDitMaman) || (buttonPushedRight == true && lieuPropose == lieuDitMaman) && phase1)
                {
                GameOver(TypeGameOver.MamanFausseReponse);
                    Debug.Log(buttonPushedRight);
                    Debug.Log(lieuDitMaman);
                    Debug.Log(lieuPropose);
                }
            }
            else
            {
                if ((buttonPushedRight == false && lieuPropose != lieuDitManon) || (buttonPushedRight == true && lieuPropose == lieuDitManon) && phase1)
                {
                GameOver(TypeGameOver.ManonFausseReponse);
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
    }


    public void R1() {
        if (phase1)
        {
            buttonPushedRight = false;
            Debug.Log(buttonPushedRight);
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

        }
    }

    public void R2() {
        if (phase1)
        {
            Debug.Log("Bouton de droite");
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
        while (coloclight.intensity < 2) {
            coloclight.intensity += 0.002f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        PorteColoc.SetTrigger("Coloc");
    }

    public void Coloc() {
        ColocBAM = true;
    }

    public void FinishColoc() {
        coloclight.intensity = 0f;
        ColocBAM = false;
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

    public void GameOver(TypeGameOver typo) {
        gameover = true;
        switch (typo) {
            case TypeGameOver.Boire:
                GameOverTexte.text = "Mort de soif";
                break;
            case TypeGameOver.Coloc:
                GameOverTexte.text = "Tes colocs t'ont emmené te bourrer la gueule.";
                break;
            case TypeGameOver.Faim:
                GameOverTexte.text = "Mort de faim";
                break;
            case TypeGameOver.MamanFausseReponse:
                GameOverTexte.text = "Mort de maman";
                break;
            case TypeGameOver.MamanTimer:
                GameOverTexte.text = "Mort de maman";
                break;
            case TypeGameOver.ManonFausseReponse:
                GameOverTexte.text = "Mort de manon";
                break;
            case TypeGameOver.ManonTimer:
                GameOverTexte.text = "Mort de manon";
                break;
            case TypeGameOver.Pipi:
                GameOverTexte.text = "Mort de pipi";
                break;
        }
        GameOverPanel.SetActive(true);
    }

    public void Retry() {
        
    }

    public void GotToMenu() {

    }
}
