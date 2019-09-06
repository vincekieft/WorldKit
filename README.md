# WorldKit api
A procedural terrain generation api created for Unity runtime and editor that uses the full power of compute shaders.

### API structure

###### Builders
The API is build upon the idea of the builder pattern.
There are different builders for different purposes but all extend from the same abstract base class `WorldKit.api.procedural.Builders.ABuilder` ( namespace included ).
Implemented builders:

- `WorldKit.api.procedural.Builders.HeightMapBuilder` used to build procedural heightmaps
- `WorldKit.api.procedural.Builders.TextureBuilder` used to build procedural textures

Every builder instance is used to create a single item of what your trying to generate (e.g. a single heightmap or a single texture).
The reason every builder represents a single item is because of performance optimization.
Every builder allocates certain buffers on the GPU and keeps them there until you releases them manually by calling the `Release()` method on the builder.
This is done to add the possibility of adding multiple layers to a builder without having to send all the data back and forth between the GPU and CPU.

###### Layers
The actual building is done by adding the right layers to the builder instance.
A layer represents a certain modification you want to perform on the builders current state.
Implemented layers:

- `WorldKit.api.procedural.Layers.Clamp` (Heightmap layer)
- `WorldKit.api.procedural.Layers.Expand` (Heightmap layer)
- `WorldKit.api.procedural.Layers.HeightContrast` (Heightmap layer)
- `WorldKit.api.procedural.Layers.HydraulicErosion` (Heightmap layer)
- `WorldKit.api.procedural.Layers.PerlinNoise` (Heightmap layer)
- `WorldKit.api.procedural.Layers.PseudoRandomNoise` (Heightmap layer)
- `WorldKit.api.procedural.Layers.Terrace` (Heightmap layer)
- `WorldKit.api.procedural.Layers.HeightMapToTexture` (Texture layer)

These layers can be added to a builder by calling the `AddLayer()` method on a builder.
This method directly adds a new layer to the builder and performs the layers modifications, meaning you dont have to manually execute the layers.
Because the layers only send minimal data to the GPU, executing layers on a builder is very efficient.

###### Utilities
WorldKit API also comes with a couple handy utility class:

- `WorldKit.api.procedural.Utils.BufferUtils`
- `WorldKit.api.procedural.Utils.MathUtils`
- `WorldKit.api.procedural.Utils.MeshGenerationUtils`
- `WorldKit.api.procedural.Utils.TerrainUtils`

### Implementation
Using the WorldKit API is very simple. You create a builder, add the layers you want to use and retrieve the output.

###### Example
Here is an example of generating a heightmap and applying it to a unity terrain.
We start by creating the builder itself:
```c#
using UnityEngine;
using WorldKit.api.procedural.Builders;
using WorldKit.api.procedural.Layers;
using WorldKit.api.procedural.Utils;

public ComputeShader shader; // WorldKit compute shader
public Terrain terrain; // Just a unity terrain

public void BuildTerrain()
{
    // Get resolution of target terrain
    int resolution = terrain.terrainData.heightmapResolution;
    
    // Construct height map builder
    HeightMapBuilder height = new HeightMapBuilder(shader, resolution);
    
    // WorldKit utility method to apply a heightmap to our unity terrain
    terrain.ApplyHeightMap(height.HeightMap());
    
    // Dont forget to release the builder when your done
    height.Release();
}
```

Now that the builder is constructed we can start adding layers to shape our height map.
We start by adding a perlin noise layer:

```c#
// Add perlin noise layer
height.AddLayer(new PerlinNoise(PerlinNoise.PerlinType.Standard, 2f, 8, 0.8f));
```

The result will look like this:

![alt text](https://i.ibb.co/M7z0JB1/Perlin-Noise.jpg)

Now we add two terrace layers to give the terrain a more interesting shape:

```c#
// Add terrace layers
height.AddLayer(new Terrace(4, 0.4f));
height.AddLayer(new Terrace(24, 0.5f));
```

The result will look like this:

![alt text](https://i.ibb.co/K0jnCQ8/Terrace.jpg)

Then to finish it off we add a hydraulic erosion layer to give the terrain a more natural look:

```c#
// Add hydraulic erosion layer
height.AddLayer(new HydraulicErosion(resolution * 150, 120, 0.03f, 6f, 0f, 0.3f, 0.02f, 0.3f));
```

Our final terrain will now look like this:

![alt text](https://i.ibb.co/y4zgqJG/Erosion.jpg)

Resulting code:
```c#
using UnityEngine;
using WorldKit.api.procedural.Builders;
using WorldKit.api.procedural.Layers;
using WorldKit.api.procedural.Utils;

public ComputeShader shader; // WorldKit compute shader
public Terrain terrain; // Just a unity terrain

public void BuildTerrain()
{
    // Get resolution of target terrain
    int resolution = terrain.terrainData.heightmapResolution;
    
    // Construct height map builder
    HeightMapBuilder height = new HeightMapBuilder(shader, resolution);
    
    // Add perlin noise layer
    height.AddLayer(new PerlinNoise(PerlinNoise.PerlinType.Standard, 2f, 8, 0.8f));
    
    // Add terrace layers
    height.AddLayer(new Terrace(4, 0.4f));
    height.AddLayer(new Terrace(24, 0.5f));
    
    // Add hydraulic erosion layer
    height.AddLayer(new HydraulicErosion(resolution * 150, 120, 0.03f, 6f, 0f, 0.3f, 0.02f, 0.3f));
    
    // WorldKit utility method to apply a heightmap to our unity terrain
    terrain.ApplyHeightMap(height.HeightMap());
    
    // Dont forget to release the builder when your done
    height.Release();
}
```

### Guide lines
Here are a guide lines to keep in mind while using the system:

- Try to minimize the amount of builders you create. Everytime a builder is constructed, data has to be send to the GPU.
- The utility function `ApplyHeightMap` is pretty slow, try to avoid unnecessary use.

### Performance
WorldKit performs very fast because of the GPU use. Generation times are normally around (These tests were run with a single heightmap builder and 4 layers: perlin noise, 2x terrace, erosion):

- 4 ms for 1k heightmaps (1024 by 1024)
- 10 ms for 2k heightmaps (2048 by 2048)
- 40 ms for 4k heightmaps (4096 by 4096)

### Requirements
The requirements for the WorldKit api are:

- Unity 5.6 or higher ( because of the use of compute shaders )
- Support for compute shaders
