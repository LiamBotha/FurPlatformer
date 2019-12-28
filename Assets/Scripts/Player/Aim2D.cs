using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim2D : MonoBehaviour {

    private Vector3 mousePos;
    private Transform target;
    private Vector3 objectPos;
    private float angle;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        mousePos = Input.mousePosition;
        mousePos.z = -20;
        objectPos = Camera.main.WorldToScreenPoint(target.position);// target = what is aimed from
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;// Rad2Deg converts to degrees 
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
