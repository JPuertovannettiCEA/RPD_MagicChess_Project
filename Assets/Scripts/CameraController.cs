using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [Header("Key Panning")]
    [SerializeField]
    private float _cameraPanSpeed = 0.5f;

    [SerializeField]
    private bool _invertedPanning = false;


    [Header("Edge Panning")]
    [SerializeField]
    private bool _useEdgePanning = false;

    [Tooltip("Amount of pixels around the edges of the screen, used for pushing the camera on edges")]
    [SerializeField]
    private int _cursorPanEdgeBoundarySize = 8;

    [SerializeField]
    private float _cameraEdgePandSpeed = 5f;


    [Header("Zooming")]
    [SerializeField]
    private Transform _zoomOutPos;

    [SerializeField]
    private Transform _zoomInPos;

    [SerializeField]
    private float _zoomReslution = 5f;

    [SerializeField]
    private float _zoomSmoothFactor = 0.12f;

    [SerializeField]
    private bool _useGMapsZoomStyle = true;

    [SerializeField]
    private LayerMask _terrainLayerMask;


    [Header("Rotating")]
    [SerializeField]
    private bool _invertedRotation = false;

    [SerializeField]
    private float _cameraRotateSpeed = 1f;


    [Header("Bounds")]
    [SerializeField]
    private Bounds _cameraBounds;

    private float _zoomValue; // between 0 and 1
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {

        Panning();
        Zooming();
        Rotating();

        transform.position = _cameraBounds.ClosestPoint(transform.position);
    }

    private void Panning()
    {
        // Pannning with mouse Middle Click
        if (Input.GetMouseButton(2))
        {
            float signMult = _invertedPanning ? 1f : -1f;
            float mouseMovementX = Input.GetAxis("Mouse X") * _cameraPanSpeed * signMult;
            float mouseMovementY = Input.GetAxis("Mouse Y") * _cameraPanSpeed * signMult;
            transform.Translate(mouseMovementX, 0f, mouseMovementY);
            return;
        }


        // Panning on Screen Edges
        if (_useEdgePanning == false) return;

        var mousePos = Input.mousePosition;
        if (mousePos.x < _cursorPanEdgeBoundarySize)
        {
            transform.Translate(-_cameraEdgePandSpeed * Time.deltaTime, 0f, 0f);
        }
        if (mousePos.y < _cursorPanEdgeBoundarySize)
        {
            transform.Translate(0f, 0f, -_cameraEdgePandSpeed * Time.deltaTime);
        }
        if (mousePos.x > Screen.width - _cursorPanEdgeBoundarySize)
        {
            transform.Translate(_cameraEdgePandSpeed * Time.deltaTime, 0f, 0f);
        }
        if (mousePos.y > Screen.height - _cursorPanEdgeBoundarySize)
        {
            transform.Translate(0f, 0f, _cameraEdgePandSpeed * Time.deltaTime);
        }
    }

    private void Zooming()
    {
        var mousePosBefore = Vector3.zero;
        if (_useGMapsZoomStyle)
        {
            mousePosBefore = GetMouseHitPoint();
        }

        var mouseScrollDelta = Input.mouseScrollDelta.y;

        _zoomValue += mouseScrollDelta / _zoomReslution;
        _zoomValue = Mathf.Clamp01(_zoomValue);

        var newCamPos = Vector3.Lerp(_zoomOutPos.localPosition, _zoomInPos.localPosition, _zoomValue);
        _cam.transform.localPosition = Vector3.Lerp(_cam.transform.localPosition, newCamPos, _zoomSmoothFactor * Time.deltaTime);

        var newCamRot = Quaternion.Slerp(_zoomOutPos.localRotation, _zoomInPos.localRotation, _zoomValue);
        _cam.transform.localRotation = Quaternion.Slerp(_cam.transform.localRotation, newCamRot, _zoomSmoothFactor * Time.deltaTime);

        if (_useGMapsZoomStyle)
        {
            var mousePosAfter = GetMouseHitPoint();
            var mouseZoomOffset = mousePosBefore - mousePosAfter;
            transform.Translate(mouseZoomOffset, Space.World);
        }
    }

    private void Rotating()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseMovementX = Input.GetAxis("Mouse X");
            mouseMovementX *= _invertedRotation ? -1f : 1f;

            transform.Rotate(0f, mouseMovementX * _cameraRotateSpeed, 0f);
        }
    }

    private Vector3 GetMouseHitPoint()
    {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 300f, _terrainLayerMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(_cameraBounds.center, _cameraBounds.size);
    }
}
