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
            rb.AddForce(force); // Add force to the lamp; 3D
        }
        else
        {
            Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
            if(rb2D != null )
            {
                rb2D.AddForce(force); // Add force to the lamp; 2D
            }
        }
    }

    private void Start()
    {
        StartCoroutine(WaitAndApply(20)); // Wait 20 seconds then apply the first force to the lamps
    }

    private IEnumerator WaitAndApply(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); // Wait
            ApplyForceToJoint(); // Apply force
            waitTime = Random.Range(20, 60); // Generate random wait time
        }
    }

}
