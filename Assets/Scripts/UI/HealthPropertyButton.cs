using TMPro;
using UnityEngine;

public class HealthPropertyButton : ItemPropertyButton
{
    [SerializeField] private TextMeshProUGUI textTMP;
    public override void UpdateView( UnitTemplate unitTemplate )
    {
        textTMP.text = unitTemplate.health.ToString ();
    }
}
