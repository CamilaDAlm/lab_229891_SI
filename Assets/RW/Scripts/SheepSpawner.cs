using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true;
    //public GameObject sheepPrefab;
    public List<GameObject> sheepPrefab = new List<GameObject>();
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;
    private List<GameObject> sheepList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void SpawnSheep()
    {
        int chooseSheep = Random.Range(0, sheepPrefab.Count);
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position; // 1
        GameObject sheep = Instantiate(sheepPrefab[chooseSheep], randomPosition, sheepPrefab[chooseSheep].transform.rotation);
        //GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); // 2
        sheepList.Add(sheep); 
        sheep.GetComponent<Sheep>().SetSpawner(this); // 4
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList) // 1
        {
            Destroy(sheep); // 2
        }

        sheepList.Clear();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
