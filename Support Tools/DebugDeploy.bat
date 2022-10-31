@ECHO ON
@CALL "%VS80COMNTOOLS%vsvars32.bat"
@gacutil -i %1
@copy %2\*.* "C:\Program Files\Microsoft BizTalk Server 2006\Developer Tools\Mapper Extensions"
@copy %2\*.* "C:\Program Files\Microsoft BizTalk Server 2006\Pipeline Components"
@copy %2\*.pdb "C:\WINDOWS\assembly\GAC_MSIL\ContextAccessor\1.0.0.0__8c24991755142725\"
