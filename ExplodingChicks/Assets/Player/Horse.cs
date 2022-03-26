using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Horse : PlayerController {
    public override string GetName() {
        return "Horse";
    }
    public override bool CanMove(Vector3Int target) {

        var dist = Vector3Int.Distance(target, GridPos());
        
        int dist_x = Mathf.Abs(target.x - GridPos().x);
        int dist_y = Mathf.Abs(target.y - GridPos().y);

        return (dist_x == 1 && dist_y == 2) || (dist_x == 2 && dist_y == 1);
    }
}