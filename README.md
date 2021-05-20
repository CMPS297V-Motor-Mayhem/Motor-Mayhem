# Motor-Mayhem
A small game project for the AUB CMPS 297V course

- [Authors](#authors)
- [Technologies](#technologies)
- [Proposal](#proposal)
- [Report](#report)
  * [Major Components](#major-components)
  * [Algorithms](#algorithms)
- [Tutorials](#tutorials)
  * [Set up unity and github for development](#set-up-unity-and-github-for-development)
  * [Use github project, issues, pull requests](#use-github-project--issues--pull-requests)
- [Resources Used](#resources-used)
  * [Assets](#assets)
  * [Sounds](#sounds)

## Authors

- Marwan Bassam
- Dhia Eddine Nini
- Hadi Hachem

## Technologies

- Unity Version 2020.3.6f1

## Proposal
![Project Proposal](/Pictures/proposal-pic.png)

## Report

### Major Components

- Car Prefabs
  * Since our game can have multiple instances of the same car we decided to utilize prefabs
- AI FSM
  * The game calls for AI agents that have multiple states (patrolling/driving towards other cars/boosting into other cars/shielding itself) hence it was necessary to use a Finite State Machine
- Game State FSM
  * The game goes throught multiple stages (start/pause/lose/win) and using another Finite State Machine makes the task much easier
- Cooldowns using coroutines
  * Cooldowns for Boosting and Shielding were necessary but we still wanted the cars to move around so Coroutines were needed, especially their WaitForSeconds() method
- Bounciness made using physics (adding force to rigidbodies)
  * We made use of Unity's physics system to create a bouncy effect whenever a car would hit anything

### Algorithms

- AI Agent Sense: we send rays from each car towards all the others, we then check if the angle between that ray and the transform.forward ray is within the specified field of view. If so then the agent has "detected" a car

## Tutorials

### Set up unity and github for development

- Clone this repository
- Add it to unity hub (if you are using it)
- Click on Window > Github
- Sign in if needed

Now any changes you do you can commit/push/pull them to this repository

### Use github project / issues / pull requests

For creating new issues (new bugs/tasks):
- Go to the issues tab
- Create a new issue
- Add title and description
- Assign it to the Motor Mayhem project (on the right)

For working on an existing issue:
- Go to the projects tab > Motor Mayhem Project
- Select an issue you would like to work on
- Within the details of the issue you can assign yourself as the person working on it
- Then within the project move the issue to the corresponding stage (in dev / done)
- Create a new branch for the issue

When done with an issue:
- Create a pull request in order to merge the branch with master
- Go to the corresponding issue and assign the pull request you just made
- If you are an admin, accept the pull request and the branches should merge
- Close the issue and move it to done in the project

## Resources Used

### Assets
- [Cartoon Car - Vehicle Pack](https://assetstore.unity.com/packages/3d/vehicles/cartoon-car-vehicle-pack-180962)
- [Lava Flowing Shader](https://assetstore.unity.com/packages/vfx/shaders/lava-flowing-shader-33635)
- [Stylized Tiles Texture](https://assetstore.unity.com/packages/2d/textures-materials/tiles/stylized-tiles-texture-192876)
- [FC Audio Tools Free Edition](https://assetstore.unity.com/packages/tools/audio/fc-audio-tools-free-edition-68940)

### Sounds
- [Funny Cartoon Sound Effects | No Copyright](https://www.youtube.com/watch?v=VmgKryu4__k&ab_channel=Everything.mp4)
