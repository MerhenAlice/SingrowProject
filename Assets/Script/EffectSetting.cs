using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UPDOWN
{
    UP,
    DOWN
}
public class EffectSetting : MonoBehaviour
{
    public Image Effect;
    public float alpha;
    public UPDOWN uPDOWN;
    public float speed;
    public float alphaSpeed;
    private void Start()
    {
        alpha = Effect.color.a;
    }
    private void Update()
    {
        Effect.color = new Color(Effect.color.r, Effect.color.g, Effect.color.b, alpha);
        if(uPDOWN == UPDOWN.UP)
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0f));
        }
        else if(uPDOWN == UPDOWN.DOWN)
        {
            transform.Translate(new Vector3(0, speed * -Time.deltaTime, 0f));
        }
        alpha -= alphaSpeed;
    }
}
