# Implementation Plan - Food Storage Management System 2.0 (.NET Edition)

We will rewrite the legacy Windows Forms application using **.NET MAUI Blazor
Hybrid**. This approach is perfect for your goals because:

1. **C# & .NET**: You keep using the language you know.
2. **Mac & Windows**: MAUI creates native apps for both Mac and Windows from one
   codebase.
3. **Visual Overhaul**: By using "Blazor Hybrid", we can use HTML/CSS for the
   UI, allowing for that "premium", "not-Windows-looking" design you want (which
   is very hard to do in standard WinForms/native MAUI).
4. **Device Access**: Being a native app, we have full access to the Mac/PC
   Camera for scanning.

## User Review Required

> [!IMPORTANT]
> **Prerequisites**: You need **.NET 8 SDK** and the **MAUI workload** installed
> on your Mac. **Architecture**: We are mixing Native C# (for
> logic/backend/device features) with Web UI (HTML/CSS/Razor) for the loop.

> [!NOTE]
> **Non-Destructive**: We will create a **new folder** (`FSMS_Hybrid`) for this
> new version. The original `FSMS_GUI` folder will remain completely untouched.
> **Legacy Compatibility**: Since we are using Supabase (Cloud), your **Old
> WinForms App** can eventually be updated to use the same database, allowing
> you to have a "Modern Windows App" (this new one) and a "Legacy Classic App"
> sharing the same data.

## Proposed Architecture

- **Framework**: .NET MAUI Blazor Hybrid (.NET 8).
- **Language**: C# (Logic), Razor/HTML (UI), CSS (Styling).
- **Database/Auth**: Supabase (via `Supabase-csharp` Nuget package).
- **External APIs**: FatSecret Platform API (C# HTTP Client).
- **Barcode**: `ZXing.Net.Maui` or HTML5-based scanner in the Blazor view.

## Proposed Changes

### 1. Project Initialization

- Create a new solution: `dotnet new maui-blazor -n FSMS_Hybrid`
- Setup **Tailwind CSS** (via CDN or separate build process) or strict CSS
  variables for the "modern" look.

### 2. Database & Auth (Supabase C#)

- Install `Supabase` NuGet package.
- **Schema Mapping**: We will replicate the exact structure from your
  `Food_Item.cs` to Supabase `inventory` table:
  - `item_name` (string) <- `_itemName`
  - `item_type` (string) <- `_itemType`
  - `expiration_date` (date/string) <- `_expirationDate`
  - `item_price` (float) <- `_itemPrice`
  - `item_quantity` (int) <- `_itemQuantity`
  - `storage_location` (string) <- `_storageLocation`
  - `item_unique_code` (string) <- `_itemUniqueCode`
  - `date_added` (date) <- `_dateAdded`
  - `is_expired` (bool) <- `_isExpired`
  - **[NEW]** `barcode` (string) - To store the scanned code.

### 3. Core Features

#### [NEW] Authentication

- Google Login: Use Supabase Auth methods
  `supabase.Auth.SignIn(Provider.Google)`.

#### [NEW] Dashboard

- Razor Pages (`Home.razor`, `Inventory.razor`).
- Modern CSS Grid layout for items.

#### [NEW] Inventory & Barcode

- **Add Item**: C# Form handling.
- **Scanning**: Use `ZXing.Net.Maui` (Camera view).
  - It overlays a camera view in your app.
  - When it detects code, it fires an event with the text (e.g. "890123...").
  - We send that text to **FatSecret API** to get the food name/calories.
- **FatSecret**: C# Service to query the API using your Key.

## Version Control Strategy

To ensure procedural changes are reflected on GitHub:

1. **Init**: Initialize Git repo and `.gitignore` for the new folder.
2. **Scaffold**: Commit basic project structure (`dotnet new`).
3. **Dependencies**: Commit addition of Nuget packages (Supabase, ZXing).
4. **Feature Commits**: Separate commits for "Added Login", "Added Database
   Model", "Added Barcode Scanner", etc.

### Release Strategy

**GitHub Releases** allow us to package your code into downloadable "products"
(like `v1.0.zip`).

- **Drafting a Release**: When we finish a milestone (e.g. "Basic Scanner
  Working"), we will tag it (e.g., `v0.1-alpha`).
- **Binaries**: We can upload the compiled Mac App (`.app`) or Windows Exe
  (`.exe`) to the release page. This lets your friend download and run the app
  _without_ needing to see the code or install VS Code.

## Verification Plan

### Manual Verification

1. **Launch**: Run `dotnet run -f net8.0-maccatalyst` (native Mac app).
2. **UI**: Verify the app looks like a modern web app, not a standard Mac
   window.
3. **Login**: Click "Login with Google", verify browser redirects and session is
   established.
4. **Database**: Add an item, restart app, verify item persists (loaded from
   cloud).
