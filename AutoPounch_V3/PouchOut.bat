@echo off
setlocal
set parameterValue=2
set scriptdir=%~dp0
start "" "%scriptdir%AutoPounch_V3.exe" %parameterValue%
endlocal
exit