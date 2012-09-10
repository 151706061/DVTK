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
        <xsl:call-template name="CID-Used-CodeSchemeDesignatorsSummary" />
      </body>
    </html>
  </xsl:template>
  <xsl:template name="CID-Used-CodeSchemeDesignatorsSummary">
    <h1>Used Code Scheme Designators</h1>
    <ul>
      <xsl:for-each select="//sr:conceptCode[generate-id(.)=generate-id(key('CodingSchemeDesignators',sr:codingSchemeDesignator/sr:value))]">
        <li>
          <xsl:value-of select="sr:codingSchemeDesignator/sr:value" /> (<xsl:value-of select="count(key('CodingSchemeDesignators',sr:codingSchemeDesignator/sr:value))" />)
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>
</xsl:stylesheet>
