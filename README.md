# Terramental_LevelEditor

Welcome to this guide for the Level Editor.

/// Button Map ///

- TAB (The TAB key switches between Tile and Entity mode, allowing designers to quickly switch between adding tiles and entities).
- Right/Left Arrow Key (The Arrow Keys can be used to cycle through each tile/entity).
- Enter (The Enter Key saves the map data and exports it to a JSON file that can be found in the level editors netcoreapp3.1 folder. 'Terramental_LevelEditor\LevelEditor\bin\Debug\netcoreapp3.1').
- L (Loads the Level from the MapData.JSON file).
- F (Fills the entire map with the current selected tile. This is useful for backgrounds.
- I (Gets the current highlighted tile/entity and sets this texture to the current one. This feature makes the level creation process easier).

/// Exporting and Importing Map Data /// Step-By-Step Guide ///

(1) To export the Map Data, simply use the Enter key to save the Level.
(2) Locate the MapData.JSON file located in the netcoreapp3.1 folder. 'Terramental_LevelEditor\LevelEditor\bin\Debug\netcoreapp3.1'
(3) Copy the MapData.JSON file.
(4) Paste the MapData.JSON file in the equivalant folder in the Terramental Project.
(5) Once the MapData.JSON file has been pasted, the level should load.
(6) Save the MapData.JSON file in the shared google drive folder to ensure the level is not deleted.

/// Load Map Data in the Level Editor ///

(1) Ensure the MapData.JSON file is present in the correct folder: 'Terramental_LevelEditor\LevelEditor\bin\Debug\netcoreapp3.1'
(2) Press the L Key

///Coming Soon///

- More Tiles
- More Entities