# Food Storage Management System (FSMS)
Version 1.0.0 | A C# .NET 8 WinForms Application by Victor Santana

A robust and intuitive desktop application designed to help users efficiently manage their food inventory, track expiration dates, and reduce waste.

**Live Demo & Installation Guide**
A complete video demonstration, including a step-by-step installation guide, is available on YouTube.

- [Click here to watch the YouTube Tutorial](https://youtu.be/htYl92kBPfg)

- [Click here to watch the YouTube Demo](https://youtu.be/vZ0mJm85vvQ)

**Full Documentation**
For a detailed technical breakdown of the project's architecture, features, and code, please see the full documentation.

[Click here to view the Project Documentation](https://docs.google.com/document/d/1Nq1Xgx2npbi6bhEQofvrDeZHvifQHePCPfij4twRYco/edit?usp=sharing)

## Key Features
- **Secure User Account Management:** Full CRUD (Create, Read, Update, Delete) operations for user accounts, complete with a password security check for sensitive actions.
- **Complete Food Inventory Management:** Full CRUD operations for food items, allowing users to add, view, edit, and delete their stored goods.
- **Intelligent Item Coding:** Items are automatically assigned a unique code based on storage location (D-#### for Dry, F-#### for Freezer), which updates dynamically if the location is changed.
- **At-a-Glance Dashboard:** The application opens to a dashboard that immediately highlights items that have already expired or are expiring within the next 7 days.
- **Real-time Search:** A live search bar on the "View All Items" page allows users to instantly filter their entire inventory by name.
- **Automated Email Summaries:** Upon exiting, users receive an automated email summarizing the status of their expiring and expired items.
- **Professional Installer:** The application is distributed via a ClickOnce installer (setup.exe) that handles all dependencies.

## How to Install and Run

1- **Download the Installer:**
- Go to the [Releases Page](https://github.com/vrsp05/Food-Storage-Management-System-GUI/releases) of this repository.
- Under the latest release (e.g., v1.0.0), download the .zip file (e.g., FSMS_installation_files.zip).

2- **Unzip the Package:**

- Locate the downloaded .zip file on your computer and unzip it to a new folder.

3- **Run the Installer:**

- Open the new folder and double-click the setup.exe file.
- Follow the on-screen instructions. If prompted by Windows Security, click "More info" and then "Run anyway."

4- **Launch the Application:**

- Once installed, the program will start, make sure to pin to taskbar or search by “FSMS_GUI” in windows search. Use this to launch the Food Storage Management System.

(For a visual guide, please watch the [YouTube tutorial](https://www.youtube.com/watch?v=htYl92kBPfg&list=PLxGNy8Ee35PZuxNzxrsd8_MUwKPNkbZVM&index=2).)

## Technologies Used

- **Language:** C#

- **Framework:** .NET 8

- **User Interface:** Windows Forms (WinForms)

## My Learning Journey

This project was a comprehensive exercise in migrating a fully functional console application to a modern, event-driven GUI application using Windows Forms. It strengthened my skills in Object-Oriented Programming, robust file handling for data persistence, and designing an intuitive, user-friendly interface. A key challenge and learning experience was refactoring the original console-based logic into decoupled services (Validation_Service.cs) that could be reused effectively within the GUI's event-based architecture. Implementing features like the security check and the ClickOnce installer provided valuable insight into building secure, distributable, real-world applications.

# Testing pull request
