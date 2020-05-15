#### Features
1. Automatic DB Backup Job
2. Automatically push backup DB to Azure Storage / AWS S3 Storage
3. Automatic Index Maintenance

#### How to user it
 * Register as windows task scheduler

#### Getting Start
1. Clone this repository<br/>
2. Open the solution in Visual Studio (2019 recommended). <br/>
3. Open <code>DbBackup.Client/App.config</code> file. <br/>
4. Check the following appSettings key value and change as your need  <br/>
```
 <add key="ServerName" value=".\SQLEXPRESS" />    <!--database server name-->
 <add key="BackupDatabases" value="AuditorDb,UrlShortenDb" />    <!--existing database names of the provided server-->
 <add key="UseRootBackupDirectory" value="false" />    <!--backup will be stored on the application hosted base directory-->
 <add key="BackupDirectoryPath" value="C:\temp\backups\" />   <!--define a specific backup location. it will activate when UseRootBackupDirectory is false-->
 <add key="RemoveBackupAfterXDays" value="5" />   <!--remove older backup after n days, empty for disable this rule--> 
```
<br/>
5. All are set now. Build and run the program. <br/>
6. Publish the client project and up it on your virtual machine and register as a windows task scheduler


#### Rebuild Index
Rebuild all index of a database in SQL Server <br/>
Command: Exec sp_msforeachtable 'SET QUOTED_IDENTIFIER ON; ALTER INDEX ALL ON ? REBUILD'

#### Reorganize Index
Reorganize all index of a database in SQL Server <br/>
Command: Exec sp_msforeachtable 'ALTER INDEX ALL ON ? Reorganize'

#### Azure VM Maintenance
Azure vm size scaling up and down using runbooks and monitoring https://www.petri.com/automatically-resize-azure-vm


