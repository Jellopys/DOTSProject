# DOTSProject

This project is a school assignment where we need to use Unity DOTS (ECS, Jobs, Burst) to make a performant space shooter game.

WASD - Movement,
Mouse - Aim & Rotate,
Left Mouse Button - Fire projectile,

The projectiles, units and players are all Entities that are controlled by the Systems that updates each entity in various ways. 
The components that you add to the entities are what tells the systems which entity to update or check for. These components can be enabled, disabled, added and removed. This makes it performant because its only one "Singleton actor" (the system) that iterates over all entities and executes commands to only the entities that matter. Then the jobs system takes over and executes these iterations with the use of CPU worker threads.

![image](https://github.com/Jellopys/DOTSProject/assets/61058386/bc7d2618-c496-4448-9203-c5ad8ff00bf5)

The Jobs system in DOTS allows me to make use of multithreading with job workers helping out with processing from the main thread.

![Skärmbild 2023-11-03 222338](https://github.com/Jellopys/DOTSProject/assets/61058386/7a0b4c51-4bc6-496d-8d71-e8ca5e031e4e)

You can see on the image above the Job Workers executing commands in parallel with the Main thread.

![Skärmbild 2023-11-03 222341](https://github.com/Jellopys/DOTSProject/assets/61058386/09828df0-dcb5-46d7-99cf-75b64cd5a396)

^ In the image above, in this case, the worker threads are not being used. This is unused performance that we could possibly makes use of.
