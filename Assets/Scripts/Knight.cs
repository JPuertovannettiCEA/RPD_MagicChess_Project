using System.Collections;
using UnityEngine;

public class Knight : Chessman
{
    [SerializeField]
    private GameObject _soldierPrefab;

    private void Awake()
    {
        Globals._canBePromotedx1 = false;
        Globals._canBePromotedx2 = false;
        Globals._canBePromotedx3 = false;
        Globals._canBePromotedx4 = false;
        Globals._canBePromotedb1 = false;
        Globals._canBePromotedb2 = false;
        Globals._canBePromotedb3 = false;
        Globals._canBePromotedb4 = false;
        Globals._x1 = null;
        Globals._x2 = null;
        Globals._x3 = null;
        Globals._x4 = null;
        Globals._b1 = null;
        Globals._b2 = null;
        Globals._b3 = null;
        Globals._b4 = null;
    }

    public override bool[,] PossibleMoves()
    {
        bool[,] r = new bool[8, 8];
        Chessman c;
        if (isWhite)
        {
            Globals._canBePromotedx1 = false;
            Globals._canBePromotedx2 = false;
            Globals._canBePromotedx3 = false;
            Globals._canBePromotedx4 = false;
            Globals._canBePromotedb1 = false;
            Globals._canBePromotedb2 = false;
            Globals._canBePromotedb3 = false;
            Globals._canBePromotedb4 = false;
            Globals._x1 = null;
            Globals._x2 = null;
            Globals._x3 = null;
            Globals._x4 = null;
            Globals._b1 = null;
            Globals._b2 = null;
            Globals._b3 = null;
            Globals._b4 = null;
        }
        else
        {
            Globals._canBePromotedx1 = false;
            Globals._canBePromotedx2 = false;
            Globals._canBePromotedx3 = false;
            Globals._canBePromotedx4 = false;
            Globals._canBePromotedb1 = false;
            Globals._canBePromotedb2 = false;
            Globals._canBePromotedb3 = false;
            Globals._canBePromotedb4 = false;
            Globals._x1 = null;
            Globals._x2 = null;
            Globals._x3 = null;
            Globals._x4 = null;
            Globals._b1 = null;
            Globals._b2 = null;
            Globals._b3 = null;
            Globals._b4 = null;

        }

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
        if (CurrentX != 0 && CurrentY != 7 && CurrentY >= 1 && CurrentX < 7)
        {
            Debug.Log(CurrentY);
            Debug.Log(CurrentX);
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
            if (c == null && CurrentY != 0 && CurrentX <= 7)
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
                        Globals._canBePromotedx1 = true;
                        Globals._x1 = c.gameObject;
                    }
                }
            }
            // Diagonal left Down Promotion
            if (CurrentX != 0 && CurrentY != 7 && CurrentY >= 1 && CurrentX < 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Debug.Log($"Left Down Can Be Promoted");
                        Globals._canBePromotedx2 = true;
                        Globals._x2 = c.gameObject;
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
                        Globals._canBePromotedx3 = true;
                        Globals._x3 = c.gameObject;
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
                        Globals._canBePromotedx4 = true;
                        Globals._x4 = c.gameObject;
                    }
                }
            }
        }
        else
        {
            //Diagonal Left Up Promotion
            if (CurrentX != 7 && CurrentY != 0 && CurrentY < 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Globals._canBePromotedb1 = true;
                        Globals._b1 = c.gameObject;
                        Debug.Log($"Left Up Black Can Be Promoted");
                    }
                }
            }
            // Diagonal left Down Promotion
            if (CurrentX != 7 && CurrentY != 0 && CurrentX >= 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && !c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Globals._canBePromotedb2 = true;
                        Globals._b2 = c.gameObject;
                        Debug.Log($"Left Down Black Can Be Promoted");
                    }
                }
            }
            // Diagonal right Up Promotion
            if (CurrentX != 7 && CurrentY != 0 && CurrentY < 7)
            {
                Debug.Log(CurrentX);
                Debug.Log(CurrentY);
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Globals._canBePromotedb3 = true;
                        Globals._b3 = c.gameObject;
                        Debug.Log($"Right Up Black Can Be Promoted");
                    }
                }
            }
            // Diagonal right Down Promotion
            if (CurrentX != 7 && CurrentY != 0 && CurrentY >= 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && !c.isWhite)
                {
                    if (c.GetType() == typeof(Pawn))
                    {
                        Globals._canBePromotedb4 = true;
                        Globals._b4 = c.gameObject;
                        Debug.Log($"Right Down Black Can Be Promoted");
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

        return r;
    }

}
