using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable: MonoBehaviour
{
    public Vector2Int Position { get; private set;  }
    private Ground ground;
    private Tile tileOn;

    public void Initialize(Ground ground, Vector2Int spawnLocation, float scale)
    {
        Position = spawnLocation;
        transform.localScale *= scale;
        this.ground = ground;

        tileOn = ground.getTile(Position.x, Position.y);
        transform.position = tileOn.transform.position;
    }

    public bool NaiveMoveTo(Tile tile)
    {
        bool moveSucceeded = tileOn.NaiveMoveObjectOnTile(tile);

        if (moveSucceeded)
        {
            tileOn = tile;
            return true;
        }

        return false;
    }
}
