# Neon-String: Filament Frontier Systems

This repository captures the evolving design + code architecture for **"Filament Frontier / Layer Zero"**, a simulation RPG empire-builder about 3D printing, market warfare, and cyberpunk rebellion.

## Source of Truth: User Prompts & Wants

The project was driven by a sequence of high-level design prompts that define scope, tone, and required systems. These were the core user requirements:

- Build a scalable Godot/Unity architecture for a 2.5D isometric simulation RPG with a full spoof brand registry, economy, combat, and progression (Bedroom → Industrial).
- Implement core printer/slicer/print-job systems and an economic simulation with dynamic markets and inventory tracking.
- Add spoof brands, progression phases, and comedy-fueled lore to power vendors, enemies, and databases.
- Implement printer physics, kinematics, and G-code interpretation with failure modeling.
- Convert slicing into RPG crafting with material science and infill/quality tradeoffs.
- Build market warfare systems, enemy generation, and filament futures pricing.
- Add 3D3D endgame tech tree and distributed manufacturing.
- Add environment simulation for power/heat constraints.
- Add staff management and automation ROI systems.
- Add cyberpunk hacking loops for DRM bypass and dark-web slicing.
- Add visual failure feedback for print artifacts and camera monitoring.

## Implementation Summary

Core systems are grouped by module:

- `src/MarketSim`: brand lore, market foes, filament futures, tech tree.
- `src/EnvironmentSim`: power grid and thermodynamics.
- `src/StaffSim`: staffing, jobs, automation ROI.
- `src/DarkWebSim`: hacking minigame, dark slicer, crypto wallet.
- `src/VisualSim`: procedural failure visuals and shader logic helpers.

## Note on GitHub "push"

This environment does not allow direct network pushes. All changes are committed locally and ready to be pushed from a standard Git client.
