using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetImageItemCount : MonoBehaviour
{
    public Image image;
    public TMP_Text text;
    public void setVars(int _count, Sprite _sprite)
    {
        text.SetText(_count.ToString());
        image.sprite = _sprite;
        image.enabled = true;
    }
}
