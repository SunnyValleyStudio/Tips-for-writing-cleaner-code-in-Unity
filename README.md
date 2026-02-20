# Tips for Writing Cleaner Code that Scales in Unity

This repository contains the source code for a series of articles about writing cleaner and scalable code in Unity. The series is designed to help developers improve their coding practices by applying object-oriented design principles, SOLID principles, and design patterns. You can read the full series of articles at [Unity Discussions](https://discussions.unity.com/t/tips-for-writing-cleaner-code-that-scales-in-unity-a-five-part-series/1570854) .

![Article 3 10 NPC Wave result processed gif](https://github.com/PetterSunnyVR/Tips-to-writing-cleaner-code-in-Unity-/assets/17239042/caa3436f-727b-41b4-9215-610e012e787d)


## Getting Started


### Prerequisites

- Unity 6000.0.17f1 or higher
- Basic understanding of C# and Unity

### Installation

This repository contains a '**Project**' folder, which can be opened in Unity Hub, and a '**Scripts for Articles**' folder, where you can find the scripts as they evolve with each article. This setup makes it easy to see how each script changes after refactoring or extending its functionality.

1. Clone or download this repository,
2. Open the "Project" folder in Unity Hub,
3. Search for "_Scripts" folder and inside it you will find folders called "Article 1" (up to "Article 5"),
4. Each folder contains "Start" and "Result" folder where you will find a unity scene that you can open and all the scripts used in it

![image](https://github.com/PetterSunnyVR/Tips-to-writing-cleaner-code-in-Unity-/assets/17239042/007f0cab-2fdd-45d0-8331-d9d4523743b9)


### Scripts for Articles

It contains scripts that I reference in the Article series to make it easier for the readers to follow along. Scripts for each article are placed in separate namespaces ex "**Tips.Part_1_Result**" or "**Tips.Part_1_Start**" so that you can follow along with our article series or preview the resuls.

## Series Overview


### Article 1: Breaking Down the Monolith to Add an AI-Driven NPC

In this article, we refactor the initial project to separate concerns and make the codebase more maintainable. We focus on:

- **Single Responsibility Principle (SRP)**: Breaking down the monolithic `PlayerMonolithic` script into smaller, focused components.
- **Dependency Inversion Principle (DIP)**: Introducing abstractions to decouple the `PlayerMonolithic` from specific input implementations.
- **Strategy Pattern**: Implementing different rotation strategies for the player and NPC.

___

### Article 2: Adding a Jump Mechanic without Spaghetti Code

This article demonstrates how to add a jumping mechanic to the game using the State Pattern, ensuring that the code remains clean and maintainable. Key topics covered:

- **State Pattern**: Separating different states (e.g., Movement, Jump, Fall) and their transitions.
- **Transitions**: Defining transition rules to manage state changes.
- **Interface Segregation Principle (ISP)**: Extending functionality without modifying existing code.

___

### Article 3: Adding a Modular Interaction System Using Interfaces

In this article, we build a modular interaction system using interfaces, enabling scalable and maintainable interactions in the game. Key topics include:

- **Creating a modular interaction system**: How the IInteractable interface allows us to create a modular and flexible interaction system.
- **Polymorphism**: How detecting objects of type IInteractable and passing a GameObject enables objects of different classes to be treated as instances of a common interface.
- **Creating different interactions**: How we utilize the IInteractable interface to create a simple "switch," a pickable weapon, and NPC interactions.

___

### Article 4: Composition vs. Inheritance

In this article, we will explore the concepts of Composition and Inheritance. We will see how both can be used to make our code architecture more maintainable. Key topics covered:

- **Inheritance vs Flexibility**: The pros and cons of each approach, and how using both is often the best solution for achieving flexibility and reusability.
- **Agent Inheritance hierarchy**: Inheritance allows for the reuse of behaviors. We'll examine how creating an Agent inheritance tree helps separate Player, NPC, and EnemyNPC-specific logic into corresponding subclasses.
- **Composition-based Movement System**: We'll explore how extracting the IAgentMover interface allows us to compose agents with different movement systems (e.g., EnemyNPC uses a NavMesh-based mover).

___

### Article 5: Interface keyword and how to use it for adding a Damage System 

In the final article of the series, we explore the interface keyword which allows us to create systems that are easily extendable with new behaviors. Topics include:

- **IDamagable interface**: How different objects can implement the same interface, allowing them to be treated uniformly—demonstrating polymorphism.
- **Health system**: The Health script implements the IDamagable interface to react to the Attack State.
- **Detecting Hit events**: Using the [Physics.SphereCast()](https://docs.unity3d.com/ScriptReference/Physics.SphereCast.html) method to detect all objects that can be hit.
- **Implementing different Damagable objects**: Player, EnemyNPC, and Tree all implement the IDamagable interface, producing different results and feedback when hit.
- **Refactoring State pattern**: To complete the project, we refactor the StateFactory() method into a separate inheritance hierarchy. This separates the responsibility of state creation from the Agent and its subclasses.

___

### Article 6: Extending Interactions — Doors, Destruction, and Locked Access Rules

In this article, we build on the foundation from Articles 1–5 by adding progressively more complex door interactions, while keeping the codebase clean and extensible. Key topics covered:

- **SRP in practice**: Separating responsibilities between `DoorInteractable` (interaction trigger) and `DoorController` (door state + animation).
- **Reusing existing systems**: Applying the `IDamagable` + `Health` setup from Article 5 to make doors (and a box) destructible.
- **DRY (Don't Repeat Yourself)**: Identifying duplicated "health <= 0" checks across multiple feedback scripts.
- **Adapter Pattern (inspired solution)**: Introducing `HealthDeathObserver` to translate `Health.OnHit` into a higher-level `OnDied` event, allowing feedback scripts to subscribe without duplicating death-condition logic.
- **Composition + Open–Closed Principle (OCP)**: Adding `IAccessRule` (e.g., `UnlockedRule`, `LockedRule`) so doors can gain new access requirements without rewriting the door interaction code.

___

### Article 7: Building a Clean Vehicle Controller with Composition

In this article, we apply the same principles to a completely different mechanic: a modular vehicle controller built with Unity's `WheelCollider`. Key topics covered:

- **Composition-first controller design**: `CarController` coordinates small modules instead of becoming a monolithic script.
- **Input via abstraction (DIP)**: Reading controls through `IVehicleInput` (implemented by `PlayerVehicleInput`) to keep input swappable.
- **Plain C# movement module**: Implementing driving behavior in `CarEngine` (non-MonoBehaviour) for reuse and easier testing.
- **Data-driven tuning**: Storing vehicle parameters in a `CarStats` `ScriptableObject` to avoid hard-coded values and support multiple vehicles.
- **Modular feedback systems**: Adding presentation-only components like `BrakeLights`, `SimpleEngineAudio`, and `SimpleSkidAudio` that receive minimal data from the controller.

## Assets Used:
- [3D Game Kit by Unity](https://assetstore.unity.com/packages/templates/tutorials/unity-learn-3d-game-kit-115747),
- [3D Game Kit Lite by Unity](https://assetstore.unity.com/packages/templates/tutorials/3d-game-kit-lite-135162),
- [Food Props by Unity by Unity](https://assetstore.unity.com/packages/3d/food-props-163295),
- [Starter Assets - ThirdPerson by Unity](https://assetstore.unity.com/packages/essentials/starter-assets-thirdperson-updates-in-new-charactercontroller-pa-196526),
- [Animations created using Unity Muse AI](https://unity.com/products/muse),

## License

This project is licensed under the MIT License.
