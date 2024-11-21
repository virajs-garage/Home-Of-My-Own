using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

public class GamePiece : MonoBehaviour
{
    public Vector3 position;
    public List<GamePiece> neighbors;
    public CardScript.Resource Recource;

    public List<Player> playersOnPiece = new List<Player>();

    GameObject SpawnPos;
    public void UpdateList()
    {
        neighbors.Clear();

        GamePiece[] allObjects = FindObjectsOfType<GamePiece>();
        var gamePieceCenter = transform.GetComponent<Renderer>().bounds.center;
        var gamePieceCenterTemp = new Vector3(gamePieceCenter.x, 0, gamePieceCenter.z);

        foreach (GamePiece gamePiece in allObjects)
        {
            var otherGamePieceCenter = gamePiece.transform.GetComponent<Renderer>().bounds.center;
            var otherGamePieceCenterTemp = new Vector3(otherGamePieceCenter.x, 0, otherGamePieceCenter.z);


            if (gamePiece.name != gameObject.name)
            {
                float d = Vector3.Distance(gamePieceCenterTemp, otherGamePieceCenterTemp);
                float radius = 1.5f;

                switch (gameObject.name)
                {
                    case "1":
                    case "13":
                    case "21":
                    case "25":
                        radius = 1.4f;
                        break;
                    case "16":
                    case "24":
                    case "4":
                    case "28":
                        radius = 2.0f;
                        break;
                    case "20":
                        radius = 1.55f;
                        break;
                }

                if (d < radius)
                {
                    neighbors.Add(gamePiece);
                }
            }
        }

        //Debug.LogFormat("Gamepiece {0} is adjacent to Gamepiece(s): {1}.", gameObject.name, string.Join(',', neighbors.Select(z => z.name).ToArray()));
    }

    public void HandlePlayers()
    {
        if (playersOnPiece.Count <= 0)
        {
            return;
        }
        float scale = 1;
        switch (playersOnPiece.Count)
        {
            case 1:
                scale = 1;
                playersOnPiece[0].multPlayerOffset = Vector3.zero;
                break;
            case 2:
                scale = .8f;
                playersOnPiece[0].multPlayerOffset = new Vector3(.03f, 0, .03f);
                playersOnPiece[1].multPlayerOffset = new Vector3(-.03f, 0, -.03f);
                break;
            case 3:
                scale = .6f;
                playersOnPiece[0].multPlayerOffset = new Vector3(.03f, 0, -.0212f);
                playersOnPiece[1].multPlayerOffset = new Vector3(-.03f, 0, -.0212f);
                playersOnPiece[2].multPlayerOffset = new Vector3(0, 0, 0.0388f);
                break;
            case 4:
                scale = .6f;
                playersOnPiece[0].multPlayerOffset = new Vector3(.03f, 0, -.03f);
                playersOnPiece[1].multPlayerOffset = new Vector3(-.03f, 0, -.03f);
                playersOnPiece[2].multPlayerOffset = new Vector3(.03f, 0, -.03f);
                playersOnPiece[3].multPlayerOffset = new Vector3(-.03f, 0, .03f);
                break;
        }

        foreach (Player p in playersOnPiece)
        {
            var offset = p.multPlayerOffset * p.board.offsetMultiplier;
            p.AdjustPlayerOnPiece(offset, scale);
        }

    }
}
