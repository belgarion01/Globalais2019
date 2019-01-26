using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
    public void FinishColoc() {
        FindObjectOfType<GameManager>().FinishColoc();
    }

    public void PorteColoc() {
        FindObjectOfType<GameManager>().Coloc();

    }
}
