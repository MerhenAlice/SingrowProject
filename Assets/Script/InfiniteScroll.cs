using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField]
    private Transform    target;
    [SerializeField]
    private float        scrollAmount;
    [SerializeField]
    private float        moveSpeed;
    [SerializeField]
    private Vector3      movDirection;

    private void Update()
    {
        transform.position += movDirection * moveSpeed * Time.deltaTime;

        if(transform.position.x <= - scrollAmount/2)
        {
            transform.position = target.position - movDirection * (scrollAmount+15);
        }
    }
}
