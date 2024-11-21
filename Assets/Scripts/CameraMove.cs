using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    public Board board;
    public SoundController sfx;
    public UI ui;
    public float cameraMoveTime;
    public List<cameraTarget> targets = new List<cameraTarget>();
    public CameraPositions CurrentPosition;
    bool isCamMoving = false;
    GameObject cam;

    List<CameraPositions> cameraQueue = new List<CameraPositions>();

    [Serializable]
    public class cameraTarget
    {
        public Transform transform;
        public Transform parent;
        public float size;
    }
    public enum CameraPositions
    {
        MainBoard,
        Dice,
        Player,
    }
    private void Awake()
    {
        cam = Camera.main.gameObject;
    }
    private void Start()
    {
        targets[2] = new cameraTarget { transform = board.currentPlayer.CameraPos, parent = board.currentPlayer.transform, size = 3 };

    }
    public void MoveCamera(CameraPositions name)
    {
        if (!isCamMoving)
        {
            sfx.PlaySound(1,0); // Whoosh
            isCamMoving = true;
            CurrentPosition = name;
            cam.transform.parent = targets[(int)name].parent;
            LeanTween.move(cam, targets[(int)name].transform.position, cameraMoveTime).setEaseInOutCubic().setOnComplete(() =>
        CheckQueue());
            LeanTween.rotate(cam, targets[(int)name].transform.rotation.eulerAngles, cameraMoveTime).setEaseInOutCubic();
            LeanTween.value(cam, (float val) => { cam.GetComponent<Camera>().orthographicSize = val; }, cam.GetComponent<Camera>().orthographicSize, targets[(int)name].size, cameraMoveTime).setEaseInOutCubic().setOnComplete(() => { isCamMoving = false; });
            ui.ShowUiLogic();
        }
        else
        {
            cameraQueue.Add(name);
        }
    }
    public void CheckQueue()
    {
        if(cameraQueue.Count > 0)
        {
            MoveCamera(cameraQueue[0]);
            cameraQueue.Remove(cameraQueue[0]);
        }
    }
}
