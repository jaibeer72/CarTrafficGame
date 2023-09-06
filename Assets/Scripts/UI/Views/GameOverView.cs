using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[UIView("GameOverView", "Assets/UI/Views/GameOver/GameOver.uxml", typeof(GameOverView))]
public class GameOverView : UIView
{
    public string backToMainMenuButtonName = "MainMenuButton";
    private Button backToMainMenuButton;

    public string restartButtonName = "RestartButton";
    private Button restartButton;
    protected override void OnViewInitialized()
    {
        base.OnViewInitialized();
        
        backToMainMenuButton = RootVisualElement.Q<Button>(backToMainMenuButtonName);
        backToMainMenuButton.clicked += BackToMainMenuButtonClicked;

        restartButton = RootVisualElement.Q<Button>(restartButtonName);
        restartButton.clicked += RestartButtonClicked;
    }

    private void RestartButtonClicked()
    {
        UIEvents.SceneAddEvent.Invoke("GameActors", OnSceneLoaded);
    }

    private void BackToMainMenuButtonClicked()
    {
        UIEvents.UIChangeEvent.Invoke("MainMenu");
    }

    private void OnSceneLoaded()
    {
        GameEvents.GameStart.Invoke();
        UIEvents.UIChangeEvent.Invoke("HUD");
    }
}
