using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public GameObject Sickle;
    public void HoldSickle()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Sickle.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    public void ReleaseSickle()
    {
        Sickle.transform.position = new Vector3(800, 700, 0);
    }
}
