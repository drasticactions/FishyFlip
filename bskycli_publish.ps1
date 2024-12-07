param(
    [Parameter()]
    [ValidateSet('clean', 'gitversion', 'bskycli_windows', 'all')]
    [string]$Target = 'all'
)

# Configuration
$ROOT = $PSScriptRoot
$BSKYCLI_ROOT = Join-Path $ROOT "apps\bskycli"
$BSKYCLI_PROJECT = Join-Path $BSKYCLI_ROOT "bskycli.csproj"
$BUILD_TYPE = "Release"
$ARTIFACTS_DIR = Join-Path $ROOT "artifacts"

# Helper function to create directory if it doesn't exist
function EnsureDirectory {
    param([string]$path)
    if (-not (Test-Path $path)) {
        New-Item -ItemType Directory -Path $path -Force | Out-Null
    }
}

# Helper function to remove directory if it exists
function RemoveDirectory {
    param([string]$path)
    if (Test-Path $path) {
        Remove-Item -Path $path -Recurse -Force
    }
}

# Install GitVersion.Tool
function Install-GitVersion {
    Write-Host "Installing GitVersion.Tool..."
    dotnet tool install --global GitVersion.Tool
    dotnet-gitversion /output buildserver
}

# Build for Windows (x64)
function Build-WindowsX64 {
    Write-Host "Building for Windows x64..."
    
    # Clean previous build
    RemoveDirectory "$ARTIFACTS_DIR\win-x64"
    
    # Build and publish
    dotnet build $BSKYCLI_PROJECT -c $BUILD_TYPE -r win-x64
    dotnet publish $BSKYCLI_PROJECT -c $BUILD_TYPE -r win-x64 -o "$ARTIFACTS_DIR\win-x64"
}

# Clean build artifacts
function Clean-Build {
    Write-Host "Cleaning build artifacts..."
    RemoveDirectory $OUTPUT_DIR
    RemoveDirectory $ARTIFACTS_DIR
}

# Main execution
switch ($Target) {
    'clean' {
        Clean-Build
    }
    'gitversion' {
        Install-GitVersion
    }
    'bskycli_windows' {
        Build-WindowsX64
    }
    'all' {
        Build-WindowsX64
    }
    default {
        Write-Host "Invalid target specified"
        exit 1
    }
}