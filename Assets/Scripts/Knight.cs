using System.Collections;
using UnityEngine;

public class Knight : Chessman
{
    public override bool[,] PossibleMoves()
    {
        bool[,] r = new bool[8, 8];
        Chessman c;

        //Move One UP
        if (CurrentY != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
            if (c == null)
                r[CurrentX, CurrentY + 1] = true;
        }

        //Move One Right
        if (CurrentX != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;
        }

        //Move One Left
        if (CurrentX != 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;
        }

        //Move One Down
        if (CurrentY != 7 && CurrentY != 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
            if (c == null)
                r[CurrentX, CurrentY - 1] = true;
        }

        // Diagonal left
        if (CurrentX != 0 && CurrentY != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
            if (c == null)
            {
                r[CurrentX - 1, CurrentY + 1] = true;
            }
        }
        // Diagonal left Down
        if (CurrentX != 0 && CurrentY != 7 && CurrentY >= 1)
        {
            Debug.Log(CurrentY);
            Debug.Log(CurrentX);
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
            if (c == null && CurrentY != 0)
            {
                r[CurrentX + 1, CurrentY - 1] = true;
            }
        }

        // Diagonal right
        if (CurrentX != 7 && CurrentY != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
            if (c == null)
                r[CurrentX + 1, CurrentY + 1] = true;
        }
        // Diagonal right Down
        if (CurrentX != 0 && CurrentY != 7 && CurrentY >= 1)
        {
            Debug.Log(CurrentY);
            Debug.Log(CurrentX);
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
            if (c == null)
                r[CurrentX - 1, CurrentY - 1] = true;
        }

        // Diagonal left Promotion
        if (CurrentX != 0 && CurrentY != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
            if (c != null && c.isWhite)
            {
                Debug.Log($"Left Can Be Promoted");
            }
        }

        // // Up left
        // Move(CurrentX - 1, CurrentY + 2, ref r);

        // // Up right
        // Move(CurrentX + 1, CurrentY + 2, ref r);

        // // Down left
        // Move(CurrentX - 1, CurrentY - 2, ref r);

        // // Down right
        // Move(CurrentX + 1, CurrentY - 2, ref r);


        // // Left Down
        // Move(CurrentX - 2, CurrentY - 1, ref r);

        // // Right Down
        // Move(CurrentX + 2, CurrentY - 1, ref r);

        // // Left Up
        // Move(CurrentX - 2, CurrentY + 1, ref r);

        // // Right Up
        // Move(CurrentX + 2, CurrentY + 1, ref r);

        return r;
    }

}
