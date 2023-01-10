using UnityEngine;
using System.Collections.Generic;
public class CityGenerator : MonoBehaviour
{
    public int buildingAmount;
    public GameObject grassPatchPrefab;

    //RANDOMLY DISABLES BUILDINGS TO MAKE SPACE 1 - ALL BUILDINGS SPAWNED I.E. 0.1 - ONLY 10% SPAWN;
    public float percentageOfBuildingsToSpawn = 1;
    public GameObject[] housesPrefabs;
    public GameObject[] highRisePrefabs;
    public GameObject[] roofs;
    public GameObject[] roofsDeco;
    //SPAWN ONLY SELECTABLE HOUSES
    public bool onlyHouses; 

    void Start()
    {
        // SpawnCity();
    }
   
    [ContextMenu("Generate city")]
    private void SpawnCity()
    {
        float spacing = 2f;

        int counterX = 0;
        int counterZ = 0;

        for (int x = 0; x < buildingAmount; x++)
        {
            //SET COUNTER FOR ROW
            counterX++;

            //IGNORE EVERY 6TH AND 7TH ROW TO MAKE SPACE FOR ROADS
            if (counterX == 5 || counterX == 6)
                continue;

            //CLAMP 
            if (counterX == 7) counterX = 1;

            //FOR EACH COLUMN
            for (int z = 0; z < buildingAmount; z++)
            {
                //SET COUNTER FOR COLUMN

                counterZ++;

                //IGNORE EVERY 6TH AND 7TH ROW TO MAKE SPACE FOR ROADS
                if (counterZ == 5 || counterZ == 6)
                    continue;

                //CLAMP 
                if (counterZ == 7) counterZ = 1;

                if (Random.value > percentageOfBuildingsToSpawn)
                    continue;

                //SET NEW POSITION
                Vector3 pos = new Vector3(x * spacing, 0, z * spacing);

                //SIMULATE DOWNTOWN (SCALE HIGHER)
                if (x > buildingAmount / 3f && x < buildingAmount / 3f * 2 && z > buildingAmount / 3f && z < buildingAmount / 3f * 2 && !onlyHouses)
                {
                    //HIGH RISE
                    SpawnHighRise(pos);
                }
                else
                {
                    SpawnHouse(pos);
                }
            }
        }

        //SPAWN GRASS PATCH BELOW EACH BULDING CLUSTER
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3 pos = new Vector3(3 + (i * spacing * 4.5f) + 3 * i, -0.45f, 3 + (j * spacing * 4.5f) + 3 * j);
                GameObject go = Instantiate(grassPatchPrefab, pos, Quaternion.identity) as GameObject;
                go.transform.parent = transform;
            }
        }
    }
    private void SpawnHouse(Vector3 pos)
    {
        //MOVE POSITION RANDOMLY 
        Vector2 randomOffset = Random.insideUnitCircle / 5f;
        pos.x += randomOffset.x;
        pos.z += randomOffset.y;

        //SPAWN RANDOM HOUSE MESH 
        GameObject go = Instantiate(housesPrefabs[Random.Range(0, housesPrefabs.Length)], pos, Quaternion.identity) as GameObject;
        go.transform.localScale += Vector3.one * Random.value / 3f;

        //ROTATE RANDOM HOUSES
        if (Random.value > 0.7f)
        {
            Vector3 rot = go.transform.localEulerAngles;
            rot.y += 90f;
            go.transform.localEulerAngles = rot;

        }
        go.transform.parent = transform;
    }
    private void SpawnHighRise(Vector3 pos)
    {
        //GENERATE SOME PRESETS
        int floors = Random.Range(4, 8);
        int type = Random.Range(0, highRisePrefabs.Length);

        //GET SCALE INCREASE
        Vector3 scaleIncrease = new Vector3(Random.value / 3f, 0, Random.value / 3f);

        for (int i = 0; i < floors; i++)
        {
            GameObject go = Instantiate(highRisePrefabs[type], pos + Vector3.up * i, Quaternion.identity) as GameObject;
            go.transform.parent = transform;
            go.transform.localScale += scaleIncrease;
        }

        //SPAWN ROOF
        GameObject goRoof = Instantiate(roofs[Random.Range(0, roofs.Length)], pos + Vector3.up * floors, Quaternion.identity) as GameObject;
        goRoof.transform.parent = transform;
        goRoof.transform.localScale += scaleIncrease;

        //SPAWN ROOF DECO
        if (Random.value > 0.5f)
        {
            Vector2 randomOffset = Random.insideUnitCircle / 5f;
            pos.x += randomOffset.x;
            pos.z += randomOffset.y;
            GameObject goRoofDeco = Instantiate(roofsDeco[Random.Range(0, roofsDeco.Length)], pos + Vector3.up * floors, Quaternion.identity) as GameObject;
            goRoofDeco.transform.parent = transform;
            goRoofDeco.transform.localScale += scaleIncrease;

            if (Random.value > 0.7f)
            {
                Vector3 rot = goRoofDeco.transform.localEulerAngles;
                rot.y += 90f;
                goRoofDeco.transform.localEulerAngles = rot;

            }
        }
    }
}
