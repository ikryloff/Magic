using UnityEngine;
using TMPro;

public class LevelPropertyButton : ItemPropertyButton
{
    [SerializeField] private TextMeshProUGUI textTMP;

    public override void UpdateView( UnitTemplate unitTemplate )
    {
        textTMP.text = unitTemplate.level.ToString();
    }
}
