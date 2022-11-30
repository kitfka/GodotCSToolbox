# Godot C# Toolbox

The [original Godot C# Tools project is here](https://github.com/redxdev/GodotCSTools).
I used that old project as a starting point.

This is a small library of utility functions that make working in C# when using [Godot](https://godotengine.org/) much easier.

## Project Goals

Adding some useful tools to make c# in Godot a little bit nicer.

## Installing

This library is [available on nuget](https://www.nuget.org/packages/GodotCSToolbox)


use 
```dotnet add package GodotCSToolbox --version 1.0.0-beta10```

The result should look something like this:

```xml
  <ItemGroup>
    <PackageReference Include="GodotCSToolbox" Version="1.0.0-beta10" />
  </ItemGroup>
```



## Building Yourself

Copy a `.mono` folder into this project's folder from an existing Godot (with mono) project. The `GodotCSToolbox.csproj` looks
for a couple of assemblies from Godot in that folder.

## Examples

### NodePathAttribute

`NodePathAttribute` gets a node at the given path.

```csharp
using System;
using Godot;
using GodotCSToolbox;

public class MyNode : Node
{
    [NodePath("Sprite")] // gets the node named "Sprite"
    private AnimatedSprite _sprite = null;

    [NodePath("Sprite/OtherNode")] // relative paths work too!
    private Node2D _otherNode = null;

    public override void _Ready()
    {
        this.SetupNodeTools(); // required to apply the effects of attributes. `this` is required due to how extension methods work.

        _sprite.Play("SomeAnimation"); // _sprite and _otherNode should now contain nodes!
    }
}
```

#### Gotchas

You will receive a warning, `CS0649: field is never assigned to`, if you declare a field as so:

```csharp
[NodePath("spritePath")]
private AnimatedSprite _sprite;
```

This is because the compiler doesn't know that GodotCSToolbox will be setting the value later. To hide the warning, give
the field a default value of null:

```csharp
[NodePath("spritePath")]
private AnimatedSprite _sprite = null;
```

### ResolveNodeAttribute

`ResolveNodeAttribute` gets a node based on a separate `NodePath` field, which will generally be an export.

```csharp
using System;
using Godot;
using GodotCSToolbox;

public class MyNode : Node
{
    [Export]
    public NodePath spritePath; // this is set in the editor

    [ResolveNode(nameof(spritePath))] // gets the node from the field named "spritePath"
    private AnimatedSprite _sprite = null;

    public override void _Ready()
    {
        this.SetupNodeTools(); // required to apply the effects of attributes. `this` is required due to how extension methods work.

        _sprite.Play("SomeAnimation"); // _sprite should now contain a node!
    }
}
```

### UniqueNodeAttribute

`UniqueNodeAttribute` gets the node that has a "unique Name". Check [Scene Unique Nodes](https://docs.godotengine.org/en/stable/tutorials/scripting/scene_unique_nodes.html)

```csharp
using System;
using Godot;
using GodotCSToolbox;

public class MyNode : Node
{
    [UniqueNode] // will call somthing similar to GetNode("$PlayerSprite")
    private AnimatedSprite PlayerSprite;

    public override void _Ready()
    {
        this.SetupNodeTools(); // required to apply the effects of attributes. `this` is required due to how extension methods work.

        _sprite.Play("SomeAnimation"); // _sprite should now contain a node!
    }
}
```

#### Gotchas

You will receive a warning, `CS0649: field is never assigned to`, if you declare a field as so:

```csharp
[ResolveNode(nameof(spritePath))]
private AnimatedSprite _sprite;
```

This is because the compiler doesn't know that GodotCSToolbox will be setting the value later. To hide the warning, give
the field a default value of null:

```csharp
[ResolveNode(nameof(spritePath))]
private AnimatedSprite _sprite = null;
```

### Node Extensions

There are a number of extensions to the `Node` class.

Note that `this` is required when calling extension methods.

The are in the namespace ```GodotCSToolbox.Extensions```

#### `Node.GetNode<T>()`

A generic variant of `GetNode()` that casts to the given type (returning null if the cast fails):

```csharp
var sprite = this.GetNode<AnimatedSprite>("MySprite");
sprite.Play("SomeAnimation");
```

`Node.GetChild<T>()` and `Node.FindNode<T>()` are also available.

## GodotFileStream

This is a wrapper around `Godot.File` that allows it to be used with any function that accepts a `Stream`. It supports read/write operations, seeking,
and compression.

It is recommended to _not_ use this class as it is quite a bit slower than built-in .NET classes. If you need to use a godot path (like `user://`) then
convert it to an absolute path with `ProjectSettings.GlobalizePath`.

```csharp
using (var stream = new GodotFileStream("user://somefile.txt", File.ModeFlags.Write))
using (var writer = new System.IO.StreamWriter(stream))
{
	writer.WriteLine("Hello, world!");
}
```

Operations on `GodotFileStream` should work similarly to standard .NET streams.

## Contributing

Contributions are welcome! Post an issue or make a pull request [here](https://github.com/kitfka/GodotCSToolbox/issues)!

Any code submitted should follow these (very general) guidelines:

- Documentation comments should be provided when it makes sense to, especially with regards to public-facing APIs
- Code should be relatively performant
- New features should help better integrate Godot with C# and only that. Adding brand new features that Godot doesn't have
out of the box isn't a goal of this project - the idea is just to "patch" Godot's implementation of C# support.
