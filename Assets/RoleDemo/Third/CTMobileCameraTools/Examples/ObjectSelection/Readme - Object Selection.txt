Object Selection example.

This example demonstrates how to:
- Implement simple object selection
- Prioritize object click over camera gestures (when player clics on any building with layer specified in settings, any gesture starting from that click will be ignored to enable precise selection, or to lock parts of the screen from executing gestures)
Any gesture that started off the prioritized layer and later continued over that layer will be uninterrupted. 

Default Controls in Editor:
- Left mouse button down and swipe - Pan camera.
- Left mouse button down and swipe horizontally and hold Z - Zoom. 
Swipe should be performed on either left or right side of the screen.

Selected settings
- Most settings are the same as in OrthoGraphic example
- Prioritize Raycast layer set to true 
- Layer to prioritize set to "Water"
- Buiding prefab has collider on "Water" layer and small script;

Scripts:
- CityGenerator.cs - spawns blocky city (run beforehand in editor) this time we use only houses, that have a SelectableBuilding.cs script attached
- SelectableBuilding.cs - listenes for MouseDown input and registers self to SelectionController
- SelectionController.cs - stores currently selected building, and deselects previously selected if exixst. 