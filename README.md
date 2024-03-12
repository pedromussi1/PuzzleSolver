
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

<h3>Void Start in Character Controller</h3>

<kbd><img src="https://i.imgur.com/H43B18B.png" alt="Level 8"></kbd>

characterController = GetComponent<CharacterController>();: This line gets the CharacterController and assigns it to the characterController variable, and it is used for controlling player movement and collisions.

Cursor.lockState = CursorLockMode.Locked;: This line locks the cursor to the center of the screen, preventing it from moving around freely.

Cursor.visible = false;: This line makes the cursor invisible while it's locked. now the player will not see the cursor when moving around in the game.

originalHeight = characterController.height; and originalCenter = characterController.center;: These lines save the original height and center position of the character controller. This is useful when the character controller's height or center position is modified during gameplay and needs to be reset later.

audioSourceWalk = gameObject.AddComponent<AudioSource>();: This line adds an AudioSource component and assigns it to the audioSourceWalk variable.

audioSourceWalk.clip = walkSound;: This line sets the audio clip that the audioSourceWalk will play when triggered.

audioSourceWalk.spatialBlend = 1.0f;: This line sets the spatial blend of the audioSourceWalk to 1.0f, indicating that the audio should be played in 3D space. This means that the sound will be perceived as coming from a specific point in the game world, which is typically the position of the GameObject to which the AudioSource is attached.

audioSourceRun = gameObject.AddComponent<AudioSource>();: This line adds another AudioSource component to the same GameObject and assigns it to the audioSourceRun variable. This will be used for playing the running sound effect.

audioSourceRun.clip = runSound;: This line sets the audio clip that the audioSourceRun will play when triggered. runSound is presumably a sound file that contains the running sound effect.

audioSourceRun.spatialBlend = 1.0f;: Similar to the audioSourceWalk, this line sets the spatial blend of the audioSourceRun to 1.0f, indicating that the audio should be played in 3D space.

<h3>Void Update in Character Controller</h3>

<kbd><img src="https://i.imgur.com/uGgnnKW.png" alt="Level 8"></kbd>

<kbd><img src="https://i.imgur.com/ZTdQagP.png" alt="Level 8"></kbd>

<kbd><img src="https://i.imgur.com/tjLfbMO.png" alt="Level 8"></kbd>

<kbd><img src="https://i.imgur.com/Yqlo610.png" alt="Level 8"></kbd>

<kbd><img src="https://i.imgur.com/4ENpp9F.png" alt="Level 8"></kbd>
