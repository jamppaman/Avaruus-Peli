using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float lifeTime = 5f;
    public bool firedByPlayer = true;
    void Start()
    {
        if (firedByPlayer == false) 
        {
            bulletSpeed = bulletSpeed * -1f;
        }

    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && firedByPlayer == true) 
        {
            Enemy target = other.gameObject.GetComponent<Enemy>();
            target.Die();
            DestroyBullet();
        }
        if (other.gameObject.tag == "Player" && firedByPlayer == false)
        {
            Player target = other.gameObject.GetComponent<Player>();
            target.PlayerDie();
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        if(firedByPlayer == true) 
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            Player playerValues = playerObject.GetComponent<Player>();
            playerValues.fireRate++;
        }
        if(firedByPlayer == false) 
        {
            GameObject enemyObject = GameObject.FindWithTag("EnemyManager");
            EnemyManager enemyValues = enemyObject.GetComponent<EnemyManager>();
            enemyValues.enemyAmmoPool++;
        }

        Destroy(this.gameObject);
    }

}
