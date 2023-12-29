using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSelectOn : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> m_BackGround = new List<Sprite>();

    public GameObject normalBG;

    private void Start()
    { 
        for(int i = 0; i<m_BackGround.Count; i++)
        {
            if (m_BackGround[i].name.ToString() == SelectDataController.Instance.selectButtonName)
            {
                normalBG.GetComponent<SpriteRenderer>().sprite = m_BackGround[i];
            }
        }
    }
}
