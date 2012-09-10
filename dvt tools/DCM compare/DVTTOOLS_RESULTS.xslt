<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	<xsl:template match="DvtDetailedResultsFile">
		<xsl:param name="ISSEQ"/>
		<xsl:param name="RESULTTYPE"/>
		<xsl:param name="TYPE"/>
		<html>
			<head/>
			<body>
				<h1>DVT Detailed Results File</h1>
				<xsl:if test="string-length(SubResultsLink/@Index)>0">
					<xsl:call-template name="SubResults">
						<xsl:with-param name="TYPE">Detail_</xsl:with-param>
					</xsl:call-template>
				</xsl:if>
				<xsl:apply-templates/>
				<xsl:call-template name="Directorysum">
					<xsl:with-param name="RESULTTYPE">Detail_</xsl:with-param>
				</xsl:call-template>
			</body>
		</html>
	</xsl:template>
	<xsl:template match="DvtSummaryResultsFile">
		<xsl:param name="ISSEQ"/>
		<xsl:param name="RESULTTYPE"/>
		<xsl:param name="TYPE"/>
		<html>
			<head/>
			<body>
				<h1>DVT Summary Results File</h1>
				<xsl:if test="string-length(SubResultsLink/@Index)>0">
					<xsl:call-template name="SubResults">
						<xsl:with-param name="TYPE">Summary_</xsl:with-param>
					</xsl:call-template>
				</xsl:if>
				<xsl:apply-templates/>
				<xsl:call-template name="Directorysum">
					<xsl:with-param name="RESULTTYPE">Summary_</xsl:with-param>
				</xsl:call-template>
			</body>
		</html>
	</xsl:template>
	<xsl:template name="SubResults">
		<xsl:param name="TYPE"/>
		<table border="1" width="100%" cellpadding="3">
			<font color="#000080">
				<tr>
					<td align="center" valign="top" class="item" colspan="5">
						<b>
					Emulator Results
									</b>
					</td>
				</tr>
				<tr>
					<td valign="top" width="10" class="item">Index</td>
					<td valign="top" class="item">Comments</td>
				</tr>
				<xsl:for-each select="SubResultsLink">
					<tr>
						<td valign="top" width="10" class="item">
							<xsl:for-each select="@Index">
								<xsl:element name="a">
									<xsl:attribute name="href"><xsl:value-of select="$TYPE"/><xsl:if test="string-length(../../SessionDetails/SessionId[.])=1">00<xsl:value-of select="../../SessionDetails/SessionId[.]"/></xsl:if><xsl:if test="string-length(../../SessionDetails/SessionId[.])=2">0<xsl:value-of select="../../SessionDetails/SessionId[.]"/></xsl:if><xsl:if test="string-length(../../SessionDetails/SessionId[.])=3"><xsl:value-of select="../../SessionDetails/SessionId[.]"/></xsl:if><xsl:if test="../../SessionDetails/ScpEmulatorType[.]='Printing'">_pr_scp</xsl:if><xsl:if test="../../SessionDetails/ScpEmulatorType[.]='Storage'">_st_scp</xsl:if>_em_res<xsl:value-of select="."/>.xml</xsl:attribute>
									<xsl:value-of select="."/>
								</xsl:element>
							</xsl:for-each>
						</td>
						<td valign="top" class="item">
							<xsl:if test="(@NrOfErrors[.>'0'])">
								<font color="#FF0000">Error: Record of related image contains an Error.<br/>
								</font>
							</xsl:if>	
							Number of Err: <xsl:for-each select="@NrOfErrors">
								<xsl:value-of select="."/>
							</xsl:for-each>
							<br/>
							Number of Wrn: <xsl:for-each select="@NrOfWarnings">
								<xsl:value-of select="."/>
							</xsl:for-each>
						</td>
					</tr>
				</xsl:for-each>
			</font>
		</table>
	</xsl:template>
	<xsl:template match="SessionDetails">
		<h2>Session Details</h2>
        Session ID: <xsl:value-of select="SessionId"/>
		<br/>
        Session Title: <xsl:value-of select="SessionTitle"/>
		<br/>
        Application Entity Name: <xsl:value-of select="ApplicationEntityName"/>
		<br/>
        Application Entity Version: <xsl:value-of select="ApplicationEntityVersion"/>
		<br/>
        Tester: <xsl:value-of select="Tester"/>
		<br/>
        Test Date: <xsl:value-of select="TestDate"/>
		<br/>
	</xsl:template>
	<xsl:template match="ByteDump">
		<br/>
		<SMALL>
			<i>
				<xsl:value-of select="Description"/> - <xsl:value-of select="ByteStream"/>
			</i>
		</SMALL>
	</xsl:template>
	<xsl:template match="Message" name="ValidationMessages">
		<br/>
		<xsl:if test="Type[.='Error']">
			<font color="#FF0000">
				<xsl:element name="a">
					<xsl:attribute name="NAME"><xsl:value-of select="@Index"/></xsl:attribute>
				</xsl:element>
			Error: 
			<xsl:for-each select="Meaning">
					<xsl:value-of select="."/>
				</xsl:for-each>
			</font>
			<xsl:if test="count(/DvtSummaryResultsFile/DvtDetailedResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Detail.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Detailed Result
								</xsl:element>
			</xsl:if>
			<xsl:if test="count(/DvtDetailedResultsFile/DvtSummaryResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Summary.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Summary Result
								</xsl:element>
			</xsl:if>
		</xsl:if>
		<xsl:if test="Type[.='Warning']">
			<font color="#FF0000">
				<xsl:element name="a">
					<xsl:attribute name="NAME"><xsl:value-of select="@Index"/></xsl:attribute>
				</xsl:element>
						Warning: 
			<xsl:for-each select="Meaning">
					<xsl:value-of select="."/>
				</xsl:for-each>
			</font>
						<xsl:if test="count(/DvtSummaryResultsFile/DvtDetailedResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Detail.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Detailed Result
								</xsl:element>
			</xsl:if>
			<xsl:if test="count(/DvtDetailedResultsFile/DvtSummaryResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Summary.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Summary Result
								</xsl:element>
			</xsl:if>>
		</xsl:if>
		<xsl:if test="Type[.='Info']">
			<br/>
			<i>
				<xsl:for-each select="Meaning">
					<xsl:value-of select="."/>
				</xsl:for-each>
			</i>
		</xsl:if>
	</xsl:template>
	<xsl:template name="VALMessages">
		<xsl:for-each select="Message">
			<tr>
				<td valign="top" width="120"/>
				<td valign="top" width="10"/>
				<td valign="top" width="10"/>
				<td valign="top" width="10"/>
				<td valign="top" width="150"/>
				<td align="center" valign="top" class="item">
					<b>
						<xsl:if test="Type[.='Info']">INFO - 
								<xsl:for-each select="Meaning">
								<xsl:value-of select="."/>
							</xsl:for-each>
						</xsl:if>
						<xsl:if test="Type[.='Error']">
							<font color="#FF0000">
								<xsl:element name="a">
									<xsl:attribute name="NAME"><xsl:value-of select="@Index"/></xsl:attribute>
								</xsl:element>
							Error: 
			<xsl:for-each select="Meaning">
									<xsl:value-of select="."/>
								</xsl:for-each>
											<xsl:if test="count(/DvtSummaryResultsFile/DvtDetailedResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Detail.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Detailed Result
								</xsl:element>
			</xsl:if>
			<xsl:if test="count(/DvtDetailedResultsFile/DvtSummaryResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Summary.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Summary Result
								</xsl:element>
			</xsl:if>							</font>
						</xsl:if>
						<xsl:if test="Type[.='Warning']">
							<font color="#FF0000">
								<xsl:element name="a">
									<xsl:attribute name="NAME"><xsl:value-of select="@Index"/></xsl:attribute>
								</xsl:element>
							Warning: 
			<xsl:for-each select="Meaning">
									<xsl:value-of select="."/>
								</xsl:for-each>
											<xsl:if test="count(/DvtSummaryResultsFile/DvtDetailedResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Detail.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Detailed Result
								</xsl:element>
			</xsl:if>
			<xsl:if test="count(/DvtDetailedResultsFile/DvtSummaryResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Summary.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Summary Result
								</xsl:element>
			</xsl:if>
							</font>
						</xsl:if>
					</b>
				</td>
			</tr>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="VALMessagesseq">
		<xsl:for-each select="Message">
			<b>
				<xsl:if test="Type[.='Info']">INFO - 
								<xsl:for-each select="Meaning">
						<xsl:value-of select="."/>
					</xsl:for-each>
				</xsl:if>
				<xsl:if test="Type[.='Error']">
					<font color="#FF0000">
						<xsl:element name="a">
							<xsl:attribute name="NAME"><xsl:value-of select="@Index"/></xsl:attribute>
						</xsl:element>
					Error: 
			<xsl:for-each select="Meaning">
							<xsl:value-of select="."/>
						</xsl:for-each>
			<xsl:if test="count(/DvtSummaryResultsFile/DvtDetailedResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Detail.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Detailed Result
								</xsl:element>
			</xsl:if>
			<xsl:if test="count(/DvtDetailedResultsFile/DvtSummaryResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Summary.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Summary Result
								</xsl:element>
			</xsl:if>
					</font>
				</xsl:if>
				<xsl:if test="Type[.='Warning']">
					<font color="#FF0000">
						<xsl:element name="a">
							<xsl:attribute name="NAME"><xsl:value-of select="@Index"/></xsl:attribute>
						</xsl:element>					
					Warning: 
			<xsl:for-each select="Meaning">
							<xsl:value-of select="."/>
						</xsl:for-each>
			<xsl:if test="count(/DvtSummaryResultsFile/DvtDetailedResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Detail.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Detailed Result
								</xsl:element>
			</xsl:if>
			<xsl:if test="count(/DvtDetailedResultsFile/DvtSummaryResultsFilename)>0">
				<br/>
				<xsl:element name="a">
					<xsl:attribute name="href">Summary.html#<xsl:value-of select="@Index"/></xsl:attribute>
									Link to Summary Result
								</xsl:element>
			</xsl:if>
					</font>
				</xsl:if>
			</b>
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="UserActivity">
		<xsl:if test="@Level[.='Error']">
			<br/>
			<font color="#FF0000">Error: 
