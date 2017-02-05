# SQL-Server-Maintenance-Schedulers

<h3>1. DbBackup Scheduler</h3>
Step 1: Open Source/DbBackupScheduler/DbBackupScheduler.sln project with visual studio. <br/>
Step 2: Open Program.cs file. <br/>
Step 3: Modify this line with your server name. Server myServer = ConnectToServer(@".\SQLEXPRESS");  <br/>
Step 4: Define your databse name. Database database = new Database { Name = "MyDb" }; <br/>
Step 5: Define your directory. string directory = CheckDirectory(@"C:\BackupDb\"); <br/>
<br/>
All are set now. Build and run the program.

