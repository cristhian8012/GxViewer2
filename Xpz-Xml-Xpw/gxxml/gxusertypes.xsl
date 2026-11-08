<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:output method="text"/>

<xsl:template match="/">
	<xsl:apply-templates select="//Classes" mode="m0"/>
	<xsl:apply-templates select="//Classes" mode="m1"/>
</xsl:template>

<!-- Se declaran las clases -->

<xsl:template match="Classes" mode="m0">
<xsl:for-each select="CExternalClassDefinition"><xsl:call-template name="procesclass"><xsl:with-param name='Class'>'<xsl:value-of select="Modifiers/CModifierDefinition[Name='friendlynamespace']/Values/Value"/>', '<xsl:value-of select="Name"/>'</xsl:with-param></xsl:call-template></xsl:for-each>
</xsl:template>

<xsl:template name="procesclass">
<xsl:param name="Class"/>	usr_dtype_i(<xsl:value-of select="$Class"/>).
<xsl:for-each select="Modifiers/CModifierDefinition"><xsl:call-template name="procesclassmodifier"><xsl:with-param name='Class'><xsl:value-of select="$Class"/></xsl:with-param></xsl:call-template></xsl:for-each>
</xsl:template>


<xsl:template name="procesclassmodifier">
<xsl:param name="Class"/>	usr_dtype_i(<xsl:value-of select="$Class"/>, '<xsl:value-of select="Name"/>', [<xsl:for-each select="Values">'<xsl:value-of select="Value"/>'<xsl:if test="position() != last()">,</xsl:if></xsl:for-each>]).
</xsl:template>

<!-- Se declaran los metodos -->

<xsl:template match="Classes" mode="m1">
	<xsl:for-each select="CExternalClassDefinition"><xsl:call-template name="procesclass2"><xsl:with-param name='Class'>'<xsl:value-of select="Modifiers/CModifierDefinition[Name='friendlynamespace']/Values/Value"/>', '<xsl:value-of select="Name"/>'</xsl:with-param></xsl:call-template></xsl:for-each>
</xsl:template>


<xsl:template name="procesclass2">
<xsl:param name="Class"/>
	<!-- Informacion de la clase -->
<xsl:call-template name="procesmethods"><xsl:with-param name='Class'><xsl:value-of select="$Class"/></xsl:with-param></xsl:call-template>
<xsl:call-template name="procesproperties"><xsl:with-param name='Class'><xsl:value-of select="$Class"/></xsl:with-param></xsl:call-template>
<xsl:call-template name="procesmethods_2"><xsl:with-param name='Class'><xsl:value-of select="$Class"/></xsl:with-param></xsl:call-template>
</xsl:template>

<!-- Se declaran los parametros -->

<xsl:template name="procesproperties">
<xsl:param name="Class"/>
<xsl:for-each select="Properties/CPropertyDefinition">	usr_dtype_property_i(<xsl:value-of select="$Class"/>, <xsl:number value="position()" format="1"/>, '<xsl:value-of select="Name"/>', <xsl:value-of select="Modifiers/CModifierDefinition[Name='PrologType']/Values/Value"/>).
</xsl:for-each>
</xsl:template>

<xsl:template name="procesmethods">
<xsl:param name="Class"/>
<xsl:for-each select="Methods/CMethodDefinition">	usr_dtype_method_i(<xsl:value-of select="$Class"/>, <xsl:number value="position()" format="1"/>, '<xsl:value-of select="Name"/>', <xsl:value-of select="Modifiers/CModifierDefinition[Name='PrologType']/Values/Value"/>).
	<xsl:for-each select="MethodModifiers/Modifiers/CModifierDefinition">
	<xsl:call-template name="methodmodifier_2"><xsl:with-param name='Class'><xsl:value-of select="$Class"/></xsl:with-param>
		<xsl:with-param name='Method'>'<xsl:value-of select="../../../Name"/>'</xsl:with-param>
		</xsl:call-template>
	</xsl:for-each>
