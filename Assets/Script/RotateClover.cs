using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateClover : MonoBehaviour
{
    public float speed;
    void Update()
    {
        this.gameObject.GetComponent<BezierTransform>().transform.Rotate(Vector3.forward*Time.deltaTime*speed);
    }
}
