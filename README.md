
<h1 align="center">PuzzleSolver</h1>

### [YouTube Demonstration](https://www.youtube.com/watch?v=wtw1QMQUFRQ)

<p align="center">
  <a href="https://www.youtube.com/watch?v=wtw1QMQUFRQ"><img src="https://img.youtube.com/vi/wtw1QMQUFRQ/0.jpg" alt="YouTube Demonstration"></a>
</p>

### [Play the Game Here!](https://play.unity.com/mg/other/firstperson-3)

<h2>Description</h2>

<p>This is a three-dimensional puzzle game based on increased levels of mechanics and difficulty as the player progresses, acquiring new skills and following tips will help the player move to the next level. This game was developed on Unity3D along with Blender for modelling required assets. It includes tips for helping the player move to the next level and introduces innovative mechanics not usually seen on games of this caliber such as trampoline, shoot tube, Simon color game, and memory card game. All assets and ideas portrayed in this game are my own, no outside sources were used.</p>

<h2>Languages and Utilities Used</h2>

<ul>
  <li><b>Unity</b></li>
  <li><b>C#</b></li>
  <li><b>Blender</b></li>
</ul>

<h2>Environments Used</h2>

<ul>
  <li><b>Windows 11</b></li>
</ul>

<h2>Game Walk-through</h2>

<h3>Level 1</h3>

<p align="center">
  <kbd><img src="https://i.imgur.com/KRMatUC.png" alt="Level 1"></kbd>
</p>

<p>In this level, we introduce the first, most basic mechanics of the game, which are how to walk and jump. In order to move to the next level, the player must move from platforms and get to the gold coin.</p>

<h3>Level 2</h3>

<p align="center">
  <kbd><img src="https://i.imgur.com/ZCM5RN7.png" alt="Level 2"></kbd>
</p>

<p>In this level, we introduce the concept of sprinting, with the idea that by using the sprint mechanism, the player can move faster and thus make longer jumps. The player must master the sprinting mechanic in order to move on to the next level.</p>

<h3>Level 3</h3>

<p align="center">
  <kbd><img src="https://i.imgur.com/AGIusI8.png" alt="Level 3"></kbd>
</p>

<p>In this level, we introduce the concept of crouching, with the idea that by using the crouching mechanism, the player can go past obstacles they would not be able to if they were standing up. By using that mechanic correctly, the player gets to move to the next level.</p>


<h3>Level 4</h3>

<p align="center">
  <kbd><img src="https://i.imgur.com/3dfoBgc.png" alt="Level 4"></kbd>
</p>

<p>In this level, we introduce the trampoline object, distinguished with a bright green color. By touching object with this green color, the player will be propelled upwards, thus being able to make higher jumps.</p>

<h3>Level 5</h3>

<p align="center">
  <kbd><img src="https://i.imgur.com/RLMFD1t.png" alt="Level 5"></kbd>
</p>

<p>This level is used as a recap of all the abilities and mechanics introduced in the game so far. By using everything the player learned with levels 1-4, they will be able to move on to the next level.</p>

<h3>Level 6</h3>

<p align="center">
  <kbd><img src="https://i.imgur.com/4mwyQXP.png" alt="Level 6"></kbd>
</p>

<p>This level is similar to a memory card game. By touching each button on the platform, a color will be revealed. The player must match the pairs of colored blocks and complete the puzzle. By doing so, the next level is unlocked.</p>

<h3>Level 7</h3>

<p align="center">
  <kbd><img src="https://i.imgur.com/Mdney5Y.png" alt="Level 7"></kbd>
</p>

<p>This level introduces the Shoot Tubes that teleport the player up, down, or forward. In this level it is used as a fun and fast way to travel through the level and collect all the rings. The pplayer only has to be careful not to fall off the platforms.</p>

<h3>Level 8</h3>

<p align="center">
  <kbd><img src="https://i.imgur.com/Miq0agH.png" alt="Level 8"></kbd>
</p>

<p>This level is similar to a Simon Color game. A big cube shows a color sequence. The player must click the buttons to match that color sequence. After three turns taht get increasingly difficult, the player completes the level.</p>

<h3>Level 9</h3>

<p align="center">
  <kbd><img src="https://i.imgur.com/kyygtZ3.png" alt="Level 9"></kbd>
</p>

<p>This level is a maze. The player must go inside the maze and collect the rings hidden inside the rooms. They must track their progress and fidn their way to all the rings to complete the level.</p>

<h2>Code Breakdown</h2>

<h3>FPSController</h3>

<kbd><img src="https://i.imgur.com/H43B18B.png" alt="Level 8"></kbd>

characterController = GetComponent<CharacterController>();: This line gets the CharacterController and assigns it to the characterController variable, and it is used for controlling player movement and collisions.

Cursor.lockState = CursorLockMode.Locked;: This line locks the cursor to the center of the screen, preventing it from moving around freely.

Cursor.visible = false;: This line makes the cursor invisible while it's locked. now the player will not see the cursor when moving around in the game.

originalHeight = characterController.height; and originalCenter = characterController.center;: These lines save the original height and center position of the character controller. This is useful when the character controller's height or center position is modified during gameplay and needs to be reset later.

<kbd><img src="https://i.imgur.com/uGgnnKW.png" alt="Level 8"></kbd>

if (!PauseMenu.GameIsPaused): This line checks whether the game is currently paused using the GameIsPaused boolean variable from the PauseMenu class. If the game is not paused, the code inside the block will be executed.

Vector3 forward = transform.TransformDirection(Vector3.forward); and Vector3 right = transform.TransformDirection(Vector3.right);: These lines calculate the forward and right directions relative to the player's orientation. TransformDirection is used to transform the given direction from local space to world space.

bool isRunning = Input.GetKey(KeyCode.LeftShift);: This line checks if the left shift key is being held down to determine whether the player is running.

float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0; and float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;: These lines calculate the current movement speed along the x and y axes based on whether the player can move (canMove), whether they are running (isRunning), and the input received from the vertical and horizontal axes.

float movementDirectionY = moveDirection.y;: This line stores the current y-component of the movement direction to be used for jumping.

moveDirection = (forward * curSpeedX) + (right * curSpeedY);: This line combines the forward and right movement vectors to determine the overall movement direction.

if (Input.GetButton("Jump") && canMove && characterController.isGrounded): This block handles jumping. If the jump button (presumably the spacebar) is pressed, the player can move (canMove) and is grounded, the moveDirection.y component is set to the jump power. Otherwise, the y-component of the movement direction remains the same as before.

if (!characterController.isGrounded): This block applies gravity when the character controller is not grounded. It decreases the y-component of the movement direction over time, simulating the effect of gravity.



<kbd><img src="https://i.imgur.com/ZTdQagP.png" alt="Level 8"></kbd>

<kbd><img src="https://i.imgur.com/tjLfbMO.png" alt="Level 8"></kbd>

<kbd><img src="https://i.imgur.com/Yqlo610.png" alt="Level 8"></kbd>

<kbd><img src="https://i.imgur.com/4ENpp9F.png" alt="Level 8"></kbd>
