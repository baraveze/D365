# Path to the CSV file
$csvFilePath = ".\DataMigration.csv"

# Set Environments URLs
$sourceEnvUrl = "https://baraveze-dev.crm2.dynamics.com"
$targetEnvUrl = "https://baraveze-test.crm2.dynamics.com"
$userName = "ezequiel.baravalle@baravezed365.onmicrosoft.com"
$pwd = "Stepy2023"

# Import CSV file
$table = Import-Csv $csvFilePath

# Validate if the table is empty
if ($table.Count -eq 0) {
    Write-Host "The table is empty." -ForegroundColor Red
    exit
}

# Create Dataverse Connection
function Validate-Connection {
    param (
        [string]$envUrl
    )
    
    Write-Host "Attempting connection to: $envUrl"
    
    try {
        # Create Auth profile in environment
        pac auth clear
        pac auth create --username $userName --password $pwd --environment $envUrl -ErrorAction Stop
        
        # Comprobar si la conexión fue exitosa
        $authStatus = pac auth list
        if ($authStatus) {
            Write-Host "Connection to: $envUrl successfully" -ForegroundColor Green
            return $true
        } else {
            Write-Host "Error: Connection couldn't be stablished: $envUrl" -ForegroundColor Red
            return $false
        }
    } catch {
        Write-Host "Error: Connection couldn't be stablished: $envUrl" -ForegroundColor Red
        return $false
    }
}

# Step 1 validate source environment Connection
$sourceConnected = Validate-Connection -envUrl $sourceEnvUrl
if (-not $sourceConnected) {
    Write-Host "Cancelling Job. Connection to source environment couldn't be stablished." -ForegroundColor Yellow
    exit
}

Write-Host "Reading CSV file to start exporting data from source environment"

# Validate if all columns have values in each row
 foreach ($row in $table) {
    if ( 
        [string]::IsNullOrEmpty($row.FolderPath) -or 
        [string]::IsNullOrEmpty($row.TableName) -or
        [string]::IsNullOrEmpty($row.AddToImport) -or  
        [string]::IsNullOrEmpty($row.FilePath)
        ) {
        Write-Host "Error: One or more columns have null or empty values."
        exit
    }
}

# Iterate through each row and analyze which entities should be migrated
foreach ($row in $table) {
    $OrderID = $row.OrderID
    $TableName = $row.TableName
    $AddToImport = $row.AddToImport
    $FolderPath = $row.FolderPath
    $FilePath = $row.FilePath
    $LastSuccessfulRun = $row.LastSuccessfulRun

    
    Write-Host "Processing information"
    
    if($AddToImport -eq "Yes"){
    
        Write-Host "Exporting data taking schema file"

        $FileName = -join ($TableName,".zip")
        $FullPath = -join ($FolderPath,$FileName)
        
        try{
            pac data export --schemaFile $FilePath --dataFile $FullPath

            Write-Host "Data for: $TableName was successfully exported" -ForegroundColor Green

        } catch{
            
            Write-Host "There was an issue trying to export data for $TableName table." -ForegroundColor Red
        }
        

        # Add a new column `lastRun` with the current timestamp
        $row | Add-Member -MemberType NoteProperty -Name "LastSuccessfulRun" -Value (Get-Date) -Force true

        # Display row with the new timestamp
        Write-Host "OrderID: $OrderID, FolderPath: $FolderPath, FilePath: $FilePath, LastSuccessfulRun: $($row.LastSuccessfulRun)"

        Write-Host "Updating Row"
        # Export the updated table with the new `lastRun` column to a CSV file
        $table | Export-Csv $csvFilePath -NoTypeInformation

        Write-Host "Row updated"

    } 

    
}
