// Need to protect the gpgPassFile from who has read permissions. The keyfile is solely to automate the signing piece. If you don't need to sign then leave this piece out for obvious reasons

config:
    <add key="gpgLocation" value="gpg.exe" />
    <add key="gpgPassFile" value="passfile" />
    <add key="gpgBOBKey" value="XXXXXXX" /> <!-- pub key ID we're encrypting to -->
    <add key="gpgALICEKey" value="XXXXXXX" /> <!-- our pub key ID for signing -->


function:

    private static string EncryptFile( string FileToEncrypt )
    {
      string encfile = FileToEncrypt + ".gpg";
      try {
        string enccmd = string.Format( "--passphrase-file {0} --yes -e -s --default-key {1} -r {2} -o {3} {4}"
                        , ConfigurationManager.AppSettings["gpgPassFile"]
                        , ConfigurationManager.AppSettings["gpgALICEKey"]
                        , ConfigurationManager.AppSettings["gpgBOBKey"]
                        , encfile
                        , FileToEncrypt
                      );

        System.Diagnostics.Process ps = System.Diagnostics.Process.Start( ConfigurationManager.AppSettings["gpgLocation"], enccmd );
        ps.EnableRaisingEvents = true;
        ps.WaitForExit();
        ps.Close();

        ps = System.Diagnostics.Process.Start( "shred.exe", FileToEncrypt );
        ps.EnableRaisingEvents = true;
        ps.WaitForExit();
        ps.Close();
      }
      catch( Exception ex ) {
      }
      return encfile;
    }
