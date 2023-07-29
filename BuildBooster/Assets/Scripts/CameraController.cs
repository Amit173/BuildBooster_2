using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private RaycastHit hit;
    [SerializeField, Range(0, 2)]
    private float mouseDragSensitivity;
    [SerializeField, Range(0, 40)]
    private float mouseScrollSensitivity;
    [SerializeField] private float zoom;
    [SerializeField]
    private FixedTouchField TF;
    private float rotationY;
    private float rotationX;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget = 3.0f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    private float zoomVeclocity = 0f;
    [SerializeField]
    private float smoothTime = 0.2f;

    [SerializeField]
    private Vector2 rotationXMinMax = new Vector2(-40, 40);
    [SerializeField]
    private float minZoom;
    [SerializeField]
    private float maxZoom;

    private float panConstant = 0;
    private void Start()
    {
        zoom = camera.fieldOfView;
        
    }

    void Update()
    {

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log(hit.collider.gameObject.name);
                Manager.instance.SetCurrentPart(hit.collider.GetComponent<Part>());
            }

        }
        Zoom();
        OrbitAround();
        Pan();
    }

    public void OrbitAround()
    {
        float mouseX = TF.touchDist.x * mouseDragSensitivity;
        float mouseY = -TF.touchDist.y * mouseDragSensitivity;

        rotationY += mouseX;
        rotationX += mouseY;

        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(rotationX, rotationY);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;
        transform.position = target.position - (transform.forward * (distanceFromTarget + zoom));
        
    }
    public void Pan()
    {
        if(Input.GetMouseButtonDown(2))
        {
            panConstant = panConstant + 1;
        }
    }
    public void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * mouseScrollSensitivity;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
       // camera.fieldOfView = Mathf.SmoothDamp(camera.fieldOfView, zoom, ref zoomVeclocity, 0.25f);
    }
}
