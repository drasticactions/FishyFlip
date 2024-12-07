ROOT=$(PWD)
BSKYCLI_ROOT=$(ROOT)/apps/bskycli
BSKYCLI_PROJECT=$(BSKYCLI_ROOT)/bskycli.csproj
BUILD_TYPE=Release
ARTIFACTS_DIR=$(ROOT)/artifacts

bskycli_linux:
	rm -rf $(ARTIFACTS_DIR)/linux-x64
	dotnet build $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r linux-x64
	dotnet publish $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r linux-x64 -o $(ARTIFACTS_DIR)/linux-x64

bskycli_macos_x64:
	rm -rf $(ARTIFACTS_DIR)/osx-x64
	dotnet build $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r osx-x64
	dotnet publish $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r osx-x64 -o $(ARTIFACTS_DIR)/osx-x64

bskycli_macos_arm64:
	rm -rf $(ARTIFACTS_DIR)/osx-arm64
	dotnet build $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r osx-arm64
	dotnet publish $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r osx-arm64 -o $(ARTIFACTS_DIR)/osx-arm64

bskycli_macos: bskycli_macos_x64 bskycli_macos_arm64

clean:
	rm -rf $(ARTIFACTS_DIR)