<xsl:value-of select="."/>
			</font>
		</xsl:if>
		<xsl:if test="@Level[.='Warning']">
			<br/>
			<font color="#FF0000">Warning: 
<xsl:value-of select="."/>
			</font>
		</xsl:if>
	</xsl:template>
	<xsl:template match="Activity">
		<xsl:if test="@Level[.='Error']">
			<br/>
			<font color="#FF0000">Error: 
<xsl:value-of select="."/>
			</font>
		</xsl:if>
		<xsl:if test="@Level[.='Warning']">
			<br/>
			<font color="#FF0000">Warning: 
<xsl:value-of select="."/>
			</font>
		</xsl:if>
		<xsl:if test="@Level[.='Scripting']">
			<br/>
			<SMALL>
				<i>
					<xsl:value-of select="."/>
				</i>
			</SMALL>
		</xsl:if>
		<xsl:if test="@Level[.='ScriptName']">
			<br/>
			<b>
						Script Name:<xsl:value-of select="."/>
			</b>
		</xsl:if>
		<xsl:if test="@Level[.='Script']">
			<br/>
			<SMALL>
				<b>
					<xsl:value-of select="."/>
					<xsl:if test="contains(.,'Reading media file: &quot;')">
			<br/>
			<br/>
			<b><i>
				Media file <xsl:element name="a">
										<xsl:attribute name="href"><xsl:value-of select="substring-before(substring-after(.,'Reading media file: &quot;'),'&quot;')"/></xsl:attribute>
										<xsl:value-of select="substring-before(substring-after(.,'Reading media file: &quot;'),'&quot;')"/>
									</xsl:element> created, click to open DCM file in viewer.

			<br/>

			</i></b>
					</xsl:if>
				</b>
			</SMALL>
		</xsl:if>
		<xsl:if test="@Level[.='None']">
			<br/>
			<SMALL>
				<i>
					<xsl:value-of select="."/>
				</i>
			</SMALL>
		</xsl:if>
		<xsl:if test="@Level[.='Info']">
			<br/>
			<i>
				<xsl:value-of select="."/>
			</i>
		</xsl:if>
		<xsl:if test="@Level[.='DCM']">
			<br/>

			<br/>

			<b><i>
				Media file <xsl:element name="a">
										<xsl:attribute name="href"><xsl:value-of select="."/></xsl:attribute>
										<xsl:value-of select="."/> 
									</xsl:element> created, click to open DCM file in viewer.

			<br/>

			</i></b>
		</xsl:if>
		<xsl:if test="@Level[.='DicomObjectRelationship']">
			<br/>
			<i>
				<xsl:value-of select="."/>
			</i>
		</xsl:if>
	</xsl:template>
	<xsl:template match="DvtDetailedResultsFilename">
	</xsl:template>
	<xsl:template match="DvtSummaryResultsFilename">

	</xsl:template>
	<xsl:template match="Send">
		<br/>
		<br/>
		<b>Send: </b>
		<xsl:apply-templates/>
		<br/>
	</xsl:template>
	<xsl:template match="Import">
		<br/>
		<br/>
		<b>Imported: </b>
		<xsl:apply-templates/>
		<br/>
	</xsl:template>
	<xsl:template match="Create">
		<br/>
		<br/>
		<b>Create: </b>
		<xsl:for-each select="CommandField">
			<b>
				<xsl:value-of select="."/> 
			</b>
		</xsl:for-each>
		<xsl:for-each select="CommandSetRefId">
			<b>
				<xsl:value-of select="."/> 
			</b>
		</xsl:for-each>
		<xsl:for-each select="IodId">
			<b>
				"<xsl:value-of select="."/>"
			</b>
		</xsl:for-each>
		<xsl:for-each select="DataSetRefId">
			<b>
				<xsl:value-of select="."/>
			</b>
		</xsl:for-each>
		<br/>
		<xsl:for-each select="CommandSet">
			<i>        Command Set attributes</i>
			<br/>
			<xsl:call-template name="Attribute">
				<xsl:with-param name="ISSEQ"/>
			</xsl:call-template>
		</xsl:for-each>
		<xsl:for-each select="DataSet">
			<i>    Data Set attributes</i>
			<br/>
			<xsl:call-template name="Attribute">
				<xsl:with-param name="ISSEQ"/>
			</xsl:call-template>
		</xsl:for-each>
		<br/>
	</xsl:template>
	<xsl:template match="Set">
		<br/>
		<br/>
		<b>Set: </b>
		<xsl:for-each select="CommandField">
			<b>
				<xsl:value-of select="."/> 
							</b>
		</xsl:for-each>
		<xsl:for-each select="CommandSetRefId">
			<b>
				<xsl:value-of select="."/> 
			</b>
		</xsl:for-each>
		<xsl:for-each select="IodId">
			<b>
				"<xsl:value-of select="."/>"
			</b>
		</xsl:for-each>
		<xsl:for-each select="DataSetRefId">
			<b>
				<xsl:value-of select="."/>
			</b>
		</xsl:for-each>
		<br/>
		<xsl:for-each select="CommandSet">
			<i>        Command Set attributes</i>
			<br/>
			<xsl:call-template name="Attribute">
				<xsl:with-param name="ISSEQ"/>
			</xsl:call-template>
		</xsl:for-each>
		<xsl:for-each select="Dataset">
			<i>    Data Set attributes</i>
			<br/>
			<xsl:call-template name="Attribute">
				<xsl:with-param name="ISSEQ"/>
			</xsl:call-template>
		</xsl:for-each>
		<br/>
	</xsl:template>
	<xsl:template match="Display">
		<br/>
