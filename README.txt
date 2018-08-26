# SideBattleRPG
A database and a map builder for a turn-based RPG game.


Overview:
This is a database interface system for a side battle RPG game called "Arcadia Carnival". The application serves as an interactive editor
to store and retrieve all information for the game. Information includes components such as maps, sprites, enemies, skills, music, etc.
Once the database is fully developed and complete: a new repository will be created for the game. That repository will only need the
database file that has been modified by this interactive editor.


Required Systems/Programs:
- Windows 10 operating system (At least  the newest 2016 version)
- .NET Framework 4.5.2+ (At least 4.5 might work)
- Visual Studio 2015
- Anything that can run SQLite, such as SQLite Studio

Other Notes:
- The program is in C#, XAML (markup language), and uses SQLite: You can learn these along the way
- The files in the Visual Studio solution is a deviation of the Model-View-Control framework
- Make sure you have at least 10 GB in your storage (Much more than required, but just in case)
- You do not need to use the command line at all


The repository is currently separated in to five different projects.
Part 1 is handled in the SQLite program, and 2-5 are managed in Visual Studio 2015:
1) The database itself
2) Interactive database, with a user interface system
3) Battle simulator
4) Map builder
5) Character creator

Go to the Docmentation section and read "Main" for extensive information on the entire project.