</xsl:for-each>
</xsl:template>

<xsl:template match="Modifiers" mode="m1">,[<xsl:for-each select="CModifierDefinition"><xsl:apply-templates select="." mode="m1"/><xsl:if test="position() != last()">,</xsl:if></xsl:for-each>]</xsl:template>

<xsl:template match="CModifierDefinition" mode="m1"><xsl:if test="contains('inout', Name)"><xsl:value-of select="Name"/></xsl:if></xsl:template>

<!-- Modo 2 (Se declaran los parametros -->

<xsl:template name="procesmethods_2">
<xsl:param name="Class"/>
<xsl:for-each select="Methods/CMethodDefinition"><xsl:call-template name="procesmethod_2"><xsl:with-param name='Class'><xsl:value-of select="$Class"/></xsl:with-param><xsl:with-param name='Method'><xsl:number value="position()"/></xsl:with-param></xsl:call-template></xsl:for-each>
</xsl:template>

<xsl:template name="procesmethod_2">
<xsl:param name="Class"/>
<xsl:param name="Method"/><xsl:for-each select="Parameters/CParamDefinition">
	<xsl:call-template name="procesparameter_2">
		<xsl:with-param name='Class'><xsl:value-of select="$Class"/></xsl:with-param>
		<xsl:with-param name='Method'><xsl:value-of select="$Method"/></xsl:with-param>
		<xsl:with-param name='Param'><xsl:number value="position()"/></xsl:with-param>
 		<xsl:with-param name='PrologType'><xsl:value-of select="Modifiers/CModifierDefinition[Name='PrologType']/Values/Value"/></xsl:with-param>
	</xsl:call-template></xsl:for-each>
</xsl:template>

<xsl:template name="procesparameter_2">
<xsl:param name="Class"/>
<xsl:param name="Method"/>
<xsl:param name="Param"/>	
<xsl:param name="PrologType"/>	usr_dtype_parameter_i(<xsl:value-of select="$Class"/>, <xsl:value-of select="$Method"/>, <xsl:value-of select="$Param"/>, name, '<xsl:value-of select="Name"/>').
	usr_dtype_parameter_i(<xsl:value-of select="$Class"/>, <xsl:value-of select="$Method"/>, <xsl:value-of select="$Param"/>, type, <xsl:value-of select="$PrologType"/>).
<xsl:for-each select="Modifiers/CModifierDefinition">
	<xsl:call-template name="procesparammodifier_2">
	<xsl:with-param name='Class'><xsl:value-of select="$Class"/></xsl:with-param>
	<xsl:with-param name='Method'><xsl:value-of select="$Method"/></xsl:with-param>
	<xsl:with-param name='Param'><xsl:number value="$Param"/></xsl:with-param>
	</xsl:call-template>
</xsl:for-each>
</xsl:template>

<xsl:template name="procesparammodifier_2">
<xsl:param name="Class"/>
<xsl:param name="Method"/>
<xsl:param name="Param"/>usr_dtype_parameter_i(<xsl:value-of select="$Class"/>, <xsl:value-of select="$Method"/>, <xsl:value-of select="$Param"/>, modifier, ['<xsl:value-of select="Name"/>'<xsl:if test="Values/Value">,</xsl:if> <xsl:for-each select="Values/Value">'<xsl:value-of select="."/>'<xsl:if test="position() != last()">,</xsl:if></xsl:for-each>]).
</xsl:template>

<xsl:template name="methodmodifier_2">
<xsl:param name="Class"/>
<xsl:param name="Method"/>	usr_dtype_method_i(<xsl:value-of select="$Class"/>, <xsl:value-of select="$Method"/>, modifier, ['<xsl:value-of select="Name"/>', <xsl:for-each select="Values/Value">'<xsl:value-of select="."/>'<xsl:if test="position() != last()">,</xsl:if></xsl:for-each>]).
</xsl:template>

</xsl:stylesheet>
