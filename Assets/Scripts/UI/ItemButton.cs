using UnityEngine;

public class ItemButton : UIButton
{
    [SerializeField]
    private int number;
    [SerializeField]
    private Unit.UnitType type;

    private void Start()
    {
        GameEvents.current.OnTabChangeEvent += UpdateButton;

    }

    private void OnDestroy()
    {
        GameEvents.current.OnTabChangeEvent -= UpdateButton;
    }


    private void UpdateButton( Unit.UnitClassProperty schoolIndex, Color32 color )
    {
        SpellProperty spellProperty = new SpellProperty (schoolIndex, type);
        UnitTemplate template = SpellsMaps.GetUnitTemplateBySpellProperty (spellProperty, number);
        if ( template != null )
        {
            _button.image.sprite = template.activeIcon;

            if ( Player.IsSpellInPlayerSpellsIDList (template.unitID) )
                _button.image.material.SetFloat (Constants.GRAYSCALE_RATIO, 0);
            else
                _button.image.material.SetFloat (Constants.GRAYSCALE_RATIO, 1);
        }
    }


    public override void Action()
    {

    }
}
