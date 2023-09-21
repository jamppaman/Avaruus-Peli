using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Spawnaus variablet
    public int enemyAmount = 3;
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();
    public int enemySpacing = 7;
    public int enemiesPerRow = 4;
    public bool extendingRows = false;
    public bool fillTillRowFull = false;

    // Liike Variablet
    public bool enemySide = false;
    public float enemySpeed = 10f;
    private float sideMultiplier = 1f;
    public float boundaries = 35f;

    // Assault variablet
    public bool assaultInProgress = false;
    public float assaultTimer;
    public float assaultSpeed = 10f;
    private int picker = 0; // valitun hyökkääjän numero
    public bool timeToReturn = false;
    private Enemy iAttack = null;

    // Ampumis variablet
    public int enemyAmmoPool = 3;

    void Start()
    {
        SpawnEnemies();
        assaultTimer = 2f;
    }

    void SpawnEnemies()
    {
        int limiter = enemiesPerRow;
        int enemiesSpawned = 0;
        int currentRow = 0;
        int rowAdder = 0;

        while(enemiesSpawned < enemyAmount)
        {
            for(int i = 0; i < limiter; i++)
            {
                GameObject enemySpawned = Instantiate(enemyPrefab, this.transform);
                enemySpawned.transform.position = new Vector3((enemySpacing * i - enemySpacing * rowAdder) - limiter/2, 31 + currentRow, 0);
                enemyList.Add(enemySpawned);

                enemiesSpawned++;
                if(fillTillRowFull == false && enemiesSpawned >= enemyAmount)
                {
                    break;
                }
            }
            currentRow -= 5;
            if (extendingRows == true)
            {
                limiter += 2;
                rowAdder++;
            }
        }
    }

    void FixedUpdate()
    {
        bool check = PositionChecker(enemyList);
        if (check == false) 
        {
            sideMultiplier = sideMultiplier * -1;
        }
        if(assaultInProgress == false && assaultTimer <= 0f) 
        {
            iAttack = AssaultStarter();
        }
        EnemyMover();
        assaultTimer -= Time.deltaTime;
    }

    private void EnemyMover()
    {
        foreach (GameObject patrol in enemyList)
        {
            Enemy enemyVal = patrol.GetComponent<Enemy>();
            if (enemyVal.assaulting == false)
            {
                patrol.transform.Translate(Vector3.left * enemySpeed * sideMultiplier * Time.deltaTime);
            }
            else
            {
                AssaultMover(patrol);
            }
        }
    }
    private Enemy AssaultStarter()
    {
        assaultInProgress = true;
        picker = Random.Range(0, enemyList.Count);
        Enemy temp = enemyList[picker].GetComponent<Enemy>();
        temp.fleetPosition = (enemyList[picker].transform.position);
        temp.returnPosition = new Vector3(temp.fleetPosition.x, temp.fleetPosition.y, temp.fleetPosition.z);
        temp.assaulting = true;
        timeToReturn = false;
        return temp;
    }
    private void AssaultMover(GameObject attacker)
    {
        Enemy attackerVal = attacker.GetComponent<Enemy>();
        attackerVal.fleetPosition = (attacker.transform.position);

        if(timeToReturn == false)
        {
            if (attackerVal.fleetPosition.y < -9f)
            {
                attackerVal.fleetPosition.y = 40f;
                timeToReturn = true;
                
            }
            attackerVal.fleetPosition.y = attackerVal.fleetPosition.y - Time.deltaTime * assaultSpeed;
            attackerVal.fleetPosition.x = Mathf.Sin(attackerVal.fleetPosition.y) * 10f;
            attacker.transform.position = attackerVal.fleetPosition;
        }
        else
        {
            float step = assaultSpeed* 2 * Time.deltaTime;
            attacker.transform.position = Vector3.MoveTowards(attackerVal.fleetPosition, attackerVal.returnPosition, step);
            float dis = Vector2.Distance(attackerVal.fleetPosition, attackerVal.returnPosition);
            if (Vector2.Distance(attackerVal.fleetPosition,attackerVal.returnPosition) < 0.2f)
            {
                attackerVal.assaulting = false;
                assaultInProgress = false;
                assaultTimer = Random.Range(0.5f, 2.4f);
                attacker.transform.position = attackerVal.returnPosition;
            }
        }
        attackerVal.returnPosition = attackerVal.returnPosition + (Vector3.left * enemySpeed * sideMultiplier * Time.deltaTime);
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