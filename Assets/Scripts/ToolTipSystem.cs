using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    private static ToolTipSystem current;

    public ToolTip tooltip;
    public GameObject ToolTip;

    private void Awake()
    {
        current = this;
    }

    public static void Show(string name, string description, float progress, Sprite icon, Color lightColor, int waterAmount, int temperature, int quality)
    {
        current.tooltip.SetText(name, description, progress, icon, lightColor, waterAmount, temperature, quality);
        current.ToolTip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.ToolTip.gameObject.SetActive(false);
    }
}
