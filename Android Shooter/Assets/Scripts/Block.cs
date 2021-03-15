using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structure;
public class Block : MonoBehaviour
{
    public Type type;
    public bool destructable = false;
    public Mesh[] meshes = new Mesh[6];
    public float durability = 100;
    MeshFilter meshFilter;
    public int directions = 0;
    Vector2Int position;

    public void Damage(float val)
    {
        if (destructable)
        {
            durability -= val;
            if (durability <= 0)
            {
                LevelController.Instance.ClearTile(position);
            }
        }
    }

    enum BlockType : int
    {
        solid = 0,
        edge = 1,
        corner = 2,
        parallel = 3,
        dCorner = 4,
        surround = 5
    }

    public void SmoothTile(Dir dirs, Vector2Int pos, LevelController gen)
    {
        // Sets block's mesh and rotation based on adjascent tiles of the same type
        position = pos;
        meshFilter = GetComponent<MeshFilter>();
        directions = (int)dirs;
        BlockType type = BlockType.solid;
        float rot = 0;
        switch (dirs)
        {
            case Dir.N:
                {
                    type = BlockType.edge;
                    break;
                }
            case Dir.E:
                {
                    type = BlockType.edge;
                    rot = 90;
                    break;
                }
            case Dir.S:
                {
                    type = BlockType.edge;
                    rot = 180;
                    break;
                }
            case Dir.W:
                {
                    type = BlockType.edge;
                    rot = 270;
                    break;
                }
            case Dir.N | Dir.E:
                {
                    type = BlockType.corner;
                    break;
                }
            case Dir.E | Dir.S:
                {
                    type = BlockType.corner;
                    rot = 90;
                    break;
                }
            case Dir.S | Dir.W:
                {
                    type = BlockType.corner;
                    rot = 180;
                    break;
                }
            case Dir.W | Dir.N:
                {
                    type = BlockType.corner;
                    rot = 270;
                    break;
                }
            case Dir.N | Dir.S:
                {
                    type = BlockType.parallel;
                    break;
                }
            case Dir.E | Dir.W:
                {
                    type = BlockType.parallel;
                    rot = 90;
                    break;
                }
            case Dir.W | Dir.N | Dir.E:
                {
                    type = BlockType.dCorner;
                    break;
                }
            case Dir.N | Dir.E | Dir.S:
                {
                    type = BlockType.dCorner;
                    rot = 90;
                    break;
                }
            case Dir.E | Dir.S | Dir.W:
                {
                    type = BlockType.dCorner;
                    rot = 180;
                    break;
                }
            case Dir.S | Dir.W | Dir.N:
                {
                    type = BlockType.dCorner;
                    rot = 270;
                    break;
                }
            case Dir.N | Dir.E | Dir.S | Dir.W:
                {
                    type = BlockType.surround;
                    break;
                }
        }

        meshFilter.sharedMesh = meshes[(int)type];
        transform.rotation = Quaternion.Euler(0, rot, 0);
    }
}
