using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

[UIView("HUD", "Assets/UI/Views/HUD/HUD.uxml", typeof(HUDview))]
public class HUDview : UIView, IObserver<PlayerStats>, IObserver<GameConfigData>
{
    public PlayerStatsData playerStatsData_Model = null;
    private IDisposable unsubscriberPlayerData;
    public GameConfig gameConfig_Model = null;
    private IDisposable unsubscriberGameConfig;

    private HealthBarComponent healthBarComponent;

    public string MoneyLableName = "MoneyLabel"; 
    private Label MoneyLable = null;

    public string TimerLableName = "TimerLabel";
    private Label TimerLable = null;


    public void OnCompleted()
    {
        //throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        //throw new NotImplementedException();
    }

    public void OnNext(PlayerStats value)
    {
        Debug.Log("Helth: " + value.Health);
        healthBarComponent.value = value.Health;
        if(value.Health <= 0)
        {
            GameEvents.GameOver.Invoke();
            UIEvents.SceneRemoveEvent.Invoke("GameActors",OnGameLossScreenRemoves);
        }
        MoneyLable.text = GetMoneyString_Ink(value.Money);
        
    }

    private void OnGameLossScreenRemoves()
    {
        UIEvents.UIChangeEvent.Invoke("GameOverView");
    }

    //TODO: Make this acync later
    protected override void OnViewInitialized()
    {
        base.OnViewInitialized();
        // Initalize the model
        playerStatsData_Model = Addressables.LoadAssetAsync<PlayerStatsData>("Assets/Data/PlayerStatsData.asset").WaitForCompletion();
        unsubscriberPlayerData = playerStatsData_Model.Subscribe(this);
        Debug.Log("Helth: " + playerStatsData_Model.stats.Health);

        healthBarComponent = RootVisualElement.Q<HealthBarComponent>();
        healthBarComponent.value = playerStatsData_Model.stats.Health;

        MoneyLable = RootVisualElement.Q<Label>(MoneyLableName);
        MoneyLable.text = GetMoneyString_Ink(playerStatsData_Model.stats.Money);

        gameConfig_Model = Addressables.LoadAssetAsync<GameConfig>("Assets/Data/GameConfig.asset").WaitForCompletion();
        unsubscriberGameConfig = gameConfig_Model.Subscribe(this);

        TimerLable = RootVisualElement.Q<Label>(TimerLableName);
        TimerLable.text = SetTimerLable(gameConfig_Model.configData.GameTimer);

    }

    private string SetTimerLable(float gameTimer)
    {
        string timerString = "";
        int minutes = Mathf.FloorToInt(gameTimer / 60F);
        int seconds = Mathf.FloorToInt(gameTimer - minutes * 60);
        timerString = string.Format("{0:0}:{1:00}", minutes, seconds);
        return timerString;
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

    public void OnNext(GameConfigData value)
    {
        Debug.Log("GameTimer: " + value.GameTimer);
        TimerLable.text = SetTimerLable(value.GameTimer); 
        if(value.GameTimer <= 0)
        {
            GameEvents.GameOver.Invoke();
        }
    }

    private void OnDestroy()
    {
        unsubscriberGameConfig.Dispose();
        unsubscriberPlayerData.Dispose();
    }
}
