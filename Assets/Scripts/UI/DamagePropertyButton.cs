using TMPro;
using UnityEngine;

public class DamagePropertyButton : ItemPropertyButton
{
    [SerializeField] private TextMeshProUGUI textTMP;
    public override void UpdateView( UnitTemplate unitTemplate )
    {
        textTMP.text = unitTemplate.damage.ToString ();

    }
}
