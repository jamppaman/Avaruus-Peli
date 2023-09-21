using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
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

    public bool assaultInProgress = false;
    private float assaultTimer;
    private int picker = 0;
    void Start()
    {
        //SpawnEnemies();
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject enemySpawned = Instantiate(enemyPrefab, this.transform);
            enemySpawned.transform.position = new Vector3(enemySpacing * i, 31, 0);
            enemyList.Add(enemySpawned);
        }

        assaultTimer = Random.Range(0.5f, 2.4f);
    }

    void SpawnEnemies()
    {
        int posX = 0;
        int posY = 34;
        int limiter = 4;
        for (int i = 0; i < enemyAmount; i++)
        {
            if(posX >= limiter) 
            {
                limiter = limiter + 2;
                posX = posX - limiter;
                posY = posY - 4;
            }
            GameObject enemySpawned = Instantiate(enemyPrefab, this.transform);
            enemySpawned.transform.position = new Vector3(enemySpacing * posX, posY, 0);
            enemyList.Add(enemySpawned);
            posX++;
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
            if(assaultInProgress == false && assaultTimer <= 0f) 
            {
                //assaultInProgress = true;
                //picker = Random.Range(0, enemyList.Count);
                //Enemy temp = enemyList[picker].GetComponent<Enemy>();
                //temp.fleetPosition.Translate(enemyList[picker].transform.position);
                //Debug.Log(temp.fleetPosition);
                //temp.assaulting = true;
            }
            foreach(GameObject patrol in enemyList) 
            {
                Enemy enemyVal = patrol.GetComponent<Enemy>();
                if(enemyVal.assaulting == false)
                {
                    patrol.transform.Translate(Vector3.left * enemySpeed * sideMultiplier * Time.deltaTime);
                }
                else
                {
                    //patrol.transform.Translate(Vector3.down * enemyVal.assaultSpeed * Time.deltaTime);
                    //enemyVal.fleetPosition.Translate( Vector3.left * enemySpeed * sideMultiplier *Time.deltaTime);
                    //if(patrol.transform.position.y <= 0)
                    //{
                    //    enemyVal.assaulting = false;
                    //    assaultInProgress = false;
                    //    assaultTimer = Random.Range(0.5f, 2.4f);

                    //    Debug.Log(enemyVal.fleetPosition);
                    //    patrol.transform.Translate(enemyVal.fleetPosition.transform.position);
                    //}
                }

            }
        }
        assaultTimer -= Time.deltaTime;
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