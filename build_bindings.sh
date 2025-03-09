#!/bin/bash

# This script is used to build the source generated bindings for FishyFlip
# It includes a list of git repos to be cloned, building FFSourceGen, and passing the directories into it
# to generate the bindings

# List of git repos to clone
REPOS=(
    "https://github.com/bluesky-social/atproto.git"
    "https://github.com/whtwnd/whitewind-blog.git"
    "https://github.com/ziodotsh/lexicons.git"
    "https://github.com/shinolabs/PinkSea.git"
    "https://github.com/mkizka/linkat.git"
    "https://github.com/likeandscribe/frontpage.git"
    "https://github.com/psky-atp/client.git"
    "https://github.com/Gregoor/skylights.git"
    "https://github.com/echo8/pastesphere.git"
    "https://github.com/marukun712/AniBlue.git"
    "https://github.com/lexicon-community/lexicon.git"
    "https://github.com/icidasset/radical-edward.git"
    "https://github.com/nperez0111/bookhive.git"
    "https://github.com/teal-fm/teal.git"
    "https://github.com/marukun712/stellar.git"
    "https://github.com/aendra-rininsland/bluemoji.git"
)

PWD=$(pwd)

# Clone the repos
for REPO in "${REPOS[@]}"
do
    user_repo=$(echo "$REPO" | grep -o '[^/]*/[^/]*\.git$' | sed 's/\.git$//')
    username=$(echo "$user_repo" | cut -d'/' -f1)
    repo_name=$(echo "$user_repo" | cut -d'/' -f2)
    target_dir="${username}-${repo_name}"
    if [ -d "../fflexicons/$target_dir" ]; then
        echo "Updating $target_dir"
        cd "../fflexicons/$target_dir"
        git pull
        cd -
    else
        echo "Cloning $target_dir"
        git clone "$REPO" "../fflexicons/$target_dir"
    fi
done

# Create list of cloned repo directories from the above list except for atproto
    
REPO_DIRS=()
for REPO in "${REPOS[@]}"
do
    user_repo=$(echo "$REPO" | grep -o '[^/]*/[^/]*\.git$' | sed 's/\.git$//')
    username=$(echo "$user_repo" | cut -d'/' -f1)
    repo_name=$(echo "$user_repo" | cut -d'/' -f2)
    target_dir="${username}-${repo_name}"
    if [ "$target_dir" != "bluesky-social-atproto" ]; then
        if [ "$target_dir" == "whtwnd-whitewind-blog" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/lexicons/com/whtwnd")
        elif [ "$target_dir" == "aendra-rininsland-bluemoji" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/schema/blue.moji")
        elif [ "$target_dir" == "marukun712-stellar" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/lexicons/stellar")
        elif [ "$target_dir" == "ziodotsh-lexicons" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/blue/zio")
        elif [ "$target_dir" == "shinolabs-PinkSea" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/PinkSea.Lexicons/com")
        elif [ "$target_dir" == "likeandscribe-frontpage" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/lexicons/fyi")
        elif [ "$target_dir" == "Gregoor-skylights" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/lexicons")
        elif [ "$target_dir" == "lexicon-community-lexicon" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/community/lexicon")
        elif [ "$target_dir" == "teal-fm-teal" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/packages/lexicons/real")
        elif [ "$target_dir" == "icidasset-radical-edward" ]; then
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/apps/byov/lexicons/ma/tokono/byov")
        else
            REPO_DIRS+=("$PWD/../fflexicons/$target_dir/lexicons")
        fi
    fi
done
    
# Build FFSourceGen
dotnet build tools/FFSourceGen/FFSourceGen.csproj
dotnet run --project tools/FFSourceGen/FFSourceGen.csproj -- generate $PWD/../fflexicons/bluesky-social-atproto/lexicons -o $PWD/src/FishyFlip/ -t "${REPO_DIRS[@]}"

if [ $? -ne 0 ]; then
    echo "FFSourceGen failed to generate bindings"
    exit 1
fi

# Build FishyFlip to verify the bindings compile

dotnet build src/FishyFlip/FishyFlip.csproj -c Release