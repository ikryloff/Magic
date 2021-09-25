using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform _bar;
    private GameObject _barGo;
    private SpriteRenderer _sprite;
    private BoardUnit _unit;
    public Color32 highColor;
    public Color32 lowColor;
    public float height;
    public float counter = 1f;

    private const string BAR_GO = "BarGO"; 
    private const string BAR = "Bar"; 
    private const string BAR_SPRITE = "BarSprite"; 

    private void Awake()
    {
        _unit = transform.parent.GetComponent<BoardUnit> ();
        _barGo = transform.Find (BAR_GO).gameObject;
        _bar = _barGo.transform.Find (BAR);
        _sprite = _bar.Find (BAR_SPRITE).GetComponent<SpriteRenderer> ();
        _sprite.sortingOrder = 300;
        _barGo.SetActive (false);

        GameEvents.current.OnNewHit += ShowHealthBar;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnNewHit -= ShowHealthBar;
    }

    private void SetHBSize( float size )
    {
        if ( size <= 0 )
        {
            _bar.localScale = new Vector3 (0f, 1f);
            SetHBColor (0f);
            return;
        }
        _bar.localScale = new Vector3 (size, 1f);
        SetHBColor (size);
    }

    private void SetHBColor( float sizeNorm )
    {
        _sprite.color = Color32.Lerp (lowColor, highColor, sizeNorm);
    }


    private void ShowHealthBar( BoardUnit unit, UnitTemplate template )
    {
        if ( _unit != unit )
            return;
        float damage = template.damage;
        unit.ChangeUnitCurrentHealth (damage);
        CheckHP (unit);
        float ratio =  CalcRatio (unit);
        if ( ratio <= 1 && ratio >= 0 )
        {
            if ( !_barGo.activeSelf )
                _barGo.SetActive (true);
            SetHBSize (ratio);
        }

        if ( ratio >= 1 )
        {
            ratio = 1;
            SetHBSize (ratio);
            _barGo.SetActive (false);
        }
    }

    private float CalcRatio(BoardUnit unit)
    {
        return unit.GetUnitCurrentHealth () / unit.GetUnitHealth ();
    }

    private void CheckHP( BoardUnit unit)
    {
        float health = unit.GetUnitHealth ();
        float currentHealth = unit.GetUnitCurrentHealth ();

        if ( currentHealth <= 0 )
            unit.MakeDeath ();
        if ( currentHealth >= health )
            unit.SetUnitCurrentHealth (health);
    }
}
