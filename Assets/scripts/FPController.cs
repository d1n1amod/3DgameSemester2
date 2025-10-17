using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;


    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Camera fpsCam;         
    public float damage = 20f;    
    public float range = 100f;    
    public Transform shootPoint;     
    public float bulletForce = 50f;

    private PlayerInventory playerInventory;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 2.5f;
    private float originalMoveSpeed;

    [Header("Pickup Settings")]
    public float pickupRange = 3f;
    public Transform holdPoint;
    private PickUpObject heldObject;

    [Header("Interaction Settings")]
    public float interactRange = 3f;
    [SerializeField] private InteractionUI interactionUI;

    private Interactable currentInteractable;

    [Header("Pause Menu Settings")]
    [SerializeField] GameObject pauseMenu;


    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    private bool hasGun = false;

    private Animator animator;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();

        if (heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
        }
    }

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        animator = GetComponentInChildren<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            interactionUI.ShowMessage(interactable.interactionMessage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable = null;
            interactionUI.HideMessage();

            if (interactable.panelToOpen != null)
            {
                interactable.panelToOpen.SetActive(false);
                Debug.Log("Closed panel from: " + interactable.name);
            }
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!hasGun) return; 
        if (context.performed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (playerInventory != null && playerInventory.UseBullet())
        {
            
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(shootPoint.forward * bulletForce, ForceMode.Impulse);

            
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
        else
        {
            Debug.Log("No ammo!");
        }
    }

    public void EquipGun(GameObject gunObject)
    {
        hasGun = true;
        gunObject.transform.SetParent(holdPoint);
        gunObject.transform.localPosition = Vector3.zero;
        gunObject.transform.localRotation = Quaternion.identity;

        
        if (gunObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
        if (gunObject.TryGetComponent<Collider>(out Collider col))
        {
            col.enabled = false;
        }

        Debug.Log("Gun equipped! Shooting enabled.");
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.height = crouchHeight;
            moveSpeed = crouchSpeed;
        }
        else if (context.performed)
        { 
           controller.height = crouchHeight;
           moveSpeed = originalMoveSpeed;
        }
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (!context.performed || hasGun) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, ~LayerMask.GetMask("Player"))) 
        {
            Debug.Log("Hit: " + hit.collider.name);

            GunPickUp gun = hit.collider.GetComponent<GunPickUp>();
            if (gun != null && !gun.isPickedUp)
            {
                gun.isPickedUp = true;
                EquipGun(gun.gameObject);
                return;
            }

            PickUpObject pickUp = hit.collider.GetComponent<PickUpObject>();
            if (pickUp != null)
            {
                pickUp.PickUp(holdPoint);
                heldObject = pickUp;
            }
        }
        else
        {
            Debug.Log("Nothing hit");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (currentInteractable != null)
        {
            interactionUI.HideMessage();

            currentInteractable.Interact();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        pauseMenu.SetActive(true);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        
        float moveMagnitude = new Vector2(moveInput.x, moveInput.y).magnitude;
        animator.SetFloat("Speed", moveMagnitude);

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void HandleLook()
    {
        if (pauseMenu.activeSelf) return;

        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit,
        verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
} 