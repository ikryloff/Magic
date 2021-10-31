using UnityEngine;

public class TabButton : UIButton
{
    [SerializeField]
    private Color32 _activeColor;
    private Color32 _unActiveColor;
    [SerializeField]
    private Unit.UnitClassProperty _tabValue;
    public static Unit.UnitClassProperty SchoolIndex = 0;

    private void Start()
    {
        _unActiveColor = new Color32 (_activeColor.r, _activeColor.g, _activeColor.b, 100);
        GameEvents.current.OnTabChangeEvent += ActivateSchool;
    }

    private void OnEnable()
    {
        if ( _tabValue == SchoolIndex )
            Action ();
    }

    private void OnDestroy()
    {
        GameEvents.current.OnTabChangeEvent -= ActivateSchool;
    }

    public override void Action()
    {
        GameEvents.current.TabChangeAction (_tabValue, _activeColor);
    }

    private void ActivateSchool( Unit.UnitClassProperty tabIndex, Color32 color )
    {
        if ( tabIndex == _tabValue )
        {
            _button.image.color = _unActiveColor;
            SchoolIndex = tabIndex;
        }
        else
        {
            _button.image.color = _activeColor;
        }
    }
}
