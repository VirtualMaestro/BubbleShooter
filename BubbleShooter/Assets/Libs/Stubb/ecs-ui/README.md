[![discord](https://img.shields.io/discord/404358247621853185.svg?label=discord)](https://discord.gg/5GZVde6)
[![license](https://img.shields.io/github/license/Leopotam/ecs-ui.svg)](https://github.com/Leopotam/ecs-ui/blob/develop/LICENSE)
# Unity uGui extension for Entity Component System framework
Easy bindings for events from Unity uGui to [ECS framework](https://github.com/Leopotam/ecs) - main goal of this extension.

> **Important! It's "structs-based" version, if you search "classes-based" version - check [classes-based branch](https://github.com/Leopotam/ecs-ui/tree/classes-based)!**

> C#7.3 or above required for this framework.

> Tested on unity 2019.1 (dependent on Unity engine) and contains assembly definition for compiling to separate assembly file for performance reason.

> Dependent on [ECS framework](https://github.com/Leopotam/ecs) - ECS framework should be imported to unity project first.

# Installation

## As unity module
This repository can be installed as unity module directly from git url. In this way new line should be added to `Packages/manifest.json`:
```
"com.leopotam.ecs-ui": "https://github.com/Leopotam/ecs-ui.git",
```

## As source
If you can't / don't want to use unity modules, code can be downloaded as sources archive of required release from [Releases page](https://github.com/Leopotam/ecs-ui/releases/tag/2020.2.22) or cloned from [classes-based branch](https://github.com/Leopotam/ecs-ui/tree/classes-based).

# Systems

## EcsUiEmitter

Ecs run-system that generates entities with events data to `ecs world`. Should be placed on root GameObject of Ui hierarchy in scene (on root Canvas, for example) and connected in `ecs world` before any systems that should process events from ui:
```csharp
public class Startup : MonoBehaviour {
    // Field that should be initialized by instance of `EcsUiEmitter` assigned to Ui root GameObject.
    [SerializeField] EcsUiEmitter _uiEmitter = null;

    EcsSystems _systems;

    void Start () {
        var world = new EcsWorld ();
        _systems = new EcsSystems (world)
            // Additional systems here...
            .Add (new TestSystem ())
            .InjectUi (_uiEmitter);
        _systems.Init ();
    }
}

public class TestSystem : IEcsInitSystem {
    // auto-injected fields.
    EcsUiEmitter _ui = null;
    [EcsUiNamed("MyButton")] GameObject _btnGo;
    [EcsUiNamed("MyButton")] Transform _btnTransform;
    [EcsUiNamed("MyButton")] Button _btn;

    public void Init () {
        // All fields above will be filled and can be used here.
        // Results of injection:
        // _ui = instance of injected EcsUiEmitter.
        // _btnGo = _ui.GetNamedObject ("MyButton");
        // _btnTransform = _ui.GetNamedObject ("MyButton").GetComponent<Transform> ();
        // _btn = _ui.GetNamedObject ("MyButton").GetComponent<Button> ();
    }
}
```
> No need to inject `EcsUiEmitter` instance through `EcsSystems.Inject` if you call `EcsSystems.InjectUi` already.

# Actions
MonoBehaviour components that should be added to uGui widgets to transfer events from them to `ecs-world` (`EcsUiClickAction`, `EcsUiDragAction` and others). Each action component contains reference to `EcsUiEmitter` in scene (if not inited - will try to find emitter automatically) and logical name `WidgetName` that can helps to detect source of event (or just get named `GameObject`) inside ecs-system.

# Components
Event data containers: `EcsUiClickEvent`, `EcsUiBeginDragEvent`, `EcsUiEndDragEvent` and others - they can be used as ecs-components with standard filtering through `EcsFilter`:
```csharp
public class TestUiClickEventSystem : IEcsRunSystem {
    // auto-injected fields.
    readonly EcsWorld _world = null;
    readonly EcsFilter<EcsUiClickEvent> _clickEvents = null;

    public void Run () {
        foreach (var idx in _clickEvents) {
            ref EcsUiClickEvent data = ref _clickEvents.Get1 (idx);
            Debug.Log ("Im clicked!", data.Sender);
        }
    }
}
```

# Initialization
```csharp
public class Startup : MonoBehaviour {
    // Field that should be initialized by instance of `EcsUiEmitter` assigned to Ui root GameObject.
    [SerializeField] EcsUiEmitter _uiEmitter = null;

    EcsWorld _world;
    EcsSystems _systems;

    void Start () {
        _world = new EcsWorld ();
        _systems = new EcsSystems (_world);
        _systems
            // Additional systems here...
            .InjectUi (_uiEmitter)
            .Init ();
    }

    void Update () {
        _systems?.Run ();
    }

    void OnDestroy () {
        if (_systems != null) {
            _systems.Destroy ();
            _systems = null;
            _world.Destroy ();
            _world = null;
        }
    }
}
```

# Examples
[Examples repo](https://github.com/Leopotam/ecs-ui.examples.git).

# License
The software released under the terms of the [MIT license](./LICENSE.md).

No support or any guarantees, no personal help.

# Special thanks (List sorted in back order, from high to low donations)
* [VirtualMaestro](https://github.com/VirtualMaestro)
* [Korchoon](https://github.com/korchoon)
* [PureEmDe](https://github.com/PureEmDee)
* [SH42913](https://github.com/SH42913)
* [Svetlozar Valchev](https://github.com/SvetlozarValchev)
* [Óscar F Gómez S](https://github.com/Racso)