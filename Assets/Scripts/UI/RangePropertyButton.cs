using TMPro;
using UnityEngine;

public class RangePropertyButton : ItemPropertyButton
{
    [SerializeField] private TextMeshProUGUI textTMP;
    public override void UpdateView( UnitTemplate unitTemplate )
    {
        textTMP.text = unitTemplate.attackRange.ToString ();
    }
}
