using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private EnemyController ec;
    private Wave [] waves;
    public WavesList wavesList;
    private SpawnPoints spawnPoints;

    private float timeBetweenWaves = 40f;
    private float timeBetweenCreeps = 10f;
    private float countdown = 2f;

    private int waveIndex = 0;

    private void Awake()
    {
        wavesList = GetComponent<WavesList>();
        waves = new Wave [3];
        spawnPoints = FindObjectOfType<SpawnPoints> ();
    }
    private void Start()
    {
       
        ec = ObjectsHolder.Instance.enemyController;
        waves = wavesList.GetWavesList (1);
    }

    private void Update()
    {
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
        for ( int i = 0; i < wave.creeps.Length; i++ )
        {
            SpawnHuman (wave.creeps [i]);
            countdown = timeBetweenWaves;
            yield return new WaitForSeconds (timeBetweenCreeps);
        }
        waveIndex++;

    }

    private void SpawnHuman( GameObject humanPrefab )
    {
        if ( humanPrefab == null )
        {
            throw new System.ArgumentNullException (nameof (humanPrefab));
        }

        int pos = Random.Range (1, 8);
        GameObject humanGO = Instantiate (humanPrefab, spawnPoints.GetRandomSpawnPoint().position, Quaternion.identity) as GameObject;
        Human newHuman = humanGO.GetComponent<Human> ();
        newHuman.SetLinePosition (pos);
        ec.humans.Add (newHuman);
        GameEvents.current.EnemyAppear ();
    }
}
