using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Ground : MonoBehaviour {

    private int numrows;
    private int numcols;

    [SerializeField] private InitialGameTiles initialGameTiles;
    
    [SerializeField] private float gridWidth;
    [SerializeField] private float gridHeight;

    public Vector2 TopLeftCorner {
        get {
            float topLeftX = transform.position.x - scale * gridWidth / 2;
            float topLefty = transform.position.y + scale * gridHeight / 2;
            return new(topLeftX, topLefty);
        }
    }

    public float TileWidth {
        get {
            return scale * gridWidth / numcols;
        }
    }

    public float TileHeight {
        get {
            return scale * gridHeight / numrows;
        }
    }


    [SerializeField] private float scale;

    [SerializeField] private Tile tilePrefab;
    private Player player;
    private List<Placeable> placeables = new();

    private Tile[,] grid;

    public float Scale { get; private set; }


    private void Awake()
    {
        numrows = initialGameTiles.numrows;
        numcols = initialGameTiles.numcols;
        
        Scale = scale;

        grid = new Tile[numcols, numrows];

        InitializeGrid();
        InitializePlaceables();
    }

    private void InitializePlaceables()
    {
        for (int i = 0; i < initialGameTiles.placeablePrefabs.Length; i++)
        {
            Placeable prefab = initialGameTiles.placeablePrefabs[i];
            Vector2Int location = initialGameTiles.placeableLocations[i];
        
            Placeable instantiatedPlaceable = Instantiate(prefab);
            instantiatedPlaceable.Initialize(this, location, scale);
        
            if (instantiatedPlaceable.TryGetComponent<Player>(out Player player))
            {
                this.player = player;
            }
            
            placeables.Add(instantiatedPlaceable);
        }
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < numcols; ++i) {
            for (int j = 0; j < numrows; ++j) {
                Tile tile = Instantiate(tilePrefab, transform); 
                tile.Initialize(i, j, this);
                grid[i, j] = tile;
            }
        }
    }

    public Tile GetTile(int x, int y)
    {
        return grid[x, y];
    }

    public Tile LocateTileInDirection(Tile tile, Vector2Int direction)
    {
        int oldX = tile.X;
        int oldY = tile.Y;
        int newX = oldX + direction.x;
        int newY = oldY + direction.y;

        if (IsValidCoordinate(oldX, oldY) && IsValidCoordinate(newX, newY))
        {
            return grid[newX, newY];
        }

        return null;
    }

    private bool IsValidCoordinate(int x, int y)
    {
        return (x >= 0 && x < numcols) && (y >= 0 && y < numrows);
    }

    public bool AttemptMove(Tile tile, Vector2Int direction)
    {
        // if there is no object on the tile, do nothing
        // (there is nothing to move0
        if (!tile.HasObjectOnTile())
        {
            return false;
        }
        
        // validate the direction
        // only (+-1, 0) and (0, +-1) are allowed
        if (direction.x * direction.y != 0 && direction.sqrMagnitude != 1)
        {
            return false;
        }

        // check whether the tile being moved to actually exists
        Vector2Int next = tile.Coords + direction;
        if (!IsValidCoordinate(next.x, next.y))
        {
            return false;
        }

        // Does that tile have an object on it?
        // If not, just move to it and return true
        Tile nextTile = grid[next.x, next.y];
        if (!nextTile.HasObjectOnTile())
        {
            return tile.MoveObjectOnTileTo(nextTile);
        }
        
        // If here, then the next tile does have an object on it
        Placeable objectOnNextTile = nextTile.GetObjectOnTile();
        Placeable objectOnCurrentTile = tile.GetObjectOnTile();
        
        
        // If that object is a container that is still accepting items,
        // AND the current placeable can be stored,
        // store the current placeable inside the container
        if (objectOnNextTile.TryGetComponent<PlaceableContainer>(out var container)
        && container.IsAcceptingItems
        && objectOnCurrentTile.IsStorable)
        {
            container.AddPlaceableFromBoard(objectOnCurrentTile);
            return true;
        }
        
        // if that object is immovable, we can't move
        if (!objectOnNextTile.Moveable)
        {
            return false;
        }
        
        // if the object is moveable, try to move it
        // the result of this depends on what is up ahead in that
        // direction from that object
        bool success = AttemptMove(nextTile, direction);
        if (!success)
        {
            return false;
        }
        
        // if the move was successful the next tile is empty now
        return tile.MoveObjectOnTileTo(nextTile);
    }

    // can't directly call Destroy(placeable) whenever we want to do so
    // because that messes up ground's bookkeeping
    // Worse, it can cause a use-after-destroy exception
    public void DestroyPlaceable(Placeable placeable)
    {
        placeables.Remove(placeable);
        placeable.tileOn.DestroyObjectOnTile();
    }
}
