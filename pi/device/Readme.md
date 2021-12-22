# Contents
pitft_setup.py	-> Enable Display

## Init
If you ever need to reinstall the MvM-Box (dont!):
create a folder '/home/pi/mvm'
chdir into that folder
enter the following lines to retrieve the most current version from github:

```
git init
git remote add pistuff https://github.com/OleAlbers/MvM
git fetch  pistuff
git checkout pistuff/master pi
```