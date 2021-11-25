using System.Collections;
using UnityEngine;

public class DarkMage : Chessman
{
    public override bool[,] PossibleMoves()
    {
        bool[,] r = new bool[8, 8];

        Chessman c, c2;
        Debug.Log(CurrentX);
        //Move One UP
        if (CurrentY != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
            if (c == null)
                r[CurrentX, CurrentY + 1] = true;
        }

        //Move Two UP
        if (CurrentY != 7)
        {
            // c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
            c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 2];
            if (c2 == null)
                r[CurrentX, CurrentY + 2] = true;
        }

        //Move One Right
        if (CurrentX != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;
        }

        //Move Two RIght
        if (CurrentX != 7)
        {
            // c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            c2 = BoardManager.Instance.Chessmans[CurrentX + 2, CurrentY];
            if (c2 == null)
                r[CurrentX + 2, CurrentY] = true;
        }

        //Move One Left
        if (CurrentX != 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;
        }

        //Move Two Left
        if (CurrentX != 0)
        {
            // c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            c2 = BoardManager.Instance.Chessmans[CurrentX - 2, CurrentY];
            if (c2 == null)
                r[CurrentX - 2, CurrentY] = true;
        }

        //Move One Down
        if (CurrentY != 7 && CurrentY != 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
            if (c == null)
                r[CurrentX, CurrentY - 1] = true;
        }

        //Move Two Down
        if (CurrentY != 7 && CurrentY >= 2)
        {
            // c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
            c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 2];
            if (c2 == null)
            {
                r[CurrentX, CurrentY - 2] = true;
            }
        }
        return r;
    }
}
