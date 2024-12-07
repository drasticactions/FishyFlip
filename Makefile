ROOT=$(PWD)
BSKYCLI_ROOT=$(ROOT)/apps/bskycli
BSKYCLI_PROJECT=$(BSKYCLI_ROOT)/bskycli.csproj
BUILD_TYPE=Release
ARTIFACTS_DIR=$(ROOT)/artifacts

bskycli_linux:
	dotnet publish $(BSKYCLI_PROJECT) -c $(BUILD_TYPE) -r linux-x64 -o $(ARTIFACTS_DIR)/linux-x64