Perspective setup example.



This example demonstrates how to:
- setup CTCameraController in perspective scene
- setup all 4 control modes (pan, rotate, zoom, tilt)
- setup camera follow with dynamically switched targets
- change camera operation bounds from code
- pause/resume camera follow from code

Default Controls in Editor:
- Left mouse button down and swipe - Pan camera.
- Left mouse button down and swipe vertically and hold T - Tilt camera.
- Left mouse button down and swipe vertically and hold R - Rotate.
- Left mouse button down and swipe horizontally and hold Z - Zoom. 
swipe should be performed on either left or right side of the screen. 
- click next/previous target - to change which car camera follows. 
- click toggle cam follow - to pause/resume camera follow task. 
- click increase/decrease bounds - to increase/decrease camera operation bounds.  

Selected settings
- Camera Projection set to Perspective
- Camera follow enabled
- Pan, Zoom, Rotate, Tilt all enabled


Scripts:
- CityGenerator.cs - spawns blocky city (run beforehand in editor)
- CityExample.cs - demonstrates how to access CTControl API from another script for actions such as: change cam follow target, toggle camera follow, change camera operation bounds (For more information check the documentation)
- CarController.cs - simple car movement controller that moves tranform between specified nodes