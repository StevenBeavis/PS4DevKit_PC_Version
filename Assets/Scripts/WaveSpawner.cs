using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Transform enemyPrefab;
        public int count;
        public float spawnRate;         
    }

    private SpawnState state = SpawnState.COUNTING;

    public Wave[] waves;
    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;

    public Text waveNumber;
    
    private int nextWave = 0;
    private float countdown; 
    private float searchCountdown = 1f;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.Log("no spawn points available");
        }

        countdown = timeBetweenWaves;        
    }

    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!EnemyState())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (countdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("spawning wave: " + _wave.waveName);

        waveNumber.text = "Wave: " + _wave.waveName;

        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);

            yield return new WaitForSeconds(1 / _wave.spawnRate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("spawning" + _enemy.name);
        
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(_enemy, _sp.position, _sp.rotation);        
    }

    void WaveCompleted()
    {
        Debug.Log("wave completed");

        state = SpawnState.COUNTING;

        countdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;

            Debug.Log("all waves completed, looping");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyState()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }       
        return true;
    }
}
