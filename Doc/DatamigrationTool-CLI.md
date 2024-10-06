
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

## Step 3: Create Schema File for Data Migration

To export and import data, you need to create a schema file that defines the structure of your entities and fields.

1. **Create a New Schema File**
   Run the following command to create a schema file for your environment:
   ```bash
   pac data schema init --output <schema file path>
   ```
   Example:
   ```bash
   pac data schema init --output schema.xml
   ```

2. **Add Entities to the Schema**
   Use the `pac data schema add-entity` command to include the entities you want to export. For example:
   ```bash
   pac data schema add-entity --name <entity name> --schemaFile <schema file path>
   ```
   Example:
   ```bash
   pac data schema add-entity --name account --schemaFile schema.xml
   ```

3. **Add Fields to the Entities**
   You can specify which fields within an entity you want to export:
   ```bash
   pac data schema add-field --name <field name> --entity <entity name> --schemaFile <schema file path>
   ```
   Example:
   ```bash
   pac data schema add-field --name name --entity account --schemaFile schema.xml
   ```

4. **Review and Modify the Schema**
   Open the `schema.xml` file in a text editor to review and make any additional modifications manually. Ensure all the necessary entities and fields are correctly included.

5. **Save the Schema File**
   Once you've created the schema file and added the required entities and fields, save it in your working directory.

## Step 4: Export Data from Source Environment
If you're migrating from an existing Power Platform environment:

1. Run the following command to export the data:
   ```bash
   pac data export --schemaFile <schema file path> --dataFile <export file path>
   ```

- The schema file defines what data is exported (e.g., entities and fields).
- The export file is where your data will be stored.

## Step 5: Prepare the Destination Environment
Make sure that the destination environment is ready for the data import.

1. Set up the required solutions and entities in the target environment.
2. Ensure the environment’s data structure matches your source data.

## Step 6: Import Data to the Destination Environment
Now, you can import the exported data into the target environment.

1. Run the following command:
   ```bash
   pac data import --dataFile <export file path>
   ```

2. This command imports the data from the source file into the destination environment.

## Step 7: Verify Data Migration
Once the import process is complete, verify that the data was successfully migrated:

1. Log in to the target Power Platform environment.
2. Check the relevant entities or tables to ensure data accuracy.

   You can also use Power BI or the Dynamics 365 interface to visualize and validate the migrated data.

## Step 8: Resolve Any Errors
If any issues arise during the migration process, Power Platform CLI will provide error logs. Address these as needed:

1. Review the log for error details.
2. Correct the issues in your source data or target environment.
3. Re-run the import process if necessary.

## Step 9: Cleanup
After a successful migration, you can remove temporary files or unnecessary backups.
```bash
rm <export file path>
```

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

This script can be run periodically to manage data migration more efficiently.