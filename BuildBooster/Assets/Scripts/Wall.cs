using System;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

[Serializable]
public class Wall
{ 
    public GameObject wallReference;
    public Door door;
    public int windowCount;
    public List<Window> windows;

    public Wall()
    {
        wallReference = null;
        door = new Door();
        windows = new List<Window>();
    }

    public void SetWallReference(GameObject wallRef)
    {
        this.wallReference = wallRef;  
    }

    public void SetWindowData(GameObject window, float width, float height, float leftOffset, float bottomOffset)
    {
        
        Window tempWindow = new Window(window, width, height, leftOffset, bottomOffset);
        this.windows.Add(tempWindow);
    }

    public void DeleteWindowData(Window window)
    {
        this.windows.Remove(window);
    }

    public void PrintData()
    {
        Debug.Log("{ " + wallReference.name + ", " + door.doorReference + ", " + door.width + ", " + door.height + ", " + door.middleOffset + " }");
    }

    public string PrintWallData()
    {
        string doorData = door != null ? door.GetDoorData() : "null";

        string windowListData = "[]";
        if (windows.Count > 0)
        {
            List<string> windowDataList = new List<string>();
            foreach (Window window in windows)
            {
                windowDataList.Add(window.GetWindowData());
            }
            windowListData = "[" + string.Join(",", windowDataList) + "]";
        }

        string data = "{ " +
                      "wallReference: " + (wallReference != null ? wallReference.name : "null") +
                      ", door: " + doorData +
                      ", windowCount: " + windowCount +
                      ", windows: " + windowListData +
                      " }";
        Debug.Log(data);

        return data;

    }
}
