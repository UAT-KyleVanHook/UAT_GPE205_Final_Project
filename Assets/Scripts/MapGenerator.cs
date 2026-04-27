using System;
using System.Collections.Generic;
using UnityEngine;


public enum MapType { Random, One, Two };
public class MapGenerator : MonoBehaviour
{
    [Header("Random Data")]
    public MapType mapType;


    [Header("Map Data")]
    public List<GameObject> maps;
    private GameObject selectedMapType;
    private GameObject currentMap;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the seed value
        //InitializeRandom();

        //TODO: Remove tihs. For testing
        //GenerateMap(); //<-- Remove THIS

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeRandom()
    {
        
   

    }

    

    public void GenerateMap()
    {

        if (mapType == MapType.Random)
        {
            if(maps.Count > 0)
            {
                selectedMapType = maps[UnityEngine.Random.Range(0, maps.Count)];
            }

        }
        else if (mapType == MapType.One)
        {
            if (maps.Count <= 1)
            {
                selectedMapType = maps[0];
            }
        }
        else if (mapType == MapType.Two)
        {
            if (maps.Count <= 2)
            {
                selectedMapType = maps[1];
            }
        }


        //Instatnitate the map
        currentMap = Instantiate<GameObject>(selectedMapType);

        //set position at zero
        currentMap.transform.position = Vector3.zero;

    }


    
    
    public void RemoveMap()
    { 

        Destroy(currentMap.gameObject);

    }
    
}
