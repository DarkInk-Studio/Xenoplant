using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    public int potID;

    private bool isMouseDown;

    private string name;
    private string description;
    private float progress;
    private Sprite icon;
    private Color lightColor;
    private int waterAmount;
    private int temperature;
    private int quality;

    private bool hover = false;

    public void SetPotID(int id)
    {
        potID = id;
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Particle Hit");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            isMouseDown = false;
        }

        if (hover)
        {
            GameObject[] plants;
            plants = GameObject.FindGameObjectsWithTag("Plant");

            foreach (GameObject plant in plants)
            {
                if (plant.GetComponent<PlantManager>().attachedPotID == potID)
                {
                    name = plant.GetComponent<PlantManager>().name;
                    description = plant.GetComponent<PlantManager>().description;
                    progress = Mathf.RoundToInt(100 - ((plant.GetComponent<PlantManager>().TimeUntilDone / plant.GetComponent<PlantManager>().TimeToGrow) * 100));
                    icon = plant.GetComponent<PlantManager>().icon;
                    lightColor = plant.GetComponent<PlantManager>().FavLightColor;
                    waterAmount = Mathf.RoundToInt(plant.GetComponent<PlantManager>().WaterAmount);
                    temperature = plant.GetComponent<PlantManager>().FavTemperature;
                    quality = Mathf.RoundToInt(plant.GetComponent<PlantManager>().Quality);

                    ToolTipSystem.Show(name, description, progress, icon, lightColor, waterAmount, temperature, quality);
                }
            }
        }
    }
    private void OnMouseEnter()
    {
        if (!isMouseDown)
        {
            hover = true;
        }
    }

    private void OnMouseExit() 
    {
        hover = false;
        ToolTipSystem.Hide();
    }
}
