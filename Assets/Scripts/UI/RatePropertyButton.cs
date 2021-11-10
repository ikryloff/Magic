using TMPro;
using UnityEngine;

public class RatePropertyButton : ItemPropertyButton
{
    [SerializeField] private TextMeshProUGUI textTMP;
    public override void UpdateView( UnitTemplate unitTemplate )
    {
        textTMP.text = unitTemplate.attackRate.ToString ();
    }
}
