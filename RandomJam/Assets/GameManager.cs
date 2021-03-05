using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform objectivesList;
    int SelectedObjective = 1000;
    Camera mainCamera;
    DirectionPointer directionPointerScript;
    Transform currentObjective;
    int selected;
    bool once;

    // Start is called before the first frame update
    void Start()
    {
        selected = 0;
        mainCamera = Camera.main;
        directionPointerScript = mainCamera.GetComponent<DirectionPointer>();
        SelectNewObjective();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentObjective.GetComponent<MissionObjective>().reached)
        {
            SelectNewObjective();
        }
    }

    void SelectNewObjective()
    {
        if (!once)
        {
            once = true;

            Debug.Log("Selecting a new objective!");
            //int selected = SelectedObjective;

            //First time
            if (SelectedObjective == 1000)
            {
                selected = UnityEngine.Random.Range(0, objectivesList.transform.childCount);
            }
            else
            {
                while (SelectedObjective == selected)
                {
                    selected = UnityEngine.Random.Range(0, objectivesList.transform.childCount);
                }

            }
            SelectedObjective = selected;
            currentObjective = objectivesList.GetChild(SelectedObjective);
            UpdateMarker();
            once = false;
        }
    }
    void UpdateMarker()
    {
        //Add sound of changing obj

        //Changing UI icon to show new destination
        directionPointerScript.target = currentObjective;
    }
}
