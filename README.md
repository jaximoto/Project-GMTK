# Configuring Unity Editor with Smart Merge:
## 1. Set Asset Serialization to Force Text
Open your Unity project.

Go to Edit > Project Settings > Editor.

Under Asset Serialization, set Mode to:
`Force Text`

This ensures Unity saves .prefab, .unity, .asset, etc. files in a text-based YAML format, which is mergeable.

## 2. Set Version Control Mode to Visible Meta Files
Now go to the version control tab in the Project settings to set Mode to:
`Visible Meta Files`

This makes sure Unity creates .meta files for assets, which are also text and can be merged.

---

# Setting up Unity Smart Merge on macOS

This guide helps you configure Git on macOS to use **Unity's Smart Merge** tool automatically for resolving Unity YAML merge conflicts.

---

## 1. Make sure you have the Unity Smart Merge wrapper script

We provide 2 `smartmerge` scripts in the repo root. One called `smartmerge.bat` for windows and `smartmege.sh` for mac. This is a small shell script that calls Unity's Smart Merge tool correctly on both windows and macOS.

---

## 2. Make the script executable

Open Terminal at project root and run:

```bash
chmod +x ./smartmerge
```
---

## 3. Change your .gitconfig
Change your .gitconfig so that `device` points to `smartmerge.sh`, not `smartmerge.bat`


## 4. Test your setup
Create a branch and modify a `.prefab` or `.unity file`.

On another branch, modify the same file differently.

Merge the branches.

If set up correctly, Git will invoke Unity Smart Merge to auto-resolve the merge conflict.
