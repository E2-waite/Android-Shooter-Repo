using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Hive hive;
    public List<Vector2Int> path;
    public int pathInd = 0;
    public float maxHealth = 100, currentHealth, speed = .1f;
    GameObject player;
    public float digRange = 1, digDamage = 250;
    bool setup;
    void Start()
    {
        player = LevelController.Instance.player;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (setup)
        {
            UpdatePath();
            FollowPath();
        }
    }

    public void Setup(List<Vector2Int> startPath, Hive parentHive)
    {
        player = LevelController.Instance.player;
        path = startPath;
        hive = parentHive;
        setup = true;
    }

    void UpdatePath()
    {
        Vector2Int playerPos = new Vector2Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.z));
        if (path.Count == 0 || playerPos != path[path.Count - 1])
        {
            List<Vector2Int> newPath = LevelController.Instance.PathToPlayer(new Vector2Int((int)transform.position.x, (int)transform.position.z));
            if (newPath.Count > 0)
            {
                path = newPath;
                pathInd = 0;
            }
        }
    }

    void FollowPath()
    {
        if (path.Count > 0)
        {
            if (transform.position == new Vector3(path[pathInd].x, 0, path[pathInd].y))
            {
                if (pathInd < path.Count - 1)
                {
                    pathInd++;
                }
            }
            else
            {
                Vector3 pathPos = new Vector3(path[pathInd].x, 0, path[pathInd].y);
                var lookPos = pathPos - transform.position;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = rotation;

                if (!DigTile())
                {
                    transform.position = Vector3.MoveTowards(transform.position, pathPos, speed * Time.deltaTime);
                }
            }
        }
    }

    public void Hit(float damage)
    {
        currentHealth -= damage * Time.deltaTime;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Hit(10);
            Destroy(gameObject);
        }
    }

    bool DigTile()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, digRange))
        {
            if (hit.collider.gameObject.CompareTag("Dirt"))
            {
                hit.collider.gameObject.GetComponent<Block>().Damage(digDamage * Time.deltaTime);
                return true;
            }
        }
        return false;
    }
}
