Set objSysInfo = CreateObject("ADSystemInfo")
strComputer = objSysInfo.ComputerName
Set objWMIService = GetObject("winmgmts:" _
    & "{impersonationLevel=impersonate}!\\"_
    & strComputer & "\root\cimv2")
Set colScheduledJobs = objWMIService.ExecQuery _
    ("Select * from Win32_ScheduledJob")
For Each objJob in colScheduledJobs
    Wscript.Echo "Command: " & objJob.Command & VBNewLine _
    & "Days Of Month: " & objJob.DaysOfMonth & VBNewLine _
    & "Days Of Week: " & objJob.DaysOfWeek & VBNewLine _
    & "Description: " & objJob.Description & VBNewLine _
    & "Elapsed Time: " & objJob.ElapsedTime & VBNewLine _
    & "Install Date: " & objJob.InstallDate & VBNewLine _
    & "Interact with Desktop: " _
        & objJob.InteractWithDesktop & VBNewLine _
    & "Job ID: " & objJob.JobId & VBNewLine _
    & "Job Status: " & objJob.JobStatus & VBNewLine _
    & "Name: " & objJob.Name & VBNewLine _
    & "Notify: " & objJob.Notify & VBNewLine _
    & "Owner: " & objJob.Owner & VBNewLine _
    & "Priority: " & objJob.Priority & VBNewLine _
    & "Run Repeatedly: " & objJob.RunRepeatedly & VBNewLine _
    & "Start Time: " & objJob.StartTime & VBNewLine _
    & "Status: " & objJob.Status & VBNewLine _
    & "Time Submitted: " & objJob.TimeSubmitted & VBNewLine _
    & "Until Time: " & objJob.UntilTime
Next