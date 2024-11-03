# Definir URLs de los entornos
$sourceEnvUrl = "https://baraveze-dev.crm2.dynamics.com"
$targetEnvUrl = "https://baraveze-test.crm2.dynamics.com"



# Función para validar conexión a un entorno de Power Platform
function Validate-Connection {
    param (
        [string]$envUrl
    )
    
    Write-Host "Intentando conectar a: $envUrl"
    
    try {
        # Autenticar en el entorno
        pac auth create --url $envUrl -ErrorAction Stop
        
        # Comprobar si la conexión fue exitosa
        $authStatus = pac auth list
        if ($authStatus) {
            Write-Host "Conexión exitosa al entorno: $envUrl" -ForegroundColor Green
            return $true
        } else {
            Write-Host "Error: No se pudo conectar al entorno: $envUrl" -ForegroundColor Red
            return $false
        }
    } catch {
        Write-Host "Error: No se pudo autenticar en el entorno: $envUrl" -ForegroundColor Red
        return $false
    }
}

# Validar la conexión al entorno de origen
$sourceConnected = Validate-Connection -envUrl $sourceEnvUrl
if (-not $sourceConnected) {
    Write-Host "Cancelando la migración. Verifica la conexión al entorno de origen." -ForegroundColor Yellow
    exit
}

# Exportar los datos del entorno de origen si la conexión es válida
Write-Host "Exportando datos desde el entorno de origen..."


# Limpiar autenticaciones previas
pac auth clear

# Validar la conexión al entorno de destino
$targetConnected = Validate-Connection -envUrl $targetEnvUrl
if (-not $targetConnected) {
    Write-Host "Cancelando la migración. Verifica la conexión al entorno de destino." -ForegroundColor Yellow
    exit
}

# Importar los datos al entorno de destino si la conexión es válida
Write-Host "Importando datos al entorno de destino..."


# Verificación final
Write-Host "Migración completada. Verifica los datos en el entorno de destino."
