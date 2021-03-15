using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Vector2Int> path;
    public List<Enemy> enemies = new List<Enemy>();

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

    public void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemies.Add(enemy.GetComponent<Enemy>());
        enemies[enemies.Count - 1].Setup(LevelController.Instance.PathToPlayer(new Vector2Int((int)transform.position.x, (int)transform.position.z)), this);
    }
}
