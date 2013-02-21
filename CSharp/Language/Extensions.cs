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


  // WinForm BackgroundProcess Safely Update Main Thread Control
  // ex: lblStatus.SetPropertyThreadSafe( () => lblStatus.Text, String.Format( "Finished Processing File {0}", FileName ) );
    private delegate void SetPropertyThreadSafeDelegate<TResult>( Control @this, Expression<Func<TResult>> property, TResult value );

    public static void SetPropertyThreadSafe<TResult>( this Control @this, Expression<Func<TResult>> property, TResult value )
    {
      var propertyInfo = ( property.Body as MemberExpression ).Member as PropertyInfo;

      if( propertyInfo == null ||
        !@this.GetType().IsSubclassOf( propertyInfo.ReflectedType ) ||
        @this.GetType().GetProperty( propertyInfo.Name, propertyInfo.PropertyType ) == null ) {
        throw new ArgumentException( "The lambda expression 'property' must reference a valid property on this Control." );
      }

      if( @this.InvokeRequired ) {
        @this.Invoke( new SetPropertyThreadSafeDelegate<TResult>( SetPropertyThreadSafe ), new object[] { @this, property, value } );
      }
      else {
        @this.GetType().InvokeMember( propertyInfo.Name, BindingFlags.SetProperty, null, @this, new object[] { value } );
      }
    }

} 
