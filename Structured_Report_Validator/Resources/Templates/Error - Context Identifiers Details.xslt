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
        <xsl:call-template name="CID-Errors" />
      </body>
    </html>
  </xsl:template>
  <xsl:template name="CID-Errors">
    <h1>CID Errors</h1>
    <span>
      Errors: <xsl:value-of select="count(//sr:message)" />
    </span>
    <xsl:for-each select="//sr:message">
      <table>
        <thead>
          <tr>
            <td>Code Value</td>
            <td>Coding Scheme Designator</td>
            <td>Coding Scheme Version</td>
            <td>Code Meaning</td>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>
              <xsl:value-of select="../../../sr:codeValue" />
            </td>
            <td>
              <xsl:value-of select="../../../sr:codingSchemeDesignator" />
            </td>
            <td>
              <xsl:value-of select="../../../sr:codingSchemeVersion" />
            </td>
            <td>
              <!--<xsl:value-of select="../../../" />-->
            </td>
          </tr>
          <tr>
            <td class="error" colspan="4">
              <xsl:value-of select="." />
            </td>
          </tr>
        </tbody>
      </table>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>
