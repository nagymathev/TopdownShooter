# TopdownShooter
 topdown shooter game with score and increasing difficulty


# ToDo list: (in roughly priority order)
- Actual game over screen (display score, restart)
- High score list :) (also needs name entry)
  -   ...online?
- Main menu, Intro* or something to let the player know at least the basics: goal, controls, "backstory" :)
- NAME for the game :)
- Game balancing, difficulty tweaking (waves timing and difficulty progression, weapons, health, powerups, enemies' speed, health, etc)
- Make the wave "wait for empty" option work
- Graphics for various things:
  -  powerups,
  -  weapons (in hand, different bullets for different weapons, muzzle flash)
  -  damage, kills (e.g. bug blood that stays on the ground instead of explosion) 
  -  enemies to SHOW when they attack ("animation"; they have an internal counter!)
  -  enemies,
  -  character,
- Audio for various things:
  -  footsteps
  -  damage, kills (e.g. bug squeak)
- Camera (how should it scale? should we add scrolling? at the very least find a good enough size for fullscreen)

# MAKE A BUILD!

## Potential extra ideas:
- environment: potentially larger world with scrolling, obstacles, etc
  -   scrolling raises the issue of where to spawn stuff, so might be a can of worms!
  -   one option could be, to open a door to a next room for the player to progress to after surviving a set number of waves :) So we could have basically the same gameplay with no scrolling and set spawn points (easy) but have some larger progression and some variety in the environment. The rooms can get bigger and each set the camera height/size to control the visible area.
- melee attack? (e.g. kick; sometimes i don't want to waste "good" ammo on just one enemy, just want to push them away. could be simply an invisible* bullet :) )
- dash?
- throwing grenade? (simple a bullet that doesn't collide for a while (as it's flying) and then switches to a larger AOE hit)
- placeable mine? (same but without moving, and activates on touch instead)
- bullet lifetime? (distance)
- more enemy types? (flying ones that can overpass crawling ones; jumping ones; ...?)
- enemies to wait a bit before attacking when they touch the player? (allows melee/kick without getting hurt!)
- enemies to stop moving for a bit before/after attacking?
- visual polish? (e.g. lighting, (fake) shadows, footprints, etc)
- environmental obstacles?
  - DESTRUCTABLE obstacles? =o) (shooting a hole on a wall can be good or bad for you ;)

## Bugs?
- your points don't reset after you die and press enter to restart
- HP and the wave counter doesn't change
