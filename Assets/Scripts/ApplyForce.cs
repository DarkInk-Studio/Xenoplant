using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    [SerializeField]
    private Vector2 force;

    [ContextMenu("ApplyForce")]
    private void ApplyForceToJoint()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForce(force);
        }
        else
        {
            Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
            if(rb2D != null )
            {
                rb2D.AddForce(force);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(WaitAndApply(20));
    }

    private IEnumerator WaitAndApply(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            ApplyForceToJoint();
            print("[ApplyForce]: Applyed force!");
            waitTime = Random.Range(20, 60);
        }
    }

}
