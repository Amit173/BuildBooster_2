using UnityEngine;
using UnityEngine.UI;

public class AddEntity : MonoBehaviour
{
    [SerializeField] GameObject building;

    public Wall north;
    public Wall east;
    public Wall south;
    public Wall west;

    public Wall currentWall;

    [SerializeField] Toggle northToggle;
    [SerializeField] Toggle eastToggle;
    [SerializeField] Toggle westToggle;
    [SerializeField] Toggle southToggle;

    [SerializeField] string northWallName;
    [SerializeField] string eastWallName;
    [SerializeField] string westWallName;
    [SerializeField] string southWallName;

    [SerializeField] Button addDoor;
    [SerializeField] Button removeDoor;

    [SerializeField] GameObject doorPrefab;
    public InputField doorWidth;
    public InputField doorHeight;
    float initialDoorWidth = 2f;
    float initialDoorHeight = 4f;
    float windowHeight = 0;
    public Slider doorLocationSlider;
    Vector3 doorScaleValue;

    public float positionMarker = 0.3048f;

    string doorCloneName = "Door(Clone)";
    string windowCloneName = "Window(Clone)";


    [SerializeField] Button addWindow;
    [SerializeField] Button removeWindow;
    [SerializeField] Text windowCountText;
    [SerializeField] GameObject windowPrefab;
    float initialWindowWidth = 4;
    float initialWindowHeight = 4;

    private void Start()
    {
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
    public void Deinit()
    {
        north.SetWallReference(null);
        east.SetWallReference(null);
        south.SetWallReference(null);
        west.SetWallReference(null);
        CheckWallData(north);
    }
    public void InitiializeWalls()
    {

        north = new Wall();
        east = new Wall();
        south = new Wall();
        west = new Wall();

        currentWall = new Wall();

        currentWall = north; // default
        CheckWallData(north);
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
        Vector3 currentWallPosition = currentWall.wallReference.transform.localPosition;
        Vector3 offset = Vector3.zero;
        Vector3 finalPosition = Vector3.zero;
        Vector3 finalRot = Vector3.zero;

        float sliderMin = 0;
        float sliderMax = 0;
        float sliderCurr = 0;

        sliderMin = doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z / 2 + positionMarker;

        float managerWidth = Manager.instance.width;
        float managerLength = Manager.instance.length;

        float doorPrefabBound = doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z / 2 - positionMarker;

        if (currentWall == north)
        {;
            finalPosition = new Vector3((managerWidth / 2) * positionMarker, 0, 0);
            sliderMax = managerWidth * positionMarker - doorPrefabBound;
            sliderCurr = (managerWidth / 2) * positionMarker;
            finalRot = new Vector3(0, 90, 0);
        }
        else if (currentWall == east)
        {
            finalPosition = new Vector3((managerWidth) * positionMarker, 0, (managerLength / 2) * positionMarker);
            sliderMax = managerLength * positionMarker - doorPrefabBound;
            sliderCurr = (managerLength / 2) * positionMarker;
            finalRot = new Vector3(0, 0, 0);
        }
        else if (currentWall == south)
        {
            finalPosition = new Vector3((managerWidth / 2) * positionMarker, 0, (managerLength) * positionMarker);
            sliderMax = managerWidth * positionMarker - doorPrefabBound;
            sliderCurr = (managerWidth / 2) * positionMarker;
            finalRot = new Vector3(0, -90, 0);
        }
        else if (currentWall == west)
        {
            finalPosition = new Vector3(0, 0, (managerLength / 2) * positionMarker);
            sliderMax = managerLength * positionMarker - doorPrefabBound;
            sliderCurr = (managerLength / 2) * positionMarker;
            finalRot = new Vector3(0, 180, 0);
        }

        InstantiateDoor(finalPosition, finalRot);

        doorLocationSlider.minValue = sliderMin;
        doorLocationSlider.maxValue = sliderMax;
        doorLocationSlider.value = sliderCurr;

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
        Destroy(currentWall.wallReference.transform.Find(doorCloneName).gameObject);
        // delete from database
        currentWall.door.DeleteDoor();

        CheckWallData(currentWall);
    }

    #endregion

    #region Door Dimension Update

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
        Vector3 currPosition = currentWall.wallReference.transform.Find(doorCloneName).localPosition;
        doorScaleValue = currentWall.door.doorReference.transform.localScale;

        // float limit = 1 - doorScaleValue.x;

        // change in 3D
        if (currentWall == north)
        {
            Transform door = currentWall.wallReference.transform.Find(doorCloneName);
            currentWall.wallReference.transform.Find(doorCloneName).localPosition = new Vector3((sliderValue), 0, 0);
        }
        else if (currentWall == east)
        {
            currentWall.wallReference.transform.Find(doorCloneName).localPosition = new Vector3((Manager.instance.width) * positionMarker, 0, sliderValue);
        }
        else if (currentWall == south)
        {
            currentWall.wallReference.transform.Find(doorCloneName).localPosition = new Vector3((sliderValue), 0, (Manager.instance.length) * positionMarker);
        }
        else if (currentWall == west)
        {
            currentWall.wallReference.transform.Find(doorCloneName).localPosition = new Vector3(0, 0, (sliderValue));
        }

        CheckWallData(currentWall);
    }

