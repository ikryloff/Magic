using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image filler;

    public void SetValue( float valueNorm )
    {
        filler.fillAmount = valueNorm;
    }
}
