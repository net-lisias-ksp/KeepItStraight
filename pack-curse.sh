#!/usr/bin/env bash

# see http://redsymbol.net/articles/unofficial-bash-strict-mode/
set -euo pipefail
IFS=$'\n\t'
source ./CONFIG.inc

clean() {
	rm -fR $FILE
	if [ ! -d Archive ] ; then
		mkdir Archive
	fi
}

pwd=$(pwd)
FILE=${pwd}/Archive/$PACKAGE-$VERSION${PROJECT_STATE}-CurseForge.zip
echo $FILE
clean
cd GameData

zip -r $FILE ./KeepItStraight/* -x ".*"

mkdir -p $pwd/bin/KeepItStraight
#cat $TARGETDIR/KeepItStraight.cfg | sed 's/curseforge_ready = False/curseforge_ready = True/g' > $pwd/bin/KeepItStraight/KeepItStraight.cfg
pushd $pwd/bin
#zip -u $FILE KeepItStraight/KeepItStraight.cfg
popd

zip -d $FILE __MACOSX "**/.DS_Store" || echo ""

set -e
cd $pwd
