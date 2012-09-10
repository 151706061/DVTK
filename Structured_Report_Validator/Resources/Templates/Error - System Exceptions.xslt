<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:sr="http://www.dvtk.org/schemas/SRValidationResult.xsd" xmlns="http://www.w3.org/1999/xhtml">
  <xsl:output method="html" indent="yes"/>
  <xsl:key name="ExceptionMessage" match="sr:visitorException" use="sr:message" />
  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
      <head>
        <link href="screen.css" rel="Stylesheet" type="text/css" media="screen" />
      </head>
      <body>
        <xsl:call-template name="Error-SystemExceptions" />
      </body>
    </html>
  </xsl:template>
  <xsl:template name="Error-SystemExceptions">
    <h1>Error - System Exceptions</h1>
    <h2>Summary</h2>
    <ul>
      <xsl:for-each select="//sr:visitorException[generate-id(.)=generate-id(key('ExceptionMessage',sr:message))]">
        <li class="error">
          <xsl:value-of select="sr:message" /> (<xsl:value-of select="count(key('ExceptionMessage',sr:message))" />)
        </li>
      </xsl:for-each>
    </ul>
    <h2>Details</h2>
    <ul>
      <xsl:for-each select="//sr:visitorException">
        <li>
          <xsl:value-of select="sr:message" />
          <pre>
            <xsl:value-of select="sr:innerException/sr:message" />

            <xsl:value-of select="sr:innerException/sr:stackTrace" />
          </pre>
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>
</xsl:stylesheet>