using UnityEngine;
using UnityEngine.UI;

public class AddEntity : MonoBehaviour
{
    [SerializeField] GameObject building;

    public Wall north;
    public Wall east;
    public Wall south;
    public Wall west;

    Wall currentWall;

    [SerializeField] Toggle northToggle;
    [SerializeField] Toggle eastToggle;
    [SerializeField] Toggle westToggle;
    [SerializeField] Toggle southToggle;

    [SerializeField] Button addDoor;
    [SerializeField] Button removeDoor;

    [SerializeField] GameObject doorPrefab;
    public InputField doorWidth;
    public InputField doorHeight;
    float initialDoorWidth = 2f;
    float initialDoorHeight = 4f;

    public Slider doorLocationSlider;
    Vector3 doorScaleValue;

    private void Start()
    {
        north = new Wall();
        east = new Wall();
        south = new Wall();
        west = new Wall();

        currentWall = new Wall();

        north.SetWallReference(building.transform.Find("North").gameObject);
        east.SetWallReference(building.transform.Find("East").gameObject);
        south.SetWallReference(building.transform.Find("South").gameObject);
        west.SetWallReference(building.transform.Find("West").gameObject);
        currentWall = north; // default

        // UI
        northToggle.isOn = true;
        eastToggle.isOn = false;
        westToggle.isOn = false;
        southToggle.isOn = false;

        northToggle.onValueChanged.AddListener(OnNorthToggle);
        eastToggle.onValueChanged.AddListener(OnEastToggle);
        southToggle.onValueChanged.AddListener(OnSouthToggle);
        westToggle.onValueChanged.AddListener(OnWestToggle);

        addDoor.onClick.AddListener(AddDoor);
        removeDoor.onClick.AddListener(RemoveDoor);

        doorWidth.text = initialDoorWidth.ToString();
        doorHeight.text = initialDoorHeight.ToString();

        addWindow.onClick.AddListener(AddWindow);
        removeWindow.onClick.AddListener(RemoveWindow);
    }

    #region SideToggles

    public void OnNorthToggle(bool isToggledOn)
    {
        if (isToggledOn)
        {
            currentWall = north;
            eastToggle.isOn = false;
            westToggle.isOn = false;
            southToggle.isOn = false;

            if (north.door.doorReference != null)
            {
                // door present
                ResetAddDoorUI(true);
            }
            else if (north.door.doorReference == null)
            {
                ResetAddDoorUI(false);
            }

            ResetAddWindowUI(north);
        }
        else
        {
            //Debug.Log("Toggle is OFF");
        }
    }

    public void OnEastToggle(bool isToggledOn)
    {
        if (isToggledOn)
        {
            currentWall = east;
            northToggle.isOn = false;
            westToggle.isOn = false;
            southToggle.isOn = false;

            if (east.door.doorReference != null)
            {
                // door present
                ResetAddDoorUI(true);
            }
            else if (east.door.doorReference == null)
            {
                ResetAddDoorUI(false);
            }

            ResetAddWindowUI(east);
        }
        else
        {
            //Debug.Log("Toggle is OFF");
        }
    }

    public void OnSouthToggle(bool isToggledOn)
    {
        if (isToggledOn)
        {
            currentWall = south;
            eastToggle.isOn = false;
            westToggle.isOn = false;
            northToggle.isOn = false;

            if (south.door.doorReference != null)
            {
                // door present
                ResetAddDoorUI(true);
            }
            else if (south.door.doorReference == null)
            {
                ResetAddDoorUI(false);
            }

            ResetAddWindowUI(south);
        }
        else
        {
            //Debug.Log("Toggle is OFF");
        }
    }

    public void OnWestToggle(bool isToggledOn)
    {
        if (isToggledOn)
        {
            currentWall = west;
            eastToggle.isOn = false;
            northToggle.isOn = false;
            southToggle.isOn = false;

            if (west.door.doorReference != null)
            {
                // door present
                ResetAddDoorUI(true);
            }
            else if (west.door.doorReference == null)
            {
                ResetAddDoorUI(false);
            }

            ResetAddWindowUI(west);
        }
        else
        {
            //Debug.Log("Toggle is OFF");
        }
    }

