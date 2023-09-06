using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class HealthBarComponent : ProgressBar
{
    private VisualElement m_FillVE;
    private void SetBackgrounColort(float value)
    {
        
        m_FillVE = this.Q<VisualElement>(className: "unity-progress-bar__progress");

        if (m_FillVE == null)
            return;

        float r = (value - lowValue) / (highValue - lowValue);

        if (r <= 0.3f)
        {
            m_FillVE.style.backgroundColor = m_LowColor;
        }
        else if (r <= 0.6f)
        {
            m_FillVE.style.backgroundColor = m_MediumColor;
        }
        else
        {
            m_FillVE.style.backgroundColor = m_HighColor;
        }
    }

    private Color m_LowColor; 
    public Color lowColor {
        get { return m_LowColor; }
        set { m_LowColor = value; }
    }

    private Color m_MediumColor;
    public Color mediumColor
    {
        get { return m_MediumColor; }
        set { m_MediumColor = value; }
    }

    private Color m_HighColor;
    public Color highColor
    {
        get { return m_HighColor; }
        set { m_HighColor = value; }
    }

    public override float value
    {
        get { return base.value; }
        set
        {
            base.value = value;
            SetBackgrounColort(value);
        }
    }

    public new class UxmlFactory : UxmlFactory<HealthBarComponent, UxmlTraits> { }
    public new class UxmlTraits : ProgressBar.UxmlTraits
    {
        UxmlColorAttributeDescription m_lowColor = new UxmlColorAttributeDescription { name = "low-color" };
        UxmlColorAttributeDescription m_MediumColor = new UxmlColorAttributeDescription { name = "mid-color" };
        UxmlColorAttributeDescription m_HighColor = new UxmlColorAttributeDescription { name = "high-color" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            HealthBarComponent healthBar = (HealthBarComponent)ve;
            healthBar.lowColor = m_lowColor.GetValueFromBag(bag, cc);
            healthBar.mediumColor = m_MediumColor.GetValueFromBag(bag, cc);
            healthBar.highColor = m_HighColor.GetValueFromBag(bag, cc);
        }
    }
}
