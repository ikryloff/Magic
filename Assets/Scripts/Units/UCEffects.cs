using UnityEngine;

public class UCEffects : MonoBehaviour
{
    protected GameObject _deathParticle;
    protected GameObject _bornParticle;
    private ParticleSystem _impactParticle;


    public void Init( UnitTemplate unitTemplate )
    {
        SetImpact (unitTemplate);
        _deathParticle = unitTemplate.deathPrefab;
        _bornParticle = unitTemplate.bornPrefab;
        ShowBorn ();
    }

    public void ShowBorn()
    {
        if ( _bornParticle != null )
            Instantiate (_bornParticle, transform.position, Quaternion.identity);
    }

    public void SetImpact( UnitTemplate template )
    {
        if ( template.impactPrefab != null )
        {
            GameObject imp = Instantiate (template.impactPrefab, transform.position, Quaternion.identity);
            imp.transform.parent = transform;
            _impactParticle = imp.GetComponent<ParticleSystem> ();
        }
    }
    public void ShowHit()
    {
        _impactParticle.Play ();
    }

    public void ShowDeath()
    {
        Instantiate (_deathParticle, transform.position, Quaternion.identity);
    }
}
