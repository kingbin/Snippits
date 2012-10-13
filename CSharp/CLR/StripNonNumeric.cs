using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
	/*
	 * SET Framework to 3.5 for 2008r2
 		
		ALTER DATABASE mRegistry
		SET COMPATIBILITY_LEVEL = 100

		sp_configure 'clr enabled', 1
		GO
		RECONFIGURE
		GO
	 * 
	 	SELECT TOP 2000 
	 	  [MedRecNo]
		  , dbo.fn_PadString([MedRecNo], '0',7) AS MRN
		  , dbo.fn_PadString( dbo.StripNonNumeric([MedRecNo]), '0',7) AS FixedMRN
		  , '------------'
		  , *
		FROM [mRegistry].[dbo].[Patient]
		ORDER BY MRN DESC
	 * 
	*/

	[Microsoft.SqlServer.Server.SqlFunction]
	public static SqlString StripNonNumeric( SqlString input )
	{
		Regex regEx = new Regex( @"\D" );
		return regEx.Replace( input.Value, "" );
	}
};