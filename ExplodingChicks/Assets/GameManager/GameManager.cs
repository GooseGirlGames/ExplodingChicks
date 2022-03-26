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

    
    private List<Vector3Int> highlightedTiles = new List<Vector3Int>();
    private void MouseLook() {
        var pointerWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int pointer = map.WorldToCell(pointerWorld);
        pointer.z = 0;

        if (highlightedTiles.Contains(pointer)) {
            return;
        }
        ClearHighlightedTiles();

        if (!activePlayer.CanMove(pointer)) {
            return;
        }

        map.SetTileFlags(pointer, TileFlags.None);
        map.SetColor(pointer, Color.blue);

        highlightedTiles.Add(pointer);
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
        MouseLook();
        if (Input.GetButtonDown("Fire1") && highlightedTiles.Count > 0) {
            activePlayer.SetGridPos(highlightedTiles[0]);
            ClearHighlightedTiles();
        }
    }

    public int RegisterPlayer(PlayerController p) {
        activePlayer = p;
        playerControllers.Add(p);
        int id = playerControllers.FindIndex(o => o == p);
        Debug.Log("Registered Player #" + id);
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
