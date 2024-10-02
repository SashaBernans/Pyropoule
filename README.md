# Project Prototype

**Unity Version:** 2023.1.9f1

This is a prototype project showcasing basic gameplay mechanics. The current state is designed for testing purposes, with certain features disabled to ease the development process.

## Controls

- **Spacebar**: Jump  
- **A / D**: Move left / right  
- **Mouse 1**: Aim and shoot  

## Features

- Player receiving damage is disabled to simplify testing and focus on other mechanics.

## Known Issues

- **Restart Functionality**: Restart does not work properly after death. To restart, the game must be relaunched. The issue is likely related to the `AssetRecycler` being destroyed when the scene reloads.
  
- **Missing Upgrade Icons**: The upgrade panel is missing icons this results in unexpected behaviours. Some icons have not been designed yet.

- **NullReferenceException in Lightning Chain**: A null exception occurs when the lightning chain is unable to find a water block to chain to. This needs further investigation.
