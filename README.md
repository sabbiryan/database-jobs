# SQL Server Maintenance

### DbBackup Scheduler
Step 1: Clone this repo using <code>git clone https://github.com/sabbiryan/SQL-Server-Maintenance-Schedulers.git</code> from your terminal or cmd.<br/>
Step 2: Open <code>Source/DbBackupScheduler/DbBackupScheduler.sln</code> project with visual studio. <br/>
Step 3: Open <code>Program.cs</code> file. <br/>
Step 4: Modify this line with your server name with this. <code>Server myServer = ConnectToServer(@".\SQLEXPRESS");</code>  <br/>
Step 5: Define your databse name here. <code>Database database = new Database { Name = "MyDb" };</code>  <br/>
Step 6: Define your directory by modifying this line of code. <code>string directory = CheckDirectory(@"C:\BackupDb\");</code>  <br/>
<br/>
All are set now. Build and run the program.


### Rebuild Index
Rebuild all index of a database in SQL Server <br/>
Command: Exec sp_msforeachtable 'SET QUOTED_IDENTIFIER ON; ALTER INDEX ALL ON ? REBUILD'

### Reorganize Index
Reorganize all index of a database in SQL Server <br/>
Command: Exec sp_msforeachtable 'ALTER INDEX ALL ON ? Reorganize'


