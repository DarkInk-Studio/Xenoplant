using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class PlantManager : MonoBehaviour
{
    public bool hasLamp = false;

    public List<Sprite> plantSkins = new List<Sprite>(); // 0-6 = Plants;

    public int WaterAmmount = 50;
    public int FoodAmount = 50;

    public Color currentLight;
    public int currentTemperature;

    public int FavTemperatureMin;
    public int FavTemperatureMax;

    public Color FavLightColor;

    public float GrowthSpeed;

    private int GrowthProgress;
    private bool fullyGrown;

    private float timePassed;

    public void Transfer(Color LightColor, int Temperature)
    {
        currentLight = LightColor;
        currentTemperature = Temperature;
    }

    public void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = plantSkins[0]; // First plant sprite

        // Resetting all values
        GrowthProgress = 0;
        fullyGrown = false;
        WaterAmmount = 50;
        FoodAmount = 50;
    }

    private void Update()
    {
        /*if (WaterAmmount > 0 && FoodAmount > 0 && )
        {
            
        }
        timePassed += Time.deltaTime;*/

    }
}
