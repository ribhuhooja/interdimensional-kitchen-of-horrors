using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    [SerializeField] private int numrows;
    [SerializeField] private int numcols;

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

    private Tile[,] grid;

    public float Scale { get; private set; }


    private void Awake() {
        Scale = scale;

        grid = new Tile[numcols, numrows];

        for (int i = 0; i < numcols; ++i) {
            for (int j = 0; j < numrows; ++j) {
                Tile tile = Instantiate(tilePrefab);
                tile.Initialize(i, j, this);
            }
        }
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
