using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    #region movement
    public AudioClip walkSound;
    public AudioClip runSound;

    private AudioSource audioSourceWalk;
    private AudioSource audioSourceRun;

    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    public float crouchSpeed = 3f;
    private bool isCrouching = false;
    private float originalHeight;
    private Vector3 originalCenter;
    public float jumpPower = 7f;
    public float gravity = 20f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;
    #endregion

    #region Shoot

    public float shootUpForce = 10f;   // Adjust the upward force as needed
    public float shootDownForce = 10f; // Adjust the downward force as needed

    //public float shootForwardForce = 10f; // Adjust the force as needed for shooting forward
    //public float shootBackwardForce = 10f; // Adjust the force as needed for shooting backward

    public float teleportDistance = 10f;



    public AudioSource ShootSound;  // Add this line

    private Dictionary<GameObject, bool> shootUpStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, bool> shootDownStates = new Dictionary<GameObject, bool>();

    private Dictionary<GameObject, bool> shootForwardStates = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, bool> shootBackwardStates = new Dictionary<GameObject, bool>();



    #endregion




    public int currentLevel = 1;
    public int nextLevelSceneIndex = 2; // Replace "NextLevel" with the actual name of your next level scene

    public GameObject LevelComp;

    public GameObject WrongWay;

    public AudioSource levelCompleteAudio;  // Add this line

    public AudioSource TrampolineSound;

    public AudioSource HaloSound;  // Add this line

    public int playerScore = 0;
    public int scoreToCompleteLevel = 10; // Set the desired score to complete the level




    #region Trampoline Interaction
    private bool isOnTrampoline = false;
    public float trampolineForce = 20f;
    #endregion



    void Start()
    {
        characterController = GetComponent<CharacterController>();



        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Save the original height and center of the character controller
        originalHeight = characterController.height;
        originalCenter = characterController.center;

        // Create AudioSources and set them up
        audioSourceWalk = gameObject.AddComponent<AudioSource>();
        audioSourceWalk.clip = walkSound;
        audioSourceWalk.spatialBlend = 1.0f; // 3D sound

        audioSourceRun = gameObject.AddComponent<AudioSource>();
        audioSourceRun.clip = runSound;
        audioSourceRun.spatialBlend = 1.0f; // 3D sound

    }



    void Update()
    {

        // Resume updating if the game is not paused
        if (!PauseMenu.GameIsPaused)
        {
            #region Handles Movement
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            #endregion

            #region Handles Jumping
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            #endregion


            // Continue with the existing code for player movement, jumping, rotation, etc.


            #region Handles Rotation
            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            #endregion




            #region Handles Crouching
            if (Input.GetKey(KeyCode.LeftControl) && canMove && !isCrouching)
            {
                // Crouch when Left Control is pressed
                characterController.height = originalHeight / 2f;
                characterController.center = originalCenter / 2f;
                isCrouching = true;
            }
            else if (!Input.GetKey(KeyCode.LeftControl) && isCrouching)
            {
                // Stand up when Left Control is released
                characterController.height = originalHeight;
                characterController.center = originalCenter;
                isCrouching = false;
            }

            // Adjust the speed based on whether the player is crouching or not
            curSpeedX = canMove ? (isRunning ? walkSpeed : (isCrouching ? crouchSpeed : walkSpeed)) * Input.GetAxis("Vertical") : 0;
            curSpeedY = canMove ? (isRunning ? walkSpeed : (isCrouching ? crouchSpeed : walkSpeed)) * Input.GetAxis("Horizontal") : 0;
            #endregion

            #region Trampoline Interaction
            if (isOnTrampoline)
            {
                // Apply an upward force when the player presses the jump button on the trampoline
                moveDirection.y = trampolineForce;
                
            }
            #endregion

        }

        bool Running = Input.GetKey(KeyCode.LeftShift);

        #region Moving Sound
        // Play walking sound
        if (canMove && characterController.isGrounded && !Running)
        {
            float movementSpeed = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));
            if (movementSpeed > 0.1f)
            {
                if (!audioSourceWalk.isPlaying)
                {
                    audioSourceWalk.Play();
                }
            }
            else
            {
                audioSourceWalk.Stop();
            }
        }
        else
        {
            audioSourceWalk.Stop();
        }

        // Play running sound
        if (canMove && characterController.isGrounded && Running)
        {
            float movementSpeed = Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"));
            if (movementSpeed > 0.1f)
            {
                if (!audioSourceRun.isPlaying)
                {
                    audioSourceRun.Play();
                }
            }
            else
            {
                audioSourceRun.Stop();
            }
        }
        else
        {
            audioSourceRun.Stop();
        }

        #endregion
        #region Reload Level when falling off the map

        if (transform.position.y < -20f)
        {
            SceneManager.LoadScene(currentLevel);
        }

        #endregion



    }




    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger with object: " + other.gameObject.name);


        if (other.gameObject.tag == "LevelComplete")
        {
            // Deactivate objects with the "Tip" tag
            DeactivateObjectsWithTipTag();

            other.gameObject.SetActive(false);

            // Play level complete audio
            if (levelCompleteAudio != null)
            {
                levelCompleteAudio.Play();
            }

            //gamewin
            LevelComp.SetActive(true);
            //LevelComp.gameObject.SetActive(true);

            PlayerPrefs.SetInt("Level_" + currentLevel.ToString(), 1);




            Invoke("LoadNextLevel", 2f);


        }
        else if (other.gameObject.tag == "InvTrig")
        {
            // Activate objects with the "WrongWay" tag for 5 seconds
            WrongWay.SetActive(true);

            // Deactivate objects with the "WrongWay" tag after 5 seconds
            StartCoroutine(DeactivateWrongWayObjectsAfterDelay(3f));


            // You may add additional logic or effects here if needed
        }
        else if (other.gameObject.tag == "Trampoline")
        {
            isOnTrampoline = true;
            
        }
        else if (other.gameObject.tag == "Halo")
        {
            other.gameObject.SetActive(false);

            AddScore(1); // You can adjust the score value as needed

            if (HaloSound != null)
            {
                HaloSound.Play();
            }

            if (playerScore >= scoreToCompleteLevel)
            {
                // Play level complete audio
                if (levelCompleteAudio != null)
                {
                    levelCompleteAudio.Play();
                }

                //gamewin
                LevelComp.SetActive(true);

                // Advance to the next level after a delay
                Invoke("LoadNextLevel", 2f);
            }

        }
        else if (other.gameObject.tag == "ShootDown")
        {
            // Toggle the shoot down state for this object
            bool currentState = ToggleState(other.gameObject, shootDownStates);

            if (currentState)
            {
                // Apply downward force to propel the player down
                PropelPlayerDown();

            }
            else
            {
                // Apply upward force to propel the player up
                PropelPlayerUp();

            }

            if (ShootSound != null)
            {
                ShootSound.Play();
            }
        }
        else if (other.gameObject.tag == "ShootUp")
        {

            bool currentState = ToggleState(other.gameObject, shootUpStates);

            if (currentState)
            {
                // Apply upward force to propel the player up
                PropelPlayerUp();

            }
            else
            {
                // Apply downward force to propel the player down
                PropelPlayerDown();

            }

            if (ShootSound != null)
            {
                ShootSound.Play();
            }
        }
        else if (other.gameObject.tag == "ShootForward")
        {

            PropelPlayerForward();

            // Optionally, play a sound effect
            if (ShootSound != null)
            {
                ShootSound.Play();
            }
        }
    }










    IEnumerator DeactivateWrongWayObjectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        WrongWay.SetActive(false);

    }

    // Add the following method to adjust the camera position when obstructed
    void AdjustCameraPosition(RaycastHit hit)
    {
        float yOffset = 0.2f; // You can adjust this value based on your scene
        playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, hit.point.y - yOffset, playerCamera.transform.position.z);
    }

    // Function to deactivate objects with the "Tip" tag
    void DeactivateObjectsWithTipTag()
    {
        GameObject[] tipObjects = GameObject.FindGameObjectsWithTag("Tip");

        foreach (GameObject tipObject in tipObjects)
        {
            tipObject.SetActive(false);
        }
    }

    void PropelPlayerUp()
    {
        // Apply upward force to the player
        moveDirection.y = shootUpForce;

        // Optionally, you can add additional effects or logic here


    }

    void PropelPlayerDown()
    {
        // Apply downward force to the player
        moveDirection.y = -shootDownForce; // Use negative force for downward movement

        // Optionally, you can add additional effects or logic here


    }


    void PropelPlayerForward()
    {
        // Calculate the teleportation distance along the local Z-axis (you can adjust this value)
        //float teleportDistanceZ = 10f;

        // Calculate the teleportation position
        Vector3 teleportPosition = transform.position + transform.TransformDirection(Vector3.forward) * teleportDistance;

        // Teleport the player to the new position
        characterController.enabled = false; // Disable the character controller temporarily
        transform.position = teleportPosition;
        characterController.enabled = true; // Enable the character controller again

        // Optionally, you can add additional effects or logic here
        Debug.Log("Player teleported forward along the local Z-axis");
    }



    bool ToggleState(GameObject obj, Dictionary<GameObject, bool> stateDictionary)
    {
        if (!stateDictionary.ContainsKey(obj))
        {
            stateDictionary[obj] = true;
        }
        else
        {
            stateDictionary[obj] = !stateDictionary[obj];
        }

        return stateDictionary[obj];
    }



    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Trampoline")
        {
            isOnTrampoline = false;
            if (TrampolineSound != null)
            {
                TrampolineSound.Play();
            }
        }
    }

    void AddScore(int points)
    {
        // Add points to the player's score
        playerScore += points;

        // Optionally, you can perform additional actions when the score is updated
    }

    // Function to load the next level
    void LoadNextLevel()
    {
        // Load the next level by scene name
        SceneManager.LoadScene(nextLevelSceneIndex);
    }

}
