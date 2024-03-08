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
            Destroy(this.gameObject);
        }
    }
}
