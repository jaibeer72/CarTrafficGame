using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

[UIView("Settings", "Assets/UI/Views/Settings/Settings.uxml",typeof(SettingsView))]
public class SettingsView : UIView
{
    public string ScrollViewName = "ScrollView";
    private ScrollView scrollView;

    public string OptionsContainerVisualTreeAssetLocationn = "Assets/UI/Components/OptionsContainer.uxml";
    public VisualTreeAsset optionsContainer = null;

    private GameConfig gameConfigData = null;

    public string MainMenuButtonName = "MainMenu"; 

    protected override void OnViewInitialized()
    {
        base.OnViewInitialized();
        scrollView = RootVisualElement.Q<ScrollView>(ScrollViewName);
        scrollView.Clear();

        optionsContainer = Addressables.LoadAssetAsync<VisualTreeAsset>(OptionsContainerVisualTreeAssetLocationn).WaitForCompletion();
        gameConfigData = Addressables.LoadAssetAsync<GameConfig>("Assets/Data/GameConfig.asset").WaitForCompletion();

        var gameTimerSliderContainer = optionsContainer.CloneTree();
        SliderInt gameTimerSlider = gameTimerSliderContainer.Q<SliderInt>();
        gameTimerSlider.value = (int)gameConfigData.configData.GameTimer;
        gameTimerSlider.lowValue = 60; 
        gameTimerSlider.highValue = 300;
        gameTimerSlider.RegisterValueChangedCallback((evt) => OnGameTimerSliderValueChanged(evt));
        Label gameTimerLable = gameTimerSliderContainer.Q<Label>();
        gameTimerLable.text = "Game Timer";

        scrollView.Add(gameTimerSliderContainer);   

        var MaxEnamiesOnBoardSliderContainer = optionsContainer.CloneTree();
        SliderInt MaxEnamiesOnBoardSlider = MaxEnamiesOnBoardSliderContainer.Q<SliderInt>();
        MaxEnamiesOnBoardSlider.value = gameConfigData.configData.MaxEnemiesOnBoard;
        MaxEnamiesOnBoardSlider.lowValue = 1;
        MaxEnamiesOnBoardSlider.highValue = 10;
        MaxEnamiesOnBoardSlider.RegisterValueChangedCallback((evt) => OnMaxEnamiesOnBoardSliderValueChanged(evt));
        Label MaxEnamiesOnBoardLable = MaxEnamiesOnBoardSliderContainer.Q<Label>();
        MaxEnamiesOnBoardLable.text = "Max Enemies On Board";

        scrollView.Add(MaxEnamiesOnBoardSliderContainer);

        var MaxCollectablesOnBoardSliderContainer = optionsContainer.CloneTree();
        SliderInt MaxCollectablesOnBoardSlider = MaxCollectablesOnBoardSliderContainer.Q<SliderInt>();
        MaxCollectablesOnBoardSlider.value = gameConfigData.configData.MaxCollectablesOnBoard;
        MaxCollectablesOnBoardSlider.lowValue = 1;
        MaxCollectablesOnBoardSlider.highValue = 10;
        MaxCollectablesOnBoardSlider.RegisterValueChangedCallback((evt) => OnMaxCollectablesOnBoardSliderValueChanged(evt));
        Label MaxCollectablesOnBoardLable = MaxCollectablesOnBoardSliderContainer.Q<Label>();
        MaxCollectablesOnBoardLable.text = "Max Collectables On Board";

        scrollView.Add(MaxCollectablesOnBoardSliderContainer);

        Button mainMenuButton = RootVisualElement.Q<Button>(MainMenuButtonName);
        mainMenuButton.clicked += () => UIEvents.UIChangeEvent.Invoke("MainMenu");
    }

    private void OnMaxCollectablesOnBoardSliderValueChanged(ChangeEvent<int> evt)
    {
        gameConfigData.configData.MaxCollectablesOnBoard = evt.newValue;
        gameConfigData.Notify();
    }

    private void OnMaxEnamiesOnBoardSliderValueChanged(ChangeEvent<int> evt)
    {
        gameConfigData.configData.MaxEnemiesOnBoard =evt.newValue;
        gameConfigData.Notify();
    }

    private void OnGameTimerSliderValueChanged(ChangeEvent<int> evt)
    {
        gameConfigData.configData.GameTimer = (float)evt.newValue;
        gameConfigData.Notify(); 
    }
}
