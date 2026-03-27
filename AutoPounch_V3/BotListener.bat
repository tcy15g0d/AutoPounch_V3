@echo off
setlocal
set parameterValue=0
set scriptdir=%~dp0
start "" "%scriptdir%AutoPounch_V3.exe" %parameterValue%
endlocal
exit