using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string BaseScene = "BaseScene";
    [SerializeField] private string UIScene = "UIScene";
    [SerializeField] private string GameActors = "GameActors";

    // Start is called before the first frame update
    private void Start()
    {
        // Load the UXML asset asynchronously using Addressables
        StartCoroutine(LoadSceneAsync(UIScene, LoadSceneMode.Additive, () => { Debug.Log("UISceneLoaded"); }));
        UIEvents.SceneAddEvent.AddListener(OnSceneAdd);
        UIEvents.SceneRemoveEvent.AddListener(OnSceneRemove);
    }

    private void OnSceneRemove(string arg0, Action arg1)
    {
        StartCoroutine(UnloadSceneAsync(arg0, arg1));
    }

    private void OnSceneAdd(string arg0, Action arg1)
    {
        StartCoroutine(LoadSceneAsync(arg0, LoadSceneMode.Additive, arg1));
    }

    private IEnumerator LoadSceneAsync(string scene, LoadSceneMode mode, System.Action onComplete = null)
    {
        if(SceneManager.GetSceneByName(scene).isLoaded)
        {
            onComplete?.Invoke();
            yield break; 
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, mode);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        onComplete?.Invoke();
    }
    private IEnumerator UnloadSceneAsync(string sceneName, System.Action onComplete = null)
    {
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        onComplete?.Invoke();
    }
}
