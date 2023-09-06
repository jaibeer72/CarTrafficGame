using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIViewRegistry
{
    public static Dictionary<string, (Type viewType, string uxmlFilePath)> ViewDictionary = new Dictionary<string, (Type, string)>();
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class UIViewAttribute : Attribute
{
    public string ViewName { get; private set; }
    public string UXMLFilePath { get; private set; }
    public Type ViewType { get; private set; }
    public UIViewAttribute(string viewName, string uxmlFilePath, Type viewType)
    {
        ViewName = viewName;
        UXMLFilePath = uxmlFilePath;
        ViewType = viewType;
        UIViewRegistry.ViewDictionary.Add(viewName, (viewType, uxmlFilePath));
    }
}
