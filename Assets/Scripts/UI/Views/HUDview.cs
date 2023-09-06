using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

[UIView("HUD", "Assets/UI/Views/HUD/HUD.uxml", typeof(HUDview))]
public class HUDview : UIView, IObserver<PlayerStats>
{
    public PlayerStatsData playerStatsData_Model = null;
    private HealthBarComponent healthBarComponent;


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
    }

    // Start is called before the first frame update
    void Awake()
    {
        playerStatsData_Model = Addressables.LoadAssetAsync<PlayerStatsData>("Assets/Data/PlayerStatsData.asset").WaitForCompletion();
        playerStatsData_Model.Subscribe(this);
        Debug.Log("Helth: " + playerStatsData_Model.stats.Health);

    }

    protected override void OnViewInitialized()
    {
        base.OnViewInitialized();
        healthBarComponent = RootVisualElement.Q<HealthBarComponent>();
        healthBarComponent.value = playerStatsData_Model.stats.Health;
    }
}