DISPLAY:				<xsl:call-template name="Attribute">
					<xsl:with-param name="ISSEQ"/>
				</xsl:call-template>
				<xsl:for-each select="CommandField">
			<b>
				<xsl:value-of select="."/> 
							</b>
		</xsl:for-each>
		<xsl:for-each select="CommandSetRefId">
			<b>
				<xsl:value-of select="."/> 
			</b>
		</xsl:for-each>
		<xsl:for-each select="IodId">
			<b>
				"<xsl:value-of select="."/>"
			</b>
		</xsl:for-each>
		<xsl:for-each select="DataSetRefId">
			<b>
				<xsl:value-of select="."/>
			</b>
		</xsl:for-each>
		<xsl:for-each select="CommandSet">
			<i>        Command Set attributes</i>
			<br/>
			<xsl:call-template name="Attribute">
				<xsl:with-param name="ISSEQ"/>
			</xsl:call-template>
		</xsl:for-each>
		<xsl:for-each select="Dataset">
			<i>    Data Set attributes</i>
			<br/>
			<xsl:call-template name="Attribute">
				<xsl:with-param name="ISSEQ"/>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="Receive">
		<br/>
		<br/>
		<b>Received: </b>
		<xsl:apply-templates/>
		<br/>
	</xsl:template>
	<xsl:template match="DicomMessage">
		<xsl:for-each select="Command">
			<br/>
			<i>
				<b>
					<xsl:value-of select="@Id"/>
				</b>
			</i>
			<br/>
			<xsl:call-template name="Attribute">
				<xsl:with-param name="ISSEQ"/>
			</xsl:call-template>
		</xsl:for-each>
		<xsl:for-each select="Dataset">
			<br/>
			<i>
				<b>
					<xsl:value-of select="@Name"/>
				</b>
			</i>
			<br/>
			<xsl:call-template name="Attribute">
				<xsl:with-param name="ISSEQ"/>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="Attribute">
		<xsl:param name="ISSEQ"/>
		<xsl:for-each select="Attribute">
			<xsl:value-of select="$ISSEQ"/>(0x<xsl:value-of select="@Group"/>
			<xsl:value-of select="@Element"/>,<xsl:value-of select="@VR"/>
			<xsl:if test="not(@VR[.='SQ'])">,"</xsl:if>
			<xsl:for-each select="Values">
				<xsl:for-each select="FileName">
					<xsl:value-of select="."/>
				</xsl:for-each>
				<xsl:for-each select="Value">
					<xsl:value-of select="."/>
					<xsl:if test="not(last()=position())">","</xsl:if>
				</xsl:for-each>
				<xsl:if test="not(@VR[.='SQ'])">"</xsl:if>
				<xsl:if test="@VR[.='SQ']">,</xsl:if>
				<xsl:for-each select="Sequence">
					<br/>
					<xsl:for-each select="Item">
						<xsl:if test="not($ISSEQ='')">
							<xsl:call-template name="Attribute">
								<xsl:with-param name="ISSEQ"><xsl:value-of select="$ISSEQ"/>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>
						<xsl:if test="$ISSEQ=''">
							<xsl:call-template name="Attribute">
								<xsl:with-param name="ISSEQ">
									<xsl:value-of select="$ISSEQ"/>></xsl:with-param>
							</xsl:call-template>
						</xsl:if>
						<xsl:if test="not(last()=position())">,<br/>
						</xsl:if>
					</xsl:for-each>
				</xsl:for-each>
			</xsl:for-each>) #
				<xsl:value-of select="@Name"/>
			<br/>
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="AssociateRq">
		<xsl:call-template name="A_ASSOCIATE_RQ_AC">
			<xsl:with-param name="ASSOCTYPE">A_ASSOCIATE_RQ</xsl:with-param>
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="AssociateAc">
		<xsl:call-template name="A_ASSOCIATE_RQ_AC">
			<xsl:with-param name="ASSOCTYPE">A_ASSOCIATE_AC</xsl:with-param>
		</xsl:call-template>
	</xsl:template>
	<xsl:template match="AssociateRj">
		<xsl:call-template name="A_ASSOCIATE_RJ"/>
	</xsl:template>
	<xsl:template match="AAbort">
		<xsl:call-template name="A_ABORT"/>
	</xsl:template>
	<xsl:template match="ReleaseRq">
		<xsl:call-template name="A_RELEASE_RQ"/>
	</xsl:template>
	<xsl:template match="ReleaseRp">
		<xsl:call-template name="A_RELEASE_RP"/>
	</xsl:template>
	<xsl:template name="A_ASSOCIATE_RQ_AC">
		<xsl:param name="ASSOCTYPE"/>
		<b>
			<xsl:value-of select="$ASSOCTYPE"/> Message</b>
		<br/>	Protocol Version: <xsl:for-each select="ProtocolVersion">
			<xsl:value-of select="."/>
		</xsl:for-each>
		<br/>	Called AE Title: <xsl:for-each select="CalledAETitle">
			<xsl:value-of select="."/>
		</xsl:for-each>
		<br/>	Calling AE Title: <xsl:for-each select="CallingAETitle">
			<xsl:value-of select="."/>
		</xsl:for-each>
		<br/>	Application Context: <xsl:for-each select="ApplicationContextName">
			<xsl:value-of select="."/>
		</xsl:for-each>
		<br/> Presentation Context Item(s):  
				<xsl:for-each select="PresentationContexts">
			<xsl:for-each select="PresentationContext">
				<br/>
				<i>
						Presentation Context ID:  ID: <xsl:value-of select="@Id"/>
				</i>
				<xsl:if test="count(@Result)>0">
					<br/>	> Result: <xsl:value-of select="@Result"/>
				</xsl:if>
				<br/>	 > Abstract Syntax: <xsl:value-of select="@AbstractSyntaxName"/>
				<xsl:for-each select="TransferSyntaxes">
					<xsl:for-each select="TransferSyntax">
						<br/>	 > Transfer Syntax: 
					<xsl:value-of select="."/>
					</xsl:for-each>
				</xsl:for-each>
				<xsl:for-each select="TransferSyntax">
					<br/>	 > Transfer Syntax: 
					<xsl:value-of select="."/>
				</xsl:for-each>
			</xsl:for-each>
		</xsl:for-each>
		<br/> User Information:  <xsl:for-each select="UserInformation">
			<xsl:if test="count(MaximumLengthReceived)>0">
				<br/>	> Maximum Length: <xsl:value-of select="MaximumLengthReceived"/>
			</xsl:if>
			<xsl:if test="count(ImplementationClassUid)>0">
				<br/>	> Implementation Class UID: <xsl:value-of select="ImplementationClassUid"/>
			</xsl:if>
			<xsl:if test="count(ImplementationVersionName)>0">
				<br/>	> Implementation Version Name: <xsl:value-of select="ImplementationVersionName"/>
			</xsl:if>
			<xsl:if test="count(AsynchronousOperationsWindow)>0">
				<br/>	Asynchronous Operations Window: 
				<xsl:for-each select="AsynchronousOperationsWindow">
					<br/> > Maximum Number Operations Invoked: <xsl:value-of select="@Invoked"/>
					<br/> > Maximum Number Operations Performed: <xsl:value-of select="@Peformed"/>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="count(ScpScuRoleSelections)>0">
				<xsl:for-each select="ScpScuRoleSelections">
					<br/>	SCP SCU Role Selection(s): <xsl:for-each select="ScpScuRoleSelection">
						<br/>	 SOP Class UID: 	<xsl:value-of select="@Uid"/>
						<br/> > SCU Role: 	<xsl:value-of select="@ScuRole"/>
						<br/> > SCP Role: 	<xsl:value-of select="@ScpRole"/>
					</xsl:for-each>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="count(SopClassExtendedNegotiations)>0">
				<xsl:for-each select="SopClassExtendedNegotiations">
					<br/>	SOP Class Extended Negotiation(s): <xsl:for-each select="SopClassExtendedNegotiation">
						<br/>	 SOP Class UID: 	<xsl:value-of select="@Uid"/>
						<br/> >  Service Class Application Information: 	<xsl:value-of select="@AppInfo"/>
					</xsl:for-each>
				</xsl:for-each>
			</xsl:if>
			<br/>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="A_ASSOCIATE_RJ">
		<b>A-ASSOCIATE-RJ Message</b>
		<br/>	Result: <xsl:for-each select="Result">
			<xsl:value-of select="."/>
			<xsl:if test="(.)='1'"> (Rejected-permanent)			</xsl:if>
			<xsl:if test="(.)='2'"> (Rejected-transient)			</xsl:if>
		</xsl:for-each>
		<br/>	Source: <xsl:for-each select="Source">
			<xsl:value-of select="."/>
			<xsl:if test="(.)='1'"> (DICOM UL service-user)			</xsl:if>
			<xsl:if test="(.)='2'"> (DICOM UL service-provider (ACSE related function))			</xsl:if>
			<xsl:if test="(.)='3'"> (DICOM UL service-provider (Presentation related function))			</xsl:if>
		</xsl:for-each>
		<br/>	Reason: <xsl:for-each select="Reason">
			<xsl:value-of select="."/>
			<xsl:if test="(../Source)='1'">
				<xsl:if test="(.)='1'"> (no-reason-given)			</xsl:if>
				<xsl:if test="(.)='2'"> (application-Context-name-not-supported)			</xsl:if>
				<xsl:if test="(.)='3'"> (calling-AE-title-not-recognized)			</xsl:if>
				<xsl:if test="(.)='4'"> (reserved)			</xsl:if>
				<xsl:if test="(.)='5'"> (reserved)			</xsl:if>
				<xsl:if test="(.)='6'"> (reserved)			</xsl:if>
				<xsl:if test="(.)='7'"> (called-AE-title-not-recognized)			</xsl:if>
				<xsl:if test="(.)='8'"> (reserved)			</xsl:if>
				<xsl:if test="(.)='9'"> (reserved)			</xsl:if>
				<xsl:if test="(.)='10'"> (reserved)			</xsl:if>
			</xsl:if>
			<xsl:if test="(../Source)='2'">
				<xsl:if test="(.)='1'"> (no-reason-given)			</xsl:if>
				<xsl:if test="(.)='2'"> (protocol-version-not-supported)			</xsl:if>
			</xsl:if>
			<xsl:if test="(../Source)='3'">
				<xsl:if test="(.)='1'"> (temporary-congestion)			</xsl:if>
				<xsl:if test="(.)='2'"> (local-limit-exceeded)			</xsl:if>
				<xsl:if test="(.)='3'"> (reserved)			</xsl:if>
				<xsl:if test="(.)='4'"> (reserved)			</xsl:if>
				<xsl:if test="(.)='5'"> (reserved)			</xsl:if>
				<xsl:if test="(.)='6'"> (reserved)			</xsl:if>
				<xsl:if test="(.)='7'"> (reserved)			</xsl:if>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="A_ABORT">
		<b>A-ABORT Message</b>
		<br/>	Reason: <xsl:for-each select="Reason">
			<xsl:value-of select="."/>
			<xsl:if test="(.)='0'"> (Reason-not-specified)			</xsl:if>
			<xsl:if test="(.)='1'"> (Unrecognized-PDU)			</xsl:if>
			<xsl:if test="(.)='2'"> (Unexpected-PDU)			</xsl:if>
			<xsl:if test="(.)='3'"> (Reserved)			</xsl:if>
			<xsl:if test="(.)='4'"> (Unrecognized-PDU parameter)	</xsl:if>
			<xsl:if test="(.)='5'"> (Unexpected-PDU parameter)		</xsl:if>
			<xsl:if test="(.)='6'"> (Invalid-PDU-parameter value)			</xsl:if>
		</xsl:for-each>
		<br/>	Source: <xsl:for-each select="Source">
			<xsl:value-of select="."/>
			<xsl:if test="(.)='0'"> (DICOM UL service-user (initiated abort))			</xsl:if>
			<xsl:if test="(.)='1'"> (Reserved)			</xsl:if>
			<xsl:if test="(.)='2'"> (DICOM UL service-provider (initiated abort))			</xsl:if>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="A_RELEASE_RQ">
		<b>A-RELEASE-RQ Message</b>
	</xsl:template>
	<xsl:template name="A_RELEASE_RP">
		<b>A-RELEASE-RP Message</b>
	</xsl:template>
	<xsl:template match="ValidationAssociateAc">
		<br/>
		<font color="#000080">
			<b>Validate: </b>
			<xsl:call-template name="Val_A_ASSOCIATE_RQ_AC">
				<xsl:with-param name="ASSOCTYPE">A_ASSOCIATE_AC</xsl:with-param>
			</xsl:call-template>
		</font>
		<br/>
	</xsl:template>
	<xsl:template match="ValidationAssociateRq">
		<br/>
		<font color="#000080">
			<b>Validate: </b>
			<xsl:call-template name="Val_A_ASSOCIATE_RQ_AC">
				<xsl:with-param name="ASSOCTYPE">A_ASSOCIATE_RQ</xsl:with-param>
			</xsl:call-template>
		</font>
		<br/>
	</xsl:template>
	<xsl:template match="ValidationDicomMessage" name="objectresults">
		<br/>
		<font color="#000080">
			<b>
				<xsl:value-of select="Name"/>
			</b>
			<br/>
			<xsl:for-each select="Module">
				<br/>
				<table border="1" width="100%" cellpadding="3">
					<font color="#000080">
						<tr>
							<td align="center" valign="top" class="item" colspan="6">
								<b>
					Module: <xsl:value-of select="@Name"/> (<xsl:value-of select="@Usage"/>)
									</b>
							</td>
						</tr>
						<tr>
							<td valign="top" width="120" class="item">Attribute</td>
							<td valign="top" width="10" class="item">VR</td>
							<td valign="top" width="10" class="item">Type</td>
							<td valign="top" width="10" class="item">Pr</td>
							<td valign="top" width="150" class="item">Attribute Name</td>
							<td valign="top" class="item">Value(s) and Comments</td>
						</tr>
						<xsl:call-template name="VALAttributes">
							<xsl:with-param name="VALISSEQ">	</xsl:with-param>
						</xsl:call-template>
					</font>
				</table>
				<xsl:call-template name="FindMessages"/>
			</xsl:for-each>
			<xsl:for-each select="AdditionalAttributes">
				<table border="1" width="100%" cellpadding="3">
					<font color="#000080">
						<tr>
							<td align="center" valign="top" class="item" colspan="6">
								<b>
					Additional Attributes
									</b>
							</td>
						</tr>
						<tr>
							<td valign="top" width="120" class="item">Attribute</td>
							<td valign="top" width="10" class="item">VR</td>
							<td valign="top" width="10" class="item">Type</td>
							<td valign="top" width="10" class="item">Pr</td>
							<td valign="top" width="150" class="item">Attribute Name</td>
							<td valign="top" class="item">Value(s) and Comments</td>
						</tr>
						<xsl:call-template name="VALAttributes">
							<xsl:with-param name="VALISSEQ">	</xsl:with-param>
						</xsl:call-template>
					</font>
				</table>
				<xsl:call-template name="FindMessages"/>
			</xsl:for-each>
			<xsl:call-template name="FindMessages"/>
		</font>
	</xsl:template>
	<xsl:template name="Directorysum">
		<xsl:param name="RESULTTYPE"/>
		<xsl:for-each select="//DirectoryRecordLinks">
			<table border="1" width="100%" cellpadding="3">
				<font color="#000080">
					<tr>
						<td align="center" valign="top" class="item" colspan="5">
							<b>
					Directory Record TOC
									</b>
						</td>
					</tr>
					<tr>
						<td valign="top" width="120" class="item">Record Type</td>
						<td valign="top" width="10" class="item">Index</td>
						<td valign="top" width="10" class="item">Record Offset</td>
						<td valign="top" width="10" class="item">Reference Count</td>
						<td valign="top" class="item">Comments</td>
					</tr>
					<xsl:for-each select="DirectoryRecordLink ">
						<tr>
							<td valign="top" width="120" class="item">
								<xsl:for-each select="@Type">
									<xsl:value-of select="."/>
								</xsl:for-each>
							</td>
							<td valign="top" width="10" class="item">
								<xsl:for-each select="@Index">
									<xsl:element name="a">
										<xsl:attribute name="href"><xsl:copy-of select="$RESULTTYPE"/><xsl:if test="string-length(../../../../../SessionDetails/SessionId[.])=1">00<xsl:value-of select="../../../../../SessionDetails/SessionId[.]"/></xsl:if><xsl:if test="string-length(../../../../../SessionDetails/SessionId[.])=2">0<xsl:value-of select="../../../../../SessionDetails/SessionId[.]"/></xsl:if><xsl:if test="string-length(../../../../../SessionDetails/SessionId[.])=3"><xsl:value-of select="../../../../../SessionDetails/SessionId[.]"/></xsl:if>_DICOMDIR_res<xsl:value-of select="."/>.xml</xsl:attribute>
										<xsl:value-of select="."/>
									</xsl:element>
								</xsl:for-each>
							</td>
							<td valign="top" width="10" class="item">
								<xsl:for-each select="@RecordOffset">
									<xsl:value-of select="."/>
								</xsl:for-each>
							</td>
							<td valign="top" width="10" class="item">
								<xsl:for-each select="@ReferenceCount">
									<xsl:value-of select="."/>
								</xsl:for-each>
							</td>
							<td valign="top" class="item">
								<xsl:if test="(@NrOfErrors[.>'0'])">
									<font color="#FF0000">Error: Record of related image contains an Error.<br/>
									</font>
								</xsl:if>	
							Number of Err: <xsl:for-each select="@NrOfErrors">
									<xsl:value-of select="."/>
								</xsl:for-each>
								<br/>
							Number of Wrn: <xsl:for-each select="@NrOfWarnings">
									<xsl:value-of select="."/>
								</xsl:for-each>
															
								
								 
							<xsl:for-each select="Messages">
									<xsl:for-each select="ValidationMessage">
										<tr>
											<td valign="top" width="120"></td>
											<td valign="top" width="10"></td>
											<td valign="top" width="10"></td>
											<td valign="top" width="10"></td>
											<td align="center" valign="top" class="item">
												<b>
													<xsl:if test="@Type[.='Error']">
														<font color="#FF0000">Error: 
			<xsl:for-each select="Meaning">
																<xsl:value-of select="."/>
															</xsl:for-each>
														</font>
													</xsl:if>
													<xsl:if test="@Type[.='Warning']">
														<font color="#FF0000">Warning: 
			<xsl:for-each select="Meaning">
																<xsl:value-of select="."/>
															</xsl:for-each>
														</font>
													</xsl:if>
												</b>
											</td>
										</tr>
									</xsl:for-each>
								</xsl:for-each>
							</td>
						</tr>
					</xsl:for-each>
				</font>
			</table>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="VALAttributes">
		<xsl:param name="VALISSEQ"/>
		<xsl:param name="seqattr"/>
		<xsl:for-each select="Attribute">
			<tr>
				<td valign="top" width="120">
					<xsl:value-of select="$VALISSEQ"/>(<xsl:value-of select="@Group"/>,<xsl:value-of select="@Element"/>)</td>
				<td valign="top" width="10">
					<xsl:value-of select="@VR"/>
				</td>
				<td valign="top" width="10">
					<xsl:value-of select="@Type"/>
				</td>
				<td valign="top" width="10">
					<xsl:value-of select="@Present"/>
				</td>
				<td valign="top" width="150">
					<xsl:value-of select="@Name"/>
				</td>
				<td valign="top">
					<xsl:for-each select="Values">
						<xsl:if test="Sequence">
							<xsl:call-template name="VALMessagesseq"/>
						</xsl:if>
						<xsl:for-each select="Value">
							<xsl:value-of select="."/>
							<xsl:if test="not(last()=position())"><br/></xsl:if>
						</xsl:for-each>
						<xsl:for-each select="Value">
							<xsl:call-template name="VALMessages"/>
						</xsl:for-each>
						<xsl:for-each select="Sequence">
							<xsl:call-template name="VALMessages"/>
							<xsl:for-each select="Item">
								<xsl:call-template name="VALMessages"/>
								<xsl:if test="not($VALISSEQ='')">
									<xsl:call-template name="VALAttributes">
										<xsl:with-param name="VALISSEQ"><xsl:value-of select="$VALISSEQ"/>></xsl:with-param>
									</xsl:call-template>
								</xsl:if>
								<xsl:if test="$VALISSEQ=''">
									<xsl:call-template name="VALAttributes">
										<xsl:with-param name="VALISSEQ">
											<xsl:value-of select="$VALISSEQ"/>></xsl:with-param>
									</xsl:call-template>
								</xsl:if>
								<xsl:if test="not(last()=position())">
									<tr>
										<td>
											<i>next Item</i>
										</td>
									</tr>
								</xsl:if>
							</xsl:for-each>
						</xsl:for-each>
						<xsl:call-template name="VALMessages"/>
					</xsl:for-each>
				</td>
			</tr>
			<xsl:call-template name="VALMessages"/>
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="Val_A_ASSOCIATE_RQ_AC">
		<xsl:param name="ASSOCTYPE"/>
		<b>
			<xsl:value-of select="$ASSOCTYPE"/> Message</b>
		<xsl:if test="count(ProtocolVersion/@Value)>0">
			<br/>	Protocol Version: <xsl:for-each select="ProtocolVersion">
				<xsl:value-of select="@Value"/>
				<xsl:apply-templates/>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="count(CalledAETitle/@Value)>0">
			<br/>	Called AE Title: <xsl:for-each select="CalledAETitle">
				<xsl:value-of select="@Value"/>
				<xsl:apply-templates/>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="count(CallingAETitle/@Value)>0">
			<br/>	Calling AE Title: <xsl:for-each select="CallingAETitle">
				<xsl:value-of select="@Value"/>
				<xsl:apply-templates/>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="count(ApplicationContextName/@Value)>0">
			<br/>	Application Context: <xsl:for-each select="ApplicationContextName">
				<xsl:value-of select="@Value"/>
				<xsl:apply-templates/>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="count(PresentationContexts/PresentationContext/Id/@Value)>0">
			<br/> Presentation Context Item(s): <xsl:for-each select="PresentationContexts">
				<xsl:for-each select="PresentationContext">
					<xsl:for-each select="Id">
						<br/>Presentation Context: ID: <xsl:value-of select="@Value"/>
						<xsl:apply-templates/>
					</xsl:for-each>
					<xsl:if test="count(Result/@Value)>0">
						<xsl:for-each select="Result">
							<br/>	> Result: <xsl:value-of select="@Value"/>
							<xsl:apply-templates/>
						</xsl:for-each>
					</xsl:if>
					<xsl:if test="count(AbstractSyntaxName/@Value)>0">
						<xsl:for-each select="AbstractSyntaxName">
							<br/>	> Abstract Syntax: <xsl:value-of select="@Value"/>
							<xsl:apply-templates/>
						</xsl:for-each>
					</xsl:if>
					<xsl:if test="count(TransferSyntaxes/TransferSyntax/@Value)>0">
						<xsl:for-each select="TransferSyntaxes">
							<xsl:for-each select="TransferSyntax">
								<br/>	 > Transfer Syntax: 
					<xsl:value-of select="@Value"/>
								<xsl:apply-templates/>
							</xsl:for-each>
							<xsl:apply-templates/>
						</xsl:for-each>
					</xsl:if>
					<xsl:if test="count(TransferSyntax/@Value)>0">
						<xsl:for-each select="TransferSyntax">
							<br/>	 > Transfer Syntax: 
					<xsl:value-of select="@Value"/>
							<xsl:apply-templates/>
						</xsl:for-each>
					</xsl:if>
				</xsl:for-each>
			</xsl:for-each>
		</xsl:if>
		<xsl:if test="count(UserInformation)>0">
			<br/> User Information:  <xsl:for-each select="UserInformation">
				<xsl:if test="count(MaximumLengthReceived)>0">
					<br/>>	Maximum Length: <xsl:for-each select="MaximumLengthReceived">
						<xsl:value-of select="@Value"/>
						<xsl:apply-templates/>
					</xsl:for-each>
				</xsl:if>
				<xsl:if test="count(ImplementationClassUid)>0">
					<br/>>	Implementation Class UID: <xsl:for-each select="ImplementationClassUid">
						<xsl:value-of select="@Value"/>
						<xsl:apply-templates/>
					</xsl:for-each>
				</xsl:if>
				<xsl:if test="count(ImplementationVersionName)>0">
					<br/>>	Implementation Version Name: <xsl:for-each select="ImplementationVersionName">
						<xsl:value-of select="@Value"/>
						<xsl:apply-templates/>
					</xsl:for-each>
				</xsl:if>
				<xsl:if test="count(AsynchronousOperationsWindow)>0">
					<br/>>	Asynchronous Operations Window: <xsl:for-each select="AsynchronousOperationsWindow">
						<br/>	 >  Maximum Number Operations Invoked: <xsl:value-of select="@Invoked"/>
						<br/>	 >  Maximum Number Operations Performed: <xsl:value-of select="@Peformed"/>
						<xsl:apply-templates/>
					</xsl:for-each>
				</xsl:if>
				<xsl:if test="count(ScpScuRoleSelections)>0">
					<xsl:for-each select="ScpScuRoleSelections">
						<br/>>	SCP SCU Role Selection(s): <xsl:for-each select="ScpScuRoleSelection">
							<br/> >> SOP Class UID: 	<xsl:value-of select="@Uid."/>
							<br/> >> SCU Role: 	<xsl:value-of select="@ScuRole"/>
							<br/> >> SCP Role: 	<xsl:value-of select="@ScpRole"/>
							<xsl:apply-templates/>
						</xsl:for-each>
					</xsl:for-each>
				</xsl:if>
				<xsl:if test="count(SopClassExtendedNegotiations)>0">
					<xsl:for-each select="SopClassExtendedNegotiations">
						<br/>	>SOP Class Extended Negotiation(s): <xsl:for-each select="SopClassExtendedNegotiation">
							<br/>	 >>  SOP Class UID: 	<xsl:value-of select="@Uid."/>
							<br/> >>  Service Class Application Information: 	<xsl:value-of select="@AppInfo"/>
							<xsl:apply-templates/>
						</xsl:for-each>
					</xsl:for-each>
				</xsl:if>
			</xsl:for-each>
		</xsl:if>
	</xsl:template>
	<xsl:template match="ValidationAssociateRj">
		<font color="#000080">
			<b>A-ASSOCIATE-RJ Message</b>
			<br/>	Result: <xsl:for-each select="Result">
				<xsl:for-each select="@Value">
					<xsl:value-of select="."/>
				</xsl:for-each>
				<xsl:apply-templates/>
			</xsl:for-each>
			<br/>	Reason: <xsl:for-each select="Reason">
				<xsl:for-each select="@Value">
					<xsl:value-of select="."/>
				</xsl:for-each>
				<xsl:apply-templates/>
			</xsl:for-each>
			<br/>	Source: <xsl:for-each select="Source">
				<xsl:for-each select="@Value">
					<xsl:value-of select="."/>
				</xsl:for-each>
				<xsl:apply-templates/>
			</xsl:for-each>
		</font>
	</xsl:template>
	<xsl:template match="ValidationAbortRq">
		<font color="#000080">
			<b>A-ABORT Message</b>
			<br/>	Reason: <xsl:for-each select="Reason">
				<xsl:for-each select="@Value">
					<xsl:value-of select="."/>
				</xsl:for-each>
				<xsl:apply-templates/>
			</xsl:for-each>
			<br/>	Source: <xsl:for-each select="Source">
				<xsl:for-each select="@Value">
					<xsl:value-of select="."/>
				</xsl:for-each>
				<xsl:apply-templates/>
			</xsl:for-each>
		</font>
	</xsl:template>
	<xsl:template match="ValidationReleaseRq">
		<font color="#000080">
			<br/><b>A-RELEASE-RQ Message</b>
			<xsl:apply-templates/>
		</font>
	</xsl:template>
	<xsl:template match="ValidationReleaseRp">
		<font color="#000080">
			<br/><b>A-RELEASE-RP Message</b>
			<xsl:apply-templates/>
		</font>
	</xsl:template>
	<xsl:template match="ValidationResults">
		<font color="#000080">
			<xsl:apply-templates/>
		</font>
	</xsl:template>
	<xsl:template name="FindMessages">
		<xsl:for-each select="Message">
			<xsl:call-template name="ValidationMessages"/>
		</xsl:for-each>
	</xsl:template>
	<xsl:template match="ValidationCounters">
		<br/>
		<br/>
		<font color="#FF0000">
			<i>
				<b>
					RESULT: <xsl:value-of select="ValidationTest"/>
					<br/>
					<xsl:for-each select="NrOfValidationErrors">
				
				Number of Validation Errors:	<xsl:value-of select="."/> - 
				
			</xsl:for-each>
					<xsl:for-each select="NrOfValidationWarnings">
				
					Number of Validation Warnings:		
					<xsl:value-of select="."/>
					</xsl:for-each>
				</b>
				<br/>
				<b>
					<xsl:for-each select="NrOfUserErrors">
				
				Number of User Validation Errors:	<xsl:value-of select="."/> - 
				
			</xsl:for-each>
					<xsl:for-each select="NrOfUserWarnings">
				
					Number of User Validation Warnings:		
					<xsl:value-of select="."/>
					</xsl:for-each>
				</b>
				<br/>
				<b>
					<xsl:for-each select="NrOfGeneralErrors">
				
				Number of General Errors:	<xsl:value-of select="."/> - 
				
			</xsl:for-each>
					<xsl:for-each select="NrOfGeneralWarnings">
				
					Number of General Warnings:		
					<xsl:value-of select="."/>
					</xsl:for-each>
				</b>
			</i>
		</font>
	</xsl:template>
	<xsl:template match="DirectoryRecord">
		<xsl:param name="ISSEQ"/>
		<br/>
		<b>DIRECTORY RECORD: <xsl:value-of select="@Index"/> (<xsl:value-of select="@Type"/>)</b>
		<br/>
		<xsl:for-each select="ValidationResults">
			<xsl:for-each select="ValidationDirectoryRecord">
				<table border="1" width="100%" cellpadding="3">
					<font color="#000080">
						<tr>
							<td align="center" valign="top" class="item" colspan="6">
								<b>	Directory Attributes </b>
							</td>
						</tr>
						<tr>
							<td valign="top" width="120" class="item">Attribute</td>
							<td valign="top" width="10" class="item">VR</td>
							<td valign="top" width="10" class="item">Type</td>
							<td valign="top" width="10" class="item">Pr</td>
							<td valign="top" width="150" class="item">Attribute Name</td>
							<td valign="top" class="item">Value(s) and Comments</td>
						</tr>
						<xsl:call-template name="VALAttributes">
							<xsl:with-param name="VALISSEQ">	</xsl:with-param>
						</xsl:call-template>
					</font>
				</table>
				<xsl:apply-templates/>
			</xsl:for-each>
			<xsl:apply-templates/>
		</xsl:for-each>
		<xsl:apply-templates/>
		<xsl:for-each select="ReferencedFile">
			<xsl:for-each select="ValidationDirectoryRecord">
				<xsl:call-template name="objectresults"/>
			</xsl:for-each>
		</xsl:for-each>
	</xsl:template>
</xsl:stylesheet>
