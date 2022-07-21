# UNOTweaks

### Description
Tweaks for Ubisoft's "UNO".

### Features
- Skip intro videos 
  - Automatically applied once `UNOTweaks` is installed
- Custom deck and card skins (by [ExiMichi](https://github.com/ExiMichi), instructions can be found [here](https://github.com/linkthehylian/UNOTweaks/wiki/UNO-Skins))
  - Displayed at the bottom right, custom card and deck skins will only be applied once you've started a match
  - You can also collapse the skin GUI by clicking the very top of it!
- Toggle to enable or disable V-Sync
- Set your preferred FPS when V-Sync is disabled
  - All accessible via the menu by pressing the `F1` key

![img](https://user-images.githubusercontent.com/20933012/180174668-39affa04-c0fc-4c5a-8f14-ccbfbb30da01.png)

### Requirements
- You **must** own UNO on Steam for these tweaks to work. https://store.steampowered.com/app/470220/UNO/

### Installation

- Drag and drop or copy and paste the `Assembly-CSharp.dll` to `Uno\UNO_Data\Managed`. ("**Replace the file in the destination**", if prompted)
- If you want to backup the original `Assembly-CSharp.dll`, make sure you do that before replacing it!

### Credits
- linkthehylian
- [ExiMichi](https://github.com/ExiMichi)

### Disclaimer
- I don't own "UNO", this is Ubisoft's game and it's their property. I've only included the classes I edited for this project and not the entire source code. You can use either of the following .NET disassemblers to make your own:
  - [ILSpy](https://github.com/icsharpcode/ILSpy)
  - [dnSpy](https://github.com/dnSpy/dnSpy)
