using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;


public class GameController : MonoBehaviour
{


    [Tooltip("GameObject enemy.")]
    public GameObject enemyObject;
    [Tooltip("GameObject battery.")]
    public GameObject batteryObject;

    private double percentBattery;
    [Tooltip("Battery text.")]
    public Text txtBattery;

    [Tooltip("Time during game.")]
    public float seconds;
    public float minuts;
    public float horus;
    public Text txtTimeGame;

    [Tooltip("An array of the spawn points this objects can spawn from.")]
    public Transform[] spawnPoints;

    [Header("Gameplay configuration")]
    [Tooltip("Time between spawn in seconds")]
    [Range(0.2F, 1.5F)]
    public float timeBetweenSpawnEnemyInSeconds;
    [Range(1.0F, 15.0F)]
    public float timeBetweenSpawnBatteryInSeconds;


    private Coroutine spawnerRoutineFood;
    private Coroutine spawnerRoutineBattery;
    private Coroutine baterryRoutine;

    void Start()
    {
        percentBattery = 100;
        spawnerRoutineFood = StartCoroutine(EnemySpawnRoutine());
        spawnerRoutineBattery = StartCoroutine(BatterySpawnRoutine());
        baterryRoutine = StartCoroutine(BaterryRoutine());
        
    }

    private void Update()
    {
        UpTime();
    }

    private void SpawnTime()
    {
        if (seconds <= 10 && minuts == 0)
            timeBetweenSpawnEnemyInSeconds = 1.0f;
        else if (seconds > 10 && seconds <= 45 && minuts == 0)
            timeBetweenSpawnEnemyInSeconds = 0.8f;
        else if (seconds <= 10 && minuts == 1)
            timeBetweenSpawnEnemyInSeconds = 0.5f;
        else if (seconds > 10 && seconds <= 45 && minuts < 3 && minuts > 1)
            timeBetweenSpawnEnemyInSeconds = 0.3f;
        else
            timeBetweenSpawnEnemyInSeconds = 0.2f;
    }

    void OnDestroy()
    {
        StopCoroutine(spawnerRoutineFood);
        StopCoroutine(baterryRoutine);
    }

    public void DownBattery(double value)
    {        
        percentBattery = percentBattery - value;
        txtBattery.text = "Power: " + percentBattery.ToString("N1") + "%";
    }

    public void UpBattery(double value)
    {
        percentBattery = percentBattery + value;
        txtBattery.text = "Power: " + percentBattery.ToString("N1") + "%";
    }

    public void UpTime()
    {
        seconds += 1 * Time.deltaTime;
        if(seconds > 60)
        {
            seconds = 0;
            minuts++;
            if (minuts > 60)
            {
                minuts = 0;
                horus++;
            }

        }
        txtTimeGame.text = Math.Floor(minuts).ToString() + ":" + Math.Floor(seconds).ToString();
    }

    private void SpawnRandomEnemy()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Instantiate(enemyObject, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    private void SpawnRandomBattery()
    {
        int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        Instantiate(batteryObject, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            SpawnRandomEnemy();
            yield return new WaitForSeconds(timeBetweenSpawnEnemyInSeconds);
        }
    }

    private IEnumerator BatterySpawnRoutine()
    {
        while (true)
        {
            SpawnRandomBattery();
            yield return new WaitForSeconds(timeBetweenSpawnBatteryInSeconds);
        }
    }

    private IEnumerator BaterryRoutine()
    {
        while (true)
        {
            DownBattery(0.1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
