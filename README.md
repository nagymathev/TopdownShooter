# TopdownShooter
 topdown shooter game with score and increasing difficulty
 
 Made for GameOffJam 2021
https://itch.io/jam/game-off-2021

 
![image](https://user-images.githubusercontent.com/55601645/142392500-eda6ed54-1a5e-4c80-9382-307c922db678.png)

# ToDo list: (in roughly priority order)
- [x] Actual game over screen (display score, restart)
- [x] High score list :) (also needs name entry)
  -   [x] ...online!
- [ ] Main menu, Intro* or something to let the player know at least the basics: goal, controls, "backstory" :)
- [x] NAME for the game :)
- [x] Game balancing, difficulty tweaking (waves timing and difficulty progression, weapons, health, powerups, enemies' speed, health, etc)
- [ ] Make the wave "wait for empty" option work
- [ ] Graphics for various things:
  -  [x] powerups,
  -  [x] weapons (in hand, different bullets for different weapons, muzzle flash)
  -  [ ] damage, kills (e.g. bug blood that stays on the ground instead of explosion) 
  -  [ ] enemies to SHOW when they attack ("animation"; they have an internal counter!)
  -  [ ] enemies,
  -  [ ] character,
- [ ] Audio for various things:
  -  [ ] footsteps
  -  [x] damage, kills (e.g. bug squeak)
- [ ] Camera (how should it scale? should we add scrolling? at the very least find a good enough size for fullscreen)
- [x] powerups to flash before they disappear
- [x] health, score etc UI to flash up / shake when change
- [ ] make player hurt sound clearly recognisable and different from bug hit/kills!
- [x] online leaderboard to store wave and time
- [ ] online leaderboard to support querying the RANK of your score (you might be 17th and not on screen, but still an achievement)
- [x] play a reload sound when switching (picking up a new) weapon
- [ ] CREDITS and README in the build (can be simply in one file)

# [x] MAKE A BUILD!

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
- [x] your points don't reset after you die and press enter to restart
- [x] HP and the wave counter doesn't change


## Credits
- sound effects
  - BFXR
  - https://freesound.org/
- Fonts
  - https://www.dafont.com/

