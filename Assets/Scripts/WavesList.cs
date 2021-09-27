using UnityEngine;

public class WavesList : MonoBehaviour
{
    [SerializeField]
    private UnitTemplate peasant, archer, spearman, horesman, swordsman, vigilante, inquisitor;

    private Wave [] waves;



    public Wave [] GetWavesList( int level )
    {
        waves = new Wave [3];
        for ( int i = 0; i < waves.Length; i++ )
        {
            waves [i] = new Wave ();
        }

        switch ( level )
        {
            case 1:
                waves [0].humans = new UnitTemplate [] { archer, peasant, peasant };
                waves [1].humans = new UnitTemplate [] { peasant, archer, peasant };
                waves [2].humans = new UnitTemplate [] { peasant, peasant, archer };
                break;
            case 2:
                waves [0].humans = new UnitTemplate [] { peasant, peasant, peasant };
                waves [1].humans = new UnitTemplate [] { peasant, peasant, peasant };
                waves [2].humans = new UnitTemplate [] { peasant, peasant, peasant };
                break;
            default:
                waves = null;
                break;
        }


        return waves;
    }
}
