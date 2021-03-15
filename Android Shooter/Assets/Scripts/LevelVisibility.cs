using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structure;
public class LevelVisibility
{
    public List<GameObject> CheckContinuous(GameObject[,] grid, int[,] layout, Vector2Int start)
    {
        List<Vector2Int> open_list = new List<Vector2Int>();
        List<Vector2Int> closed_list = new List<Vector2Int>();
        open_list.Add(start);

        // Loop until checks returns to start position
        while (open_list.Count > 0)
        {
            Vector2Int current = open_list[0];
            open_list.Remove(current);
            closed_list.Add(current);
            for (int i = 0; i < 4; i++)
            {
                Vector2Int position = PosFromDir(current, i);
                bool contains = false;
                foreach (Vector2Int pos in open_list)
                {
                    if (pos.x == position.x &&
                        pos.y == position.y)
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                {
                    foreach (Vector2Int pos in closed_list)
                    {
                        if (pos.x == position.x &&
                            pos.y == position.y)
                        {
                            contains = true;
                            break;
                        }
                    }
                }
                if (!contains && layout[position.x, position.y] == (int)Type.space)
                {
                    open_list.Add(position);
                }
            }
        }



        List<GameObject> visibleBlocks = new List<GameObject>();
        for (int i = 0; i < closed_list.Count; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector2Int pos = PosFromDir(closed_list[i], j);
                if ((layout[pos.x, pos.y] == (int)Type.dirt || layout[pos.x, pos.y] == (int)Type.stone) && !visibleBlocks.Contains(grid[pos.x, pos.y]))
                {
                    visibleBlocks.Add(grid[pos.x, pos.y]);
                }
            }
        }

        Debug.Log(visibleBlocks.Count.ToString());
        return visibleBlocks;
    }

    Vector2Int PosFromDir(Vector2Int pos, int dir)
    {
        // Get new pos from direction
        if (dir == 0)
        {
            pos.y += 1;
        }
        else if (dir == 1)
        {
            pos.x += 1;
        }
        else if (dir == 2)
        {
            pos.y -= 1;
        }
        else if (dir == 3)
        {
            pos.x -= 1;
        }
        return pos;
    }
}
