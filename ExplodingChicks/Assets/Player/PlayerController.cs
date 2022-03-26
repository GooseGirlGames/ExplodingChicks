using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Tilemap map;
    
    private List<Vector3Int> coloredTiles = new List<Vector3Int>();

    // Start is called before the first frame update
    void Start() {
        AlignToGrid();
    }

    // Update is called once per frame
    void Update() {
        MouseLook();
        if (Input.GetButtonDown("Fire1") && coloredTiles.Count > 0) {
            transform.position = map.CellToWorld(coloredTiles[0]);
            ClearColoredTiles();
        }
    }

    void MouseLook() {
        var pointerWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int pointer = map.WorldToCell(pointerWorld);
        pointer.z = 0;

        if (coloredTiles.Contains(pointer)) {
            return;
        }
        ClearColoredTiles();

        var dist = Vector3Int.Distance(pointer, GridPos());
        if (dist == 0 || dist > 1.5) {
            return;
        }

        map.SetTileFlags(pointer, TileFlags.None);
        map.SetColor(pointer, Color.blue);

        coloredTiles.Add(pointer);
    }

    public Vector3Int GridPos() {
        return map.WorldToCell(transform.position);
    }
    private void SetGridPos(Vector3Int pos) {
        transform.position = map.CellToWorld(pos);
    }

    private void AlignToGrid() {
        var pos = GridPos();
        SetGridPos(pos);
    }

    private void ClearColoredTiles() {
        foreach (var pos in coloredTiles) {
            map.SetColor(pos, Color.white);
        }
        coloredTiles.Clear();
    }
}
