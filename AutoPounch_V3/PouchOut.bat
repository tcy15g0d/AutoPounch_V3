@echo off
setlocal

rem 設定簽到退參數(1:簽到、2:簽退)
set parameterValue=2

rem 設置目錄路徑
set scriptdir=%~dp0

rem 執行.exe並且傳遞簽到退參數
start "" "%scriptdir%\AutoPounch_V3.exe" %parameterValue%

endlocal
exit