using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public (int, int) MyPos;
    Color tileColor = new Color(255 / 255f, 193 / 255f, 204 / 255f);
    SpriteRenderer MySpriteRenderer;

    private void Awake()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Set((int, int) targetPos)
    {
        // targetPos로 이동시키고, 색깔을 지정
        // --- TODO ---
        MyPos = targetPos;
        Transform ts = GetComponent<Transform>();

        Vector3 setPos = Utils.ToRealPos((MyPos.Item1, MyPos.Item2));
     
        ts.position = new Vector3(setPos.x, setPos.y,1);

        int x = targetPos.Item1;
        int y = targetPos.Item2;

        Color whiteColor = new Color(255, 255, 255);

        if (y % 2 == 0)
        {
            if (x % 2 == 0)
            {
                MySpriteRenderer.color = whiteColor;
            }
            else
            {
                MySpriteRenderer.color = tileColor;
            }
        }
        else
        {
            if (x % 2 == 0)
            {
                MySpriteRenderer.color = tileColor;
            }
            else
            {
                MySpriteRenderer.color = whiteColor;
            }
        }
      
        // ------
    }
}
