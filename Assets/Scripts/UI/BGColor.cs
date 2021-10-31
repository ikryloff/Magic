using UnityEngine;
using UnityEngine.UI;

public class BGColor : MonoBehaviour
{
    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image> ();
    }

    private void OnEnable()
    {
        GameEvents.current.OnTabChangeEvent += ChangeColor;
    }

    private void OnDisable()
    {
        GameEvents.current.OnTabChangeEvent -= ChangeColor;
    }


    private void ChangeColor( Unit.UnitClassProperty schoolIndex, Color32 color )
    {
        _image.color = color;
    }
}
