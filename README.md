# Space Tower Defense game

## Scenes
There are currently 3 scenes in the game. The main menu scene, the level selector and a balanced level.

## Gameplay
The game is a classic tower defense, with a small twist: the enemies are spaceships and the towers are build on planets that orbit around the sun.

There are pointers on the UI for where the waves are coming from, and information about the wave as well (timer, enemy and how many enemies).

The player can build towers on the planets by pressing the + button on the planet, and can select among the available towers.

Further, the tower can be upgraded at the cost of some in game money, which is earned by killing enemy spaceships.

When hovering on building or upgrading a tower, there is a note on the UI that shows the stats (and eventually the new stats) of the tower.

When all the enemies are killed, the win game over screen is displayed.

The game can be paused at any time by pressing `ESC`.

## Technical details

### Shaders

There are some custom shaders present in the game:
- There is a dissolve effect when the enemy spaceships die
- There is a pixelated effect on the sun as it takes damage
- There is a rotating effect on the planet
- There is a portal effect in the level selector when the spaceship enters the portal

### AI

The enemies are navigating in the space and treating planets like obstacles, avoiding them and targeting the sun. It is done mostly with the help of the Unity AI Navigation system.

### Managers

There are multiple managers (singleton) that handle the state of the game, such as:
- Wave Manager, which contains information about the waves (timers, enemies and enemy count)
- Prefab Manager, which links enemy/tower names with prefabs, as well as enemy and tower stats
- Enemy Manager, which has an array of enemies which is updated in real time
- Planet Manager, which holds references for all the planets in the scene and for the sun
- Materials Manager, which holds references to materials for each tower level (whenever a tower levels up, its color changes)
- Pause Manager, which holds helper functions to pause the game
- Canvas Manager, which holds the GUI and the pause/game over canvases
- Balance Manager, which holds starting sun damage and starting coins

### Wave spawning

There are some wave spawner empty objects in the scene, that provide the Wave Manager with the positions of the spawners where the enemies should be spawned.

### Shooting
It's the same method applied for both towers and enemies. They check for one of the planets/enemies provided by the managers, namely the closest one in range.