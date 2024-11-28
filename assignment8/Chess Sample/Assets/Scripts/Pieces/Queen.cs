using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override MoveInfo[] GetMoves()
    {
        // --- TODO ---
        return new MoveInfo[]
              {

                new MoveInfo(1, 0, 8),
                new MoveInfo(-1, 0, 8),
                new MoveInfo(0, 1, 8),
                new MoveInfo(0, -1, 8),

                new MoveInfo(1, 1, 8),   // 대각선 공격
                new MoveInfo(-1, 1, 8),
                 new MoveInfo(1, -1, 8),   // 대각선 공격
                new MoveInfo(-1, -1, 8)// 대각선 공격
              };
        // ------
    }
}