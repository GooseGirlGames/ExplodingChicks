using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Basic : PlayerController {
    public override string GetName() {
        return "Basic";
    }

    public override bool LastTurn() {
        return GameManager.Instance.CurrentTurn() == 5;
    }


    public override bool CanMove(Vector3Int target) {
        var dist = Vector3Int.Distance(target, GridPos());

        return dist > 0 && dist < 1.5 && GameManager.Instance.HasTile(target);
    }
}