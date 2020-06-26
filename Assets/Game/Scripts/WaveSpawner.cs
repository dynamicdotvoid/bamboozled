﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{

    BuildManager build;
    Shop shop;

    [Header("Enemies")]
    public GameObject normalEnemy;
    public GameObject explosiveEnemy;
    public GameObject bigEnemy;

    [Header("Prefabs")]
    public Transform SpawnPoint;
    public TextMeshProUGUI waveSpawnerCountdown;
    public TextMeshProUGUI waveNumber;

    [Header("Attributes")]
    public float TimeBetweenWaves = 5f;
    public float waitTime = 0.5f;

    private int waveIndex = 0;
    private float countDown = 2f;
    private int wave = 0;

    private bool isstarted = false;
    public GameObject startButton;

    public GameObject[] spawnpoints;

    private void Start()
    {

        build = BuildManager.instance;
        shop = Shop.instance;

        startButton.SetActive(false);

    }

    void Update()
    {

        if (build.created == true)
        {

            startButton.SetActive(true);

        }

        waveNumber.text = wave.ToString();

        if (countDown <= 0f)
        {

            wave++;
            shop.balance += wave * 6;
            StartCoroutine(spawnWave());
            countDown = TimeBetweenWaves;

        }

        if (isstarted == true)
        {

            countDown -= Time.deltaTime;

        }

    }

    IEnumerator spawnWave()
    {

        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {

            spawnEnemy();
            yield return new WaitForSeconds(waitTime);

        }


    }

    void spawnEnemy()
    {

        float whichEnemy = Random.value;

        int whichSpawnPoint = Random.Range(0, 27);

        SpawnPoint = spawnpoints[whichSpawnPoint].transform;

        if (whichEnemy <= 0.10)
        {

            Instantiate(bigEnemy, SpawnPoint.transform.position, gameObject.transform.rotation);

        }
        if (whichEnemy <= 0.09)
        {

            Instantiate(explosiveEnemy, SpawnPoint.transform.position, gameObject.transform.rotation);

        }
        if (whichEnemy <= 0.81)
        {

            Instantiate(normalEnemy, SpawnPoint.transform.position, gameObject.transform.rotation);

        }

    }

    public void startTimer()
    {

        isstarted = true;
        Destroy(startButton);

    }

}