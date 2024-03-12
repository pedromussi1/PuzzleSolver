<h2>Code Breakdown</h2>

### <h3>FPSController</h3>

<kbd><img src="https://i.imgur.com/H43B18B.png" alt="Level 8"></kbd>

characterController = GetComponent<CharacterController>();: This line gets the CharacterController and assigns it to the characterController variable, and it is used for controlling player movement and collisions.

Cursor.lockState = CursorLockMode.Locked;: This line locks the cursor to the center of the screen, preventing it from moving around freely.

Cursor.visible = false;: This line makes the cursor invisible while it's locked. now the player will not see the cursor when moving around in the game.

originalHeight = characterController.height; and originalCenter = characterController.center;: These lines save the original height and center position of the character controller. This is useful when the character controller's height or center position is modified during gameplay and needs to be reset later.

<kbd><img src="https://i.imgur.com/uGgnnKW.png" alt="Level 8"></kbd>

if (!PauseMenu.GameIsPaused): This line checks whether the game is currently paused using the GameIsPaused boolean variable from the PauseMenu class. If the game is not paused, the code inside the block will be executed.

Vector3 forward = transform.TransformDirection(Vector3.forward); and Vector3 right = transform.TransformDirection(Vector3.right);: These lines calculate the forward and right directions relative to the player's orientation. TransformDirection is used to transform the given direction from local space to world space.

bool isRunning = Input.GetKey(KeyCode.LeftShift);: This line checks if the left shift key is being held down to determine whether the player is running.

float curSpeedX  and float curSpeedY: These lines calculate the current movement speed along the x and y axes based on whether the player can move (canMove), whether they are running (isRunning), and the input received from the vertical and horizontal axes.

float movementDirectionY = moveDirection.y;: This line stores the current y-component of the movement direction to be used for jumping.

moveDirection = (forward * curSpeedX) + (right * curSpeedY);: This line combines the forward and right movement vectors to determine the overall movement direction.

if (Input.GetButton("Jump") && canMove && characterController.isGrounded): This block handles jumping. If the jump button (presumably the spacebar) is pressed, the player can move (canMove) and is grounded, the moveDirection.y component is set to the jump power. Otherwise, the y-component of the movement direction remains the same as before.

if (!characterController.isGrounded): This block applies gravity when the character controller is not grounded. It decreases the y-component of the movement direction over time, simulating the effect of gravity.

<kbd><img src="https://i.imgur.com/ZTdQagP.png" alt="Level 8"></kbd>

characterController.Move(moveDirection * Time.deltaTime);: This line moves the character controller in the direction specified by moveDirection. The movement is multiplied by Time.deltaTime to ensure smooth and frame rate-independent movement.

if (canMove){rotationX += -Input.GetAxis("Mouse Y") * lookSpeed; rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);} : This conditional check ensures that rotation only occurs when the player is allowed to move (canMove). The movement inside the condotional handles vertical rotation of the player's camera based on mouse input. The variable rotationX stores the current vertical rotation. The value of Input.GetAxis("Mouse Y") represents the vertical movement of the mouse. This value is multiplied by lookSpeed to control the sensitivity of the rotation. The result is then added to rotationX. Mathf.Clamp is used to restrict the vertical rotation within a specified range (-lookXLimit to lookXLimit). This prevents the player from looking too far up or down.

playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);: This line sets the local rotation of the player's camera based on the calculated rotationX. It rotates the camera vertically (up-down) relative to its local coordinate system.

transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);: This line handles horizontal rotation of the player based on mouse input. It modifies the rotation of the player's transform (transform.rotation) by applying a rotation around the y-axis. This rotates the player left or right based on horizontal mouse movement. The rotation is relative to the world coordinate system.

<kbd><img src="https://i.imgur.com/tjLfbMO.png" alt="Level 8"></kbd>

if (Input.GetKey(KeyCode.LeftControl) && canMove && !isCrouching): This block checks if the left control key is being held down, the player is allowed to move (canMove), and the player is not already crouching. If all conditions are met, the character controller's height and center are reduced by half (originalHeight / 2f and originalCenter / 2f, respectively), effectively making the player character crouch. The isCrouching flag is set to true to indicate that the player is crouching.

else if (!Input.GetKey(KeyCode.LeftControl) && isCrouching): This block checks if the left control key is released and the player is currently crouching (isCrouching is true). If both conditions are true, the character controller's height and center are reset to their original values (originalHeight and originalCenter, respectively), bringing the player character back to a standing position. The isCrouching flag is set to false to indicate that the player is no longer crouching.


curSpeedX = canMove ? (isRunning ? walkSpeed : (isCrouching ? crouchSpeed : walkSpeed)) * Input.GetAxis("Vertical") : 0; and curSpeedY = canMove ? (isRunning ? walkSpeed : (isCrouching ? crouchSpeed : walkSpeed)) * Input.GetAxis("Horizontal") : 0;: These lines adjust the player's movement speed (curSpeedX and curSpeedY) based on whether the player is crouching or not. If the player is crouching (isCrouching is true), the movement speed is set to crouchSpeed. Otherwise, it defaults to walkSpeed. The movement speed is also adjusted if the player is running (isRunning is true).


<kbd><img src="https://i.imgur.com/Yqlo610.png" alt="Level 8"></kbd>

if (isOnTrampoline): This block checks if the player character is currently on a trampoline (isOnTrampoline is true). If the player is indeed on the trampoline, it applies an upward force to the player's movement in the y-direction. This is achieved by setting the moveDirection.y component to trampolineForce.TrampolineForce is assigned to 20 in the code previously.

<kbd><img src="https://i.imgur.com/4ENpp9F.png" alt="Level 8"></kbd>

