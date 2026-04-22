# Persistent ID [1.0.0]
### Description
This is a modification for the game "Data Center". It overwrites the base game unique ID generation and provides GUID as unique IDs instead. This has been tested personally and no problems have yet to occur.

Persistent ID start with the prefix "404ID:" and then branch to either "Switch", "PatchPanel" or "Server". Customer is not included as there no conflicting IDs involving it. There is no duplicate detection implemented at this time as GUIDs are mostly guaranteed unique. With 8 character length, it allows upto 4+ billion combinations. Chance of getting duplicate is extemely low. If one is found, please post issue.

This mod requires Melon Loader to be installed with Data Center.

## Recommendation
It is recommended you start new save when playing with this mod. If playing with existing save, MAKE A BACKUP! This mod WILL OVERWRITE YOUR SAVE DATA and attempt to remap all existing servers, switches & patch panels. If you experience duplicate or weirdness after loading your save with the mod, then your save is already victim of duplication issues and can not be saved easily.

Save Location: "%AppData%/LocalLow/WASEKU/Saves"

Again, strongly recommended to start new save for full effect! Use on existing saves at own risk!

You have been notified and warned!

### How To Install
1. Install MelonLoader To Data Center
2. Download latest mod .dll from https://github.com/404NyanFound/Persistent-ID/releases/tag/Release
3. Copy to "~/Data Center/Mods"
4. Play Game

### Dependencies
* Data Center
* 0Harmony
* Il2CppInterop.Runtime
* Il2CppMSCoreLib
* MelonLoader
* Unity.Entities
* UnityEngine.CoreModule

All dependencies are found with MelonLoader. No other external dependencies required.

### AI Usage
Gemini (Google AI) was used to learn about MelonLoader and for initial prototyping of logic. Later was cleaned up and refactored by myself into working binary.
