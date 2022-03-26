using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class PlayerController : MonoBehaviour {
    
    private int id = -1;
    private Vector3Int? nextPosition = null;
    public abstract string GetName();
    public abstract bool CanMove(Vector3Int target);
    public abstract bool LastTurn();

    void Awake() {
        id = GameManager.Instance.RegisterPlayer(this);
        AlignToGrid();
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

    public void SetNextPos(Vector3Int? pos) {
        nextPosition = pos;
    }
    public void Move() {
        if (nextPosition is Vector3Int pos) {
            // TODO interaction & stuff
            SetGridPos(pos);
        }
    }

}