if (transform.position.y < -20f): This block checks if the player's current y-coordinate position is below -20 units. This condition is used to determine if the player has fallen off the map. If the player's y-coordinate position is indeed below -20 units, it reloads the current scene using SceneManager.LoadScene(). It loads the scene specified by the currentLevel variable.

<kbd><img src="https://i.imgur.com/G9LlY4B.png" alt="Level 8"></kbd>

void PropelPlayerUp(): This function applies an upward force to the player's movement. It sets the moveDirection.y component to the value of shootUpForce, causing the player to move upwards.

void PropelPlayerDown(): This function applies a downward force to the player's movement. It sets the moveDirection.y component to the negative value of shootDownForce, causing the player to move downwards.

void PropelPlayerForward(): This function handles teleporting the player forward. It calculates the new position (teleportPosition) by adding the player's current position with a vector representing the forward direction (transform.TransformDirection(Vector3.forward)) multiplied by teleportDistance. Then, it temporarily disables the character controller, sets the player's position to the calculated teleportation position, and finally enables the character controller again. This effectively moves the player to a new position in the game world without any intermediate movement.

### <h3>Simon Color Game</h3>

<kbd><img src="https://i.imgur.com/tSatkQs.png" alt="Level 8"></kbd>

<h4>Start()</h4>

defaultMaterial = GetComponent<Renderer>().material;: This line gets the default material of the object to which this script is attached and assigns it to the defaultMaterial variable. This material is used to reset the object's appearance after flashing the color sequence.

colorSequence = new List<Material>();: This line initializes a new list called colorSequence to hold the materials representing the color sequence.

GenerateRandomSequence();: This function call generates the initial random color sequence.

StartCoroutine(FlashSequence());: This line starts a coroutine named FlashSequence(), which will flash the color sequence over time.

<h4>GenerateRandomSequence()</h4>

colorSequence.Clear();: This line clears any existing color sequence from the colorSequence list.

for (int i = 0; i < sequenceLength; i++): This loop iterates sequenceLength times to generate a new random color sequence.

Material randomColor = GetRandomColor();: This line calls the GetRandomColor() function to get a random color material.

colorSequence.Add(randomColor);: This line adds the random color material to the colorSequence list.

<h4>GetRandomColor()</h4>

Material[] colors = { redMaterial, blueMaterial, greenMaterial, yellowMaterial };: This line creates an array containing the different color materials available.

return colors[Random.Range(0, colors.Length)];: This line returns a random color material from the colors array using Random.Range() to generate a random index.

<kbd><img src="https://i.imgur.com/VmtIAGv.png" alt="Level 8"></kbd>

<h4>Color Flashing Loop and Logic</h4>

foreach (Material colorMaterial in colorSequence): This loop iterates through each material in the colorSequence list.

ShowColor(colorMaterial);: This function call sets the object's material to the current color material from the sequence, effectively displaying that color.

yield return new WaitForSeconds(flashDelay);: This pauses the coroutine for flashDelay seconds before proceeding to the next step.

ShowColor(defaultMaterial);: This sets the object's material back to the default material (or white) after displaying the color for a brief period.

yield return new WaitForSeconds(0.5f);: This adds a short delay between flashes to enhance the visual effect.

yield return new WaitForSeconds(sequencePause);: After the entire color sequence has been flashed, this line adds a longer pause before starting the next sequence.

<h4>Checking Player Progress</h4>

if (playerProgress == sequenceLength): This condition checks if the player has completed the entire sequence.
If the player has completed the sequence, the coroutine: Resets the player's progress (playerProgress) and the current color index (currentColorIndex). Increases the sequence length (sequenceLength) and the sets completed (setsCompleted) counters.

Checks if three sets have been completed (setsCompleted >= 3): If so, it activates a level completion object (LevelComp) and invokes the LoadNextLevel() function after a delay. The LoadNextLevel() function loads the next level in the game.

<kbd><img src="https://i.imgur.com/WoQWuzB.png" alt="Level 8"></kbd>
    
<h4>ShowColor</h4>

This method is responsible for displaying a color on the object.

It takes a Material parameter colorMaterial, representing the material to be shown.

Inside the method, it sets the object's renderer material to colorMaterial, effectively changing the appearance of the object to the specified color.

<h4>public Material GetNextColor()</h4>

This method returns the next color material in the color sequence.

It checks if currentColorIndex is within the bounds of the colorSequence list.

If it is, it returns the color material at index currentColorIndex in the colorSequence list and increments currentColorIndex for the next call.

If currentColorIndex is out of bounds (indicating that the sequence is complete), it returns null to signal that the sequence is complete.

<h4>public Material PeekNextColor()</h4>

This method is similar to GetNextColor() but doesn't advance currentColorIndex.

It returns the next color material in the sequence without incrementing currentColorIndex.

This is useful for peeking at the next color without actually progressing through the sequence.

### <h3>Memory Card Game</h3>

<kbd><img src="https://i.imgur.com/fQ8rkq6.png" alt="Level 8"></kbd>

private static Color[] predeterminedColors: This line declares a private static array named predeterminedColors of type Color[]. The static keyword means that this array is shared among all instances of the class it belongs to. It also initializes the array with a set of Color objects enclosed in curly braces {}.

Each Color object is created using the new Color(float r, float g, float b) constructor, where r, g, and b represent the red, green, and blue components of the color, respectively, each ranging from 0 to 1.

<kbd><img src="https://i.imgur.com/roORhUH.png" alt="Level 8"></kbd>

<kbd><img src="https://i.imgur.com/EI3L0Y8.png" alt="Level 8"></kbd>

<kbd><img src="https://i.imgur.com/93aAfYG.png" alt="Level 8"></kbd>

