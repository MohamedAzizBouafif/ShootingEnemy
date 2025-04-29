using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GunController))]
public class Player : MonoBehaviour
{
    //initialisation
    InputManager inputManager;
    CharacterController characterController;
    Camera Viewcamera;


    [Header("mouvment")]
    [SerializeField] Vector3 moveToVector;
    [SerializeField] public float speed = 10f;
    [SerializeField] bool onMouvment = false;
    
    
    //shooting input
    bool isSooting = false;
    GunController gunController;

    private void Awake()
    {
        inputManager = new InputManager();
        characterController = GetComponent<CharacterController>();
        gunController = GetComponent<GunController>();
        Viewcamera = Camera.main;
    }

    private void Start()
    {
        inputManager.Player.move.started += MouvmentInput;
        inputManager.Player.move.performed += MouvmentInput;
        inputManager.Player.move.canceled += MouvmentInput;

        inputManager.Player.shoot.started += ShootingInput;
        inputManager.Player.shoot.canceled += ShootingInput;
    }

    private void Update()
    {
        lookAt();
        Shoot();
    }
    private void FixedUpdate()
    {
        Move(moveToVector, onMouvment);        
    }

    public void lookAt()
    {
        Ray ray = Viewcamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 lookAtDirection = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(lookAtDirection);
        }
    }
    public void Move(Vector3 _moveToVector, bool _onMouvment)
    {
        if (_onMouvment)
        {
            characterController.Move(_moveToVector * Time.deltaTime);
        }
    }

    private void ShootingInput(InputAction.CallbackContext ctx)
    {
        isSooting = ctx.ReadValueAsButton();
    }

    private void Shoot()
    {
        if (isSooting)
        {
            gunController.shoot();
        }
    }
    private void MouvmentInput(InputAction.CallbackContext ctx)
    {
        Vector2 _inputVector = ctx.ReadValue<Vector2>();
      
        Vector3 _moveToDirection;
        _moveToDirection.x = _inputVector.x;
        _moveToDirection.y = 0f;
        _moveToDirection.z = _inputVector.y;

        moveToVector = _moveToDirection.normalized * speed;

        onMouvment = (_moveToDirection.x != 0f || _moveToDirection.z != 0f);
    }
    private void OnEnable()
    {
        inputManager.Player.Enable();
    }
    private void OnDisable()
    {
        inputManager.Player.Disable();
        inputManager.Player.move.started -= MouvmentInput;
        inputManager.Player.move.performed -= MouvmentInput;
        inputManager.Player.move.canceled -= MouvmentInput;

        inputManager.Player.shoot.started -= ShootingInput;
        inputManager.Player.shoot.canceled -= ShootingInput;
    }
}
