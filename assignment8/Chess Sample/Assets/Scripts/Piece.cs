using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public (int, int) MyPos;
    public int PlayerDirection = 1;
    
    public Sprite WhiteSprite;
    public Sprite BlackSprite;
    
    protected GameManager MyGameManager;
    protected SpriteRenderer MySpriteRenderer;

    void Awake()
    {
        MyGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    public void initialize((int, int) targetPos, int direction)
    {
        PlayerDirection = direction;
        initSprite(PlayerDirection);
        MoveTo(targetPos);
    
    }

    void initSprite(int direction)
    {
        // direction에 따라 sprite를 설정하고 회전함
        // --- TODO ---
 
        if (MySpriteRenderer != null)
        {
    
            if (direction == 1)
            {
                MySpriteRenderer.sprite = WhiteSprite;
            }

            if (direction == -1)
            {
                MySpriteRenderer.sprite = BlackSprite;
                Transform tr = GetComponent<Transform>();
                tr.Rotate(new Vector3(0, 0, 180));
            }
        
        }
        // ------
    }

    public void MoveTo((int, int) targetPos)
    {
        // 말을 이동시킴
        // --- TODO ---
        if (MyGameManager != null)
        {
            if (MyGameManager.Pieces[targetPos.Item1, targetPos.Item2] != null)
            {
                if (MyGameManager.Pieces[targetPos.Item1, targetPos.Item2].PlayerDirection != this.PlayerDirection)
                {
               
                    MyGameManager.Pieces[targetPos.Item1, targetPos.Item2].gameObject.SetActive(false);
                    Destroy(MyGameManager.Pieces[targetPos.Item1, targetPos.Item2]);
           
                    MyGameManager.Pieces[targetPos.Item1, targetPos.Item2] = null; //만약 상대 말이면 삭제
                }

            }

                MyGameManager.Pieces[MyPos.Item1, MyPos.Item2] = null; //원래있던 곳 비워주기

                MyPos = targetPos;

                MyGameManager.Pieces[targetPos.Item1, targetPos.Item2] = this; //배열 속 위치 바꾸기


                Vector3 realPos = Utils.ToRealPos((MyPos.Item1, MyPos.Item2));
           
                this.transform.position = realPos;

       
        }
        // ------
    }

    public abstract MoveInfo[] GetMoves();
}
