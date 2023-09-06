using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[UIView("MainMenu", "Assets/UI/Views/MainMenu/MainMenu.uxml", typeof(MainMenuview))]
public class MainMenuview : UIView
{
    public string PlayButtonName = "PlayButton";
    private Button playButton;
    public string QuitButtonName = "QuitButton";
    private Button quitButton;
    public string SettingsButtonName = "SettingsButton";
    private Button settingsButton;
    override protected void OnViewInitialized()
    {
        base.OnViewInitialized();

        playButton = RootVisualElement.Q<Button>(PlayButtonName);
        playButton.clicked += OnPlayButtonClicked;

        quitButton = RootVisualElement.Q<Button>(QuitButtonName);
        quitButton.clicked += () => Application.Quit();

        settingsButton = RootVisualElement.Q<Button>(SettingsButtonName);
        settingsButton.clicked += () => UIEvents.UIChangeEvent.Invoke("Settings");

    }

    private void OnPlayButtonClicked()
    {
        UIEvents.SceneAddEvent.Invoke("GameActors" , OnSceneLoaded);
    }

    private void OnSceneLoaded()
    {
        GameEvents.GameStart.Invoke();
        UIEvents.UIChangeEvent.Invoke("HUD");
    }
}
