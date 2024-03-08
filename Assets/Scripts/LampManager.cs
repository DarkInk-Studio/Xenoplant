using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LampManager : MonoBehaviour
{
    public GameObject Lamp;
    public Slider TemperatureSlider;
    public TextMeshProUGUI TemperatureText;

    public List<Color> L_colors = new List<Color>(); //Index: 0 - 8; White 0; Red 1; Green 2; Blue 3; Yellow 4; Orange 5; Purple 6; Black 7;
    
    private int currentColorIndex = 0; //Index: 0 - 8

    public Color currentColor = Color.white;
    public int currentTemperature = 0;

    private bool available = true; //Is the Lamp still available or already taken

    private void Start()
    {
        TemperatureSlider.onValueChanged.AddListener(delegate { ChangeTemperature(); });
    }

    private void OnMouseOver() //Check if mouse is over the Lamp
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLClick();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnRClick();
        }
    }

    private void OnLClick()
    {
       if(currentColorIndex >= 6)
        {
            currentColorIndex = 0;
            Lamp.GetComponent<Light2D>().color = L_colors[currentColorIndex]; //Change color to next color
            currentColor = Lamp.GetComponent<Light2D>().color; //Current color update
        }
        else
        {
            currentColorIndex++;
            Lamp.GetComponent<Light2D>().color = L_colors[currentColorIndex]; //Change color to previous color
            currentColor = Lamp.GetComponent<Light2D>().color; //Current color update
        }

    }

    private void OnRClick()
    {
        if (currentColorIndex <= 0)
        {
            currentColorIndex = 6;
            Lamp.GetComponent<Light2D>().color = L_colors[currentColorIndex]; //Change color to next color
            currentColor = Lamp.GetComponent<Light2D>().color; //Current color update
        }
        else
        {
            currentColorIndex--;
            Lamp.GetComponent<Light2D>().color = L_colors[currentColorIndex]; //Change color to next color
            currentColor = Lamp.GetComponent<Light2D>().color; //Current color update
        }
    }

    public void ChangeTemperature()
    {
        currentTemperature = (int)TemperatureSlider.value;
        TemperatureText.text = $"{(int)TemperatureSlider.value}°C";
    }

}
