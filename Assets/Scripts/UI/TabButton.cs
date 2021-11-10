using UnityEngine;
using UnityEngine.UI;

public class TabButton : UIButton
{
    [SerializeField]
    private Unit.UnitClassProperty _tabValue;
    [SerializeField]
    private Sprite _imageActive;
    [SerializeField]
    private Sprite _imageUnactive;

    public static Unit.UnitClassProperty SchoolIndex = Unit.UnitClassProperty.Elemental;

    
    private void OnEnable()
    {
        if(GameEvents.current != null)
            GameEvents.current.OnTabChangeEvent += ActivateSchool;
    }


    private void OnDisable()
    {
        GameEvents.current.OnTabChangeEvent -= ActivateSchool;
    }

    public override void Action()
    {
        GameEvents.current.TabChangeAction (_tabValue);
    }

    private void ActivateSchool( Unit.UnitClassProperty tabIndex )
    {
        if ( tabIndex == _tabValue )
        {
           SchoolIndex = tabIndex;
            _button.image.sprite = _imageActive;
        }
        else
        {
            _button.image.sprite = _imageUnactive;
        }
    }
}
