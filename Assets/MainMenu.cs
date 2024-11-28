using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Canvas tableMenu;
    [SerializeField] private Canvas overlayMenu;
    [SerializeField] private CinemachineVirtualCamera camera;

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private TextMeshProUGUI playText;
    private TextMeshProUGUI quitText;

    private void Start()
    {
        playText = playButton.GetComponentInChildren<TextMeshProUGUI>();
        quitText = quitButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer enter");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointer exit");
        
        playText.color = Color.white;
        
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
    
    
        
}
