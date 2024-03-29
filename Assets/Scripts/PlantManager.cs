using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class PlantManager : MonoBehaviour
{
    public List<Sprite> plantSkins = new List<Sprite>(); // 0 = Seed; 1-6 = Plants;

    public int WaterAmmount = 50;
    public int FoodAmount = 50;

    public int FavTemperatureMin;
    public int FavTemperatureMax;

    public Color FavLightColor;

    public float GrowthSpeed;

    private int GrowthProgress;
    private bool fullyGrown;

    public void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = plantSkins[1]; // First plant sprite

        // Resetting all values
        GrowthProgress = 0;
        fullyGrown = false;
        WaterAmmount = 50;
        FoodAmount = 50;
    }
}
