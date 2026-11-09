using UnityEngine;
using UnityEngine.InputSystem;

public class playerView : MonoBehaviour
{

    //Player Input to rodzaj taki nasz zbiór inputów który już ma wbudowane dużo logiki sam w sobi
    private PlayerInput playerInput;
    private Vector2 movement;
    //mam ci to tłumaczyć? nie wiesz co to speed?
    public float speed = 5f;

    //zmienne dotyczące zooma
    public float zoomSpeed = 0.5f;
    public float minZoom = -3f;
    public float maxZoom = -10f;

    
    //transform dla camery
    private Transform cameraTransform;

    void Awake()
    {

        //pobieranie odpowiedniednich zmiennych 
        playerInput = GetComponent<PlayerInput>();

        cameraTransform = GetComponentInChildren<Camera>().transform;

    }

    void Update()
    {
        // tu ogólnie jest dużo logiki w unity dzięki komponentowi input. 
        // pobiera wartość akcji z input Move i przekształca automatycznie na Vector2
        movement = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 move = new Vector2(
            movement.x,
            movement.y
        );
        // przesunięcie
        transform.Translate(move * speed * Time.deltaTime);

        //logika scrolla, wartość pobierana jak poprzednio
        Vector2 scroll = playerInput.actions["Zoom"].ReadValue<Vector2>();

        float zoom = scroll.y * zoomSpeed;

        Vector3 cameraPosition = cameraTransform.localPosition;

        cameraPosition.z += zoom;

        //sprawdzanie czy zoom mieści się w ramach
        cameraPosition.z = Mathf.Clamp(cameraPosition.z, maxZoom, minZoom);

        cameraTransform.localPosition = cameraPosition;
    }
}