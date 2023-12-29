using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLoading : MonoBehaviour
{
    public GameObject tf;
    public float speed;

    private void Update()
    {
        tf.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }
}
