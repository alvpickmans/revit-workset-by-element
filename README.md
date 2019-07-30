# Revit Workset by Element
Simple Revit Command to change the active workset on a Shared Model by setting the workset of a selected element. Cannot get simpler than that.

![Workset by Elememt](./assets/WorksetByElement.gif)

# Installation

Copy the files from the for Revit version that are under the [`dist`](./dist) folderto the corresponding Revit addins folder, to the corresponding folder on your machine. For more info on these folders, see [this useful gist](https://gist.github.com/teocomi/5986db4eba261ec4baeb7325329aa226).

# Usage

Simply click on the Command under `Addins > External Tools` tab in Revit. If an element was already selected, it will set its workset as active, otherwise it will prompt you to select a valid element.

If multiple elements are selected at the same time, it use the first element that has a valid workset.

Revit allows to set a custom shortcut to all commands so I would recommend to set a shortcut like `WS` to the command. Makes your life easier.

# License
This project is under MIT license, so basically means that you are free to do as you please with it, but don't blame if something goes south. For a more formal description, see the [LICENSE](./LICENSE) file.
