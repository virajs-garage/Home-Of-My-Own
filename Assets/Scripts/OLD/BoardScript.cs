using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardScript : MonoBehaviour
{
    public GameObject SpacesHolder;
    public List<Space> SpacesOnBoard = new List<Space>();
    public float searchRadius;

    public class Space
    {
        public Transform transform;
        public List<Transform> closeSpaces = new List<Transform>();
    }
    private void Start()
    {
        foreach (Transform child in SpacesHolder.transform)
        {
            SpacesOnBoard.Add(new Space { transform = child, closeSpaces = new List<Transform>() });
        }
        FindAllCloseSpaces();
    }
    public void FindAllCloseSpaces()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Space");
        foreach (Space space in SpacesOnBoard)
        {
            foreach (GameObject taggedObject in taggedObjects)
            {
                float distance = Vector3.Distance(taggedObject.transform.position, space.transform.position);
                if (distance <= searchRadius)
                {
                    space.closeSpaces.Add(taggedObject.transform);
                }
            }
        }
    }
}
