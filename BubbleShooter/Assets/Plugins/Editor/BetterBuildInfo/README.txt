README - Better Build Info:
---------------------------
Many thanks for purchasing 
Better Build Info! Before getting your hands on the tool please have a read.


Contact / Support: 
------------------
Support/Feedback: support@dmprog.pl
Twitter:          @gwiazdorrr
Quick Demo:       https://www.youtube.com/watch?v=ohHBt32QitY
Support Page:     http://dmprog.pl/unity-plugins/


Legal Stuff / Licensing:
------------------------
Better Build Info is licensed per-seat, as with all other editor extensions, according to Unity's EULA.
This plugin is shipped with zipinfo (http://www.info-zip.org/) for peeking into .apk archives on Windows. 
The license is in Win32 directory.


FAQ:
------------
Q: What's the setup?
A: None. The tool is enabled by default - every build made since importing the package is going to 
be analyzed and report is going to be generated.

Q: How to open the Better Build Info Window?
A: Window -> Better -> Better Build Info. If you want more than one instance (for comparing etc.) 
select: Window -> Better -> Better Build Info (New Instance)

Q: How to disable report generating/change output path?
A: In the Better Build Info window, click Settings -> Open. The settings asset will be focused in 
the Inspector.

Q: How do I use the grid / map view?
A: Clicking selects and asset. You can do ctrl+click to select multiple assets. In the grid mode, 
shift+select also works. Since selection is integrated with the Unity selection, Undo & Redo work 
just fine.

Q: What's with the question marks in the grid view?
A: If asset can't be loaded and proper thumbnail generated, the question mark is used instead.

Q: What are the supported platforms?
A: Works on all of them, with an exception of calculating scene sizes, which currently only works 
for Standalone, iOS and Android (working on it).

Q: Can I move the package to another directory?
A: Not yet. It has to live in Assets/Plugins/Editor/Better.

Q: What info gets collected if I check "Collect Asset Details?"
A: The list is below (currently no way to expand it).
- Texture: Format, Mipmaps (count), Width, Height, CompressedSize (if enabled), NPOT
- AudioClip: Frequency, Length, Channels, LoadType, Format, Preload, LoadInBackground
- Scene: GameObjects (count), Components (count); if static batching is enabled: Vertices (count), 
  Triangles (count), VertexFormat (Unity 5.4+), LightmapTexture (count)
- Prefab: GameObjects (count), Components (count)
- Model: Optimize, Vertices (count), Triangles (count), BlendShapes (count), VertexFormat (Unity 5.4+)
- MonoBehaviors: ScriptClass, ScriptReferences
- Material: Keywords, Keywords count

Q: How do I resize Asset Details columns?
A: Drag left edge of a header cell.

Q: What's the deal with BetterBuildInfo.SetExpectedScenesPaths?
A: When building a scene that's currently opened in the Editor, Unity actually builds a temporary copy with a temporary name. When building from
Build Settings dialog, the tool can check which scenes were enabled and guess which scene the temporary copy represents. When using 
BuildPipeline.BuildPlayer there's no way for the tool to guess. You can either make sure you close the scene before building it or call
BetterBuildInfo.SetExpectedScenesPaths with the same paths you pass to BuildPipeline.BuildPlayer. It needs to happen immediately before calling
the latter.