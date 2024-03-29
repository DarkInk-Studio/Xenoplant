using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 endPoint;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Vector3 LineEnd)
    {
        endPoint = LineEnd;
    }

    private void Update()
    {
        if (!GetComponent<LampManager>().available)
        {
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
        
}
