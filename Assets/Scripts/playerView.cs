using UnityEngine;
using UnityEngine.InputSystem;

public class playerView : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 movement;

    public float speed = 5f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        movement = playerInput.actions["Move"].ReadValue<Vector2>();

        Vector3 move = new Vector3(
            movement.x,
            movement.y,
            0
            
        );

        transform.Translate(move * speed * Time.deltaTime);
    }
}