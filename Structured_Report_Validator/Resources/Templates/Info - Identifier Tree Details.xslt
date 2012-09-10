<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:sr="http://www.dvtk.org/schemas/SRValidationResult.xsd" xmlns="http://www.w3.org/1999/xhtml">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
      <head>
        <title>Structured Report Validation Results</title>
        <link href="screen.css" rel="Stylesheet" type="text/css" media="screen" />
      </head>
      <body>
        <xsl:call-template name="Identifier-Tree" />
      </body>
    </html>
  </xsl:template>
  
  <xsl:template name="Identifier-Tree">
    <h1>Identifier Tree</h1>
    <ul class="contentItems">
      <xsl:for-each select="/sr:structuredReports/sr:structuredReport/sr:contentItem">
        <li class="contentItem">
          <xsl:value-of select="@identifier" /><xsl:text> </xsl:text><xsl:value-of select="sr:conceptName/sr:conceptCode/sr:codeMeaning/sr:value" /><xsl:text> (</xsl:text><xsl:value-of select="sr:conceptName/sr:conceptCode/sr:codingSchemeDesignator/sr:value" /><xsl:text>, </xsl:text><xsl:value-of select="sr:conceptName/sr:conceptCode/sr:codeValue/sr:value" /><xsl:text>)</xsl:text>
          <xsl:for-each select="sr:childContentItems">
            <xsl:call-template name="Content-Items" />
          </xsl:for-each>
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>

  <xsl:template name="Content-Items">
    <ul class="contentItems">
      <xsl:for-each select="sr:contentItem">
        <li class="contentItem">
          <span class="identifier"><xsl:value-of select="@identifier" /></span>
          <xsl:call-template name="Show-ConceptName" />
          <xsl:call-template name="Show-ValueTypeSpecific" />
          <xsl:for-each select="sr:childContentItems">
            <xsl:call-template name="Content-Items" />
          </xsl:for-each>
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>

  <xsl:template name="Show-ConceptName">
    <xsl:for-each select="sr:conceptName">
      <xsl:if test="not(sr:conceptCode/sr:validationResults)">
        <xsl:call-template name="Show-ConceptCode-NoError" />
      </xsl:if>
      <xsl:if test="sr:conceptCode/sr:validationResults">
        <xsl:call-template name="Show-ConceptCode-Error" />
      </xsl:if>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="Show-ValueTypeSpecific">
    <xsl:for-each select="sr:valueTypeSpecific">
      <xsl:for-each select="sr:code">
        <div class="code">
          <xsl:if test="not(sr:conceptCode/sr:validationResults)">
            <xsl:call-template name="Show-ConceptCode-NoError" />
          </xsl:if>
          <xsl:if test="sr:conceptCode/sr:validationResults">
            <xsl:call-template name="Show-ConceptCode-Error" />
          </xsl:if>
          <!--<xsl:call-template name="Show-Code-NoError" />-->
        </div>
      </xsl:for-each>
      <xsl:for-each select="sr:num">
        <div class="num">
          <xsl:call-template name="Show-Num-NoError" />
        </div>
      </xsl:for-each>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="Show-Num-NoError">
    <xsl:for-each select="sr:measuredValue">
      <div class="measuredValue">
        <span class="numericValue"><xsl:value-of select="sr:numericValue/sr:value" /></span>
        <xsl:for-each select="sr:measurementUnits/sr:conceptCode">
        <span class="measurementUnits"><xsl:call-template name="Show-ConceptCode" /></span>
          </xsl:for-each>
      </div>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="Show-ConceptCode-NoError">
    <xsl:for-each select="sr:conceptCode">
      <xsl:call-template name="Show-ConceptCode" />
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="Show-ConceptCode-Error">
    <xsl:for-each select="sr:conceptCode">
      <div class="error">
        <xsl:call-template name="Show-ConceptCode" />
        <xsl:call-template name="Show-ValidationResults" />
      </div>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="Show-ConceptCode">
    <span class="conceptCode"><xsl:value-of select="sr:codeMeaning/sr:value" /><xsl:text> (</xsl:text><xsl:value-of select="sr:codingSchemeDesignator/sr:value" /><xsl:text>, </xsl:text><xsl:value-of select="sr:codeValue/sr:value" /><xsl:text>)</xsl:text></span>
  </xsl:template>

  <xsl:template name="Show-ValidationResults">
    <xsl:for-each select="sr:validationResults">
      <xsl:for-each select="sr:validationResult">
        <div class="validationResult">
          <div class="context"><xsl:value-of select="sr:context"/></div>
          <div class="message"><xsl:value-of select="sr:message"/></div>
        </div>
      </xsl:for-each>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>