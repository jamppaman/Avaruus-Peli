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
            DestroyPlayerBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && firedByPlayer == true) 
        {
            Enemy target = other.gameObject.GetComponent<Enemy>();
            target.Die();
            DestroyPlayerBullet();
        }
    }

    void DestroyPlayerBullet()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        Player playerValues = playerObject.GetComponent<Player>();
        playerValues.fireRate++;

        Destroy(this.gameObject);
    }

}
