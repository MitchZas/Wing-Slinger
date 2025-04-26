using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private string _firstSceneToLoadOnStart; //scene must be added to build setting to load at runtime

    public void StartGame()
    {
        SceneManager.LoadScene(_firstSceneToLoadOnStart);
    }
    
    public void ShowCredits()
    {
        _mainPanel.SetActive(false);
        _creditsPanel.SetActive(true);
    }
    
    public void HideCredits()
    {
        _mainPanel.SetActive(true);
        _creditsPanel.SetActive(false);
    }
}
