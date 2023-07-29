using System;
using UnityEngine;

[Serializable]
public class Door
{
    public GameObject doorReference;
    public float width;
    public float height;
    public float middleOffset;

    public Door()
    {
        doorReference = null;
        width = 0;
        height = 0;
        middleOffset = 0;
    }

    public void SaveDoor(GameObject door, float width, float height, float middleOffset)
    {
        this.doorReference = door;
        this.width = width;
        this.height = height;
        this.middleOffset = middleOffset;
    }
    public void DeleteDoor()
    {
        this.doorReference = null;
        this.width = 0;
        this.height = 0;
        this.middleOffset = 0;
    }

    public void UpdateDimensions(float width, float height)
    {
        this.width = width;
        this.height = height;
    }

    public void UpdateLocation(float middleOffset)
    {
        this.middleOffset = middleOffset;
    }

    public string GetDoorData()
    {
        string data = "{ doorReference: " + doorReference +
                        ", width: " + width +
                        ", height: " + height +
                        ", middleOffset: " + middleOffset + " }";

        return data;
    }
}
