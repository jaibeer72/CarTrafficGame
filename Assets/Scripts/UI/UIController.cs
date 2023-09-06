using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    UIDocument UIDocument;
    [SerializeField] private string FirstUIView = "MainMenu";
    // Start is called before the first frame update
    void Start()
    {
        UIDocument = GetComponent<UIDocument>();
        UIEvents.UIChangeEvent.AddListener(OnChangeUI);
        _ = InitializeUIAsync();
    }

    private void OnDestroy()
    {
        UIEvents.UIChangeEvent.RemoveListener(OnChangeUI);
    }

    private void OnChangeUI(string arg0)
    {
        _= ChangeViewAsync(arg0);
    }

    private async Task InitializeUIAsync()
    {
        // Your async code here. For example:
        await ChangeViewAsync(FirstUIView);
    }

    private async Task ChangeViewAsync(string viewKey)
    {
        if (UIViewRegistry.ViewDictionary.TryGetValue(viewKey, out var viewData))
        {
            var rootElement = UIDocument.rootVisualElement;
            rootElement.Clear();
            UIView[] views = gameObject.GetComponents<UIView>();
            foreach (var view in views)
            {
                Destroy(view);
            }

            // Load the UXML asset asynchronously using Addressables
            var uxmlAssetLoadOperation = Addressables.LoadAssetAsync<VisualTreeAsset>(viewData.uxmlFilePath);
            VisualTreeAsset uxmlAsset = await uxmlAssetLoadOperation.Task;

            if (uxmlAsset != null)
            {
                var visualTree = uxmlAsset.CloneTree();
                rootElement.Add(visualTree);

                

                // Instantiate the view script and call the PreShow and PostShow methods
                UIView newView = (UIView)gameObject.AddComponent(viewData.viewType);
                newView.InitializeView(rootElement);
            }
            else
            {
                Debug.LogError("Failed to load UXML asset: " + viewData.uxmlFilePath);
            }
        }
    }
}
