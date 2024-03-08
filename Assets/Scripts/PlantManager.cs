using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public List<Sprite> plantSkins = new List<Sprite>();
    public int WaterAmmount = 50;
    public int FoodAmount = 50;

    public int FavTemperature;
    public Color FavLightColor;

    public float GrowthSpeed;

    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = plantSkins[0];
    }
}
