using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetToKnowLeaf : MonoBehaviour
{
    public Image leaf;
    public List<Sprite> sprites = new List<Sprite>();
    private void Update()
    {
        leaf.sprite = sprites[ConsensusUIEvent.leapCount];
    }
}
