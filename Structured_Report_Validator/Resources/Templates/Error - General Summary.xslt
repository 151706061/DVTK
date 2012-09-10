<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:sr="http://www.dvtk.org/schemas/SRValidationResult.xsd" xmlns="http://www.w3.org/1999/xhtml">
  <xsl:output method="html" indent="yes"/>
  <xsl:key name="CodingSchemeDesignators" match="sr:conceptCode" use="sr:codingSchemeDesignator/sr:value" />
  <xsl:key name="CodeValue" match="sr:conceptCode" use="sr:codeValue/sr:value" />
  <xsl:key name="ValidationResults" match="sr:validationResults" use="sr:validationResult/sr:message" />
  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
      <head>
        <link href="screen.css" rel="Stylesheet" type="text/css" media="screen" />
      </head>
      <body>
        <xsl:call-template name="Error-Summary" />
      </body>
    </html>
  </xsl:template>
  <xsl:template name="Error-Summary">
    <h1>Error Summary</h1>
    <ul>
      <xsl:for-each select="//sr:validationResults[generate-id(.)=generate-id(key('ValidationResults',sr:validationResult/sr:message))]">
        <li class="error">
          <xsl:value-of select="sr:validationResult/sr:message" /> (<xsl:value-of select="count(key('ValidationResults',sr:validationResult/sr:message))" />)
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>
</xsl:stylesheet>
