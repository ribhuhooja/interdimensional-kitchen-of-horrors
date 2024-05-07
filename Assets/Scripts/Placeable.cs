using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A component that must be attached to any object that can be placed on the game grid
public class Placeable: MonoBehaviour
{
    public Vector2Int Position { get; private set;  }
    public Ground ground;
    public Tile tileOn;

    // whether this object can move when pushed by other things
    [SerializeField] private bool moveable;
    [SerializeField] private StoredPlaceable prefabForStorage;

    public bool IsStorable => prefabForStorage != null;
    
    public bool Moveable { get => moveable;
        private set => moveable = value;
    }
    public void Initialize(Ground ground, Vector2Int spawnLocation, float scale)
    {
        Position = spawnLocation;
        transform.localScale *= scale;
        this.ground = ground;

        tileOn = ground.GetTile(Position.x, Position.y);
        transform.position = tileOn.transform.position;
        tileOn.SpawnObjectOnTile(this);
    }

    public void UpdateLocation(Tile tile)
    {
        tileOn = tile;
        Position = tileOn.Coords;
        transform.position = tileOn.transform.position;
    }

    public StoredPlaceable PrefabForStorage()
    {
        return prefabForStorage;
    }
}
