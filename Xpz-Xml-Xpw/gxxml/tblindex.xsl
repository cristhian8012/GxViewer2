<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxml="urn:schemas-microsoft-com:xslt"
	xmlns:gx="urn:schemas-artech-com:gx"
	exclude-result-prefixes="msxml gx">

<xsl:include href="include.xsl"/>

<xsl:template match="/">

<html>
	<xsl:call-template name="HtmlHeader"/>
<body>
	<xsl:call-template name="HtmlScript"/>
	<form action="" id="FORM1" method="post" name="FORM1">
		<DIV><TABLE width="100%">
			<TR><TD><xsl:apply-templates select="//TblIdxs"/></TD></TR>
		</TABLE></DIV>
	</form>
</body>
</html>
</xsl:template>

<!-- TABLEIDXS TEMPLATE -->
<xsl:template match="TblIdxs">
	<xsl:call-template name="GenerateTable">
		<xsl:with-param name="ID">ListTableIndices</xsl:with-param>
		<xsl:with-param name="Title">Indices</xsl:with-param>
	</xsl:call-template>
</xsl:template>


<!-- INDEX TEMPLATE -->
<xsl:template match="Index" mode="TableRow">
	<TD><xsl:value-of select="IdxName"/></TD>
	<TD><xsl:value-of select="IdxType"/></TD>
	<TD nowrap="yes"><xsl:apply-templates select="IdxAtts"/></TD>
</xsl:template>


<!-- IndexAtts TEMPLATE -->
<xsl:template match="IdxAtts">
	<TABLE cellspacing="0" cellpadding="0">
	<TR class="textoGrisTabla">
	<TD noWrap="yes">
		<xsl:for-each select="IdxAtt">
			<xsl:if test="position() != 1">, </xsl:if>
			<xsl:apply-templates select="Attribute"/>
			<xsl:apply-templates select="IdxOrder"/>
		</xsl:for-each>
	</TD>
	</TR>
	</TABLE>
</xsl:template>	


<xsl:template match="IdxOrder">
	<xsl:if test="text()[.='D']">
		<xsl:text> </xsl:text>
		<xsl:call-template name="RenderImage">
			<xsl:with-param name="Id">IdxDescending</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
</xsl:template>

<xsl:template match="text()|@*"><xsl:value-of select="."/></xsl:template>


</xsl:stylesheet>