    #endregion

    #region Add and Remove Window
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

            Vector3 offset = Vector3.zero;
            Vector3 finalPosition = Vector3.zero;
            Vector3 finalRot = Vector3.zero;

            windowHeight = (Manager.instance.height / 2) * positionMarker;

            float sliderMin = 0;
            float sliderMax = 0;
            float sliderCurr = 0;

            float managerWidth = Manager.instance.width;
            float managerLength = Manager.instance.length;

            sliderMin = int.Parse(doorWidth.text) * positionMarker;

            float sliderMaxFactor = positionMarker - int.Parse(doorWidth.text) * positionMarker;

            if (currentWall == north)
            {
                finalPosition = new Vector3((managerWidth / 4) * positionMarker, windowHeight / 2, 0);
                sliderMax = managerWidth * sliderMaxFactor;
                sliderCurr = (managerWidth / 2) * positionMarker;
                finalRot = new Vector3(0, 90, 0);
            }
            else if (currentWall == east)
            {
                finalPosition = new Vector3((managerWidth) * positionMarker, windowHeight / 2, (managerLength / 4) * positionMarker);
                sliderMax = managerLength * sliderMaxFactor;
                sliderCurr = (managerLength / 2) * positionMarker;
                finalRot = new Vector3(0, 0, 0);
            }
            else if (currentWall == south)
            {
                finalPosition = new Vector3((managerWidth / 4) * positionMarker, windowHeight / 2, (managerLength) * positionMarker);
                sliderMax = managerWidth * sliderMaxFactor;
                sliderCurr = (managerWidth / 2) * positionMarker;
                finalRot = new Vector3(0, -90, 0);
            }
            else if (currentWall == west)
            {
                finalPosition = new Vector3(0, windowHeight / 2, (managerLength / 4) * positionMarker);
                sliderMax = managerLength * sliderMaxFactor;
                sliderCurr = (managerLength / 2) * positionMarker;
                finalRot = new Vector3(0, 180, 0);
            }
            InstantiateWindow(finalPosition, finalRot);
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
        Destroy(currentWall.wallReference.transform.Find(windowCloneName + "_" + currentWall.windowCount).gameObject);
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
        instantiatedWindow.name = windowCloneName + "_" + currentWall.windowCount;
        currentWall.SetWindowData(instantiatedWindow, initialWindowWidth, initialWindowHeight, 0.0f, 0.0f);
        // add in heirarchy
        currentWall.windows[currentWall.windowCount - 1].windowReference.transform.parent = currentWall.wallReference.transform;
    }

    #endregion

    #region Default Helper Functions
    public void UpdateDoorPositions()
    {
        if (north.wallReference.transform.Find(doorCloneName))
        {
            north.wallReference.transform.Find(doorCloneName).transform.localPosition = new Vector3((Manager.instance.width / 2) * positionMarker, 0, 0);
        }

        if (east.wallReference.transform.Find(doorCloneName))
        {
            east.wallReference.transform.Find(doorCloneName).transform.localPosition = new Vector3((Manager.instance.width) * positionMarker, 0, (Manager.instance.length / 2) * positionMarker);
        }

        if (south.wallReference.transform.Find(doorCloneName))
        {
            south.wallReference.transform.Find(doorCloneName).transform.localPosition = new Vector3(0, 0, (Manager.instance.length / 2) * positionMarker);
        }

        if (west.wallReference.transform.Find(doorCloneName))
        {
            west.wallReference.transform.Find(doorCloneName).transform.localPosition = new Vector3(0, 0, (Manager.instance.length / 2) * positionMarker);
        }

    }

    public void UpdateWindowPositions()
    {
        windowHeight = (Manager.instance.height / 2) * positionMarker;

        if (north.wallReference.transform.Find(windowCloneName))
        {
            north.wallReference.transform.Find(windowCloneName).transform.localPosition = new Vector3((Manager.instance.width / 4) * positionMarker, windowHeight / 2, 0);
        }

        if (east.wallReference.transform.Find(windowCloneName))
        {
            east.wallReference.transform.Find(windowCloneName).transform.localPosition = new Vector3((Manager.instance.width) * positionMarker, windowHeight / 2, (Manager.instance.length / 4) * positionMarker);
        }

        if (south.wallReference.transform.Find(windowCloneName))
        {
            south.wallReference.transform.Find(windowCloneName).transform.localPosition = new Vector3((Manager.instance.width / 4) * positionMarker, windowHeight / 2, (Manager.instance.length) * positionMarker);
        }

        if (west.wallReference.transform.Find(windowCloneName))
        {
            west.wallReference.transform.Find(windowCloneName).transform.localPosition = new Vector3(0, windowHeight / 2, (Manager.instance.length / 4) * positionMarker);
        }
    }

    #endregion

    #region Utility Functions

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

    public void CheckWallData(Wall wall)
    {
        wall.PrintWallData();
    }

    #endregion
}