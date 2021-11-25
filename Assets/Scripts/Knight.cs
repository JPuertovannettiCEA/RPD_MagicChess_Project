using System.Collections;
using UnityEngine;

public class Knight : Chessman
{
    public override bool[,] PossibleMoves()
    {
        bool[,] r = new bool[8, 8];
        Chessman c;

        #region //Movement +
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
        #endregion

        #region //Movement x
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
        #endregion

        #region //White Promotion x
        if (isWhite)
        {
            // Diagonal left Up Promotion
            if (CurrentX != 0 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                if (c != null && c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Debug.Log($"Left Up Can Be Promoted");
                    }
                }
            }
            // Diagonal left Down Promotion
            if (CurrentX != 0 && CurrentY != 7 && CurrentY >= 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Debug.Log($"Left Down Can Be Promoted");
                    }
                }
            }
            // Diagonal right Up Promotion
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Debug.Log($"Right Up Can Be Promoted");
                    }
                }
            }
            // Diagonal right Down Promotion
            if (CurrentX != 0 && CurrentY != 7 && CurrentY >= 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Debug.Log($"Right Down Can Be Promoted");
                    }
                }
            }
        }
        else
        {
            //Diagonal Left Up Promotion
            if (CurrentX != 0 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Debug.Log($"Left Up Can Be Promoted");
                    }
                }
            }
            // Diagonal left Down Promotion
            if (CurrentX != 0 && CurrentY != 7 && CurrentY >= 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && !c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Debug.Log($"Left Down Can Be Promoted");
                    }
                }
            }
            // Diagonal right Up Promotion
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Debug.Log($"Right Up Can Be Promoted");
                    }
                }
            }
            // Diagonal right Down Promotion
            if (CurrentX != 0 && CurrentY != 7 && CurrentY >= 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && !c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Debug.Log($"Right Down Can Be Promoted");
                    }
                }
            }

        }
        #endregion

        #region //Kill Opposite Player
        Move(CurrentX + 1, CurrentY, ref r); // up
        Move(CurrentX - 1, CurrentY, ref r); // down
        Move(CurrentX, CurrentY - 1, ref r); // left
        Move(CurrentX, CurrentY + 1, ref r); // right
        Move(CurrentX + 1, CurrentY - 1, ref r); // up left
        Move(CurrentX - 1, CurrentY - 1, ref r); // down left
        Move(CurrentX + 1, CurrentY + 1, ref r); // up right
        Move(CurrentX - 1, CurrentY + 1, ref r); // down right
        #endregion

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
