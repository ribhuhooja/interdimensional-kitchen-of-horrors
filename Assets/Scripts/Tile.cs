using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public int X { get; private set; }
    public int Y { get; private set; }
    
    public bool Spawnable { get; private set; }

    public Vector2Int Coords {
        get => new Vector2Int(X, Y);
    }

    private Ground ground;

    // the object the tile, if it exists
    private Placeable objectOnTile;

    public void Initialize(int x, int y, Ground ground) {
        X = x;
        Y = y;
        Spawnable = true; // TODO: better starting config

        this.ground = ground;
        transform.localScale *= ground.Scale;
        SetPosition();
    }

    private void SetPosition() {
        // if the index-coordinates of the tile are x, y
        // then the top-left corner of the tile will be at coordinate
        // (TOP_LEFT_X + x * tileWidth, TOP_LEFT_Y - y * tileWidth
        Vector2 coords = new(ground.TopLeftCorner.x + X * ground.TileWidth, ground.TopLeftCorner.y - Y * ground.TileHeight);
        transform.position = new(coords.x, coords.y, 0);
    }

    public Placeable GetObjectOnTile()
    {
        return objectOnTile;
    }

    public bool HasObjectOnTile()
    {
        return objectOnTile != null;
    }

    private void SetObjectOnTile(Placeable newObject)
    {
        objectOnTile = newObject;
        objectOnTile.UpdateLocation(this);
    }

    public bool MoveObjectOnTileTo(Tile other)
    {
        if (other.GetObjectOnTile())
        {
            return false;
        }
        
        other.SetObjectOnTile(objectOnTile);
        objectOnTile = null;

        return true;
    }

    public void SpawnObjectOnTile(Placeable obj)
    {
        if (!Spawnable || HasObjectOnTile())
        {
            return;
        }

        objectOnTile = obj;
    }

    public void DestroyObjectOnTile()
    {
        Debug.Log(objectOnTile);
        Destroy(objectOnTile.gameObject);
        objectOnTile = null;
    }
    
}

