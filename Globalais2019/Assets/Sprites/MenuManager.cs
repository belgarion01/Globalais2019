using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void Credit() {
        SceneManager.LoadScene(2);
    }

    public void Quit() {
        Application.Quit();
    }

    public void Menu() {
        SceneManager.LoadScene(0);
    }
}
