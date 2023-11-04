# DOTSProject

This project is a school assignment where we need to use Unity DOTS (ECS, Jobs, Burst) to make a performant space shooter game. Using Entities was initially really difficult to grasp, a lot of the documentation I found were deprecated information and it was hard to read how the flow of the code actually works, because it's so different from the regular GameObjects way of structuring the code. However, I learned a lot and definitely saw the benefits of using entities and jobs to create scalable products. With 10.000+ entities I managed to hold a solid 120 fps still.

WASD - Movement,
Mouse - Aim & Rotate,
Left Mouse Button - Fire projectile,

The projectiles, units and players are all Entities that are controlled by the Systems that updates each entity in various ways. 
The components that you add to the entities are what tells the systems which entity to update or check for. These components can be enabled, disabled, added and removed. This makes it performant because its only one "Singleton actor" (the system) that iterates over all entities and executes commands to only the entities that matter. Then the jobs system takes over and executes these iterations with the use of CPU worker threads.

![image](https://i.imgur.com/LKYQg2b.png)

The Jobs system in DOTS allows me to make use of multithreading with job workers helping out with processing from the main thread.

![image2](https://i.imgur.com/jlOvVDk.png)

You can see on the image above the Job Workers executing commands in parallel with the Main thread.

![image3](https://i.imgur.com/LhHqVhP.png)

^ In the image above, in this case, the worker threads are not being used. This is unused performance that we could possibly makes use of.