    private void ResetAddDoorUI(bool isDoorPresent)
    {
        if (isDoorPresent)
        {
            addDoor.interactable = false;
            removeDoor.interactable = true;
        }
        else
        {
            addDoor.interactable = true;
            removeDoor.interactable = false;

        }
    }

    #endregion

    #region Adding and Removing door
    private void AddDoor()
    {
        // UI
        addDoor.interactable = false;
        removeDoor.interactable = true;

        // 3D
        InstantiateDoor(currentWall.wallReference.transform.localPosition, currentWall.wallReference.transform.localEulerAngles);

        if (doorWidth.onValueChanged.GetPersistentEventCount() == 0)
        {
            // If no listener is attached, add the listener
            doorWidth.onValueChanged.AddListener(OnDoorWidthInput);
        }
        if (doorHeight.onValueChanged.GetPersistentEventCount() == 0)
        {
            // If no listener is attached, add the listener
            doorHeight.onValueChanged.AddListener(OnDoorHeightInput);
        }
        if (doorLocationSlider.onValueChanged.GetPersistentEventCount() == 0)
        {
            doorLocationSlider.onValueChanged.AddListener(OnDoorSliderValueChanged);
        }

        CheckWallData(currentWall);

    }

    public void InstantiateDoor(Vector3 position, Vector3 eulerAngle)
    {
        eulerAngle = new Vector3(eulerAngle.x, eulerAngle.y - 90, eulerAngle.z);
        Quaternion rotation = Quaternion.Euler(eulerAngle);

        // add in database
        currentWall.door.SaveDoor(Instantiate(doorPrefab, position, rotation), initialDoorWidth, initialDoorHeight, 0.0f);
        // add in heirarchy
        currentWall.door.doorReference.transform.parent = currentWall.wallReference.transform;
    }

    private void RemoveDoor()
    {
        // UI
        removeDoor.interactable = false;
        addDoor.interactable = true;

        // 3D
        // delete from heirarchy
        Destroy(currentWall.wallReference.transform.Find("Door(Clone)").gameObject);
        // delete from database
        currentWall.door.DeleteDoor();

        CheckWallData(currentWall);
    }

    #endregion

    public void OnDoorWidthInput(string input)
    {
        // TODO: consider the condition of horizontal offset while increasing the width
        float scaleChange = float.Parse(input);

        Vector3 originalDoorScale = currentWall.door.doorReference.transform.localScale;
        Vector3 originalWallScale = currentWall.wallReference.transform.localScale;

        float finalWidth = initialDoorWidth;

        if (scaleChange <= 0)
        {
            finalWidth = initialDoorWidth / originalWallScale.z;
        }
        else if (scaleChange > originalWallScale.z)
        {
            finalWidth = 1;
        }
        else
        {
            finalWidth = scaleChange / originalWallScale.z;
        }

        // change in 3D
        currentWall.door.doorReference.transform.localScale = new Vector3(finalWidth, originalDoorScale.y, originalDoorScale.z);
        // save in database
        currentWall.door.UpdateDimensions(finalWidth, originalDoorScale.y);

        CheckWallData(currentWall);
    }

    private void OnDoorHeightInput(string input)
    {
        float scaleChange = float.Parse(input);

        Vector3 originalDoorScale = currentWall.door.doorReference.transform.localScale;
        Vector3 originalWallScale = currentWall.wallReference.transform.localScale;

        float finalHeight = initialDoorHeight;

        if (scaleChange <= 0)
        {
            finalHeight = initialDoorHeight / originalWallScale.y;
        }
        else if (scaleChange > originalWallScale.y)
        {
            finalHeight = 1;
        }
        else
        {
            finalHeight = scaleChange / originalWallScale.y;
        }

        // change in 3D
        currentWall.door.doorReference.transform.localScale = new Vector3(originalDoorScale.x, finalHeight, originalDoorScale.z);
        // save in database
        currentWall.door.UpdateDimensions(originalDoorScale.x, finalHeight);

        CheckWallData(currentWall);
    }

