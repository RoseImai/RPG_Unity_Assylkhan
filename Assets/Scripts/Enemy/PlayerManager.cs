using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;
    InteractableUI interactableUI;

    public GameObject interactableUIGameObject;
    public GameObject itemInteractableGameObject;

    public bool isInteracting;
    
    //Added
    public bool isUsingRightHand;
    public bool isUsingLeftHand;

    public float timeUntilBarIsHidden = 0;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        interactableUI = FindObjectOfType<InteractableUI>();
    }

    private void Update()
    {
        //Added
        isUsingRightHand = animator.GetBool("isUsingRightHand");
        isUsingLeftHand = animator.GetBool("isUsingLeftHand");
        
        inputManager.HandleAllInputs();
        CheckForInteractableObject();

        timeUntilBarIsHidden -= Time.deltaTime;

        if (timeUntilBarIsHidden <= 0)
        {
            timeUntilBarIsHidden = 0;
            itemInteractableGameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        playerLocomotion.isRolling = animator.GetBool("isRolling");
        playerLocomotion.canDoCombo = animator.GetBool("canDoCombo");
        playerLocomotion.canRotate = animator.GetBool("canRotate");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
    }

    public void CheckForInteractableObject()
    {
        RaycastHit hit;

        if(Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraManager.collisionLayers))
        {
            if(hit.collider.tag == "Interactable")
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                if(interactableObject != null)
                {
                    string interactableText = interactableObject.interactableText;
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);
                    

                    if(inputManager.interactableInput)
                    {
                        inputManager.interactableInput = false;
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if(interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }

            if(itemInteractableGameObject != null && inputManager.interactableInput)
            {
                inputManager.interactableInput = false;
                itemInteractableGameObject.SetActive(false);
            }
        }
    }
}
