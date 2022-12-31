using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Rigidbody rb;
    public MapManager MM;

    //
    Vector3 vector;

    private void Awake() {
        MM = GameObject.Find("MapManager").GetComponent<MapManager>();
    }
    private void Start() {
        rb = GetComponent<Rigidbody>();
        vector.y = MM.getSpeed() ;
        rb.AddForce(vector, ForceMode.VelocityChange);
    }
}
