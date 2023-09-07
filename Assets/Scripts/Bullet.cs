using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float lifeTime = 5f;
    void Start()
    {

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

    void DestroyBullet()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        Player playerValues = playerObject.GetComponent<Player>();

        Destroy(this.gameObject);
    }

}