    private void OnDoorSliderValueChanged(float sliderValue)
    {
        Vector3 currPosition = currentWall.wallReference.transform.Find("Door(Clone)").localPosition;
        doorScaleValue = currentWall.door.doorReference.transform.localScale;

        float limit = 1 - doorScaleValue.x;
        // change in 3D
        if (sliderValue <= limit && sliderValue >= -limit)
            currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3(currPosition.x, currPosition.y, sliderValue * -0.5f);
        else if (sliderValue > limit)
            currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3(currPosition.x, currPosition.y, -0.5f * limit);
        else if (sliderValue < -limit)
            currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3(currPosition.x, currPosition.y, 0.5f * limit);

        // save in database
        currentWall.door.UpdateLocation(-currentWall.wallReference.transform.Find("Door(Clone)").localPosition.z);

        CheckWallData(currentWall);
    }

    [SerializeField] Button addWindow;
    [SerializeField] Button removeWindow;
    [SerializeField] Text windowCountText;
    [SerializeField] GameObject windowPrefab;
    float initialWindowWidth = 4;
    float initialWindowHeight = 4;
    private void AddWindow()
    {
        currentWall.windowCount++;

        if (currentWall.windowCount > 0)
        {
            removeWindow.interactable = true;
        }

        if (currentWall.windowCount < 10)
        {
            windowCountText.text = currentWall.windowCount.ToString();
            InstantiateWindow(currentWall.wallReference.transform.position, currentWall.wallReference.transform.localEulerAngles);
        }
        if (currentWall.windowCount >= 10)
        {
            currentWall.windowCount = 10;
            windowCountText.text = currentWall.windowCount.ToString();
            addWindow.interactable = false;
        }

        CheckWallData(currentWall);
    }

    private void RemoveWindow()
    {
        // PROBABLE BUG: null reference exception when trying to delete a window after maxing out window count
        // delete from 3D
        Destroy(currentWall.wallReference.transform.Find("Window(Clone)_" + currentWall.windowCount).gameObject);
        // remove from database
        currentWall.DeleteWindowData(currentWall.windows[currentWall.windowCount - 1]);
        currentWall.windowCount--;

        windowCountText.text = currentWall.windowCount.ToString();

        ResetAddWindowUI(currentWall);

        CheckWallData(currentWall);
        
    }
    public void InstantiateWindow(Vector3 position, Vector3 eulerAngle)
    {
        eulerAngle = new Vector3(eulerAngle.x, eulerAngle.y - 90, eulerAngle.z);
        Quaternion rotation = Quaternion.Euler(eulerAngle);

        // add in database
        GameObject instantiatedWindow = Instantiate(windowPrefab, position, rotation);
        instantiatedWindow.name = "Window(Clone)_" + currentWall.windowCount;
        currentWall.SetWindowData(instantiatedWindow, initialWindowWidth, initialWindowHeight, 0.0f, 0.0f);
        // add in heirarchy
        currentWall.windows[currentWall.windowCount - 1].windowReference.transform.parent = currentWall.wallReference.transform;

        
    }


    private void ResetAddWindowUI(Wall currentWall)
    {
        windowCountText.text = currentWall.windowCount.ToString();
        if (currentWall.windowCount == 0)
        {
            addWindow.interactable = true;
            removeWindow.interactable = false;
        }
        else if (currentWall.windowCount == 10)
        {
            addWindow.interactable = false;
            removeWindow.interactable = true;
        }
        else
        {
            addWindow.interactable = true;
            removeWindow.interactable = true;
        }
    }

    private void CheckWallData(Wall wall)
    {
        wall.PrintWallData();
    }
}