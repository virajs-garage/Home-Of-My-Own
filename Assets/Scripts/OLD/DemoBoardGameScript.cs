using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DemoBoardGameScript : MonoBehaviour
{
    public GameObject CubeHolder;
    public List<Transform> CubeList = new List<Transform>();
    public Transform Player;
    public Transform MainCameraParent;
    public Rigidbody Dice;
    public FloorDiceCollisionScript floor;
    public int MaxSpaces = 1;
    public bool isMainCamera;
    Vector3 startingPos;
    Quaternion startingRotation;

    public float minDiceTorque = 50f;
    public float maxDiceTorque = 100f;
    public float minDiceMag = 10f;
    public float maxDiceMag = 10f;

    public TMP_Text MoveCounter;
    public GameObject RollButton;

    public List<CameraTargetProperties> CameraPosList = new List<CameraTargetProperties>();

    [Serializable]
    public class CameraTargetProperties
    {
        public Transform transform;
        public float size;
    }

    private void Start()
    {
        RollButton.SetActive(true);
        startingPos = Dice.gameObject.transform.position;
        startingRotation = Dice.gameObject.transform.rotation;
        foreach (Transform child in CubeHolder.transform)
        {
            CubeList.Add(child);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Space")
                {
                    float distX = Mathf.Abs(hit.transform.position.x - Player.transform.position.x);
                    float distZ = Mathf.Abs(hit.transform.position.z - Player.transform.position.z);
                    //Debug.Log("X: " + distX + " Z: " + distZ);
                    if ((distX < (1.2 * MaxSpaces) && distZ < .5) || (distZ < (1.2 * MaxSpaces) && distX < .5))
                    {
                        Move(hit.transform.position);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CheckToRoll();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            isMainCamera = !isMainCamera;
            MoveCamera(isMainCamera ? 0 : 2);
        }
    }
    public void CheckArrowMovement()
    {
        foreach (Transform t in CubeList)
        {
            float distX = Mathf.Abs(Player.transform.position.x - t.position.x);
            float distZ = Mathf.Abs(Player.transform.position.z - t.position.z);
            if ((distX >= 1 && distX <= 1.5) || (distZ >= 1 && distZ <= 1.5))
            {
                Vector3 direction = Vector3.zero;
                direction += t.position - Player.transform.position;
                Debug.Log(GetCardinalDirection(direction));
            }
        }
    }
    private string GetCardinalDirection(Vector3 direction)
    {
        Vector3 referenceRight = Vector3.right;
        Vector3 referenceLeft = Vector3.left;
        Vector3 referenceUp = Vector3.forward;
        Vector3 referenceDown = Vector3.back;

        float angleRight = Vector3.Angle(direction, referenceRight);
        float angleLeft = Vector3.Angle(direction, referenceLeft);
        float angleUp = Vector3.Angle(direction, referenceUp);
        float angleDown = Vector3.Angle(direction, referenceDown);

        float minAngle = Mathf.Min(angleRight, angleLeft, angleUp, angleDown);

        if (minAngle == angleRight)
        {
            return "Right";
        }
        else if (minAngle == angleLeft)
        {
            return "Left";
        }
        else if (minAngle == angleUp)
        {
            return "Up";
        }
        else if (minAngle == angleDown)
        {
            return "Down";
        }
        else
        {
            return "";
        }
    }

    public void CheckToRoll()
    {
        if (MaxSpaces == 0)
        {
            MoveCamera(1);
        }
    }
    void Move(Vector3 targetPos)
    {
        LeanTween.moveX(Player.gameObject, targetPos.x, .4f).setEaseInOutCirc();
        LeanTween.moveZ(Player.gameObject, targetPos.z, .4f).setEaseInOutCirc().setOnComplete(() => AfterMove());
        Debug.Log("MOVE");
        float xChange = Mathf.Abs(Player.position.x - targetPos.x);
        float zChange = Mathf.Abs(Player.position.z - targetPos.z);
        bool alreadyOne = false;
        if (MaxSpaces == 1)
        {
            alreadyOne = true;
        }
        MaxSpaces = MaxSpaces - Mathf.RoundToInt((xChange + zChange) / 1.2f);
        if (MaxSpaces == 1 && alreadyOne == true)
        {
            MaxSpaces = 0;
        }
        MoveCounter.text = "Moves: " + MaxSpaces;
        if (MaxSpaces == 0)
        {
            RollButton.SetActive(true);
        }
    }
    public void AfterMove()
    {
        CheckArrowMovement();
    }
    public void ToggleCamera()
    {
        isMainCamera = !isMainCamera;
        MoveCamera(isMainCamera ? 0 : 2);
    }
    public void MoveCamera(int num)
    {
        if(num != 2)
        {
            Camera.main.transform.SetParent(MainCameraParent);
        }
        LeanTween.move(Camera.main.gameObject, CameraPosList[num].transform.position, .7f).setEaseInOutCubic();
        LeanTween.value(Camera.main.gameObject, (float val) => { Camera.main.orthographicSize = val; }, Camera.main.orthographicSize, CameraPosList[num].size, .7f).setEaseInOutCubic();
        LeanTween.rotate(Camera.main.gameObject, CameraPosList[num].transform.rotation.eulerAngles, .7f).setEaseInOutCubic().setOnComplete(() => {
            switch (num)
            {
                case 0:
                    break;
                case 1:
                    RollDice();
                    break;
                case 2:
                    Camera.main.transform.SetParent(CameraPosList[num].transform);
                    break;
            }
        });
    }
    void RollDice()
    {
        Dice.gameObject.transform.position = startingPos;
        Dice.gameObject.transform.rotation = startingRotation;

        Vector3 forceDirection = transform.up;
        Vector3 force = forceDirection * UnityEngine.Random.Range(minDiceMag, maxDiceMag);
        Dice.AddForce(force, ForceMode.Impulse);

        float torqueForce = UnityEngine.Random.Range(minDiceTorque, maxDiceTorque);
        Vector3 torqueAxis = UnityEngine.Random.insideUnitSphere;
        Dice.AddTorque(torqueAxis * torqueForce, ForceMode.Impulse);
        StartCoroutine(WaitForDice());
    }
    IEnumerator WaitForDice()
    {
        yield return new WaitForSeconds(1.45f);
        MaxSpaces = floor.DiceNumber;
        RollButton.SetActive(false);
        MoveCounter.text = "Moves: " + MaxSpaces;
        MoveCamera(isMainCamera ? 0 : 2);
    }
}
