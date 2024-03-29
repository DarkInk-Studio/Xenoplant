using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LampManager : MonoBehaviour
{
    public GameObject Lamp;
    public GameObject currentAttPlant;
    public UnityEngine.UI.Slider TemperatureSlider;
    public TextMeshProUGUI TemperatureText;

    public List<Color> L_colors = new List<Color>(); //Index: 0 - 8; White 0; Red 1; Green 2; Blue 3; Yellow 4; Orange 5; Purple 6; Black 7;

    private int currentColorIndex = 0; //Index: 0 - 8

    public Color currentColor = Color.white;
    public int currentTemperature = 0;

    public bool available = true; //Is the Lamp still available or already taken

    private void Start()
    {
        GetComponent<LineRenderer>().enabled = false;
        TemperatureSlider.onValueChanged.AddListener(delegate { ChangeTemperature(); });
    }

    private void OnMouseOver() //Check if mouse is over the Lamp
    {
        print("hover");
        if (!available)
        {
            GetComponent<LineRenderer>().enabled = true;
            print("Object: " + this.gameObject.name + " enabled LineRenderer");
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnLClick();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnRClick();
        }
    }

    private void OnMouseExit()
    {
        GetComponent<LineRenderer>().enabled = false;
        print("Object: " + this.gameObject.name + " desabled LineRenderer");
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

    public GameObject FindClosestPlant()
    {
        try
        {
            List<GameObject> Lgos;
            GameObject closest = null;
            Lgos = new List<GameObject>(GameObject.FindGameObjectsWithTag("Plant"));

            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in Lgos)
            {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
            }
            return closest;
        }
        catch
        {
            return null;
        }
    }

    private void Update()
    {

        GameObject closestPlant = FindClosestPlant();
        if(closestPlant != null)
        {
            Vector3 diff = closestPlant.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;

            try
            {
                Vector3 diffCurrent = currentAttPlant.transform.position - transform.position;
                float curDistanceCurrent = diffCurrent.sqrMagnitude;

                if (curDistanceCurrent >= 15)
                {
                    print("Disconnect!");
                    currentAttPlant.GetComponent<PlantManager>().hasLamp = false;
                    currentAttPlant = null;
                    available = true;
                }
            }
            catch { }

            if (available && curDistance <= 15 && !closestPlant.GetComponent<PlantManager>().hasLamp && currentAttPlant == null || currentAttPlant != null)
            {
                if (currentAttPlant == null)
                {
                    currentAttPlant = closestPlant;
                }
                currentAttPlant.GetComponent<PlantManager>().Transfer(currentColor, currentTemperature);
                currentAttPlant.GetComponent<PlantManager>().hasLamp = true;
                available = false;
                GetComponent<LineController>().SetUpLine(currentAttPlant.transform.position);
            }
        }
    }
}
