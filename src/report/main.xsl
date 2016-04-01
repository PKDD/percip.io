<?xml version='1.0'?>
<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
      xmlns:ms="urn:schemas-microsoft-com:xslt"
      xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <xsl:template match="/">
    <html>
      <head>
        <script src="https://code.jquery.com/jquery-1.12.2.min.js"></script>
        <script src="jquery.filtertable.min.js"></script>
        <script src="sorttable.js"></script>
        <script src="main.js"></script>
        <link rel="stylesheet" type="text/css" href="main.css"/>
      </head>
      <body>
        
        <h2>Time report</h2>
        <table border="1" class="sortable">
          <tr bgcolor="#9acd32">
            <th>Stamp</th>
            <th>User</th>
            <th>Direction</th>
          </tr>

          <xsl:for-each select="TimeStampCollection/TimeStamps/TimeStamp">
            <tr>
              <xsl:variable name="d" select="Stamp"/>
              <td>
                <!--<xsl:value-of select="$d"/>-->
                <xsl:value-of select="ms:format-date($d, 'dd.MM.yyyy ')"/>
                <xsl:value-of select="ms:format-time($d, 'HH:mm:ss')"/>
              </td>

              <td>
                <xsl:value-of select="User"/>
              </td>
              <td>
                <xsl:value-of select="Direction"/>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
