using UnityEngine;

public class ItemPropertyButton : UIButton
{

    private void OnEnable()
    {
        if ( GameEvents.current != null )
            GameEvents.current.OnSpellItemViewOpenedEvent += UpdateView;
    }

    private void OnDisable()
    {
        GameEvents.current.OnSpellItemViewOpenedEvent -= UpdateView;
    }

    public virtual void UpdateView(UnitTemplate unitTemplate)
    {
    }

    public override void Action()
    {
    }


}
