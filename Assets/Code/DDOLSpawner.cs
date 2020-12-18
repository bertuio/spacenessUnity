﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLSpawner : MonoBehaviour
{
    static DDOLSpawner spawner;

    [SerializeField]
    private List<GameObject> objectList;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (spawner)
        {
            Destroy(gameObject);
        }
        else
        {
            spawner = this;
            spawnObjects();
        }
    }

    public void spawnObjects() 
    {
        foreach (GameObject gameobject in objectList) 
        {
            Instantiate(gameobject);
        }
    } 
}