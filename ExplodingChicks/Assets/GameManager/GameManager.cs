using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;

    [SerializeField]
    private Tilemap map;

    private List<PlayerController> playerControllers = new List<PlayerController>();
    private PlayerController activePlayer = null;
    private int turn = 0;

    
    private List<Vector3Int> highlightedTiles = new List<Vector3Int>();
    private void HighlightCells(Vector3Int pointer) {

        if (highlightedTiles.Contains(pointer)) {
            //return;
        }
        ClearHighlightedTiles();

        if (!activePlayer.CanMove(pointer)) {
            //return;
        }


        var bounds = map.cellBounds;
        foreach (var pos in bounds.allPositionsWithin) {
            if (activePlayer.CanMove(pos)) {
                map.SetTileFlags(pos, TileFlags.None);
                map.SetColor(pos, Color.green);
            }
            highlightedTiles.Add(pos);
        }

        map.SetTileFlags(pointer, TileFlags.None);
        map.SetColor(pointer, Color.blue);
        highlightedTiles.Add(pointer);

    }

    public Vector3Int CellUnderPointer() {
        var pointerWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int pointer = map.WorldToCell(pointerWorld);
        pointer.z = 0;
        return pointer;
    }

    private void ClearHighlightedTiles() {
        foreach (var pos in highlightedTiles) {
            map.SetColor(pos, Color.white);
        }
        highlightedTiles.Clear();
    }


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    void Start() {
        
    }

    void Update() {
        var pointer = CellUnderPointer();
        HighlightCells(pointer);
        
        if (Input.GetButtonDown("Fire1") && activePlayer.CanMove(pointer)) {
            Debug.Log("Turn #" + turn);
            activePlayer.SetGridPos(pointer);
            ClearHighlightedTiles();

            ++turn;
        }
    }

    public int RegisterPlayer(PlayerController p) {
        activePlayer = p;
        playerControllers.Add(p);
        int id = playerControllers.FindIndex(o => o == p);
        Debug.Log("Registered Player #" + id + " of type " + p.GetName());
        HighlightCells(Vector3Int.zero);
        return id;
    }

    public Vector3Int GridPos(Vector3 worldPos) {
        return map.WorldToCell(worldPos);
    }
    public Vector3 WorldPos(Vector3Int gridPos) {
        return map.CellToWorld(gridPos);
    }
    public bool HasTile(Vector3Int gridPos) {
        return map.HasTile(gridPos);
    }
}
