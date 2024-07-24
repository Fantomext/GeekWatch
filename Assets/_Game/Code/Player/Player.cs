using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public PlayerInputAction _playerInput;

    [Space]
    [SerializeField] GameObject playerCanvas;

    private void Awake()
    {
        _playerInput = new PlayerInputAction();

        _playerInput.Player.Jump.started += ctx => Jump();

        playerCanvas.SetActive(true);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Start()
    {
        transform.parent = null;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (_controller.enabled)
        {
            Vector2 input = _playerInput.Player.Move.ReadValue<Vector2>();
            float horizontal = input.x;
            float vertical = input.y;

            groundedPlayer = _controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = transform.forward * vertical + transform.right * horizontal;
            _controller.Move(move * _speed * Time.deltaTime);

            if (!CheckGround())
            {
                if (CheckRoof())
                {
                    playerVelocity.y = -0.5f;
                }
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            _controller.Move(playerVelocity * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (_playerInput.Player.Jump.WasPressedThisFrame() && CheckGround())
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    public bool CheckGround()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckRoof()
    {
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //public void Teleport(Transform position)
    //{
    //    StartCoroutine(TeleportPlayer(position));
    //}

    //IEnumerator TeleportPlayer(Transform positionTeleport)
    //{
    //    _controller.enabled = false;
    //    _camera.enabled = false;
    //    yield return new WaitForSeconds(0.01f);
    //    transform.localPosition = positionTeleport.position;
    //    _camera.ChangeRotation(positionTeleport);
    //    yield return new WaitForSeconds(0.01f);
    //    _controller.enabled = true;
    //    _camera.enabled = true;
    //}
}
