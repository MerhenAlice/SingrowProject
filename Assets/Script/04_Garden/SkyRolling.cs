using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRolling : MonoBehaviour
{
    public GameObject SkyBox;
    public float speed;
    private void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
