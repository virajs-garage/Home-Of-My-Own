using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static WinCondition;

public class Player : MonoBehaviour
{
    [System.NonSerialized]
    public Board board;

    private UI ui;
    private CameraMove cm;
    private SoundController sfx;
    public GamePiece PlayerIsOn;
    public GamePiece prevPiece;

    List<GamePiece> moveQueue = new List<GamePiece>();

    private float startingScale;

    public Transform CameraPos;

    public bool isKid;

    bool isMoving;

    public Vector3 multPlayerOffset = Vector3.zero;

    private void Awake()
    {
        board = FindObjectOfType<Board>();
        cm = FindObjectOfType<CameraMove>();
        ui = FindObjectOfType<UI>();
        sfx = FindObjectOfType<SoundController>();
        Vector3 center = board.SpawnPiece.transform.gameObject.GetComponent<MeshCollider>().bounds.center;
        Vector3 spawnPos = new Vector3(center.x, gameObject.transform.position.y, center.z);
        gameObject.transform.position = spawnPos;
        PlayerIsOn = board.SpawnPiece;
        startingScale = transform.localScale.x;
    }


    private void Update()
    {
        if(board.currentPlayer == this)
        {
            if (Input.GetMouseButtonDown(0)) // If left clicking
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Space")
                    {
                        if (PlayerIsOn.neighbors.Contains(hit.transform.GetComponent<GamePiece>()))
                        {
                            //MovePlayer(hit.transform.GetComponent<GamePiece>());
                        }
                    }
                    if(hit.transform.gameObject.tag == "Arrow")
                    {
                        if(board.ts.isTutorialStarted && (board.ts.stepNumber == 5))
                        {
                            Debug.Log("NO");
                        }
                        else
                        {
                            var piece = hit.transform.gameObject.GetComponent<ArrowData>().piece;
                            MovePlayer(piece);
                        }
      
                    }
                }
            }
        }
    }

    public void MovePlayer(GamePiece piece)
    {
        if(piece != prevPiece)
        {
            if (!isMoving)
            {
                if (board.spacesToMove != 0)
                {
                    prevPiece = PlayerIsOn;
                    prevPiece.playersOnPiece.Remove(this);

                    isMoving = true;
                    PlayerIsOn = piece;
                    PlayerIsOn.playersOnPiece.Add(this);

                    sfx.PlaySound(2,0);

                    Vector3 center = piece.transform.gameObject.GetComponent<MeshCollider>().bounds.center;
                    Vector3 piecePos = new Vector3(center.x, gameObject.transform.position.y, center.z);
                    LeanTween.move(gameObject, piecePos, .4f).setEaseInOutCubic().setOnComplete(() =>
                    {
                        isMoving = false;

                        PlayerIsOn.HandlePlayers();
                        prevPiece.HandlePlayers();

                        //Debug.Log($"Need to spawn " + piece.neighbors.Count + " arrows");

                        ShowArrows(piece);

                        if(board.ts.stepNumber == 4) 
                        {
                            board.ts.IncrementStep();
                        }

                        if (piece.Recource != CardScript.Resource.None && board.spacesToMove != 0)
                        {
         
                        }

                        if (board.spacesToMove == 0) // When done moving
                        {
                            prevPiece = null;
                            transform.GetChild(1).gameObject.SetActive(false);

                            if(board.ts.stepNumber == 3 || board.ts.stepNumber == 6 || board.ts.stepNumber == 8)
                            {
                                board.ts.IncrementStep();
                            }

                            if (piece.Recource != CardScript.Resource.None)
                            {
                                sfx.PlaySound(3,0);
                                ui.ShowCard(piece.Recource);
                            }
                            else
                            {
                                cm.MoveCamera(CameraMove.CameraPositions.MainBoard);
                                ui.SetRollButton(true);
                                board.SetNextPlayer();
                            }
                        }
                        CheckQueue();
                    });
                    board.spacesToMove--;
                    ui.SetMoveText(board.spacesToMove);
                    ui.ShowUiLogic();
                }
            }
            else
            {
                moveQueue.Add(piece);
            }
        }
    }
    public void ShowArrows(GamePiece piece)
    {
        transform.GetChild(1).gameObject.SetActive(true);
        for (int i = 0; i < 4; i++) // Set all unactive
        {
            var arrow = transform.GetChild(1).GetChild(i).gameObject;
            arrow.SetActive(false);
        }

        for (int i = 0; i < piece.neighbors.Count; i++) // Set active and do stuff for each neighbor
        {
            var arrow = transform.GetChild(1).GetChild(i).gameObject;
            arrow.SetActive(true);
            var pieceCenter = piece.neighbors[i].transform.GetComponent<Renderer>().bounds.center; // CENTER OF THE PIECE
            Vector3 directionToNeighbor = pieceCenter - transform.position;
            float angle = Mathf.Atan2(directionToNeighbor.x, directionToNeighbor.z) * Mathf.Rad2Deg;

            // Create a Quaternion rotation only around the y-axis
            Quaternion yRotation = Quaternion.Euler(90f, angle, 0f);

            // Apply the rotation to the arrow's local rotation
            arrow.transform.localRotation = yRotation;

            arrow.transform.localPosition = Vector3.zero;
            arrow.transform.localPosition = arrow.transform.localPosition + directionToNeighbor + (transform.up / 2);


            arrow.GetComponent<ArrowData>().piece = piece.neighbors[i];
            if(prevPiece == piece.neighbors[i])
            {
                arrow.tag = "Unable";
                arrow.GetComponent<MeshRenderer>().material = board.ArrowDark;
            }
            else
            {
                arrow.tag = "Arrow";
                arrow.GetComponent<MeshRenderer>().material = board.ArrowLight;

            }
        }

    }

    public void CheckQueue()
    {
        if(moveQueue.Count > 0)
        {
            MovePlayer(moveQueue[0]);
            moveQueue.Remove(moveQueue[0]);
        }
    }

    public void MoveDirection(bool clockWise)
    {
        if (clockWise)
        {
            int index = board.clockWise.IndexOf(PlayerIsOn);
            int counter = index;
            int savedSpacesToMove = board.spacesToMove;
            for (int i = 0; i < savedSpacesToMove; i++)
            {
                counter++;
                if (counter >= board.clockWise.Count)
                {
                    counter = 0;
                }
                MovePlayer(board.clockWise[counter]);
            }
        }
        else 
        {
            int index = board.clockWise.IndexOf(PlayerIsOn);
            int counter = index;
            int savedSpacesToMove = board.spacesToMove;
            for (int i = 0; i < savedSpacesToMove; i++)
            {
                counter--;
                if (counter <= -1)
                {
                    counter = board.clockWise.Count -1;
                }
                MovePlayer(board.clockWise[counter]);
            }
        }
    }

    public void AdjustPlayerOnPiece(Vector3 offset, float scale)
    {
        Vector3 center = PlayerIsOn.transform.gameObject.GetComponent<MeshCollider>().bounds.center;
        Vector3 piecePos = new Vector3(center.x, gameObject.transform.position.y, center.z);
        LeanTween.move(gameObject, piecePos + offset, .4f).setEaseInOutCubic();
        LeanTween.value(gameObject, (Vector3 scale) => { transform.localScale = scale; }, transform.localScale, new Vector3(scale, scale, scale) * startingScale, .4f).setEaseInOutCubic();
    }

    //public void AdjustPlayerOnPiece(float scaleMultiplier, Transform parent)
    //{
    //    Vector3 Scale = startingScale * scaleMultiplier;
    //    transform.localScale = Scale;
    //    transform.parent = parent;
    //    transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
    //}
}
