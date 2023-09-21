using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultPaths : MonoBehaviour
{
    EnemyManager eManager;
    int currentPattern;
    void Start()
    {
        eManager = GetComponent<EnemyManager>();
    }

    public void SetAttackParameters()
    {
        currentPattern = Random.Range(0,3);
    }
    public Vector3 AssaultPattern(int patternNum, GameObject mover)
    {
        Vector3 returnPos = new Vector3 (0,0,0);
        Enemy attackerVal = mover.GetComponent<Enemy>();

        switch (patternNum)
        {
            // Straight down
            case 0:
                attackerVal.fleetPosition.y = attackerVal.fleetPosition.y - Time.deltaTime * eManager.assaultSpeed;
                break;

            // dodge and weave
            case 1:
                attackerVal.fleetPosition.y = attackerVal.fleetPosition.y - Time.deltaTime * eManager.assaultSpeed;
                attackerVal.fleetPosition.x = Mathf.Sin(attackerVal.fleetPosition.y) * 10f;
                break;

            // Ympyrä >:(
            case 2:

                break;

            //wacky
            case 3:

                break;

            default:
                attackerVal.fleetPosition.y = attackerVal.fleetPosition.y - Time.deltaTime * eManager.assaultSpeed;
                break;
        }

        return returnPos;
    }
}


/*
 * 
 * Pyörylä:

void circle()

{

int i;

float x;

float y;

float a;

y = 100; // radius

x = 0;

a= 0.01; // resolution


for(i = 0;i < 627;i++) // 627 == (2 * Pi / a ) - 1

{

x += y*0.01;

y -= x*0.01;

// liike tähän

}

}

------------------------------------------------
Pii:
     
float perimeter = 2.0f * Mathf.PI * radius;

 * 
 */