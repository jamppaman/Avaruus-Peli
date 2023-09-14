using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemyAmount = 3;
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();
    public int enemySpacing = 7;
    public float boundaries = 35f;
    public int enemyAmmoPool = 3;

    public bool enemySide = false;
    public float enemySpeed = 10f;
    private float sideMultiplier = 1f;
    void Start()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject enemySpawned = Instantiate(enemyPrefab, this.transform);
            enemySpawned.transform.position = new Vector3(enemySpacing * i, 31, 0);
            enemyList.Add(enemySpawned);
        }
    }


    void FixedUpdate()
    {
        if (enemySide == false) 
        {
            bool check = PositionChecker(enemyList);
            if (check == false) 
            {
                sideMultiplier = sideMultiplier * -1;
            }
            foreach(GameObject patrol in enemyList) 
            {
                patrol.transform.Translate(Vector3.left * enemySpeed * sideMultiplier * Time.deltaTime);
            }
        }
    }

    private bool PositionChecker(List<GameObject> enemies) 
    {
        bool result = true;
        foreach(GameObject enemy in enemies) 
        {
            if (enemy.transform.position.x > boundaries || enemy.transform.position.x < -boundaries) 
            {
                result = false;
            }
        }
        return result;
    }
}
