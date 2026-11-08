<?xml version='1.0' ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:gx="urn:shemas-artech-com:gx">
	<xsl:output method="html" encoding="iso-8859-1" />
	<xsl:template match="/">
		<html>
			<head>
			</head>
			<body>
				<form>
					<table width="100%" border="0">
						<xsl:for-each select="/GeneXusClasses/Classes">
							<tr>
								<td style="font-size:70%">
									<xsl:apply-templates select="." mode="m1" />
								</td>
							</tr>
						</xsl:for-each>
					</table>
				</form>
			</body>
		</html>
</xsl:template>
	
<xsl:template match="CExternalClassDefinition" mode="m1">
	<xsl:apply-templates select="Methods/CMethodDefinition" mode="m1" />
</xsl:template>
	
<xsl:template match="Methods/CMethodDefinition" mode="m1">
	<br />
	<table cellSpacing="1" border="0" cellPadding="1">
		<tr>
			<td valign="top">
				<xsl:value-of select="Type" />
				<xsl:text>  </xsl:text>
				<Strong>
					<xsl:value-of select="Name/text()" />
				</Strong>
				<Font size="3">
					<xsl:apply-templates select="Parameters" mode="m1" />
				</Font>
			</td>
		</tr>
	</table>
</xsl:template>
	
<xsl:template match="Parameters" mode="m1">
	(<xsl:text> </xsl:text>
	<xsl:for-each select="CParamDefinition">
			<xsl:text> </xsl:text>
				<xsl:value-of select="Type" />
			<xsl:text> </xsl:text>
			<i><xsl:value-of select="Name" /></i>
			<xsl:if test="position() != last()">, </xsl:if>
	</xsl:for-each>
	)<xsl:text> </xsl:text>
	<br />
</xsl:template>
</xsl:stylesheet>
