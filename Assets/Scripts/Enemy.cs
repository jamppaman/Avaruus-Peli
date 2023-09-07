using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager manager;

    void Start()
    {
        manager = GetComponentInParent<EnemyManager>();

    }

    public void Die() 
    {
        manager.enemyList.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
