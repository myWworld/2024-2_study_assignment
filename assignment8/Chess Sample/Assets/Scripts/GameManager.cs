using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject[] PiecePrefabs;
    public GameObject EffectPrefab;

    private Transform TileParent;
    private Transform PieceParent;
    private Transform EffectParent;
    private MovementManager movementManager;
    private UIManager uiManager;

    public int CurrentTurn = 1;
    public Tile[,] Tiles = new Tile[Utils.FieldWidth, Utils.FieldHeight];
    public Piece[,] Pieces = new Piece[Utils.FieldWidth, Utils.FieldHeight];

    void Awake()
    {
        TileParent = GameObject.Find("TileParent").transform;
        PieceParent = GameObject.Find("PieceParent").transform;
        EffectParent = GameObject.Find("EffectParent").transform;

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        movementManager = gameObject.AddComponent<MovementManager>();
        movementManager.Initialize(this, EffectPrefab, EffectParent);

        InitializeBoard();
    }

    void InitializeBoard()
    {
        // 8x8로 타일들을 배치
        // --- TODO ---
        for (int i = 0; i < Utils.FieldWidth; i++)
        {
            for (int j = 0; j < Utils.FieldHeight; j++)
            {
                GameObject tileObj = Instantiate(TilePrefab, TileParent);
                Tile tile = tileObj.GetComponent<Tile>();

         
                Tiles[i, j] = tile;
                Tiles[i, j].Set((i,j));
            }
        }
        // ------

        PlacePieces(1);
        PlacePieces(-1);
    }

    void PlacePieces(int direction)
    {
        // 체스 말들을 배치
        // --- TODO ---

        int placeHeight = 0;
        int pawnPlaceHeight = 0;

        int widthFromEnd = Utils.FieldWidth - 1;

        if (direction == 1)
        {
            placeHeight = 1;
            pawnPlaceHeight = placeHeight--;
        }
        else if (direction == -1)
        {
            placeHeight = Utils.FieldHeight - 2; ;
            pawnPlaceHeight = placeHeight++;
        }

        #region MakePawn
        for (int i = 0; i < Utils.FieldWidth; i++) //폰 생성
        {
        
            Piece pawnPiece = PlacePiece(5, (i, pawnPlaceHeight), direction);


        }
        #endregion

        #region MakeBishop
        {
   
            Piece BishopPieceLeft = PlacePiece(2, (2, placeHeight), direction);
            Piece BishopPieceRight = PlacePiece(2, (widthFromEnd  - 2, placeHeight), direction);

     
        }
        #endregion

        #region MakeRook
        { 

            Piece RookPieceLeft = PlacePiece(4, (0, placeHeight), direction);
            Piece RookPieceRight = PlacePiece(4, (widthFromEnd, placeHeight), direction);

           
        }
        #endregion

        #region MakeKnight
        {
          

            Piece KnightPieceLeft = PlacePiece(3, (1, placeHeight), direction);
            Piece KnightPieceRight = PlacePiece(3, (widthFromEnd - 1, placeHeight), direction);

          
        }
        #endregion

        #region MakeKing
        {

            Piece kingPiece = PlacePiece(0, (3,placeHeight), direction);
       

        }
        #endregion

        #region MakeQueen
        {
            
            Piece queenPiece = PlacePiece(1, (4,placeHeight), direction);
        }
        #endregion

       
    }


    Piece PlacePiece(int pieceType, (int, int) pos, int direction)
    {
        // 체스 말 하나를 배치 후 initialize
        // --- TODO ---

         GameObject pieceObj = Instantiate(PiecePrefabs[pieceType], PieceParent);
         Piece piece = pieceObj.GetComponent<Piece>();

        if (piece != null)
        {
        
            piece.MyPos = (pos.Item1, pos.Item2);
            piece.initialize(pos, direction);

        }
        return piece;
    }

       public bool IsValidMove(Piece piece, (int, int) targetPos)
       {
           return movementManager.IsValidMove(piece, targetPos);
       }

       public void ShowPossibleMoves(Piece piece)
       {
           movementManager.ShowPossibleMoves(piece);
       }

       public void ClearEffects()
       {
           movementManager.ClearEffects();
       }


       public void Move(Piece piece, (int, int) targetPos)
       {
           if (!IsValidMove(piece, targetPos)) return;

        // 체스 말을 이동하고, 만약 해당 자리에 상대 말이 있다면 삭제
        // --- TODO ---

            piece.MoveTo(targetPos);
            ChangeTurn();
        
           // ------
       }

       void ChangeTurn()
       {
        // 턴을 변경하고, UI에 표시
        // --- TODO ---
        if (CurrentTurn == 1)
            CurrentTurn = -1;
        else if(CurrentTurn == -1)
            CurrentTurn = 1;

        if(uiManager != null)
        {
            uiManager.UpdateTurn(CurrentTurn);
            uiManager.ShowMessage(uiManager.TurnText.text);
        }
           // ------
       }

    
}
