using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    // Start is called before the first frame update
    public Image img;
    public float r;
    public float g;
    public float b;
    public List<Sprite> sprites = new List<Sprite> ();
    public string name;
    public GameObject go;
    GameObject gogo;
    void OnEnable()
    {
        r= img.color.r; g= img.color.g; b= img.color.b;
        StartCoroutine(kkk());
        gogo = Instantiate(go,img.gameObject.transform);
    }
    public AudioSource source;
    IEnumerator kkk()
    {
        
        for (int i = 0; i < 1; i++)
        {
            for(int j =0; j<sprites.Count; j++)
            {
                if(img.name == sprites[j].name )
                {
                    img.sprite = sprites[j];
                    img.SetNativeSize();
                }
            }
            string tmp = img.name.Replace(DataSave.Instance.StairTemp, (int.Parse( DataSave.Instance.StairTemp) + 1).ToString());
            yield return new WaitForSeconds(1.0f);
            for(int j =0; j<sprites.Count; j++)
            {
                if(tmp == sprites[j].name )
                {
                    img.sprite = sprites[j];
                    img.SetNativeSize();
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
        img.name = img.sprite.name;
        source.Play();
        Destroy(gogo);
        this.gameObject.GetComponent<FadeInOut>().enabled = false;
    }
}
