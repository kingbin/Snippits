// Collection of useful Extensions I've used in my projects

public static class MyExtensions
{
  public static int WordCount( this String str )
  {
    return str.Split( new char[] { ' ', '.', '?' },
             StringSplitOptions.RemoveEmptyEntries ).Length;
  }

  
  public static string Slice(this string source, int start, int end)
  {
    if (end < 0) // Keep this for negative end support
    {
        end = source.Length + end;
    }
    int len = end - start;               // Calculate length
    return source.Substring(start, len); // Return Substring of length
    }


  public static int? ToNullableInt32( this String s )
  {
    int i;
    if( Int32.TryParse( s, out i ) ) return i;
    return null;
  }
} 
