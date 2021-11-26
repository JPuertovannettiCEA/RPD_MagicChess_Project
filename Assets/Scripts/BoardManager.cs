using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; set; }
    private bool[,] allowedMoves { get; set; }

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Quaternion whiteOrientation = Quaternion.Euler(0, 270, 0);
    private Quaternion blackOrientation = Quaternion.Euler(0, 180, 0);

    public Chessman[,] Chessmans { get; set; }
    private Chessman selectedChessman;

    public bool isWhiteTurn = true;

    private Material previousMat;
    public Material selectedMat;

    [SerializeField]
    private GameObject _restart, _mainMenu, _quit, _blackWinsInfo, _whiteWinsInfo, _whiteTurn, _blackTurn;

    [SerializeField]
    private EventSystem _eventSystem;

    [SerializeField]
    private AudioSource _healerSFX;

    [SerializeField]
    private AudioSource _darkMageSFX;

    [SerializeField]
    private AudioSource _dyingSFX;

    [SerializeField]
    private AudioSource _placingSFX;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        SpawnAllChessmans();
        _eventSystem.enabled = false;
        _restart.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(false);
        _quit.gameObject.SetActive(false);
        _blackWinsInfo.gameObject.SetActive(false);
        _whiteWinsInfo.gameObject.SetActive(false);
        _blackTurn.gameObject.SetActive(false);
        _whiteTurn.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelection();
        if (isWhiteTurn)
        {
            _whiteTurn.gameObject.SetActive(true);
            _blackTurn.gameObject.SetActive(false);
        }
        else
        {
            _blackTurn.gameObject.SetActive(true);
            _whiteTurn.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log(selectionX);
            // Debug.Log(selectionY);
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedChessman == null)
                {
                    // Select the chessman
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    // Move the chessman
                    MoveChessman(selectionX, selectionY);
                }
            }
        }
        if (Globals._canBePromotedx1 == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _healerSFX.Play();
                SpawnChessman(13, ((int)Globals._x1.transform.position.x), ((int)Globals._x1.transform.position.y + 1), true);
                Destroy(Globals._x1.gameObject);
                Globals._canBePromotedx1 = false;
            }
        }
        if (Globals._canBePromotedx2 == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _healerSFX.Play();
                SpawnChessman(13, ((int)Globals._x2.transform.position.x), ((int)Globals._x2.transform.position.y + 1), true);
                Destroy(Globals._x2.gameObject);
                Globals._canBePromotedx2 = false;
            }
        }
        if (Globals._canBePromotedx3 == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _healerSFX.Play();
                SpawnChessman(13, ((int)Globals._x3.transform.position.x), ((int)Globals._x3.transform.position.y + 1), true);
                Destroy(Globals._x3.gameObject);
                Globals._canBePromotedx3 = false;
            }
        }
        if (Globals._canBePromotedx4 == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _healerSFX.Play();
                SpawnChessman(13, ((int)Globals._x4.transform.position.x), ((int)Globals._x4.transform.position.y + 1), true);
                Destroy(Globals._x4.gameObject);
                Globals._canBePromotedx4 = false;
            }
        }
        if (Globals._canBePromotedb1 == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _healerSFX.Play();
                SpawnChessman(12, ((int)Globals._b1.transform.position.x), 6, false);
                Debug.Log((int)Globals._b1.transform.position.x);
                Debug.Log((int)Globals._b1.transform.position.y);
                Destroy(Globals._b1.gameObject);
                Globals._canBePromotedb1 = false;
            }
        }
        if (Globals._canBePromotedb2 == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _healerSFX.Play();
                Debug.Log((int)Globals._b2.transform.position.x);
                Debug.Log((int)Globals._b2.transform.position.y);
                SpawnChessman(12, ((int)Globals._b2.transform.position.x), 6, false);
                Destroy(Globals._b2.gameObject);
                Globals._canBePromotedb2 = false;
            }
        }
        if (Globals._canBePromotedb3 == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _healerSFX.Play();
                Debug.Log((int)Globals._b3.transform.position.x);
                Debug.Log((int)Globals._b3.transform.position.y);
                SpawnChessman(12, ((int)Globals._b3.transform.position.x), 6, false);
                Destroy(Globals._b3.gameObject);
                Globals._canBePromotedb3 = false;
            }
        }
        if (Globals._canBePromotedb4 == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _healerSFX.Play();
                Debug.Log((int)Globals._b4.transform.position.x);
                Debug.Log((int)Globals._b4.transform.position.y);
                SpawnChessman(12, ((int)Globals._b4.transform.position.x), 6, false);
                Destroy(Globals._b4.gameObject);
                Globals._canBePromotedb4 = false;
            }
        }
    }

    private void SelectChessman(int x, int y)
    {
        if (Chessmans[x, y] == null) return;

        if (Chessmans[x, y].isWhite != isWhiteTurn) return;

        bool hasAtLeastOneMove = false;

        allowedMoves = Chessmans[x, y].PossibleMoves();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (allowedMoves[i, j])
                {
                    hasAtLeastOneMove = true;
                    i = 8;
                    break;
                }
            }
        }

        if (!hasAtLeastOneMove)
            return;

        selectedChessman = Chessmans[x, y];
        previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
        selectedMat.mainTexture = previousMat.mainTexture;
        selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;
        BoardHighlights.Instance.HighLightAllowedMoves(allowedMoves);
    }

    private void MoveChessman(int x, int y)
    {
        if (allowedMoves[x, y])
        {
            Chessman c = Chessmans[x, y];

            if (c != null && c.isWhite != isWhiteTurn)
            {
                // Capture a piece
                if (c.GetType() == typeof(DarkMage))
                {
                    _darkMageSFX.Play();
                    Destroy(c.gameObject);
                    Destroy(selectedChessman.gameObject);
                    Debug.Log($"Kill Me and DarkMage");
                }

                if (c.GetType() == typeof(King))
                {
                    // End the game
                    EndGame();
                    return;
                }
                if (selectedChessman.GetType() == typeof(King))
                {
                    Destroy(c.gameObject);
                    Debug.Log($"King Killed EnemyF");
                }
                _dyingSFX.Play();
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }
            _placingSFX.Play();
            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
            Chessmans[x, y] = selectedChessman;
            isWhiteTurn = !isWhiteTurn;
        }

        selectedChessman.GetComponent<MeshRenderer>().material = previousMat;

        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main) return;

        // Debug.Log($"RayCasting");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }
    public void SpawnChessman(int index, int x, int y, bool isWhite)
    {
        Vector3 position = GetTileCenter(x, y);
        GameObject go;

        if (isWhite)
        {
            go = Instantiate(chessmanPrefabs[index], position, Quaternion.identity) as GameObject;
        }
        else
        {
            Debug.Log($"i worked");
            go = Instantiate(chessmanPrefabs[index], position, blackOrientation) as GameObject;
        }

        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        activeChessman.Add(go);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;

        return origin;
    }

    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];

        /////// White ///////

        // King
        SpawnChessman(0, 3, 0, true);

        // Queen
        SpawnChessman(1, 4, 0, true);

        // Rooks
        SpawnChessman(2, 0, 0, true);
        SpawnChessman(2, 7, 0, true);

        // Bishops
        SpawnChessman(3, 2, 0, true);
        SpawnChessman(3, 5, 0, true);

        // Knights
        SpawnChessman(4, 1, 0, true);
        SpawnChessman(4, 6, 0, true);

        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(5, i, 1, true);
        }


        /////// Black ///////

        // King
        SpawnChessman(6, 4, 7, false);

        // Queen
        SpawnChessman(7, 3, 7, false);

        // Rooks
        SpawnChessman(8, 0, 7, false);
        SpawnChessman(8, 7, 7, false);

        // Bishops
        SpawnChessman(9, 2, 7, false);
        SpawnChessman(9, 5, 7, false);

        // Knights
        SpawnChessman(10, 1, 7, false);
        SpawnChessman(10, 6, 7, false);

        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, i, 6, false);
        }
    }

    private void EndGame()
    {
        UnHide();
        if (isWhiteTurn)
        {
            Debug.Log("White wins");
            _whiteWinsInfo.gameObject.SetActive(true);
        }

        else
        {
            _blackWinsInfo.gameObject.SetActive(true);
            Debug.Log("Black wins");
        }

    }

    private void UnHide()
    {
        _eventSystem.enabled = true;
        _restart.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(true);
        _quit.gameObject.SetActive(true);
    }
}


