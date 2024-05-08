# interdimensional-kitchen-of-horrors
Gather ingredients and cook food to feed hungry monsters at night! If you don't have enough food, they eat you! 

Play at: https://rustymechanical.itch.io/feed-the-horrors

## Note to DALI -
The project was still very incomplete by the time I submitted and the cooking and monsters described weren't implemented.
The `main` branch contains the code I submitted. I wanted to keep working on this project for fun after the submission deadline; that work is in the `post-submit` branch (If you want you can take a look at that too). The README for each branch contains a description
of what has been implemented by that time. The build is still just what was submitted.

## Tutorial
The game has two broad phases - day and night. In the day, you can gather ingredients and cook food
by pushing it into the cauldron. At night, monsters spawn and you have to deal with the monsters. 

### Controls
Use WASD to move and P to pause/unpause.
You can only interact with objects by pushing them.
Push ingredients into the cauldron to cook

### Game conditions
If any monster touches you or the cauldron, you lose. There are no win conditions;
survive as long as you can

### Action Economy
You move slower at night, and food cooks much slower at night, so you need to be careful with your
movements and do as much in as few moves as possible, especially at night.

### Recipes
A recipe needs at least one of Lettuce/Tomato and one of Potato/Meat. The moment
the cauldron is filled with ingredients that satisfy this rule, it will start cooking.
When it cooks, it will cook ALL the ingredients inside it, not just the minimum two required.


### Monsters and monster meat
(Unimplemented)
Werewolves wait a little while, then charge. Zombies move around, slow but steady. Vampires are in-between: Like werewolves
they move in straight lines but they don't wait and charge, but are steady like zombies.

Each monster is attracted by some food and will drop their target to instead go to that food. Werewolves 
will interrupt their charge to go get food, and will then resume their charge. This allows you to redirect them and 
potentially make them attack other monsters.

Werewolves - Recipe must contain meat
Zombies - Recipe must contain potato
Vampires - Recipe must contain tomato

Upon eating food a monster will be satiated for a while and stop moving. If the food was cooked with a fairy-berry,
the monster will despawn.

### Day-Night cycle
Dawn: Ingredients spawn, leftover cooked material or ingredients (unless stored) disappear. Monsters despawn.
Noon: More ingredients spawn 
Dusk: Monsters spawn
Midnight: A fairy-berry spawns



## Currently Unimplemented
These are features mentioned in the tutorial that aren't implemented

- Cooking - items can be pushed into the cauldron but they don't cook right now
- Fairy-berries don't move
- There is no pause 
- Monsters haven't been implemented yet


## Future implementation plans
These are planned features, that aren't yet implemented and also aren't mentioned in the tutorial.
This is sort-of like a TODO list for the "next update"

- Tutorial popups
- Being able to pull things, so they don't get stuck at the edge
- Somewhat de-randomizing spawning to ensure fairness
- More recipes, ingredients monsters
- Storage - being able to store select ingredients
- Knowing which monsters spawn in advance; "choosing" which monsters spawn, FTL-style 
- Merchants - either introducing some sort of currency, or have the option to only choose one of three items "sold"
- The moon cycle - there are a fixed number of werewolves that keeps decreasing as you cure them, which increases on full moons. Gives an incentive
to cure werewolves instead of simply feeding them.


## Known Issues
Layer sorting works correctly in the editor but not in the build

