using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float accelerarion = 300f;
    [SerializeField] private Animator animator;

    [Header("Controls")]
    [SerializeField] InputActionReference move;
    //[SerializeField] InputActionReference shoot;

    [Header("Shooting")]
    [SerializeField] private float ratioDisparo;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private GameObject spawnPoint;
    private float temporizador = 0.5f;
    private int vidas = 3;
    [SerializeField] LifePlayer lifePlayer;
    [SerializeField] private AudioSource shootAudioSource;

    [Header("GameOver")]
    [SerializeField] private GameManager gameManager;


    private void OnEnable()
    {
        move.action.Enable();
        //shoot.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;
        //shoot.action.started += OnShoot;
    }

    private void Start()
    {
        lifePlayer.lifes = vidas;
    }

    private void Awake()
    {

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>(); // Animator hijo
        }
    }


    Vector2 currentVelocity = Vector2.zero;
    const float rawMoveThresholdForBraking = 1f;

    void Update()
    {
        Movimiento();
        DelimitarMovimiento();
        Disparar();
        //Anmation
        UpdateRawMoveAnima();

    }

    void Movimiento()
    {
        if (rawMove.magnitude < rawMoveThresholdForBraking)
        {
            currentVelocity *= 0.1f * Time.deltaTime;
        }

        currentVelocity += rawMove * accelerarion * Time.deltaTime; // m/s * s = m/s

        float linerVelocity = currentVelocity.magnitude;
        linerVelocity = Mathf.Clamp(linerVelocity, 0, maxSpeed);
        currentVelocity = currentVelocity.normalized * linerVelocity;

        transform.Translate(currentVelocity * Time.deltaTime);

    }

    Vector2 rawMoveAnima = Vector2.zero;
    private void UpdateRawMoveAnima()
    {
        if (Keyboard.current == null) return; // por seguridad, en editor a veces es null
        {
            bool upPressed = Keyboard.current.wKey.isPressed;
            bool downPressed = Keyboard.current.sKey.isPressed;

            bool moveUp = upPressed && !downPressed;
            bool moveDown = downPressed && !upPressed;

            animator.SetBool("MoveUp", moveUp);
            animator.SetBool("MoveDown", moveDown);
        }

    }

    void DelimitarMovimiento()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -8.1f, 8.1f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.41f, 4.41f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }
    void Disparar()
    {
        temporizador += 1 * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && temporizador > ratioDisparo)
        {
            Instantiate(shootPrefab, spawnPoint.transform.position, Quaternion.identity);
            // Reproducir el sonido
            if (shootAudioSource != null)
            {
                shootAudioSource.Play();
            }
            temporizador = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("ShootEnemy") || elOtro.gameObject.CompareTag("Enemy"))
        {
            vidas -= 1;
            lifePlayer.lifes = vidas;
            Destroy(elOtro.gameObject);
            if(vidas <= 0) 
            {
                if (gameManager != null)
                {
                    gameManager.GameOver();
                }
                Destroy(this.gameObject);
            }
        }
    }
    
    private void OnDisable()
    {
        move.action.Disable();
        //shoot.action.Disable();
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;
        //shoot.action.started -= OnShoot;
    }

    Vector2 rawMove;
    private void OnMove(InputAction.CallbackContext obj)
    {
        rawMove = obj.ReadValue<Vector2>();
    }

    //private void OnShoot(InputAction.CallbackContext obj)
    //{
        //Instantiate(shootPrefab, transform.position, Quaternion.identity);
    //}
}
