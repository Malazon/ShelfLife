using UnityEngine;

public class CameraSingleton : MonoBehaviour
{
    [SerializeField] private float moveRate = 5f;

    public Transform TargetTransform;
    [SerializeField] private float turnRate = 720f;
    [SerializeField] private Camera _camera;

    
    public static CameraSingleton Active { get; private set; }
    
    public Camera Camera => _camera;

    private void Start()
    {
        if (Active != null)
            if (Active.gameObject.activeInHierarchy)
            {
                Debug.LogWarning("Tried to initiate a second primary camera.");
                gameObject.SetActive(false);
                return;
            }

        Active = this;
        TargetTransform = _camera.transform;
    }

    private void Update()
    {
        _camera.transform.position =
            Vector3.MoveTowards(_camera.transform.position, TargetTransform.position, moveRate * Time.deltaTime);
        _camera.transform.rotation = Quaternion.RotateTowards(_camera.transform.rotation, TargetTransform.rotation,
            turnRate * Time.deltaTime);
    }
    
    // World Mouse Position
    private static Vector3? _worldMousePosition;
    private static int _frameMousePositionUpdated = -1;

    private static void updateWorldMousePosition()
    {
        _frameMousePositionUpdated = Time.frameCount;
        
        // Check if active hasn't been set.
        if (Active == null)
        {
            _worldMousePosition = null;
            return;
        }
        
        // Check if the mouse is in frame.
        Rect screenRect = new Rect(0,0, Screen.width, Screen.height);
        if (!screenRect.Contains(Input.mousePosition))
            _worldMousePosition = null;
        
        // Update the position based on the current camera position.
        var mouseRay = Active._camera.ScreenPointToRay(Input.mousePosition);
        float distanceToGround = mouseRay.origin.y / mouseRay.direction.y;

        _worldMousePosition = mouseRay.origin + mouseRay.direction * distanceToGround;
    }
    public static Vector3? MouseWorldPosition
    {
        get
        {
            if (_frameMousePositionUpdated != Time.frameCount)
            {
                updateWorldMousePosition();
            }

            return _worldMousePosition;
        }
    }
}