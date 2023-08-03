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
        Debug.Log("Deinit");
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

        //north.SetWallReference(building.transform.Find(northWallName).gameObject);
        //east.SetWallReference(building.transform.Find(eastWallName).gameObject);
        //south.SetWallReference(building.transform.Find(southWallName).gameObject);
        //west.SetWallReference(building.transform.Find(westWallName).gameObject);
        currentWall = north; // default
        Debug.Log("walls initialized");
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

        if (currentWall == north)
        {
            Debug.Log("north");
            finalPosition = new Vector3((Manager.instance.width / 2) * positionMarker, 0, 0);
            sliderMin = doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z/2 + positionMarker;
            sliderMax = Manager.instance.width * positionMarker - doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z / 2 -positionMarker;
            sliderCurr = (Manager.instance.width / 2) * positionMarker;
            finalRot = new Vector3(0, 90, 0);
            // offset = something;
        }
        else if (currentWall == east)
        {
            Debug.Log("east");
            finalPosition = new Vector3((Manager.instance.width) * positionMarker, 0, (Manager.instance.length / 2) * positionMarker);
            sliderMin = doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z / 2 + positionMarker;
            sliderMax = Manager.instance.length * positionMarker - doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z / 2 - positionMarker;
            sliderCurr = (Manager.instance.length / 2) * positionMarker;
            finalRot = new Vector3(0, 0, 0);
            // offset = something;
        }
        else if (currentWall == south)
        {
            Debug.Log("south");
            finalPosition = new Vector3((Manager.instance.width / 2) * positionMarker, 0, (Manager.instance.length) * positionMarker);
            sliderMin = doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z/2 + positionMarker;
            sliderMax = Manager.instance.width * positionMarker - doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z / 2 - positionMarker;
            sliderCurr = (Manager.instance.width / 2) * positionMarker;
            finalRot = new Vector3(0, -90, 0);
            // offset = something;
        }
        else if (currentWall == west)
        {
            Debug.Log("west");
            finalPosition = new Vector3(0, 0, (Manager.instance.length / 2) * positionMarker);
            sliderMin = doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z/2 + positionMarker;
            sliderMax = Manager.instance.length * positionMarker - doorPrefab.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh.bounds.size.z / 2 - positionMarker;
            sliderCurr = (Manager.instance.length / 2) * positionMarker;
            finalRot = new Vector3(0, 180, 0);
            // offset = something;
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
        Debug.Log("OnSliderValue :-" + sliderValue);
        Vector3 currPosition = currentWall.wallReference.transform.Find("Door(Clone)").localPosition;
        doorScaleValue = currentWall.door.doorReference.transform.localScale;

        float limit = 1 - doorScaleValue.x;
        // change in 3D
        if (currentWall == north)
        {
            Debug.Log("north");
            Transform door = currentWall.wallReference.transform.Find("Door(Clone)");
            currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3((sliderValue), 0, 0);
        }
        else if (currentWall == east)
        {
            Debug.Log("east");
            currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3((Manager.instance.width) * positionMarker, 0, sliderValue);
        }
        else if (currentWall == south)
        {
            Debug.Log("south");
            currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3((sliderValue), 0, (Manager.instance.length) * positionMarker);
        }
        else if (currentWall == west)
        {
            Debug.Log("west");
            currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3(0, 0, (sliderValue));
        }


        //if (sliderValue <= limit && sliderValue >= -limit)
        //    currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3(currPosition.x, currPosition.y, sliderValue * -0.5f);
        //else if (sliderValue > limit)
        //    currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3(currPosition.x, currPosition.y, -0.5f * limit);
        //else if (sliderValue < -limit)
        //    currentWall.wallReference.transform.Find("Door(Clone)").localPosition = new Vector3(currPosition.x, currPosition.y, 0.5f * limit);

        // save in database
        //currentWall.door.UpdateLocation(-currentWall.wallReference.transform.Find("Door(Clone)").localPosition.z);

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
            Vector3 offset = Vector3.zero;
            Vector3 finalPosition = Vector3.zero;
            Vector3 finalRot = Vector3.zero;
            windowHeight = (Manager.instance.height / 2) * positionMarker;
            float sliderMin = 0;
            float sliderMax = 0;
            float sliderCurr = 0;
            if (currentWall == north)
            {
                Debug.Log("north");
                finalPosition = new Vector3((Manager.instance.width / 4) * positionMarker, windowHeight / 2, 0);
                sliderMin = int.Parse(doorWidth.text) * positionMarker;
                sliderMax = Manager.instance.width * positionMarker - int.Parse(doorWidth.text) * positionMarker;
                sliderCurr = (Manager.instance.width / 2) * positionMarker;
                finalRot = new Vector3(0, 90, 0);
                // offset = something;
            }
            else if (currentWall == east)
            {
                Debug.Log("east");
                finalPosition = new Vector3((Manager.instance.width) * positionMarker, windowHeight / 2, (Manager.instance.length / 4) * positionMarker);
                sliderMin = int.Parse(doorWidth.text) * positionMarker;
                sliderMax = Manager.instance.length * positionMarker - int.Parse(doorWidth.text) * positionMarker;
                sliderCurr = (Manager.instance.length / 2) * positionMarker;
                finalRot = new Vector3(0, 0, 0);
                // offset = something;
            }
            else if (currentWall == south)
            {
                Debug.Log("south");
                finalPosition = new Vector3((Manager.instance.width / 4) * positionMarker, windowHeight / 2, (Manager.instance.length) * positionMarker);
                sliderMin = int.Parse(doorWidth.text) * positionMarker;
                sliderMax = Manager.instance.width * positionMarker - int.Parse(doorWidth.text) * positionMarker;
                sliderCurr = (Manager.instance.width / 2) * positionMarker;
                finalRot = new Vector3(0, -90, 0);
                // offset = something;
            }
            else if (currentWall == west)
            {
                Debug.Log("west");
                finalPosition = new Vector3(0, windowHeight / 2, (Manager.instance.length / 4) * positionMarker);
                sliderMin = int.Parse(doorWidth.text) * positionMarker;
                sliderMax = Manager.instance.length * positionMarker - int.Parse(doorWidth.text) * positionMarker;
                sliderCurr = (Manager.instance.length / 2) * positionMarker;
                finalRot = new Vector3(0, 180, 0);
                // offset = something;
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

    public void CheckWallData(Wall wall)
    {
        wall.PrintWallData();
    }

    public void updateDoorPositions()
    {
        Debug.Log(north.wallReference.transform.Find("Door(Clone)"));
        
        if(north.wallReference.transform.Find("Door(Clone)"))
        {
            north.wallReference.transform.Find("Door(Clone)").transform.localPosition = new Vector3((Manager.instance.width / 2) * positionMarker, 0, 0);
        }
       if(east.wallReference.transform.Find("Door(Clone)"))
            east.wallReference.transform.Find("Door(Clone)").transform.localPosition = new Vector3((Manager.instance.width) * positionMarker, 0, (Manager.instance.length / 2) * positionMarker);
       if(south.wallReference.transform.Find("Door(Clone)"))
            south.wallReference.transform.Find("Door(Clone)").transform.localPosition = new Vector3(0, 0, (Manager.instance.length / 2) * positionMarker);
        if (west.wallReference.transform.Find("Door(Clone)"))
            west.wallReference.transform.Find("Door(Clone)").transform.localPosition = new Vector3(0, 0, (Manager.instance.length / 2) * positionMarker);
    }

    public void updateWindowPositions()
    {
        windowHeight = (Manager.instance.height / 2) * positionMarker;
        if(north.wallReference.transform.Find("Window(Clone)"))
        north.wallReference.transform.Find("Window(Clone)").transform.localPosition = new Vector3((Manager.instance.width / 4) * positionMarker, windowHeight / 2, 0);
      
        if (east.wallReference.transform.Find("Window(Clone)"))
            east.wallReference.transform.Find("Window(Clone)").transform.localPosition = new Vector3((Manager.instance.width) * positionMarker, windowHeight / 2, (Manager.instance.length / 4) * positionMarker);
        if(south.wallReference.transform.Find("Window(Clone)"))
            south.wallReference.transform.Find("Window(Clone)").transform.localPosition = new Vector3((Manager.instance.width / 4) * positionMarker, windowHeight / 2, (Manager.instance.length) * positionMarker);
        if(west.wallReference.transform.Find("Window(Clone)"))
        west.wallReference.transform.Find("Window(Clone)").transform.localPosition = new Vector3(0, windowHeight / 2, (Manager.instance.length / 4) * positionMarker);
    }
}