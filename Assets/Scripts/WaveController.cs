using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public static int HumanCount = 0;
    private Wave [] waves;
    public WavesList wavesList;
    private SpawnPoints spawnPoints;

    private float timeBetweenWaves = 20f;
    private float timeBetweenCreeps = 10f;
    private float countdown = 2f;

    private int waveIndex = 0;

    private bool isOn;

    private void Awake()
    {
        wavesList = GetComponent<WavesList> ();
        waves = new Wave [3];
        spawnPoints = FindObjectOfType<SpawnPoints> ();
    }
    public void Init(int level)
    {
        GameEvents.current.OnDieEvent += RemoveOneHuman;
        waves = wavesList.GetWavesList (level);
        isOn = true;
        HumanCount = waves.Length * waves [0].humans.Length;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnDieEvent -= RemoveOneHuman;
    }

    private void Update()
    {
        if ( !isOn ) return;
        if ( waves.Length == 0 )
            return;
        if ( waveIndex == waves.Length )
            enabled = false;
        if ( countdown <= 0 )
        {
            print ("NextWave");
            StartCoroutine (SpawnWave ());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves [waveIndex];
        print ("WI " + (waveIndex + 1));
        for ( int i = 0; i < wave.humans.Length; i++ )
        {
            SpawnHuman (wave.humans [i]);
            countdown = timeBetweenWaves;
            yield return new WaitForSeconds (timeBetweenCreeps);
        }
        waveIndex++;

    }

    private void SpawnHuman( UnitTemplate humanTemplate )
    {
        if ( humanTemplate == null )
        {
            throw new System.ArgumentNullException (nameof (humanTemplate));
        }

        int pos = Random.Range (1, 8);
        GameObject humanGO = Instantiate (humanTemplate.unitPrefab, spawnPoints.GetSpawnPoint(pos).position, Quaternion.identity) as GameObject;
        Human newHuman = humanGO.GetComponent<Human> ();
        newHuman.Activate (pos, humanTemplate);
        UnitsOnBoard.AddHumanToLineHumansList (newHuman, pos);
    }

    private void RemoveOneHuman(BoardUnit boardUnit)
    {
        if ( boardUnit.GetUnitType () != Unit.UnitType.Human ) return;
        HumanCount -= 1;
        Debug.Log ("Humans left " + HumanCount);
        if ( HumanCount == 0 )
            GameEvents.current.GameStateChangedAction (GameManager.GameState.LevelComplete);
    }
}
