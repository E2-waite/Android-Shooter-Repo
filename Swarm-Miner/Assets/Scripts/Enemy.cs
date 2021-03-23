using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Hive hive;
    public List<Vector2Int> path;
    public int pathInd = 0;
    public float maxHealth = 100, currentHealth, speed = .1f;
    public float chaseDist = 1;
    Player player;
    public float digRange = .5f, digDamage = 250;
    bool setup;
    public float pathTimer, pathCooldown = 1;
    bool returningHome = false;
    void Start()
    {
        pathTimer = pathCooldown;
        player = LevelController.Instance.player;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (setup)
        {
            if (player == null)
            {
                PathHome();
                FollowPath();
            }
            else
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance <= chaseDist)
                {
                    ChasePlayer();
                }
                else
                {

                    FollowPath();
                    if (pathTimer > 0)
                    {
                        pathTimer -= Time.deltaTime;
                    }
                    else
                    {
                        pathTimer = pathCooldown;
                        UpdatePath();
                    }
                }
            }
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
        if (player != null)
        {
            Vector2Int playerPos = new Vector2Int((int)(player.transform.position.x), (int)(player.transform.position.z));
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
    }

    void PathHome() // Create path to this enemy's hive if player is killed
    { 
        if (!returningHome)
        {
            Pathfinding pathfinding = new Pathfinding();
            List<Vector2Int> newPath = pathfinding.FindPath(LevelController.Instance.currentLevel.layout, false, new Vector2Int((int)transform.position.x, (int)transform.position.z),
                new Vector2Int((int)hive.transform.position.x, (int)hive.transform.position.z));
            if (newPath.Count > 0)
            {
                Debug.Log("PATHING HOME");
                path = newPath;
                pathInd = 0;
                returningHome = true;
            }
        }
    }

    void ChasePlayer() // Move directly towards player if in range
    {
        var lookPos = player.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;

        if (!DigTile())
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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

                if (returningHome || (!DigTile() && !BehindEnemy()))
                {
                    transform.position = Vector3.MoveTowards(transform.position, pathPos, speed * Time.deltaTime);
                }
            }
        }
    }

    bool BehindEnemy() // Check if another enemy AI is in front of this AI
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + (transform.forward * digRange), Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out hit, digRange))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }

    public void Hit(float damage) // Take damage
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            LevelController.Instance.DestroyEnemy(this);
        }
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Hit();
            LevelController.Instance.DestroyEnemy(this);
        }
    }

    bool DigTile()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + (transform.forward * digRange), Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out hit, digRange))
        {
            if (hit.collider.gameObject.CompareTag("Dirt"))
            {
                hit.collider.gameObject.GetComponent<Block>().Damage(digDamage * Time.deltaTime, false);
                return true;
            }
        }
        return false;
    }
}
