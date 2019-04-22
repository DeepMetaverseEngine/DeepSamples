@SET SolutionDir=%1
@SET ProjectDir=%2
@SET TargetDir=%3
@xcopy /I/E/Y "%TargetDir%*.dll"  "%ProjectDir%..\..\SampleEditors\GameEditor\bin"
@xcopy /I/E/Y "%TargetDir%*.pdb"  "%ProjectDir%..\..\SampleEditors\GameEditor\bin"
@xcopy /I/E/Y "%TargetDir%*.exe"  "%ProjectDir%..\..\SampleEditors\GameEditor\bin"