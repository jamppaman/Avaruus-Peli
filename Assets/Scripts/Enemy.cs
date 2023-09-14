using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager manager;
    public GameObject enemyBullet;
    public GameObject playerTarget;

    private bool enemyReloading = false;
    public float reloadMax = 1.5f;
    public float reloadMin = 0.1f;
    private float reloadCurrent = 0f;
    private bool firingPrimed = false;
    void Start()
    {
        manager = GetComponentInParent<EnemyManager>();
        playerTarget = GameObject.FindWithTag("Player");
        enemyReloading = false;
        firingPrimed = false;
    }

    public void OpenFire()
    {
        if(enemyReloading == false && manager.enemyAmmoPool > 0)
        {
            reloadCurrent = Random.Range(reloadMax, reloadMin);
            firingPrimed = true;
            enemyReloading = true;
            manager.enemyAmmoPool--;
        }
    }

    void FixedUpdate()
    {
        CheckDistance();
        if(firingPrimed == true )
        {
            reloadCurrent -= Time.deltaTime;
            if(reloadCurrent <= 0) 
            {
                EnemyShoot();
                firingPrimed = false;
                enemyReloading = false;
            }
        }

    }

    public void EnemyShoot() 
    {
        Transform firePosition = this.transform;
        Instantiate(enemyBullet, new Vector3(firePosition.position.x, firePosition.position.y, firePosition.position.z), firePosition.rotation);
        //fireRate--;
    }
    
    public void CheckDistance()
    {
        Vector2 playerXPos = new Vector2(playerTarget.transform.position.x, 0);
        Vector2 enemyXPos = new Vector2(this.transform.position.x, 0);
        if (Vector2.Distance(playerXPos, enemyXPos) < 0.003f)
        {
            OpenFire();
        }
    }

    public void Die() 
    {
        manager.enemyList.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
