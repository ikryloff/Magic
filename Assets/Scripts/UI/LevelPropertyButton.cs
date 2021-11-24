using UnityEngine;
using TMPro;

public class LevelPropertyButton : ItemPropertyButton
{

    [SerializeField] private Sprite [] levels;

    public override void UpdateView( UnitTemplate unitTemplate )
    {
        _button.image.sprite = levels [unitTemplate.level - 1];
    }
}
