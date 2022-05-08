:: PACMAN - Overview :: - Maiti Puertas Crucelli

In this Pacman version, the ghosts have 4 states:

CHASE: the ghosts follow the pacman, each one has it's own behaviour.
SCATTER: a direction is randomly generated, giving pacman a breath-period.
VULNERABLE: is triggered when pacman eats a power pellet. In this state, the ghosts run away from the pacman.
DEAD: when in vulnerable state, the ghosts are eaten by pacman. Then, in DEAD state, the ghosts return to home, to respawn.

In the maze, there are TurningPoints at the intersections, and they work as triggers to the ghosts' AI determine which is the next direction to follow.
The ghosts start the game in Chase state. If any of them eats Pacman, all the ghosts change to Scatter state, during 10 seconds, then they back to Chase state.
Pacman takes 2 seconds to reborn.
When the ghosts are in vulnerable state, it takes 8 seconds, and then, they go back to Chase state.
The ghosts have different respawn times, which are setted in GhostView, at "atHomeDuration" field.