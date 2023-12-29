using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCurvePoints : MonoBehaviour
{

    public GameObject target;
    public Vector3[] point;
    private int x, z;
    public float t;
    public float s;
    public float _newPointDistanceFromStartTr = 6f;
    public float _newPointDistanceFromEndTr = 3f;

    public Transform player;

    private void Start()
    {
        s = 0.1f;
        point = new Vector3[4];
        // 시작 지점.
        point[0] = player.position;

        // 시작 지점을 기준으로 랜덤 포인트 지정.
        point[1] = player.position +
                   (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * player.right) + // X (좌, 우 전체)
                   (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * player.up) + // Y (위, 아래 전체)
                   (_newPointDistanceFromStartTr * Random.Range(-1.0f, 1.0f) * player.forward); // Z (앞, 뒤 전체)

        // 도착 지점을 기준으로 랜덤 포인트 지정.
        point[2] = target.transform.position +
                   (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * target.transform.right) + // X (좌, 우 전체)
                   (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * target.transform.up) + // Y (위, 아래 전체)
                   (_newPointDistanceFromEndTr * Random.Range(-1.0f, 1.0f) * target.transform.forward); // Z (앞, 뒤 전체)

        // 도착 지점.
        point[3] = target.transform.position;
    }

    private void Update()
    {
        this.transform.position = new Vector3(
            FourPointBezier(point[0].x, point[1].x, point[2].x, point[3].x), FourPointBezier(point[0].y, point[1].y, point[2].y, point[3].y),
            FourPointBezier(point[0].z, point[1].z, point[2].z, point[3].z)
        );

    }


    float FourPointBezier(float a, float b, float c, float d)
    {
        t += Time.deltaTime;
        if (t >= s) t = s;

        return Mathf.Pow((1 - t), 3) * a
               + Mathf.Pow((1 - t), 2) * 3 * t * b
               + Mathf.Pow(t, 2) * 3 * (1 - t) * c
               + Mathf.Pow(t, 3) * d;

    }
}
