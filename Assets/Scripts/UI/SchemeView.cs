using UnityEngine;
using UnityEngine.UI;

public class SchemeView : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image> ();
    }

    private void OnEnable()
    {
        if ( GameEvents.current != null )
            GameEvents.current.OnSpellItemViewOpenedEvent += UpdateView;
    }

    private void OnDisable()
    {
        GameEvents.current.OnSpellItemViewOpenedEvent -= UpdateView;
    }


    private void UpdateView( UnitTemplate unitTemplate )
    {
        if(unitTemplate.scheme != null)
            _image.sprite = unitTemplate.scheme;
    }
}
