using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION { NORTH, SOUTH, EAST, WEST };

public class Direction {
    public static DIRECTION toDirection(string dir) {
        if (dir == "NORTH") return DIRECTION.NORTH;
        if (dir == "SOUTH") return DIRECTION.SOUTH;
        if (dir == "EAST") return DIRECTION.EAST;
        if (dir == "WEST") return DIRECTION.WEST;
        throw new ArgumentException("[ Class : Direction / Method : toDirection ] dir string argument is wrong");
    }

    public static string toString(DIRECTION dir) {
        if (dir == DIRECTION.NORTH) return "NORTH";
        if (dir == DIRECTION.SOUTH) return "SOUTH";
        if (dir == DIRECTION.EAST) return "EAST";
        return "WEST";
    }

    public static Vector2 toMoveDirection(DIRECTION dir) {
        if (dir == DIRECTION.NORTH) return Vector2.down;
        if (dir == DIRECTION.SOUTH) return Vector2.up;
        if (dir == DIRECTION.EAST) return Vector2.left;
        return Vector2.right;
    }

    public static Vector2 toSpawnPosition(DIRECTION dir) {
        return NoteGenerator.spawnPoints[dir];
    }
}
