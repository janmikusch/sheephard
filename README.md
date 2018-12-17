### Youtube: https://youtu.be/904qsuJIvfo ###
 
# Short Overview
## Game1:
* Initialize Game
* Call Update/Draw on GameController for each frame 
## GameController
* Check for Input to close the Game etc. 
* Call Update of objects (like player) for the current state 
* Draw Menu or Game (check current state)
* Init for a new Round & new Game 
## MainMenu 
* Menu for the game 
* Set game settings like player count and rounds to play 
## PlayerObject 
* NPC & Player inherit from PlayerObject 
* Has a state for animation/movement 
* Has a sheep-object, which represent the visual character 
* Collision detection 
## NPC 
* Creates random movement for the next interval 
## Sheep 
* Handles the Animation & deligates drawing of the sheep to the Animation 
## InputManager 
* Handles the gamepad state for each player. 
## ScoreBoard 
* Draws a Scoreboard on the screen 
* Is holding the Score for each player 
