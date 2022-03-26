using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class PlayerController : MonoBehaviour {
    
    private int id = -1;


    void Start() {
        id = GameManager.Instance.RegisterPlayer(this);
        AlignToGrid();
    }


    public virtual bool CanMove(Vector3Int target) {
        var dist = Vector3Int.Distance(target, GridPos());

        return dist > 0 && dist < 1.5 && GameManager.Instance.HasTile(target);
    }

    public Vector3Int GridPos() {
        return GameManager.Instance.GridPos(transform.position);
    }
    public void SetGridPos(Vector3Int pos) {
        transform.position = GameManager.Instance.WorldPos(pos);
    }

    private void AlignToGrid() {
        var pos = GridPos();
        SetGridPos(pos);
    }

}
