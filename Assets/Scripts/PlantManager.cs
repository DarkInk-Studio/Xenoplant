using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;

public class PlantManager : MonoBehaviour
{
    public bool hasLamp = false;

    public List<Sprite> plantSkins = new List<Sprite>(); // 0-5 = Plants;

    public float WaterAmount = 100;

    public Color currentLight;
    public int currentTemperature;

    public int FavTemperature;
    public int FavTempMax;
    public int FavTempMin;

    public Color FavLightColor;

    public int PlantWorth;

    private float Quality;

    public float TimeToGrow; // Time it takes to fully grow in mins
    [SerializeField] private float TimeUntilDone;

    public float GrowSpeed;

    private int GrowState;
    private bool fullyGrown;

    public TextMeshProUGUI moneyText;
    private int Money;

    public void Transfer(Color LightColor, int Temperature)
    {
        currentLight = LightColor;
        currentTemperature = Temperature;
    }

    public void Awake()
    {

        GetComponent<SpriteRenderer>().sprite = plantSkins[0]; // First plant sprite

        TimeToGrow *= 60; // Changing min to sec
        TimeUntilDone = TimeToGrow;

        // Resetting all values
        GrowState = 0;
        fullyGrown = false;
        WaterAmount = 100;
        Quality = 100;
        FavTemperature = Random.Range(FavTempMin, FavTempMax);
    }

    public void Pour(float Pour_Water = 10f)
    {
        if (WaterAmount + Pour_Water >= 100)
        {
            WaterAmount = 100;
        }
        else
        {
            WaterAmount += Pour_Water;
        }
    }

    public void Harvest()
    {
        if(TimeUntilDone <= 0)
        {
            Money += PlantWorth;
            moneyText.text = $"{Money} $";
            gameObject.transform.position = new Vector3(200, 200, 200);
            Destroy(this.gameObject, 1);
        }
    }

    private void Update()
    {
        if (CanGrow())
        {
            TimeUntilDone -= (WaterAmount / 100) * GrowSpeed * Time.deltaTime;
            WaterAmount -= 1.5f * Time.deltaTime;
            if (WaterAmount <= 0 ) 
            {
                WaterAmount = 0;
            }

            if (Mathf.RoundToInt(TimeToGrow / 6 * 1) == Mathf.RoundToInt(TimeUntilDone)) //5. Wachsen
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[5];
                GrowState = 5;
                FavTemperature = Random.Range(FavTempMin, FavTempMax);
            }
            else if(Mathf.RoundToInt(TimeToGrow / 6 * 2) == Mathf.RoundToInt(TimeUntilDone)) //4. Wachsen
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[4];
                GrowState = 4;
                FavTemperature = Random.Range(FavTempMin, FavTempMax);
            }
            else if (Mathf.RoundToInt(TimeToGrow / 6 * 3) == Mathf.RoundToInt(TimeUntilDone)) //3. Wachsen
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[3];
                GrowState = 3;
                FavTemperature = Random.Range(FavTempMin, FavTempMax);
            }
            else if (Mathf.RoundToInt(TimeToGrow / 6 * 4) == Mathf.RoundToInt(TimeUntilDone)) //2. Wachsen
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[2];
                GrowState = 2;
                FavTemperature = Random.Range(FavTempMin, FavTempMax);
            }
            else if (Mathf.RoundToInt(TimeToGrow / 6 * 5) == Mathf.RoundToInt(TimeUntilDone)) //1. Wachsen
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[1];
                GrowState = 1;
                FavTemperature = Random.Range(FavTempMin, FavTempMax);
            }

            if (TimeUntilDone <= 0)
            {
                fullyGrown = true;
                TimeUntilDone = 0;
                WaterAmount = 100;
                GrowState = 5;
            }
        }
    }

    private bool CanGrow()
    {
        if (WaterAmount > 0 && hasLamp && currentLight == FavLightColor && currentTemperature == FavTemperature && !fullyGrown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
