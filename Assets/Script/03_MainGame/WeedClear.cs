using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedClear : MonoBehaviour
{
    public GameObject Pick;
    public void WeedClearClick()
    {
        StartCoroutine(Weedpick());
    }
    IEnumerator Weedpick()
    {
        GameObject inatantPick = Instantiate(Pick, gameObject.transform);
        yield return new WaitForSeconds(1.0f);
        Destroy(inatantPick);
        Destroy(gameObject);
    }
}
