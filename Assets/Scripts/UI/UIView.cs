using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIView : MonoBehaviour
{
    protected VisualElement RootVisualElement { get; private set; }
    // Wied dependency injection .. why not do it via constructor cause i suck
    public void InitializeView(VisualElement rootVisualElement)
    {
        this.RootVisualElement = rootVisualElement;
        OnViewInitialized();
    }

    protected virtual void OnViewInitialized()
    {
        // Can be overridden by subclasses to perform initialization when the view is initialized with the rootVisualElement
    }
}
