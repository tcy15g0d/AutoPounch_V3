@echo off
setlocal

rem 設定Bot監聽參數(0:Bot監聽)
set parameterValue=0

rem 設置目錄路徑
set scriptdir=%~dp0

rem 執行.exe並且傳遞Bot監聽參數
start "" "%scriptdir%\AutoPounch_V3.exe" %parameterValue%

endlocal
exit