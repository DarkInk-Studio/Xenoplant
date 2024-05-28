using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    public GameObject Plant;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pot")
        {
            collision.gameObject.tag = "Full";
            GameObject C_Plant = Instantiate(Plant, collision.transform);
            C_Plant.transform.position += new Vector3(0, 0.35f, 0);
            C_Plant.GetComponent<PlantManager>().SetPot(collision.gameObject.GetComponent<PotScript>().potID); // Setting the potID to the new plant
            Destroy(this.gameObject);
        }
    }
}
