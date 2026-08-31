# VaderConsulting.Helper

C# .NET Framework 3.5 class library of shared helpers for VaderConsulting WinForms apps. `Methods` checks the current Windows user against an Active Directory group via `System.DirectoryServices.AccountManagement`, expands path/date/user tokens such as `#DESKTOP#`, `#DATE#`, and Australian `#TAXQUARTER#` (July-June) using `Microsoft.VisualBasic.DateAndTime`, converts strings and bytes to hex, and selects a ComboBox item by text. Compiled types also include `ComboBoxItem` (display text plus ID), `FlashWindow` (P/Invoke `user32.dll` `FlashWindowEx` to flash caption and taskbar on Windows 2000 or later), and a `Properties` bag with `ShowEmulatedState`. Extra files on disk but not in the `.csproj` Compile list are `frmProgress` (read-only log window that minimises instead of closing), `CustomToolTip` (owner-drawn bitmap tooltip), `Attribute`, `Enums.DataLayerType` (iServer/SQL), and `Settings` (`AppSettings<T>` JSON via `JavaScriptSerializer`); leftover `App.config` Entity Framework 6 LocalDB and `packages.config` AsyncBridge 0.1.1 are not referenced by the project.

**Source last updated:** 2015-03-15 · **Language:** C# · **Target:** .NET Framework 3.5 · **Output:** class library (`Library`)

## Solution structure

| Project | Language | Type | Purpose |
|---------|----------|------|---------|
| `VaderConsulting.Helper` (`VaderConsulting.Helper.csproj`) | C# | class library (`net35`, WinForms) | Shared helpers: AD group membership, token replacement, `ComboBoxItem`, and `FlashWindow`. |

## How to open

Open `VaderConsulting.Helper.csproj` in Visual Studio 2013 or later (ToolsVersion 12.0). There is no `.sln` in this folder. `frmProgress.cs`, `CustomToolTip.cs`, `Attribute.cs`, `Enums.cs`, and `Settings.cs` are present but not listed in the `.csproj` Compile items.

## Attribution and provenance

Working copy from Dave Robinson's OneDrive Historical Dev folder `VaderConsulting.Helper`. Assembly title/product `VaderConsulting.Helper`; the Visual Studio template still has company/copyright Microsoft 2015. Namespace `VaderConsulting.Helper`. `packages.config` lists AsyncBridge 0.1.1; `App.config` has leftover Entity Framework 6 LocalDB section. Neither is referenced by the `.csproj`.

## License

MIT © 2026 VaderConsulting. See `LICENSE`.
