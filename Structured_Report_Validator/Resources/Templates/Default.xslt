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
        <xsl:call-template name="CID-Used-CodeSchemeDesignatorsSummary" />
        <xsl:call-template name="CID-CodeValues">
          <xsl:with-param name="CodingSchemeDesignator" select="'99SEQUOIA'" />
        </xsl:call-template>
        <xsl:call-template name="CID-Used-CodeSchemeDesignatorsDetails" />
        <xsl:call-template name="Identifier-Tree" />
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
  <xsl:template name="Identifier-Tree">
    <h1>Identifier Tree</h1>
    <xsl:for-each select="/sr:structuredReports/sr:structuredReport/sr:contentItem">
      <ul>
        <li>
          <xsl:value-of select="@identifier" />
          <xsl:for-each select="sr:childContentItems">
            <xsl:call-template name="Content-Items" />
          </xsl:for-each>
        </li>
      </ul>
    </xsl:for-each>
  </xsl:template>
  <xsl:template name="Content-Items">
    <xsl:for-each select="sr:contentItem">
      <ul>
        <li>
          <xsl:value-of select="@identifier" />
          <xsl:for-each select="sr:childContentItems">
            <xsl:call-template name="Content-Items" />
          </xsl:for-each>
        </li>
      </ul>
    </xsl:for-each>
  </xsl:template>
  <xsl:template name="CID-Used-CodeSchemeDesignatorsDetails">
    <h1>Detailed usage Code Scheme Designators</h1>
    <ul>
      <xsl:for-each select="//sr:conceptCode/sr:codingSchemeDesignator/sr:value">
        <li>
          <xsl:value-of select="." />
        </li>
      </xsl:for-each>
    </ul>
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
  <xsl:template name="CID-CodeValues">
    <xsl:param name="CodingSchemeDesignator" />
    <h1>
      Values for <xsl:value-of select="$CodingSchemeDesignator" />
    </h1>
    <ul>
      <xsl:for-each select="//sr:conceptCode[generate-id(.)=generate-id(key('CodeValue',sr:codeValue/sr:value))]">
        <xsl:sort select="sr:codeValue/sr:value" />
        <xsl:if test="sr:codingSchemeDesignator/sr:value=$CodingSchemeDesignator">
          <li>
            <xsl:value-of select="sr:codeValue/sr:value" />
          </li>
        </xsl:if>
      </xsl:for-each>
    </ul>
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
