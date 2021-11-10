using TMPro;
using UnityEngine;

public class ManaCostPropertyButton : ItemPropertyButton
{
    [SerializeField] private TextMeshProUGUI textTMP;
    public override void UpdateView( UnitTemplate unitTemplate )
    {
        textTMP.text = unitTemplate.cost.ToString ();
    }
}
