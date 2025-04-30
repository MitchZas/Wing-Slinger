using UnityEngine;
using System.Collections;
using System;

public class WingSpawn : MonoBehaviour
{
    public GameObject wingPrefab;
    public GameObject wingSpawnPoint;
    public float respawnTime = 1.0f;

    private float wing1x = 4.57f;
    private float wing1y = 3.59f;

    public bool isActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(WingMaker());
        //wing.SetActive(false);
        //isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnWing();
        }
    }

    private IEnumerator WingMaker()
    {
        yield return new WaitForSeconds(respawnTime);
        //wingItem.SetActive(true);
        isActive = true;
    }

    public void SpawnWing()
    {
        GameObject w = Instantiate(wingPrefab) as GameObject;
        w.transform.position = new Vector2(wing1x, wing1y);
    }
}
