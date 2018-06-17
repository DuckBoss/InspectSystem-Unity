# NOTICE:
I will be reworking the scripts and assets in this repository to make it more streamlined for Unity 2018.2  
I will be making the reworked version the main branch, and move the current pre-2018 version to a legacy branch.  


# InspectSystem-Unity
An inspection/examination system to read in-game signs, walls, etc.  
Tested on Unity 2017/2018.

[![GitHub release](https://img.shields.io/badge/Build-1.03-brightgreen.svg)](https://github.com/DuckBoss/InspectSystem-Unity/releases/latest)
[![Packagist](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/DuckBoss/InspectSystem-Unity/blob/master/LICENSE)

## Dependencies
1) <b>Text Mesh Pro</b> (Available for Free on the Unity Asset Store, used for the in-game UI, can be replaced with default Unity UI elements by messing with the code very easily)  
<i> Note:  Text Mesh Pro is now a built-in package with Unity 2018+ </i>

2) <b>Unity Standard Assets</b> (For the demo project, this isn't required for implementing into your own games)

## Demo Scene
- Press 'E' while looking at the sign and standing next to it.

## Features
- Supports markup language in text dialogues
- Supports pitch modulated sfx for text dialogues
- Configuration for reinspectable or non-reinspectable objects. (can/cannot inspect items more than once)
- Implemented unityevents that can be executed at the end of inspection dialogues
- Tooltips to help explain inspector variables

## Current Issues/Notifications
- <b> Please use the updated release builds for maximum compatibility! </b>
