using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.tag == "Plant" && collision.gameObject.GetComponent<PlantManager>().GrowState == 5)
            {
                collision.gameObject.GetComponent<PlantManager>().GrowState = 0;
                print("Harvest!");
                anim.Play("Harvest");
                StartCoroutine(WaitHarvest(collision.gameObject));
            }
        }
        catch
        {
            print("Error!");
        }
    }

    private void Awake()
    {
        this.transform.position = new Vector3(300, 200, 0);
    }

    private IEnumerator WaitHarvest(GameObject Plant)
    {
        yield return new WaitForSeconds(0.5f);
        Plant.gameObject.GetComponent<PlantManager>().Harvest();
    }
}
