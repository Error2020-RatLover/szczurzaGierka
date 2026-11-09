using UnityEngine;
using UnityEngine.InputSystem;

public class playerView : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 movement;

    public float speed = 5f;


    public float zoomSpeed = 0.5f;
    public float minZoom = -3f;
    public float maxZoom = -10f;

    

    private Transform cameraTransform;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        cameraTransform = GetComponentInChildren<Camera>().transform;

    }

    void Update()
    {
        movement = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 move = new Vector2(
            movement.x,
            movement.y
        );

        transform.Translate(move * speed * Time.deltaTime);


        Vector2 scroll = playerInput.actions["Zoom"].ReadValue<Vector2>();

        float zoom = scroll.y * zoomSpeed;

        Vector3 cameraPosition = cameraTransform.localPosition;

        cameraPosition.z += zoom;

        cameraPosition.z = Mathf.Clamp(cameraPosition.z, maxZoom, minZoom);

        cameraTransform.localPosition = cameraPosition;
    }
}