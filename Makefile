ROOT=$(PWD)
BSKYCLI_ROOT=$(ROOT)/apps/bskycli
BSKYCLI_PROJECT=$(BSKYCLI_ROOT)/bskycli.csproj
BUILD_TYPE=Release
OUTPUT_DIR=$(ROOT)/output
ARTIFACTS_DIR=$(ROOT)/artifacts

gitversion:
	dotnet tool install --global GitVersion.Tool

bskycli_linux:
	rm -rf $(OUTPUT_DIR)/linux-x64
	rm -rf $(ARTIFACTS_DIR)/linux-x64
	dotnet build $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r linux-x64
	dotnet publish $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r linux-x64 -o $(OUTPUT_DIR)/linux-x64
	@echo "Creating zip file"
	@mkdir -p $(ARTIFACTS_DIR)/linux-x64
	@zip -r $(ARTIFACTS_DIR)/linux-x64/bskycli-linux-x64-$(shell dotnet-gitversion /showvariable AssemblySemFileVer).zip $(OUTPUT_DIR)/linux-x64

bskycli_macos_x64:
	rm -rf $(OUTPUT_DIR)/osx-x64
	rm -rf $(ARTIFACTS_DIR)/osx-x64
	dotnet build $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r osx-x64
	dotnet publish $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r osx-x64 -o $(OUTPUT_DIR)/osx-x64
	@echo "Creating zip file"
	@mkdir -p $(ARTIFACTS_DIR)/osx-x64
	@zip -r $(ARTIFACTS_DIR)/osx-x64/bskycli-osx-x64-$(shell dotnet-gitversion /showvariable AssemblySemFileVer).zip $(OUTPUT_DIR)/osx-x64

bskycli_macos_arm64:
	rm -rf $(OUTPUT_DIR)/osx-arm64
	rm -rf $(ARTIFACTS_DIR)/osx-arm64
	dotnet build $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r osx-arm64
	dotnet publish $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r osx-arm64 -o $(OUTPUT_DIR)/osx-arm64
	@echo "Creating zip file"
	@mkdir -p $(ARTIFACTS_DIR)/osx-arm64
	@zip -r $(ARTIFACTS_DIR)/osx-arm64/bskycli-osx-arm64-$(shell dotnet-gitversion /showvariable AssemblySemFileVer).zip $(OUTPUT_DIR)/osx-arm64


bskycli_macos: bskycli_macos_x64 bskycli_macos_arm64

clean:
	rm -rf $(OUTPUT_DIR)
	rm -rf $(ARTIFACTS_DIR)