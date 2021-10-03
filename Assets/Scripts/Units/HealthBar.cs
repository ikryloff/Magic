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
    private float _health;
    private float _currentHealth;

    private const string BAR_GO = "BarGO"; 
    private const string BAR = "Bar"; 
    private const string BAR_SPRITE = "BarSprite"; 

    public void Init(BoardUnit unit)
    {
        _unit = unit;
        _health = _unit.GetUnitTemplate().health;
        _currentHealth = _health;
        _barGo = transform.Find (BAR_GO).gameObject;
        _bar = _barGo.transform.Find (BAR);
        _sprite = _bar.Find (BAR_SPRITE).GetComponent<SpriteRenderer> ();
        _sprite.sortingOrder = 300;
        _barGo.SetActive (false);

        GameEvents.current.OnNewHit += TakeDamage;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnNewHit -= TakeDamage;
    }

    private void TakeDamage( BoardUnit unit, UnitTemplate damageTemplate )
    {
        if ( unit != _unit )
            return;
        _unit.SetHitState();
        ShowHealthBar (damageTemplate);
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


    private void ShowHealthBar( UnitTemplate damageTemplate )
    {
        float damage = damageTemplate.damage;
        ChangeUnitCurrentHealth (damage);
        CheckHP ();
        float ratio =  CalcRatio ();
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

    public void ChangeUnitCurrentHealth( float damage )
    {
        _currentHealth -= damage;
    }

    private float CalcRatio()
    {
        return _currentHealth/ _health;
    }

    private void CheckHP()
    {
        if ( _currentHealth <= 0 )
        {
            _unit.MakeDeath ();
            return;
        }
        if ( _currentHealth >= _health )
            _currentHealth = _health;
    }
}
