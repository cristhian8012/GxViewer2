<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:output method="text"/>

<xsl:template match="/">
/*
 * Automatically generated using gxdatatypes.xsl
 */
<xsl:apply-templates select="//Classes" mode="definition"/>
<xsl:apply-templates select="//Classes" mode="properties"/>
<xsl:apply-templates select="//Classes" mode="methods"/>
<xsl:apply-templates select="//Classes" mode="interfaces"/>
</xsl:template>

<!-- Se declaran las clases -->

<xsl:template match="Classes" mode="definition">
<xsl:for-each select="CExternalClassDefinition[Modifiers/CModifierDefinition[Name='specname']]">
dt_i('<xsl:value-of select="Modifiers/CModifierDefinition[Name='specname']/Values/Value"/>', ['<xsl:value-of select="Name"/>']).<xsl:text>
</xsl:text>
</xsl:for-each>
</xsl:template>

<!-- Process Properties -->

<xsl:template match="Classes" mode="properties">
<xsl:for-each select="CExternalClassDefinition[Modifiers/CModifierDefinition[Name='specname']]">
<xsl:call-template name="ProcessClassProperty"><xsl:with-param name='ClassName'><xsl:value-of select="Modifiers/CModifierDefinition[Name='specname']/Values/Value"/></xsl:with-param></xsl:call-template>
</xsl:for-each>
</xsl:template>

<xsl:template name="ProcessClassProperty">
<xsl:param name="ClassName"/>
<xsl:for-each select="Properties/CPropertyDefinition">
dt_property_i('<xsl:value-of select="$ClassName"/>', '<xsl:call-template name="NormalizeProperty"><xsl:with-param name="PropName"><xsl:value-of select="Name"/></xsl:with-param></xsl:call-template>', <xsl:call-template name="SpecifierName"><xsl:with-param name="TypeName"><xsl:value-of select="Type"/></xsl:with-param></xsl:call-template>, <xsl:call-template name="BooleanValue"><xsl:with-param name='ModifierName'><xsl:value-of select="'readonly'"/></xsl:with-param></xsl:call-template>).<xsl:text>
</xsl:text>
</xsl:for-each>
</xsl:template>

<!-- Process Methods -->

<xsl:template match="Classes" mode="methods">
<xsl:for-each select="CExternalClassDefinition[Modifiers/CModifierDefinition[Name='specname']]">
<xsl:call-template name="ProcessClassMethod"><xsl:with-param name='ClassName'><xsl:value-of select="Modifiers/CModifierDefinition[Name='specname']/Values/Value"/></xsl:with-param></xsl:call-template>
</xsl:for-each>
</xsl:template>

<xsl:template name="ProcessClassMethod">
<xsl:param name="ClassName"/>
	<xsl:for-each select="Methods/CMethodDefinition">
dt_method_i('<xsl:value-of select="$ClassName"/>', $<xsl:call-template name="MakeLower"><xsl:with-param name="ToLower"><xsl:value-of select="Name"/></xsl:with-param></xsl:call-template>($, <xsl:call-template name="SpecifierName"><xsl:with-param name="TypeName"><xsl:value-of select="Type"/></xsl:with-param></xsl:call-template>, [<xsl:call-template name="ProcessMethodParameters"/>]).<xsl:text>
</xsl:text>
	</xsl:for-each>
</xsl:template>

<xsl:template name="ProcessMethodParameters">
	<xsl:variable name="ChildCount" select="count(Parameters/CParamDefinition)"/>
	<xsl:for-each select="Parameters/CParamDefinition">
		<xsl:call-template name="SpecifierName">
			<xsl:with-param name="TypeName">
				<xsl:value-of select="Type"/>
			</xsl:with-param>
		</xsl:call-template>
		<xsl:if test="$ChildCount != position()">, </xsl:if>
	</xsl:for-each>
</xsl:template>

<!-- -->

<xsl:template match="Classes" mode="interfaces">
<xsl:for-each select="CExternalClassDefinition[Modifiers/CModifierDefinition[Name='interface']]">
dt_interface_i('<xsl:value-of select="Modifiers/CModifierDefinition[Name='specname']/Values/Value"/>'<xsl:call-template name="InterfaceParameters"/>).<xsl:text>
</xsl:text>
</xsl:for-each>
</xsl:template>

<xsl:template name="InterfaceParameters">
<xsl:for-each select="Modifiers/CModifierDefinition[Name='interface']/Values/Value">, '<xsl:value-of select="text()"/>'</xsl:for-each>
</xsl:template>

<!--
 Gets the specifier name
 -->
<xsl:template name="SpecifierName">
<xsl:param name="TypeName"/>
<xsl:variable name="NormalizedName">
	<xsl:call-template name="MakeLower">
		<xsl:with-param name="ToLower">
			<xsl:value-of select="$TypeName"/>
		</xsl:with-param>
	</xsl:call-template>
</xsl:variable>
<xsl:choose>
	<xsl:when test="ancestor::*//CExternalClassDefinition[Name=$TypeName]/Modifiers/CModifierDefinition[Name='specname']/Values/Value"><xsl:call-template name="MakeLower"><xsl:with-param name="ToLower"><xsl:value-of select="ancestor::*//CExternalClassDefinition[Name=$TypeName]/Modifiers/CModifierDefinition[Name='specname']/Values/Value"/></xsl:with-param></xsl:call-template></xsl:when>
	<!-- Map names -->
	<xsl:when test="$NormalizedName = 'short'">int</xsl:when>
	<xsl:when test="$NormalizedName = 'void'">[]</xsl:when>
	<xsl:when test="$NormalizedName != ''"><xsl:value-of select="$NormalizedName"/></xsl:when>
	<!-- Default -->
	<xsl:otherwise>[]</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--  -->
<xsl:template name="BooleanValue">
<xsl:param name="ModifierName"/>
	<xsl:choose>
		<xsl:when test="Modifiers/CModifierDefinition[Name=$ModifierName]">1</xsl:when>
		<xsl:otherwise>0</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--
 Normalize properties names: lower case except first letter
 -->
<xsl:template name="NormalizeProperty">
<xsl:param name="PropName"/>
	<xsl:call-template name="MakeUpper">
		<xsl:with-param name="ToUpper">
			<xsl:value-of select="substring($PropName, 1, 1)"/>
		</xsl:with-param>
	</xsl:call-template>
	<xsl:call-template name="MakeLower">
		<xsl:with-param name="ToLower">
			<xsl:value-of select="substring($PropName, 2, string-length($PropName)-1)"/>
		</xsl:with-param>
	</xsl:call-template>
</xsl:template>

<!--  -->
<xsl:template name="MakeLower">
<xsl:param name="ToLower"/>
	<xsl:value-of select="translate($ToLower, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')"/>
</xsl:template>

<xsl:template name="MakeUpper">
<xsl:param name="ToUpper"/>
	<xsl:value-of select="translate($ToUpper, 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
</xsl:template>

</xsl:stylesheet>
