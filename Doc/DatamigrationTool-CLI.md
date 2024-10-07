
# Power Platform CLI Data Migration Guide

## Step 1: Install Power Platform CLI
First, ensure that Power Platform CLI is installed on your machine.

1. Download the installer from [Microsoft](https://aka.ms/PowerPlatformCli).
2. Follow the installation instructions for your operating system.

To confirm installation, run the following command in your terminal:
```bash
pac --version
```

## Step 2: Authenticate to Power Platform Environment
You need to log in to your Power Platform environment.

1. Open your terminal.
2. Run the following command:
```bash
pac auth create --url https://<your-env-name>.crm.dynamics.com
```
Replace `<your-env-name>` with your environment's URL.

3. A browser window will open for you to authenticate using your credentials.

If you have already created the connection previously, run following command:
```bash
pac env list
```
this command will list you all the environments you have tied to your connection.

Then run this command to pick one of the environments of that list:
```bash
pac env select -env <environment name>
```
<environment name> could be this informantion from the environment list (ID, url, unique name, or partial name)

## Step 3: Create Schema File for Data Migration

To export and import data, you need to create a schema file that defines the structure of your entities and fields. This is the only step you need to do using Data Migration Configuration Tool.

1. **Open Configuration Data Migration Tool**
   Run the following command to create a schema file for your environment:
   ```bash
   pac tool cmt
   ```

2. **Select create schema**
   
   [Select Create Schema](Doc/DataMigrationTool/1-CreateSchema.png)

3. **Connect to your source environment**

You will be asked to connect to your source environment to create schema file.

3. **Add the entity you want to migrate**
   
   The tool let you add more than one entity per file but I recommend to create one schema file per entity. Add the full entity if you want to migrate all fields for that entity or you can select which fields you want to migrate. 
   If you want to keep same guid along the environments you have to add the uniqueidentifier of the table in the schema. Also, by defualt the schema will consider to export all records. But you can add a filter, using Fetch XML, to only export a sub set of data (Active records, for instance).

4. **Save and export schema file**
   Once you finish setting schema entity, you have to click on "Save and Export" button. Choose an appropriate name for it, a good name could be [table name] + Schema.xml. For example: accountSchema.xml. Save the file in a place you can search it easily. My suggestion would be place it in the same directory where you will run the CLI.

   After that step, the tool will ask you if you want to export data since you've created a schema file. Click on "No" because you are gonna use CLI method to import data.

## Step 4: Export Data from Source Environment

1. Run the following command to export the data:
   ```bash
   pac data export --schemaFile <schema file path> --dataFile <export file path>
   ```

- The schema file defines what data is exported (e.g., entities and fields).
- The export file is where your data will be stored.

**Extensions:**
- Schema file should have .xml extension
- Data file should have .zip extension

For example:
```bash
   pac data export --schemaFile accountSchema.xml --dataFile accountData.zip
   ```

## Step 5: Prepare the Destination Environment
Make sure that the destination environment is ready for the data import. Also, check if you see the target environment in the environmen list. To check that, run this command:
```bash
   pac env list
   ```
Select your target environment running this command:
```bash
pac env select -env <environment name>
```

## Step 6: Import Data to the Destination Environment
Now, you can import the exported data into the target environment. This command imports the data from the source file into the destination environment.

1. Run the following command:
   ```bash
   pac data import --data <data.zip file path> [--verbose true / false]
   ```
   For example:
   ```bash
   pac data import --data accountData.zip
   ```
   verbose is an optional parameter that can add more output information during data import.


## Step 7: Verify Data Migration
Once the import process is complete, verify that the data was successfully migrated:

## Optional: Automating with Scripts
If you plan to migrate data regularly, consider creating a script to automate the export/import steps.

Example PowerShell script:
```powershell
# Authenticate
pac auth create --url https://<your-env-name>.crm.dynamics.com

# Export Data
pac data export --schemaFile "schema.xml" --dataFile "data.zip"

# Import Data
pac data import --dataFile "data.zip"
```

This script can be run periodically to manage data migration more efficiently.pac