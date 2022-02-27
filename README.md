#### Features
1. Automatic DB Backup Job
2. Automatically create a zip copy of backup file
3. Automatically push the zip backup file to Azure Storage / AWS S3 Storage (upcoming)
4. Automatic Index Maintenance

#### How to user it
 * Register as windows task scheduler

#### Getting Start
* Clone this repository<br/>
* Open the solution in Visual Studio (2019 recommended). <br/>
* Open <code>DatabaseJobs.Client/App.config</code> file. <br/>
* Check the following appSettings key value and change as your need  <br/>
```
 <add key="ServerName" value=".\SQLEXPRESS" />    <!--database server name-->
 <add key="BackupAllDatabases" value="true" />   <!--backup all database of the provided server except system databases-->
 <add key="BackupDatabases" value="AuditorDb,UrlShortenDb" />    <!--existing database names of the provided server-->
 <add key="UseRootBackupDirectory" value="false" />    <!--backup will be stored on the application hosted base directory-->
 <add key="BackupDirectoryPath" value="C:\temp\backups\" />   <!--define a specific backup location. it will activate when UseRootBackupDirectory is false-->
 <add key="RemoveBackupAfterXDays" value="5" />   <!--remove older backup after n days, empty for disable this rule--> 
 <add key="RemoveBakFileAfterZip" value="true" /> <!--remove .bak file after zip completion-->
 <add key="PushToAzureStorage" value="true" />   <!--true for push the backup zip file to your azure storage blob--> 
```
* Configur your azure storage connection string (`only if you make PushToAzureStorage is true`)  `<add name="AzureStroage" connectionString="DefaultEndpointsProtocol=https;AccountName=yourAzureStorageAccountName;AccountKey=yourAzureStroageAccountKey;EndpointSuffix=core.windows.net" /> `
* All are set now. Build and run the program. <br/>
* Publish the client project and up it on your virtual machine and register as a windows task scheduler


#### Rebuild Index
Rebuild all index of a database in SQL Server <br/>
Command: `Exec sp_msforeachtable 'SET QUOTED_IDENTIFIER ON; ALTER INDEX ALL ON ? REBUILD'`

#### Reorganize Index
Reorganize all index of a database in SQL Server <br/>
Command: `Exec sp_msforeachtable 'ALTER INDEX ALL ON ? Reorganize'`

#### Delete Old IIS Logs by Script
```
sLogFolder = "c:\inetpub\logs\LogFiles"
iMaxAge = 30   'in days
Set objFSO = CreateObject("Scripting.FileSystemObject")
set colFolder = objFSO.GetFolder(sLogFolder)
For Each colSubfolder in colFolder.SubFolders
        Set objFolder = objFSO.GetFolder(colSubfolder.Path)
        Set colFiles = objFolder.Files
        For Each objFile in colFiles
                iFileAge = now-objFile.DateCreated
                if iFileAge > (iMaxAge+1)  then
                        objFSO.deletefile objFile, True
                end if
        Next
Next
```
* Save the file into C:\inetpub\logs\_Deletion.vbs
* Create a task scheduler using this script
* Follow https://docs.microsoft.com/en-us/iis/manage/provisioning-and-managing-iis/managing-iis-log-file-storage#03

#### Azure VM Maintenance
Azure vm size scaling up and down using runbooks and monitoring https://www.petri.com/automatically-resize-azure-vm


