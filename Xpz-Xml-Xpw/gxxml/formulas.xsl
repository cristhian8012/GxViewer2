<?xml version='1.0' encoding='iso-8859-1'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">



<!-- FORMULAS TEMPLATES -->

<!-- AGGREGATE SELECT TEMPLATE -->
<xsl:template match="AggSelFormulas">
	<xsl:if test="Formula">
	<table>
	<TR class="textoGrisTabla"><TD> <FONT class="gxsource">Formulas:</FONT></TD></TR>
	<TR class="textoGrisTabla"><TD>
	<xsl:for-each select="Formula">
		<B>Navigation to evaluate: </B><xsl:value-of select="FormulaName"/>
		<xsl:value-of select="FormulaType"/><xsl:apply-templates select="FormulaOutputAttri"/>)<DIV/>
		<OL>
		Where: <xsl:apply-templates select="FormulaWhere"/><DIV/>
		Given: <xsl:apply-templates select="FormulaGivenAttris"/><DIV/>
		Index: <xsl:value-of select="FormulaIndex"/><DIV/>
		Start From: <UL><xsl:apply-templates select="StartFrom"/></UL>
		Loop While: <UL><xsl:apply-templates select="LoopWhile"/></UL><DIV/>
		Returning: <xsl:apply-templates select="FormulaReturnAttri"/><DIV/>
		</OL>
		<xsl:apply-templates select="NavigationTree"/>
	</xsl:for-each>
	</TD></TR>
	</table>
	</xsl:if>
</xsl:template>
<!-- END AGGREGATE SELECT TEMPLATE -->

<!-- VERTICAL FORMULAS-->
<xsl:template match="FormulaWhere">
	<xsl:call-template name="ProcessList"/>
</xsl:template>

<xsl:template match="VerticalFormulas">
	<xsl:if test="VerticalFormulaGroup">
	<table class="textoGrisTabla">
	<TR><TD> <FONT class="gxsource">Formulas:</FONT></TD></TR>
	<TR><TD>
	<xsl:for-each select="VerticalFormulaGroup">
		<b> Navigation to evaluate: </b><xsl:apply-templates select="FormulasInGroup"/>
		<xsl:apply-templates select="NavigationTree"/>
	</xsl:for-each>
	</TD></TR>
	</table>
	</xsl:if>	
</xsl:template>

<xsl:template match="FormulaOutputAttri">
	<xsl:call-template name="ProcessList">
		<xsl:with-param name="Sep">,</xsl:with-param>
	</xsl:call-template>
</xsl:template>

<xsl:template match="FormulasInGroup">
	<xsl:call-template name="ProcessList">
		<xsl:with-param name="Sep">,</xsl:with-param>
	</xsl:call-template>
</xsl:template>

<!-- REDUNDANTFORMULAS -->
<xsl:template match="RedundantFormulas">
	<FONT class="gxformtitle">Redundant Formulas:</FONT>
	<xsl:apply-templates select="FormulaToUpdate"/>
</xsl:template>

<xsl:template match="FormulaToUpdate">
	<xsl:apply-templates/>
</xsl:template>


</xsl:stylesheet>
