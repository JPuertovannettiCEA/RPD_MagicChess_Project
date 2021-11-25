using System.Collections;
using UnityEngine;

public class Soldier : Chessman
{

    public override bool[,] PossibleMoves()
    {
        bool[,] r = new bool[8, 8];

        Chessman c, c2;

        if (isWhite)
        {
            ////// White team move //////

            // Diagonal left
            if (CurrentX != 0 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX - 1, CurrentY + 1] = true;
            }

            // Diagonal right
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX + 1, CurrentY + 1] = true;
            }

            // Middle
            if (CurrentY != 7)
            {
                // Debug.Log($"moveahead");
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                if (c == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }

            //MoveBehind
            if (CurrentY != 7)
            {
                // Debug.Log($"movebehind");
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                // Debug.Log(CurrentX);
                if (c == null)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }

            //MoveLeft
            if (CurrentX != 7 && CurrentY != 7 && CurrentX != 0)
            {
                // Debug.Log($"moveLeft");
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
                Debug.Log(CurrentX);
                Debug.Log(CurrentY);
                if (c == null)
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
            }

            //MoveRight
            if (CurrentX != 7 && CurrentY != 7 && CurrentX != 0)
            {
                // Debug.Log($"moveRight");
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
                if (c == null)
                {
                    r[CurrentX - 1, CurrentY] = true;
                }
            }

            // Middle on first move
            if (CurrentY == 1)
            {
                // Debug.Log($"moveaheadbutok");
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 2];
                if (c == null && c2 == null)
                    r[CurrentX, CurrentY + 2] = true;
            }
        }
        else
        {
            ////// Black team move //////

            // Diagonal left
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;
            }

            // Diagonal right
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }

            //MoveLeft
            if (CurrentX != 7 && CurrentY != 7 && CurrentX != 0)
            {
                // Debug.Log($"moveLeft");
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];
                Debug.Log(CurrentX);
                Debug.Log(CurrentY);
                if (c == null)
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
            }
            //MoveBehind
            if (CurrentY != 7)
            {
                // Debug.Log($"movebehind");
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                // Debug.Log(CurrentX);
                if (c == null)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }

            //MoveRight
            if (CurrentX != 7 && CurrentY != 7 && CurrentX != 0)
            {
                // Debug.Log($"moveRight");
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];
                if (c == null)
                {
                    r[CurrentX - 1, CurrentY] = true;
                }
            }

            // Middle
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                if (c == null)
                    r[CurrentX, CurrentY - 1] = true;
            }

            // Middle on first move
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 2];
                if (c == null && c2 == null)
                    r[CurrentX, CurrentY - 2] = true;
            }
        }

        return r;
    }
}
