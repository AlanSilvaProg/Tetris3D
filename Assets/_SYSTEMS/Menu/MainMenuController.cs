using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveSystem;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject rankScreen;
    public GameObject enterScreen;
    public TextMeshProUGUI inputNameText;

    public void PlayClick()
    {
        PlayerStats.InitializeNewPlayer(inputNameText.text);
        SceneManager.LoadScene("Game");
    }

    public void EnterNameClick()
    {
        mainScreen.SetActive(false);
        enterScreen.SetActive(true);
        rankScreen.SetActive(false);
    }

    public void RankingClick()
    {
        mainScreen.SetActive(false);
        enterScreen.SetActive(false);
        rankScreen.SetActive(true);
    }

    public void BackClick()
    {
        mainScreen.SetActive(true);
        enterScreen.SetActive(false);
        rankScreen.SetActive(false);
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
