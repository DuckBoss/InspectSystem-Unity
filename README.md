# InspectSystem-Unity
An inspection/examination system to read in-game signs, walls, etc
Tested on Unity 2017.

[![GitHub release](https://img.shields.io/badge/Build-1.03-brightgreen.svg)](https://github.com/DuckBoss/InspectSystem-Unity/releases/latest)
[![Packagist](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/DuckBoss/InspectSystem-Unity/blob/master/LICENSE)

## Dependencies
1) <b>Text Mesh Pro</b> (Available for Free on the Unity Asset Store, used for the in-game UI, can be replaced with default Unity UI elements by messing with the code very easily)

2) <b>Unity Standard Assets</b> (For the demo project, this isn't required for implementing into your own games)

## Demo Scene
- Press 'E' while looking at the sign and standing next to it.

## Recently Added Features
- Added configuration for reinspectable or non-reinspectable objects. (can/cannot inspect items more than once)
- Implemented unityevents that can be executed at the end of inspection dialogues
- Added Tooltips to help explain inspector variables

## Current Issues
- Text Mesh Pro sometimes doesn't display the instructions above the sign in the demo scene on first start up.
(Not an issue with the Inspect System)
- <b> Please use the updated release builds for maximum compatibility! </b>
