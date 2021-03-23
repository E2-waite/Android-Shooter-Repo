using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Vector2Int> path;
    public float currentHealth, maxHealth = 1000;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Spawn();
        }
    }

    public void FindPath()
    {
        path = LevelController.Instance.PathToPlayer(new Vector2Int((int)transform.position.x, (int)transform.position.z));
    }



    public void Hit(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            LevelController.Instance.DestroyHive(this);
        }
    }

    public void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        LevelController.Instance.enemies.Add(enemy.GetComponent<Enemy>());
        LevelController.Instance.enemies[LevelController.Instance.enemies.Count - 1].Setup(LevelController.Instance.PathToPlayer(new Vector2Int((int)transform.position.x, (int)transform.position.z)), this);
    }


}
