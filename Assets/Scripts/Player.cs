using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float playerSpeed = 20f;
    public int fireRate = 3;

    public GameObject bullet;
    public GameObject enemyManager;
    private float playerBoundary;
    void Start()
    {
       EnemyManager evil = enemyManager.GetComponent<EnemyManager>();
        playerBoundary = evil.boundaries;
    }
    void FixedUpdate()
    {

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            bool playerCheck = PlayerChecker(this.gameObject, true);
            if (playerCheck == true)
            {
                transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            bool playerCheck = PlayerChecker(this.gameObject, false);
            if (playerCheck == true)
            {
                transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            
            Shoot();
        }
    }
    void Shoot() 
    {
        if (fireRate > 0)
        {

            Transform firePosition = this.transform;
            Instantiate(bullet, new Vector3(firePosition.position.x, firePosition.position.y, firePosition.position.z), firePosition.rotation);
            fireRate--;
        }
    }
    private bool PlayerChecker(GameObject playerModel, bool goingLeft)
    {
        bool result = true;

            if (playerModel.transform.position.x > playerBoundary && goingLeft == false|| playerModel.transform.position.x < -playerBoundary && goingLeft == true)
            {
                result = false;
            }
        return result;
    }

    public void PlayerDie() 
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}
