REM
REM Build script by using VS2017 DevEnv.exe 
REM

CD %~dp0\Ben.Demo.Purple.Robot
SET devenvCmd="C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\devenv.exe"
%devenvCmd% Ben.Demo.Purple.Robot.sln /Build "Release"

PAUSE