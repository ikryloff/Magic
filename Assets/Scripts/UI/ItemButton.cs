using System;
using UnityEngine;

public class ItemButton : UIButton
{
    [SerializeField]
    Material _greyScaleMat;
    [SerializeField]
    private int number;
    [SerializeField]
    private Unit.UnitType type;

    private Animator _animator;

    private UnitTemplate _unitTemplate;


    private new void Awake()
    {
        base.Awake ();
        _animator = GetComponent<Animator> ();
    }

    private void OnEnable()
    {
        if(GameEvents.current != null)
            GameEvents.current.OnTabChangeEvent += UpdateButton;

        if ( _animator != null )
            PlayPulseAnimation ();
    }

    private void PlayPulseAnimation()
    {
        _animator.Play ("pulse");
    }

    private void OnDisable()
    {
        GameEvents.current.OnTabChangeEvent -= UpdateButton;
    }


    public virtual void UpdateButton( Unit.UnitClassProperty schoolIndex )
    {
        SpellProperty spellProperty = new SpellProperty (schoolIndex, type);
        _unitTemplate = SpellsMaps.GetUnitTemplateBySpellProperty (spellProperty, number);
        if ( _unitTemplate != null )
        {
            _button.image.sprite = _unitTemplate.activeIcon;

            if ( Player.IsSpellInPlayerSpellsIDList (_unitTemplate.unitID) )
                SetColorized ();
            else
                SetGray ();
            // for test only
            if ( number == 2 )
                SetGray ();
        }
    }

    public virtual void SetGray()
    {
        _button.image.material = _greyScaleMat;
    }

    public virtual void SetColorized()
    {
        _button.image.material = null;
    }


    public override void Action()
    {
        // for test only
        //SetColorized ();
        GameEvents.current.ItemButtonClickedAction (_unitTemplate);

    }
}
