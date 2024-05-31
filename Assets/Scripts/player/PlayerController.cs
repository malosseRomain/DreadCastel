using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float sprintSpeed = 7.5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Vector2 mouseSensitivity = Vector2.one;
    [SerializeField] private Transform eyes;

    public Timer timer;
    private float reductionRate = 0.90f;

    public GameObject monstrePrefab;
    private bool monstreInstantiated = false;
    public Camera mainCamera;

    private Vector3 velocity;
    private float currentSpeed;

    private Vector2 moveInputs, lookInputs;
    private bool jumpPerformed;
    private bool isCrouching = false;
    private bool isRunning = false;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        //On force la souris à se placer au centre de l'écran pour que la caméra soit orienté puis on bloque la souris dans le cadre du jeu et on la cache
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }


    public void BroadcastEndGame() {
        gameObject.SetActive(false);
    }

    //Le reste
    private void Update()
    {
        Look();

        if (Keyboard.current.leftCtrlKey.isPressed)
        {
            isCrouching = true;
            characterController.height = 0.5f;
        }
        else
        {
            isCrouching = false;
            characterController.height = 2.0f;
        }

        if (Keyboard.current.leftShiftKey.isPressed)
            isRunning = true;
        else
            isRunning = false;


        float remainingTime = timer.GetRemainingTime();

        if (remainingTime <= 15 && remainingTime > 6)
        {
            ReducePlayerSpecs(Time.deltaTime);
        }

        if (remainingTime <= 5 && !monstreInstantiated)
        {
            Quaternion playerRotation = mainCamera.transform.rotation;

            Vector3 spawnDirection = playerRotation * Vector3.forward;

            float spawnDistance = 10f; 
            Vector3 spawnPosition = transform.position + spawnDirection * spawnDistance;

            // Faire en sorte que le monstre regarde dans la direction du joueur
            Quaternion monsterRotation = Quaternion.LookRotation(spawnDirection);

            // Appliquer la position et la rotation calculées au monstre
            //monstrePrefab.transform.position = spawnPosition;
            //monstrePrefab.transform.rotation = monsterRotation;
            monstrePrefab.SetActive(true);
            monstreInstantiated = true;


        }


    }

    //Toute la physique
    private void FixedUpdate()
    {
        if (isCrouching)
            currentSpeed = crouchSpeed;
        else if (isRunning)
            currentSpeed = sprintSpeed;
        else
            currentSpeed = normalSpeed;

        Vector3 _horizontalVelocity = currentSpeed * new Vector3(moveInputs.x, 0f, moveInputs.y);
        float _gravityVelocity = Gravity(velocity.y);

        velocity = _horizontalVelocity + _gravityVelocity * Vector3.up;

        TryJump();

        Vector3 _move = transform.forward * velocity.z + transform.right * velocity.x + transform.up * velocity.y;

        characterController.Move(_move * Time.deltaTime);
    }

    private void Look()
    {
        //Rotation de gauche a droite.
        transform.Rotate(lookInputs.x * Time.deltaTime * mouseSensitivity.x * Vector3.up);

        //Rotation de haut en bas.
        float _eyeAngleX = eyes.localEulerAngles.x - lookInputs.y * Time.deltaTime * mouseSensitivity.y;

        //Clamp la rotation pour pas faire nimporte quoi avec la tete.
        if (_eyeAngleX <= 90) _eyeAngleX = _eyeAngleX > 0 ? Mathf.Clamp(_eyeAngleX, 0, 85) : _eyeAngleX;
        if (_eyeAngleX > 270) _eyeAngleX = Mathf.Clamp(_eyeAngleX, 275, 360);

        eyes.localEulerAngles = Vector3.right * _eyeAngleX;
    }

    private float Gravity(float _verticalVelocity)
    {
        if (characterController.isGrounded) return 0f;

        _verticalVelocity += Physics.gravity.y * Time.fixedDeltaTime;

        return _verticalVelocity;
    }

    private void TryJump()
    {
        if (!jumpPerformed || !characterController.isGrounded) return;

        velocity.y += jumpForce;
        jumpPerformed = false;
    }

    private void ReducePlayerSpecs(float deltaTime)
    {
        normalSpeed *= Mathf.Pow(reductionRate, deltaTime);
        crouchSpeed *= Mathf.Pow(reductionRate, deltaTime);
        sprintSpeed *= Mathf.Pow(reductionRate, deltaTime);
        jumpForce *= Mathf.Pow(reductionRate, deltaTime);
    }



    public void MovePerformed(InputAction.CallbackContext _ctx) => moveInputs = _ctx.ReadValue<Vector2>();
    public void JumpPerformed(InputAction.CallbackContext _ctx) => jumpPerformed = _ctx.performed;
    public void LookPerformed(InputAction.CallbackContext _ctx) => lookInputs = _ctx.ReadValue<Vector2>();


}