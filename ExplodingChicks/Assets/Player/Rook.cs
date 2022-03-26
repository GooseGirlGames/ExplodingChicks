using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Rook : Basic {
    public override string GetName() {
        return "Rook";
    }
    public override bool CanMove(Vector3Int target) {

        var dist = Vector3Int.Distance(target, GridPos());
        
        int dist_x = target.x - GridPos().x;
        int dist_y = target.y - GridPos().y;

        return dist > 0 && (dist_x == 0 || dist_y == 0);
    }
}