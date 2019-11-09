﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public List<GameObject> objects;
    public LevelManager lvlManager;

    public int nbTrashSpawned;

    public float minPosX;
    public float minPosZ;
    public float maxPosX;
    public float maxPosZ;

    public float ejectionDistance;
    public float ejectionDistanceMultiplicator;

    public float maxTrashEjectionTime;
    public float minTrashEjectionTime;
    public float ejectionTimer;

    // Start is called before the first frame update
    void Start()
    {
        lvlManager.totalNbTrash += nbTrashSpawned;
        SpawnInitialTrash();
        ejectionTimer = maxTrashEjectionTime;
    }

    
    // Update is called once per frame
    void Update()
    {
        ejectionTimer -= Time.deltaTime;
        if(ejectionTimer<0)
        {
            SpawnTrash();
        }
    }

    private void SpawnInitialTrash()
    {
        for(int i = 0;i<nbTrashSpawned;i++)
        {
            GameObject go = Instantiate(objects[UnityEngine.Random.Range(0, objects.Count)]);
            go.transform.position = new Vector3(UnityEngine.Random.Range(minPosX, maxPosX), 3,UnityEngine.Random.Range(minPosZ, maxPosZ));
            float angle = UnityEngine.Random.Range(-Mathf.PI, Mathf.PI);
            go.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Cos(angle)*ejectionDistance, ejectionDistance,  Mathf.Sin(angle)*ejectionDistance),ForceMode.Impulse);
        }
    }

    private void SpawnTrash()
    {
        ejectionTimer = UnityEngine.Random.Range(minTrashEjectionTime, maxTrashEjectionTime);
        GameObject go = Instantiate(objects[UnityEngine.Random.Range(0, objects.Count)]);
        go.transform.position = gameObject.transform.position;
        float angle = UnityEngine.Random.Range(-Mathf.PI, Mathf.PI);
        go.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Cos(angle) * ejectionDistance * ejectionDistanceMultiplicator, ejectionDistance*ejectionDistanceMultiplicator, Mathf.Sin(angle) * ejectionDistance*ejectionDistanceMultiplicator), ForceMode.Impulse);
        nbTrashSpawned++;
        lvlManager.totalNbTrash++;
    }
}
