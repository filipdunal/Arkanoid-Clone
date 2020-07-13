using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button newGameButton;
    public Button continueGameButton;

    CanvasGroup canvasGroup;
    public Progress progress;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void SwitchPauseMenu(bool condition)
    {
        canvasGroup.alpha = condition ? 1f : 0f;
        canvasGroup.interactable = condition;
        canvasGroup.blocksRaycasts = condition;

        Time.timeScale = condition? 0f:1f;
        
    }

    public void AllowContinue(bool condition)
    {
        continueGameButton.interactable = condition;
    }
}
