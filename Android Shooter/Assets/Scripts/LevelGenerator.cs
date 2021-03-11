using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structure;
public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> tilePrefabs = new List<GameObject>();

    public class Tile
    {
        public GameObject[,] grid;
        public Vector2Int size;

        public Tile(ref GameObject[,] grid, ref int[,] layout, int offX, int offY, List<GameObject> prefabs)
        {
            Templates templates = new Templates();
            int tNum = Random.Range(0, templates.template.Count);
            Template template = templates.template[tNum];

            // Generate and add tiles to grid array based on templates
            for (int y = 0; y < Constants.TILESIZE; y++)
            {
                for (int x = 0; x < Constants.TILESIZE; x++)
                {
                    Vector2Int pos = new Vector2Int(x + (offX * Constants.TILESIZE), y + (offY * Constants.TILESIZE));

                    layout[pos.x, pos.y] = template.layout[x, y];

                    if (template.layout[x,y] == (int)Type.space)
                    {

                    }
                    if (template.layout[x, y] == (int)Type.dirt)
                    {
                        grid[pos.x, pos.y] = Instantiate(prefabs[(int)Type.dirt], new Vector3(pos.x, 0, pos.y), Quaternion.identity);
                    }
                    if (template.layout[x, y] == (int)Type.stone)
                    {
                        grid[pos.x, pos.y] = Instantiate(prefabs[(int)Type.stone], new Vector3(pos.x, 0, pos.y), Quaternion.identity);
                    }
                    Instantiate(prefabs[(int)Type.floor], new Vector3(pos.x, -1, pos.y), Quaternion.identity);
                }
            }
        }
    }

    public class Level
    {
        public GameObject[,] blocks;
        public int[,] layout;
        public Vector2Int size;
        int radius;

        public Level(int rad, List<GameObject> prefabs)
        {
            int tSize = (rad * 2) + 1;
            Tile[,] tiles = new Tile[tSize, tSize];
            size = new Vector2Int((Constants.TILESIZE * tSize), (Constants.TILESIZE * tSize));
            blocks = new GameObject[size.x, size.y];
            layout = new int[size.x, size.y];
            for (int y = 0; y < tSize; y++)
            {
                for (int x = 0; x < tSize; x++)
                {
                    tiles[x, y] = new Tile(ref blocks, ref layout, x, y, prefabs);
                }
            }

            MakeBorder(ref blocks, ref layout, prefabs);
        }

        void MakeBorder(ref GameObject[,] grid, ref int[,] layout, List<GameObject> prefabs)
        {
            // Surround level with stone blocks
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (x == 0 || y == 0 || x == grid.GetLength(0) - 1 || y == grid.GetLength(1) - 1)
                    {
                        layout[x, y] = 2;
                        Destroy(grid[x, y]);
                        grid[x, y] = Instantiate(prefabs[(int)Type.stone], new Vector3(x, 0, y), Quaternion.identity);
                    }
                }
            }
        }
    }

    public Vector2Int levelSize = new Vector2Int(10, 10);
    Level currentLevel;
    void Start()
    {
        SmoothTiles(currentLevel = new Level(10, tilePrefabs));
    }

    void SmoothTiles(Level level)
    {
        for (int y = 0; y < level.size.y; y++)
        {
            for (int x = 0; x < level.size.x; x++)
            {
                SmoothTile(x, y);
            }
        }
    }

    void SmoothTile(int x, int y)
    {
        // Smooth tile by checking for adjascent tiles of the same type
        if (currentLevel.layout[x, y] == (int)Type.dirt || currentLevel.layout[x, y] == (int)Type.stone)
        {
            bool[] dirs = new bool[4];
            int inc = 0;
            for (int i = 0; i < 4; i++)
            {
                if (CheckTile(currentLevel.layout, PosFromDir(new Vector2Int(x, y), i), (Type)currentLevel.layout[x, y]))
                {
                    dirs[inc] = true;
                }
                inc++;
            }
            PassDirs(new Vector2Int(x, y), dirs);
        }
    }

    public void ClearTile(Vector2Int pos)
    {
        // Destroy tile, update level layout and smooth adjascent tiles
        currentLevel.layout[pos.x, pos.y] = 0;
        for (int i = 0; i < 4; i++)
        {
            Vector2Int newPos = PosFromDir(pos, i);
            if (InGrid(newPos, currentLevel.size.x))
            {
                SmoothTile(newPos.x, newPos.y);
            }
        }
        Destroy(currentLevel.blocks[pos.x, pos.y]);
    }

    void PassDirs(Vector2Int pos, bool[] dirs)
    {
        // Pass matching directions to block tile to update to corresponding mesh
        Dir dir = 0;
        for (int i = 0; i < 4; i++)
        {
            if (dirs[i])
            {
                if (i == 0)
                {
                    dir = dir | Dir.N;
                }
                else if (i == 1)
                {
                    dir = dir | Dir.E;
                }
                else if (i == 2)
                {
                    dir = dir | Dir.S;
                }
                else if (i == 3)
                {
                    dir = dir | Dir.W;
                }
            }
        }

        currentLevel.blocks[pos.x, pos.y].GetComponent<Block>().SmoothTile(dir, pos, GetComponent<LevelGenerator>());
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

    bool InGrid(Vector2Int pos, float max)
    {
        // Check if position is within the level grid
        if (pos.x < 0 || pos.y < 0 || pos.x >= max || pos.y >= max)
        {
            return false;
        }
        return true;
    }

    bool CheckTile(int[,] layout, Vector2Int pos, Type type)
    {
        if (InGrid(pos, layout.GetLength(0)))
        {
            // Check if adjascent tile is in grid and is stone or empty tile
            if (type == Type.dirt && (layout[pos.x, pos.y] == (int)Type.space || layout[pos.x, pos.y] == (int)Type.stone))
            {
                return true;
            }

            // Check if adjascent tile is in grid and is dirt or empty tile
            if (type == Type.stone && (layout[pos.x, pos.y] == (int)Type.space || layout[pos.x, pos.y] == (int)Type.dirt))
            {
                return true;
            }
        }
        return false;
    }
}
