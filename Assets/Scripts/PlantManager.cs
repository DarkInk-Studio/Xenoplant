using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Device;

public class PlantManager : MonoBehaviour
{
    public GameObject WariningIcon;
    private GameObject warningicon;
    private bool isWarningDisplayed;

    public bool hasLamp = false;
    public int attachedPotID;

    public List<Sprite> plantSkins = new List<Sprite>(); // 0 - 5 = Plants;

    public float WaterAmount = 100;

    public Color currentLight;
    public int currentTemperature;

    public int FavTemperature;
    public int FavTempMax;
    public int FavTempMin;

    public Color FavLightColor;

    public int PlantWorth;
    public Money GlobalMoney;

    public float Quality = 100;
    private bool IsRemovingQuality = false;

    public float TimeToGrow; // Time it takes to fully grow in mins
    public float TimeUntilDone;

    public float GrowSpeed;

    public int GrowState;
    private bool fullyGrown;

    public string description;
    public string name;
    public Sprite icon;

    public void SetPot(int potID = 0)
    {
        attachedPotID = potID;
    }

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

    public void Pour(float Pour_Water = 5f)
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
            GlobalMoney.money += Mathf.RoundToInt(PlantWorth * (Quality / 100)); // Add the cash
            gameObject.transform.position = new Vector3(200, 200, 200); // Disconnect Lamp
            SetPot();
            Destroy(this.gameObject, 1);

            GameObject[] pots = GameObject.FindGameObjectsWithTag("Full");

            foreach(GameObject pot in pots) 
            {
                if(pot.GetComponent<PotScript>().potID == attachedPotID) // Clear the used pot
                {
                    pot.tag = "Pot";
                }
            }
        }
    }

    private void Update()
    {
        if (!fullyGrown)
        {
            this.transform.localPosition = new Vector3(0, 0.35f, 0);
        }

        if (CanGrow())
        {
            TimeUntilDone -= (WaterAmount / 100) * GrowSpeed * Time.deltaTime;
            WaterAmount -= 1.5f * Time.deltaTime;
            if (WaterAmount <= 0 ) 
            {
                WaterAmount = 0;
            }

            if (Mathf.RoundToInt(TimeToGrow / 6 * 1) == Mathf.RoundToInt(TimeUntilDone)) //5. Grow
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[5];
                GrowState = 5;
                if(Random.Range(0, 2) == 1)
                {
                    FavTemperature = Random.Range(FavTempMin, FavTempMax);
                }
            }
            else if(Mathf.RoundToInt(TimeToGrow / 6 * 2) == Mathf.RoundToInt(TimeUntilDone)) //4. Grow
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[4];
                GrowState = 4; 
                if (Random.Range(0, 2) == 1)
                {
                    FavTemperature = Random.Range(FavTempMin, FavTempMax);
                }
            }
            else if (Mathf.RoundToInt(TimeToGrow / 6 * 3) == Mathf.RoundToInt(TimeUntilDone)) //3. Grow
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[3];
                GrowState = 3;
                if (Random.Range(0, 2) == 1)
                {
                    FavTemperature = Random.Range(FavTempMin, FavTempMax);
                }
            }
            else if (Mathf.RoundToInt(TimeToGrow / 6 * 4) == Mathf.RoundToInt(TimeUntilDone)) //2. Grow
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[2];
                GrowState = 2;
                if (Random.Range(0, 2) == 1)
                {
                    FavTemperature = Random.Range(FavTempMin, FavTempMax);
                }
            }
            else if (Mathf.RoundToInt(TimeToGrow / 6 * 5) == Mathf.RoundToInt(TimeUntilDone)) //1. Grow
            {
                TimeUntilDone -= 1;
                GetComponent<SpriteRenderer>().sprite = plantSkins[1];
                GrowState = 1;
                if (Random.Range(0, 2) == 1)
                {
                    FavTemperature = Random.Range(FavTempMin, FavTempMax);
                }
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
            if (isWarningDisplayed)
            {
                isWarningDisplayed = false;
                Destroy(warningicon);
            }
            return true;
        }
        else
        {
            if (!isWarningDisplayed && !fullyGrown)
            {
                isWarningDisplayed = true;
                warningicon = Instantiate(WariningIcon, this.transform);
                warningicon.transform.localPosition = new Vector3(-1, 0.6f, 0);
            }

            if (!IsRemovingQuality)
            {
                IsRemovingQuality = true;
                StartCoroutine(RemoveQuality());
            }
            
            return false;
        }
    }

    private IEnumerator RemoveQuality()
    {
        yield return new WaitForSeconds(5);
        if(!CanGrow() && !fullyGrown)
        {
            if((Quality - 5) <= 0)
            {
                Quality = 0;
            }
            else
            {
                Quality -= 5;
            }
        }
        IsRemovingQuality = false;
    }
}
