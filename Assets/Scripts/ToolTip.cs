using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;

    public TextMeshProUGUI Progress;
    public Image ProgressBar;
    public Image Icon;

    public Image FavLightColor;
    public TextMeshProUGUI WaterAmount;
    public TextMeshProUGUI Temperature;
    public TextMeshProUGUI Quality;

    public void SetText(string name, string description, float progress, Sprite icon, Color lightColor, int waterAmount, int temperature, int quality)
    {
        NameText.text = name;
        DescriptionText.text = description;

        Progress.text = $"Progress: {progress} %";
        ProgressBar.fillAmount = progress / 100;
        Icon.sprite = icon;

        FavLightColor.color = lightColor;
        WaterAmount.text = $"{waterAmount} %";
        Temperature.text = $"{temperature} °C";
        Quality.text = quality.ToString();
    }

    private void Update()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }
}
