using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.AddressableAssets;

[UIView("GameOverView", "Assets/UI/Views/GameOver/GameOver.uxml", typeof(GameOverView))]
public class GameOverView : UIView
{
    public string backToMainMenuButtonName = "MainMenuButton";
    private Button backToMainMenuButton;

    public string restartButtonName = "RestartButton";
    private Button restartButton;

    public string scoreLabelName = "ScoreLabel";
    public Label scoreLabel;

    public PlayerStatsData playerStatsData_Model = null;
    private IDisposable unsubscriberPlayerData;

    protected override void OnViewInitialized()
    {
        base.OnViewInitialized();
        
        backToMainMenuButton = RootVisualElement.Q<Button>(backToMainMenuButtonName);
        backToMainMenuButton.clicked += BackToMainMenuButtonClicked;

        restartButton = RootVisualElement.Q<Button>(restartButtonName);
        restartButton.clicked += RestartButtonClicked;

        playerStatsData_Model = Addressables.LoadAssetAsync<PlayerStatsData>("Assets/Data/PlayerStatsData.asset").WaitForCompletion();

        scoreLabel = RootVisualElement.Q<Label>(scoreLabelName);
        scoreLabel.text = "Score : " + GetMoneyString_Ink(playerStatsData_Model.stats.Money);
    }

    private string GetMoneyString_Ink(int money)
    {
        // money in string so if its 1000 it will be 1k
        if (money >= 1000)
        {
            return (money / 1000).ToString() + "k";
        }
        return money.ToString();
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
