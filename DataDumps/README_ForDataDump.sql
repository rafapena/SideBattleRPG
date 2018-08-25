Notes on the Data Dump:
Document by Raf
August 22, 2018


In order to add the database tables to your own SQLiteStudio local workspace and retrieve the data from
Visual Studio, follow the steps below:


1. Open SQLite Studio (or your program managing the database): Create a new database and connect to it.

2. Copy and paste everything from "TablesAug2018.sql" onto your SQLite editor.

3. Highlight everything (CTRL+A) then run the code (F5)

4. If running gives an error (i.e. your database isn't populated with tables),
then try to fix it somehow or refer to the next step. If running does work, then go to step 6.

5. If fixing it doesn't work, then contact me and I'll give you a copy of the db file.
You won't be needing the new database you just created. You can either delete it or use it as a test/backup database.

6. If you're having security issues with writing on the database:
place the db file in "C:\Users\<YourUserName>".

7. Go to SideBattleRPG/Database/Utilities/SQLDB.cs (In your cloned/forked repository)

8. On line 50, change "C:\Users\User\" to the directory your database resides in,
then hit CTRL+S to save.


Please note that I don't know how to properly do version controlling in sqlite. I suggest exporting your current
version of the database, then sending it to the DataDump folder, for now.

