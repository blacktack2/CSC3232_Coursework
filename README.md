# Newcastle University CSC3232 Coursework

Coursework for the Newcastle University CSC3232 (Gaming Technologies and Simulations) Module.

## Notes for Markers

### Controls

An in-game tutorial area will be made before the final submission.

- a - Walk left
- d - Walk right
- s - Crouch
- space - Jump
- shift - Run
- left mouse - sword1 primary attack
- shift + left mouse - sword1 secondary attack
- right mouse - sword2 primary attack
- shift + right mouse - sword2 secondary attack

### Overview

The current implementation of the game contains the following:

- Menu system
- Tilemap system
- Player character
- Simple enemy
- Simple player-controlled weapon
- Simple physics obstacles
- Jump pad
- Key/door

Some components have been given simple custom-made animations to get the system working (primarily to allow working with animation events and to get visual feedback on setting animator parameters).

#### Newtonian Physics

The game has basic newtonian physics as defined by the rigidbody2D component. Whilst holding a weapon, the player can attack a physics obstacle to either destroy or push them. Some physics objects can be set to break when they receive a sufficiently powerful impact.

Whilst there were plans to implement a hookshot-like ability I simply ran out of time. I may implement this in the final version anyway though.

#### Collision Detection

The player character's hitbox will change when crouching, allowing them to fit into smaller gaps. Weapons dynamically change their attack trigger whilst attacking, following a set of trigger colliders in order. Each trigger can have their own damage statistics allowing for easy future implementation of multi-stage attacks.

#### State-based Behaviours

The player, weapon, and enemy scripts utilize custom heirarchichal state machines to organize their behaviour (though the player only has one state at current implementation). See StateMachines.png for a visual representation of these state machines.
