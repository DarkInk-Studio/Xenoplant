using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public Rigidbody2D selectedObject;
    Vector3 offset;
    Vector3 mousePosition;

    public float maxSpeed = 10;
    Vector2 mouseForce;
    Vector3 lastPosition;

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (selectedObject)
        {
            mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
            mouseForce = Vector2.ClampMagnitude(mouseForce * 0.2f, maxSpeed);
            lastPosition = mousePosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject && targetObject.gameObject.tag != "Dont Drag")
            {
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = selectedObject.transform.position - mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {

            selectedObject.velocity = Vector2.zero;
            selectedObject.AddForce(mouseForce, ForceMode2D.Impulse);
            selectedObject = null;
        }
    }

    void FixedUpdate()
    {
        if (selectedObject)
        {
            selectedObject.MovePosition(mousePosition + offset);
        }
    }
}