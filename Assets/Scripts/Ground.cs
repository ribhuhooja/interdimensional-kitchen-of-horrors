using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Cauldron cauldronPrefab;
    private Player player;

    private Tile[,] grid;

    public float Scale { get; private set; }


    private void Awake()
    {
        numrows = initialGameTiles.numrows;
        numcols = initialGameTiles.numcols;
        
        Scale = scale;

        grid = new Tile[numcols, numrows];

        for (int i = 0; i < numcols; ++i) {
            for (int j = 0; j < numrows; ++j) {
                Tile tile = Instantiate(tilePrefab, transform);
                tile.Initialize(i, j, this);
                grid[i, j] = tile;
            }
        }

        player = Instantiate(playerPrefab);
        player.Initialize(this, initialGameTiles.playerSpawnLocation, scale);
        
        //TODO: where do I actually store all these placeable objects?
        Cauldron cauldron = Instantiate(cauldronPrefab, transform);
        cauldron.Initialize(this, initialGameTiles.cauldronLocation, scale);
        
    }

    public Tile getTile(int x, int y)
    {
        return grid[x, y];
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
