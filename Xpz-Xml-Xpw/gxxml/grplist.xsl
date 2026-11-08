<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxml="urn:schemas-microsoft-com:xslt"
	xmlns:gx="urn:shemas-artech-com:gx"
	exclude-result-prefixes="msxml gx">

<xsl:include href="include.xsl"/>

<xsl:template match="/">

<html>
	<xsl:call-template name="HtmlHeader"/>
<body>
	<xsl:call-template name="HtmlScript"/>
	<form action="" id="FORM1" method="post" name="FORM1">
		<TABLE width="100%">
		<xsl:for-each select="ObjSpecs/GroupList">
			<TR><TD><xsl:apply-templates select="."/></TD></TR>
		</xsl:for-each>
		</TABLE>
	</form>
</body>
</html>
</xsl:template>

<xsl:template match="GroupList">
	<xsl:for-each select="Composition">
		<xsl:call-template name="GenerateTable">
			<xsl:with-param name="ID">Groups</xsl:with-param>
			<xsl:with-param name="Title">Group <xsl:text> </xsl:text><xsl:apply-templates select="../Object/ObjName"/></xsl:with-param>
			<xsl:with-param name="HeaderMain">true</xsl:with-param>
		</xsl:call-template>
	</xsl:for-each>
	<xsl:if test="not(Composition/GroupElement)">
		<TR><TD colspan="3"><B>Empty Group</B></TD></TR>
	</xsl:if>
</xsl:template>

<xsl:template match="Composition" mode="TableRow">
	<xsl:apply-templates/>
</xsl:template>

<xsl:template match="ObjName">
	
	<xsl:if test="../ObjId[.= 0]">
		*None
	</xsl:if>
	<xsl:value-of select="."/>
</xsl:template>

<xsl:template match="GroupElement" mode="TableRow">
	<TD><xsl:apply-templates select="SubType"/></TD><TD ><xsl:apply-templates select="ElementType"/></TD>
	<TD><xsl:apply-templates select="SuperType"/></TD>
</xsl:template>

<xsl:template match="SubType|SuperType">
	<xsl:apply-templates select="Attribute"/>
</xsl:template>

<xsl:template match="ElementType">
	<xsl:value-of select="."/>
</xsl:template>

</xsl:stylesheet>