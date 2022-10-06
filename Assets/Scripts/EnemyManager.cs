using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

    public static EnemyManager instance;
    //this makes it so that we can access this script from anwyehere easily

    void Awake ()
    {
        instance = this;
    }

    public void OnPlayerMove()
    {
        StartCoroutine(MoveEnemies());
    }

    IEnumerator MoveEnemies()
    {
        yield return new WaitForFixedUpdate();

        foreach(Enemy enemy in enemies)
        {
            if(enemy != null)
                enemy.Move();
        }
    }

}
