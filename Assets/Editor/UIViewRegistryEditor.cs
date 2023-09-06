#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

[InitializeOnLoad]
public class UIViewRegistryEditor
{
    static UIViewRegistryEditor()
    {
        RegisterAllViews();
    }

    [MenuItem("Tools/Register All Views")]
    private static void RegisterAllViews()
    {
        UIViewRegistry.ViewDictionary.Clear();

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                var attributes = type.GetCustomAttributes(typeof(UIViewAttribute), true);
                foreach (UIViewAttribute attribute in attributes)
                {
                    if (!UIViewRegistry.ViewDictionary.ContainsKey(attribute.ViewName))
                    {
                        UIViewRegistry.ViewDictionary.Add(attribute.ViewName, (type, attribute.UXMLFilePath));
                    }
                    else
                    {
                        Debug.LogWarning("View with name " + attribute.ViewName + " is already registered.");
                    }

                }
            }
        }

        Debug.Log("All views registered.");
    }
}
#endif
