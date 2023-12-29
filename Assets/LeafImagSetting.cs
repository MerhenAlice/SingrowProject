using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeafImagSetting : MonoBehaviour
{
    public Image LeafImage;
    public List<Sprite> Sprites = new List<Sprite>();
    private void Update()
    {
        LeafImage.sprite = Sprites[ConsensusUIEvent.leapCount];
    }
}
