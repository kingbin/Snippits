//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;

// http://www.pawlowski.cz/2010/09/sql-server-2005-and-sql-server-2008-regular-expressions-and-pattern-matching-2/

public class SQLRegEx
{
    private class RegExRow
    {
        /// <summary>
        /// Private class for passing matches of the RegExMatches to the FillRow method
        /// </summary>
        /// <param name=”rowId”>ID of the Row</param>
        /// <param name=”matchId”>ID of the Match</param>
        /// <param name=”groupID”>ID of the Group within the Match</param>
        /// <param name=”value”>Value of the particular group</param>
        public RegExRow(int rowId, int matchId, int groupID, string value)
        {
            RowId = rowId;
            MatchId = matchId;
            GroupID = groupID;
            Value = value;
        }

        public int RowId;
        public int MatchId;
        public int GroupID;
        public string Value;
    }

    /// <summary>
    /// Applies Regular Expression on the Source string and returns value of particular group from withing a specified match
    /// </summary>
    /// <param name=”sourceString”>Source string on which the regular expression should be applied</param>
    /// <param name=”pattern”>Regular Expression pattern</param>
    /// <param name=”matchId”>ID of the Match to be returned 1 inex-based</param>
    /// <param name=”groupId”>ID of the group from within a match to return. GroupID 0 returns complete match</param>
    /// <returns>Value of the Group from within a Match</returns>
    [SqlFunction(IsDeterministic=true)]
    public static SqlChars RegExMatch(string sourceString, string pattern, int matchId, int groupId)
    {
        Match m = null;
        Regex r = new Regex(pattern, RegexOptions.Compiled);

        if (matchId == 1)
        {
            m = r.Match(sourceString);
        }
        else if (matchId > 1)
        {
            MatchCollection mc = r.Matches(sourceString);
            m = mc != null && mc.Count >= matchId ? mc[matchId - 1] : null;
        }

        return m != null && m.Groups.Count > groupId ? new SqlChars(m.Groups[groupId].Value) : SqlChars.Null;
    }

  /// <summary>
  /// Applies Regular Expression o the Source strings and return all matches and groups
  /// </summary>
  /// <param name=”sourceString”>Source string on which the regular expression should be applied</param>
  /// <param name=”pattern”>Regular Expression pattern</param>
  /// <returns>Returns list of RegExRows representing the group value</returns>
  [SqlFunction( FillRowMethodName = "FillRegExRow", TableDefinition = "rowID int, matchID int, groupID int, value nvarchar(30)" )]
  public static IEnumerable RegExMatches( string sourceString, string pattern )
  {
    Regex r = new Regex( pattern, RegexOptions.Compiled );
    int rowId = 0;
    int matchId = 0;
    foreach( Match m in r.Matches( sourceString ) ) {
      matchId++;
      for( int i = 0; i < m.Groups.Count; i++ ) {
        yield return new RegExRow( ++rowId, matchId, i, m.Groups[i].Value );
      }
    }
  }

  /// <summary>
  /// FillRow method to populate the output table
  /// </summary>
  /// <param name=”obj”>RegExRow passed as object</param>
  /// <param name=”rowId”>ID or the returned row</param>
  /// <param name=”matchId”>ID of returned Match</param>
  /// <param name=”groupID”>ID of group in the Match</param>
  /// <param name=”value”>Value of the Group</param>
  public static void FillRegExRow( Object obj, out int rowId, out int matchId, out int groupID, out SqlChars value )
  {
    RegExRow r = (RegExRow)obj;
    rowId = r.RowId;
    matchId = r.MatchId;
    groupID = r.GroupID;
    value = new SqlChars( r.Value );
  }
}
