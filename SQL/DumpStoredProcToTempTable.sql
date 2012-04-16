/*
sp_configure 'Show Advanced Options', 1
GO
RECONFIGURE
GO
sp_configure 'Ad Hoc Distributed Queries', 1
GO
RECONFIGURE
GO
*/

SELECT * INTO #MyTempTable FROM OPENROWSET('SQLNCLI', 'Server=#########;Trusted_Connection=yes;',
  'EXEC DB.[dbo].[sp_Charges] @Clinic = N''###'', @Sent = 0, @Deleted = 0, @Holding = 0')
