using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject effectPrefab;
    private Transform effectParent;
    private List<GameObject> currentEffects = new List<GameObject>();

    public void Initialize(GameManager gameManager, GameObject effectPrefab, Transform effectParent)
    {
        this.gameManager = gameManager;
        this.effectPrefab = effectPrefab;
        this.effectParent = effectParent;
    }

    private bool TryMove(Piece piece, (int, int) targetPos, MoveInfo moveInfo)
    {
        // moveInfo의 distance만큼 direction을 이동시키며 이동이 가능한지를 체크
        // 보드에 있는지, 다른 piece에 의해 막히는지 등을 체크
        // 폰에 대한 예외 처리를 적용
        // --- TODO ---
        int dist = moveInfo.distance;
        int direcX = moveInfo.dirX;
        int direcY = moveInfo.dirY;

        bool isPawn = piece is Pawn;

        for (int i = 0; i < dist; i++)
        {
            int ny = piece.MyPos.Item2 + direcY * (i + 1);
            int nx = piece.MyPos.Item1 + direcX * (i + 1);

            if (!Utils.IsInBoard((nx, ny)))
                break;


            if (isPawn == false)
            {
                if (gameManager.Pieces[nx, ny] != null) //같은편 있는 곳은 못감
                {
                    if (gameManager.Pieces[nx, ny].PlayerDirection == piece.PlayerDirection)
                        break;
                    else
                    {
                        if (nx == targetPos.Item1 && ny == targetPos.Item2)
                            return true;
                        else
                            break;
                    }


                }
                else
                {
                    if (nx == targetPos.Item1 && ny == targetPos.Item2)
                        return true;  
                }
            }
            else
            {
                if(direcX != 0 &&  direcY != 0)
                {
                    if (nx == targetPos.Item1 && ny == targetPos.Item2
                            && gameManager.Pieces[nx, ny] != null
                                && gameManager.Pieces[nx, ny].PlayerDirection != piece.PlayerDirection)
                    {
                        return true;
                    }
                }
                else
                {
                    if(gameManager.Pieces[nx, ny] != null)
                    {
                        break;
                    }
                    else
                    {
                        if (nx == targetPos.Item1 && ny == targetPos.Item2)
                                return true;
                    }

                }
            }
       
            
                       

        }

        return false;
        // ------
    }

    private bool IsValidMoveWithoutCheck(Piece piece, (int, int) targetPos)
    {
        

        if (!Utils.IsInBoard(targetPos) || targetPos == piece.MyPos) return false;

       

        foreach (var moveInfo in piece.GetMoves())
        {
            if (TryMove(piece, targetPos, moveInfo))
            {  
                return true;
            }
        }
        
        return false;
    }

    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMoveWithoutCheck(piece, targetPos)) return false;

        // 체크 상태 검증을 위한 임시 이동
        var originalPiece = gameManager.Pieces[targetPos.Item1, targetPos.Item2];
        var originalPos = piece.MyPos;

        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = piece;
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = null;
        piece.MyPos = targetPos;

        bool isValid = !IsInCheck(piece.PlayerDirection);

        // 원상 복구
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = piece;
        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = originalPiece;
        piece.MyPos = originalPos;

        return isValid;
    }

    private bool IsInCheck(int playerDirection)
    {
        (int, int) kingPos = (-1, -1); // 왕의 위치
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var piece = gameManager.Pieces[x, y];
                if (piece is King && piece.PlayerDirection == playerDirection)
                {
                    kingPos = (x, y);
                    break;
                }
            }
            if (kingPos.Item1 != -1 && kingPos.Item2 != -1) break;
        }

        // 왕이 지금 체크 상태인지를 리턴
        // gameManager.Pieces에서 Piece들을 참조하여 움직임을 확인
        // --- TODO ---

        for (int i = 0; i < Utils.FieldWidth; i++) //우리팀 왕위치를 상대 말이 확인 할 수 있는지 확인
        {
            for (int j = 0; j < Utils.FieldHeight; j++)
            {
                var piece = gameManager.Pieces[i, j];
                if(piece != null  && piece.PlayerDirection != playerDirection)

                if (IsValidMoveWithoutCheck(piece,(kingPos.Item1, kingPos.Item2)))
                {

                        return true;
                }
            }

        }

        return false;
        // ------
    }

    public void ShowPossibleMoves(Piece piece)
    {
        ClearEffects();


        // 가능한 움직임을 표시
        // IsValidMove를 사용
        // effectPrefab을 effectParent의 자식으로 생성하고 위치를 적절히 설정
        // currentEffects에 effectPrefab을 추가
        // --- TODO ---

        if(piece == null) return;
       

        for (int i = 0; i < Utils.FieldWidth; i++)
        {
            for(int j = 0;j < Utils.FieldHeight; j++)
            {
                if (IsValidMove(piece, (i, j)))
                {
            
                    GameObject effetObj = Instantiate(effectPrefab, effectParent);

                    Vector3 setPos = Utils.ToRealPos((i, j));
               
                    effetObj.transform.position = new Vector3(setPos.x, setPos.y, 1); 
                    currentEffects.Add(effetObj);
                }
            }
            
        }
        


        // ------
    }

    public void ClearEffects()
    {
        foreach (var effect in currentEffects)
        {
            if (effect != null) Destroy(effect);
        }
        currentEffects.Clear();
    }
}