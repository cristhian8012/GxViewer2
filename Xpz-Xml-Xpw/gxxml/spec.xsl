<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">


<xsl:template match="/">
	<html>
		<xsl:call-template name="HtmlHeader"/>
		<body bgcolor="#FFFFFF" text="#000000" link="#000000" alink="#000000" vlink="#000000" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
			<xsl:call-template name="HtmlScript"/>

			<form action="" id="FORM1" method="post" name="FORM1">
				<table width="100%" border="0" cellspacing="4">
				<xsl:for-each select="ObjSpecs">
					<xsl:if test="position() != 1">
						<TR><TD><xsl:text> </xsl:text></TD></TR>
					</xsl:if>
					<xsl:if test="ReorgSpec">
					<TR>
						<TD><xsl:apply-templates select="ReorgSpec"/><BR/></TD>
					</TR>
					</xsl:if>
					<xsl:if test="ObjectSpec">
					<TR>
						<TD><xsl:apply-templates select="ObjectSpec"/><BR/></TD>
					</TR>
					</xsl:if>
				</xsl:for-each>
				</table>
			</form>
		</body>
	</html>
</xsl:template>

<xsl:include href="include.xsl"/>
<xsl:include href="reorg.xsl"/>
<xsl:include href="formulas.xsl"/>
<xsl:include href="genexusSpecific.xsl"/>



<!-- FROM FORMULA TEMPLATE -->
<xsl:template match="FromFormula|FromValue">
	<xsl:apply-templates select="*"/>
</xsl:template>


<!-- RULES TEMPLATES -->

<!-- RULES-->
<xsl:template match="StandAloneRules|StandAloneWithModeRules|BaseTableRule|AfterConfirmRules|AfterInsertRules|AfterUpdateRules">
	<table>
		<xsl:apply-templates select="Action"></xsl:apply-templates>
	</table>
</xsl:template>

<xsl:template match="AfterDeleteRules|AfterTrnRules|AfterLevelRules|NotIncludedRules">
	<table><xsl:apply-templates select="Action"/></table>
</xsl:template>

<xsl:template match="Rules">
	<TR class="textoGrisTabla"><TD> 
		<table><xsl:apply-templates select="Action"/></table>
	</TD></TR>
	<xsl:if test="NonTriggeredActions/Action">
		<xsl:call-template name="TitledRules">
			<xsl:with-param name="Title">No Triggered Actions</xsl:with-param>
			<xsl:with-param name="Node">NonTriggeredActions</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
</xsl:template>

<xsl:template match="NonTriggeredActions">
	<xsl:apply-templates/>
</xsl:template>
<!-- END RULES -->


<!-- ACTION TEMPLATE -->
<xsl:template match="Action">
<table cellspacing="0" cellpadding="0" class="textoGrisTabla">
	<xsl:choose>
		<xsl:when test="ActionType[.='Formula']">
			<TR>
				<TD colspan="2"><xsl:apply-templates select="FormulaName"/></TD>
				<TD><xsl:text> </xsl:text><xsl:apply-templates select="FormulaExpression"/></TD>
			</TR>
		</xsl:when>
		<xsl:when test="ActionType[.='ReadTable' or .='ReadBaseTable']">
			<TR>
				<TD><xsl:text>READ  </xsl:text></TD>
				<TD><xsl:apply-templates select="Table"/></TD>
				<TD/>
			</TR>
			<TR>
				<TD/>
				<TD><xsl:text>WHERE </xsl:text></TD>
				<TD/>
			</TR>
			<TR><TD/><TD/></TR>
			<TR >
				<TD/>
				<TD/>
				<TD><xsl:apply-templates select="JoinConditions"/></TD>
			</TR>
			<xsl:if test="Into/Attribute">
				<TR>
					<TD/>
					<TD valign="top"><xsl:text>INTO </xsl:text></TD>
					<TD><xsl:apply-templates select="Into"/></TD>
				</TR>
			</xsl:if>
		</xsl:when>
		<xsl:when test="ActionType[.='BusinessRule']">
			<TR><TD>
				<xsl:choose>
					<xsl:when test="CALL">
						<xsl:apply-templates select="CALL"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:apply-templates select="RuleType"/>
						(
						<xsl:apply-templates select="Parameters"/>
						)
						<xsl:if test="Condition/*">
							IF <xsl:apply-templates select="Condition"/>
						</xsl:if>
						<BR/>	
					</xsl:otherwise>
				</xsl:choose>
			</TD></TR>
		</xsl:when>
		<xsl:when test="ActionType[.='SubType']">
			<TR><TD>
				<table cellspacing="0" cellpadding="0" class="textoGrisTabla">
					<xsl:apply-templates select="SubtypeAction"/>
				</table>
			</TD></TR>
		</xsl:when>
		<xsl:when test="ActionType[.='VerticalFormulas']">
			<TR><TD>
				<FONT class="textoGrisTabla"><B>
					<xsl:if test="not($Product!='Deklarit')">VERTICAL</xsl:if>
					FORMULAS:
				</B></FONT>
				<xsl:apply-templates select="VerticalFormulasToCalc"/>
			</TD></TR>
		</xsl:when>
		<xsl:when test="ActionType[.='ReadCKey']">
			<TR>
				<TD><xsl:text>UNIQUE </xsl:text></TD>
				<TD><xsl:apply-templates select="Table"/></TD>
				<TD/>
			</TR>
			<TR>
				<TD/>
				<TD><xsl:text>WHERE </xsl:text></TD>
				<TD/>
			</TR>
			<TR>
				<TD/>
				<TD/>
				<TD><xsl:apply-templates select="JoinConditions"/></TD>
			</TR>
			<xsl:if test="Into/Attribute">
				<TR>
					<TD/>
					<TD><xsl:text>INTO </xsl:text></TD>
					<TD><xsl:apply-templates select="Into"/></TD>
				</TR>
			</xsl:if>
		</xsl:when>
		<xsl:when test="RuleType[.='ErrorRule']">
			<TR><TD>
			<FONT class="gxsource">Error</FONT>
			(
				<xsl:apply-templates select="Parameters"/>
			)
			</TD><TD>
			if
			</TD><TD>
			<xsl:text> </xsl:text>
			<xsl:apply-templates select="Condition"/>
			</TD></TR>
		</xsl:when>
		<xsl:otherwise>
			<TR><TD><xsl:apply-templates/></TD></TR>
		</xsl:otherwise>
	</xsl:choose>
</table>
</xsl:template>

<!-- SUBTYPEACTION TEMPLATE -->
<xsl:template match="SubtypeAction">
	<xsl:value-of select="Supertype"/> = <xsl:value-of select="Subtype"/>
</xsl:template>

<xsl:template match="JoinLocation">
	<xsl:value-of select="."/>
</xsl:template>


<!-- RULE TYPE TEMPLATE -->
<xsl:template match="RuleType">
	<xsl:choose>
		<xsl:when test="text()[.='NoacceptRule']">
			NoAccept	
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="."/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template match="Into|VerticalFormulasToCalc">
	<xsl:call-template name="ProcessList"/>
<!--	<xsl:for-each select="Attribute">
		<xsl:if test="position() != 1">, </xsl:if> 
		<xsl:apply-templates select="."/>
	</xsl:for-each>-->
</xsl:template>

<!-- JOIN CONDITIONS -->
<xsl:template match="JoinConditions">
	<xsl:for-each select="JoinCondition">
		<xsl:apply-templates select="../Table"/>.<xsl:apply-templates select="AttributeTo"/>
		=
		<xsl:if test="Table/TableName"><xsl:apply-templates select="Table"/>.</xsl:if>
		<xsl:apply-templates select="AttributeFrom"/>
		<br/>
	</xsl:for-each>
	<xsl:apply-templates select="//JoinLocation"/>
	<xsl:apply-templates select="..//JoinLocation"/>
</xsl:template>


<!-- END JOIN CONDITIONS -->

<!-- END RULE TEMPLATES -->



<xsl:template match="BaseTable">
	<xsl:apply-templates select="Table"/>
</xsl:template>

<xsl:template match="Generator">
	<table class="textoGrisTabla">
	<TR>
		<TD><xsl:apply-templates select="GenId"/></TD>
		<TD class="textoGrisTabla"><xsl:value-of select="GenName"/></TD>
	</TR>
	</table>
</xsl:template>	

<!--  Standard Templates  -->




<xsl:template match="ObjClsName">
	<xsl:variable name="Classes" select="document('classes.xml')"/>
	<xsl:variable name="ClassName"><xsl:value-of select="."/></xsl:variable>
	<xsl:choose>
		<xsl:when test="$Classes//Class[Name=$ClassName]/MapedName">
			<xsl:value-of select="$Classes//Class[Name=$ClassName]/MapedName"/>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="."/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="MainInfo">
<table class="textoGrisTabla" border="0" cellPadding="0" cellSpacing="0" width="100%">
  <TR class="textoGrisTabla">
	<TD>Name</TD>
	<TD><xsl:apply-templates select="Object" mode="icon"/></TD>
  </TR>
  <xsl:call-template name="EndRow"/>
  <TR class="textoGrisTabla">
	<TD>Description</TD>
	<TD>
		<xsl:value-of select="Object/ObjDesc"/>
	</TD>
  </TR>
  <xsl:call-template name="EndRow"/>
  <xsl:if test="Result!='genreq'">
	  <TR class="textoGrisTabla">
		<TD>Status</TD>
		<TD>
			<xsl:apply-templates select="Result"/>
		</TD>
	  </TR>
  </xsl:if>
  <xsl:call-template name="EndRow"/>
  <xsl:if test="$Product='Deklarit'">
	<xsl:if test="Parameters/*">
		<TR class="textoGrisTabla">
			<TD>Parameters</TD>
			<TD><xsl:apply-templates select="Parameters"/></TD>
		</TR>
		<xsl:call-template name="EndRow"/>
	</xsl:if>
  </xsl:if>
  <xsl:if test="not($Product='Deklarit')">
	<xsl:call-template name="EndRow"/>
	<xsl:if test="OutputDevices">
		<TR class="textoGrisTabla">
			<TD>Output Devices</TD>
			<TD><xsl:apply-templates select="OutputDevices"/></TD>
		</TR>
		<xsl:call-template name="EndRow"/>
	</xsl:if>
	<xsl:if test="Main">
		<TR class="textoGrisTabla"><TD>Main</TD><TD>Yes</TD></TR>
		<xsl:call-template name="EndRow"/>
	</xsl:if>
  </xsl:if>
</table>
</xsl:template>

<xsl:template name="Enviroment">
<table class="textoGrisTabla" border="0" cellPadding="0" cellSpacing="0" width="100%">
	<TR class="textoGrisTabla">
		<TD>Environment</TD><TD><xsl:apply-templates select="Generator"/></TD>
	</TR>
	<xsl:call-template name="EndRow"/>
	<TR class="textoGrisTabla">
		<TD>Spec. Version</TD>
		<TD><xsl:apply-templates select="SpecVersion"/></TD>
	</TR>
	<xsl:call-template name="EndRow"/>
	<xsl:if test="FormClass">
		<TR class="textoGrisTabla">
			<TD>Form Class</TD>
			<TD><xsl:value-of select="FormClass"/></TD>
		</TR>
		<xsl:call-template name="EndRow"/>
	</xsl:if>
	<xsl:if test="Object/ObjPgmName">
		<TR>
			<TD>Program Name</TD>
			<TD><xsl:value-of select="Object/ObjPgmName"/></TD>
		</TR>
		<xsl:call-template name="EndRow"/>
	</xsl:if>
	<xsl:if test="CallProtocol">
		<TR class="textoGrisTabla">
			<TD>Call Protocol</TD>
			<TD><xsl:value-of select="CallProtocol"/></TD>
		</TR>
		<xsl:call-template name="EndRow"/>
	</xsl:if>
	<xsl:if test="Parameters">
		<TR class="textoGrisTabla">
			<TD>Parameters</TD>
			<TD><xsl:apply-templates select="Parameters"/></TD>
		</TR>
		<xsl:call-template name="EndRow"/>
	</xsl:if>
</table>
</xsl:template>

<xsl:template name="Levels">
<!-- BEGIN LEVELS -->
<xsl:if test="Levels/Level">    
<TR><TD colspan="2">
	<table width="100%" class="textoGrisTabla">
		<TR><TD>
			<DIV>
				<xsl:call-template name="TableHeaderSubMain">
					<xsl:with-param name="id">level</xsl:with-param>
					<xsl:with-param name="title">Levels</xsl:with-param>
				</xsl:call-template>
				<DIV id="content">
					<xsl:apply-templates select="Levels"/>
				</DIV>
			</DIV>
		</TD></TR>
	</table>
</TD></TR>
</xsl:if>
<!-- END LEVELS -->
</xsl:template>


<xsl:template match="ObjectSpec">
  <DIV>
	  <xsl:call-template name="TableHeaderMain">
		<xsl:with-param name="id">Obj</xsl:with-param>
		<xsl:with-param name="title">
			<xsl:apply-templates select="Object/ObjClsName"/>
			<xsl:text> </xsl:text>
			<xsl:apply-templates select="Object" mode="xRef"/>
			<xsl:text> Navigation Report</xsl:text>
		</xsl:with-param>
	  </xsl:call-template>
	  <DIV id="content">
		<TABLE width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CDAAAD" style="border-collapse:collapse">
		<TR><TD>
			<table width="100%" cellpadding="0" cellspacing="0">
			<TR bordercolor="#CDAAAD" border="1" class="textoGrisTabla">
				<TD valign="top"><xsl:call-template name="MainInfo"/></TD>
				<TD valign="top">
					<xsl:if test="not($Product='Deklarit')">
						<xsl:call-template name="Enviroment"/>
					</xsl:if>
				</TD>
			</TR>
			<xsl:if test="Errors/Error">
				<TR><TD colspan="2">
						<xsl:apply-templates select="Errors"/>
				</TD></TR>
			</xsl:if>
			<xsl:if test="Warnings/Warning">
				<TR><TD colspan="2">
					<xsl:apply-templates select="Warnings"/>
				</TD></TR>
			</xsl:if>
			<xsl:if test="NotIncludedRules/Action">
				<TR class="textoGrisTabla">
					<TD><FONT class="titulosRosa">Rules not included</FONT></TD>
				</TR>
				<TR class="textoGrisTabla">
					<TD colspan="2"><xsl:apply-templates select="NotIncludedRules"/></TD>
				</TR>
			</xsl:if>

			<xsl:call-template name="Levels"/>

			<xsl:if test="not($Product='Deklarit')">
				<xsl:if test="MenuBar">
					<TR><TD colspan="2">
					<xsl:apply-templates select="MenuBar"/>
					</TD></TR>
				</xsl:if>

				<xsl:if test="MenuOptions">
					<TR><TD colspan="2">
					<xsl:apply-templates select="MenuOptions"/>
					</TD></TR>
				</xsl:if>
			</xsl:if>

			<xsl:if test="Event/CALL|Event/Level">
				<TR><TD colspan="2">
				<DIV><xsl:attribute name="id">nugget</xsl:attribute>
					<xsl:call-template name="TableHeader">
						<xsl:with-param name="id">nugget</xsl:with-param>
						<xsl:with-param name="title">Events</xsl:with-param>
					</xsl:call-template>
				<DIV id="content">
					<table width="100%" class="textoGrisTabla" cellspacing="0" cellpadding="0" style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
					<xsl:for-each select="Event">
						<TR class="textoGrisTabla"><TD>
							<xsl:apply-templates select="."/>
						</TD></TR>
						<xsl:call-template name="EndRow"/>
					</xsl:for-each>
					</table>
				</DIV>
				</DIV>
				</TD></TR>
			</xsl:if>

			<xsl:if test="ImplicitForEach">
				<xsl:for-each select="ImplicitForEach">
					<TR><TD colspan="2">
						<xsl:apply-templates select="."/>
					</TD></TR>
				</xsl:for-each>
			</xsl:if>

			<xsl:if test="not($Product='Deklarit')">
				<xsl:if test="Prompts">
				<TR><TD colspan="2">   
				<xsl:apply-templates select="Prompts"/>
				</TD></TR>
				</xsl:if>

				<xsl:if test="DynamicCombos/DynamicCombo">
					<TR><TD colspan="2">   
						<xsl:apply-templates select="DynamicCombos"/>
					</TD></TR>
				</xsl:if>
			</xsl:if>

			</table>
			</TD></TR>
		</TABLE>
	  </DIV>
  </DIV>
</xsl:template>


<xsl:template match="SpecVersion">
	<table cellpadding="0" cellspacing="0" class="textoGrisTabla">
		<TR><TD>
				<xsl:call-template name="RenderImage">
					<xsl:with-param name="Id">Spec</xsl:with-param>
				</xsl:call-template>
			</TD>
			<TD>
				<xsl:value-of select="."/>
			</TD>
		</TR>
	</table>
</xsl:template>



<xsl:template match="AttriName">
	<xsl:if test="position() != 1">, </xsl:if>
	<xsl:apply-templates/>
</xsl:template>


<xsl:template match="NavigationTree">
	<DIV>
		<UL><xsl:apply-templates select="Table" mode="Tree"/></UL>
	</DIV>
</xsl:template>

<xsl:template match="KeyAttributes">
	<I> ( 
	<xsl:for-each select="Attribute">
		<xsl:if test="position() != 1">, </xsl:if>
		<xsl:apply-templates select="."/>
	</xsl:for-each>
	 ) </I>
</xsl:template>



<!-- LEVEL TEMPLATE -->

<xsl:template match="Levels">
	<table align="right" width="100%" cellspacing="0" cellpadding="0">
		<xsl:for-each select="Level">
			<TR><TD><xsl:apply-templates select="."/></TD></TR>
		</xsl:for-each>
	</table>
</xsl:template>

<xsl:template match="Order">
	<xsl:choose> 
		<xsl:when test="Attribute">
			<xsl:for-each select="Attribute">
				<xsl:if test="position() != 1">, </xsl:if>
				<xsl:apply-templates select="."/>
				<xsl:apply-templates select="AttriOrder"/>
			</xsl:for-each>
		</xsl:when>
		<xsl:otherwise>None</xsl:otherwise>
	</xsl:choose> 
</xsl:template>

<xsl:template match="Optimizations">
	<xsl:if test="Optimization">
		<table width="100%" cellpadding="0" cellspacing="0">
			<TR class="textoGrisTabla" valign="top" align="left">
				<TD width="15%" >
					<FONT class="gxnavigfil">Optimizations:</FONT>
				</TD>
				<TD width="85%">
					<table width="100%" cellpadding="0" cellspacing="0">
						<xsl:for-each select="Optimization">
							<TR class="textoGrisTabla" valign="top" align="left">
								<xsl:apply-templates select="."/>
							</TR>
						</xsl:for-each>
					</table>
				</TD>
			</TR>
		</table>
	</xsl:if>
</xsl:template>

<xsl:template match="Optimization">
	<xsl:choose>
		<xsl:when test="Type[.='Delete']">Delete</xsl:when>
		<xsl:when test="Type[.='Aggregate']"><xsl:apply-templates select="Expression"/> </xsl:when>
		<xsl:when test="Type[.='InsertWSubSelect']">Insert with subselect</xsl:when>
		<xsl:otherwise>Unknown optimization</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template match="LevelOptions">
	<table width="100%" cellpadding="0" cellspacing="0">
		<TR class="textoGrisTabla" valign="top" align="left">
			<TD width="15%">
				<FONT class="gxnavigfil">Options:</FONT>
			</TD>
			<TD width="85%">
				<xsl:for-each select="LevelOption">
					<xsl:if test="position() != 1">, </xsl:if>
					<xsl:apply-templates select="."/>
				</xsl:for-each>
			</TD>
		</TR>
	</table>
</xsl:template>

<xsl:template match="ConditionalOrders">
<xsl:if test="ConditionalOrder">
<table width="100%" cellpadding="0" cellspacing="0">
<TR class="textoGrisTabla" valign="top" align="left">
	<TD width="15%">
		<FONT class="gxnavigfil">Order:</FONT>
	</TD>
	<TD width="85%">
	<table width="100%" cellpadding="0" cellspacing="0">
		<xsl:for-each select="ConditionalOrder">
			<TR class="textoGrisTabla">
				<td colspan="2">
					<xsl:apply-templates select="Order"/><xsl:text> </xsl:text>
					<xsl:choose> 
						<xsl:when test="Condition/*">
							<font class="gxwarn">WHEN<xsl:text> </xsl:text></font>
							<xsl:apply-templates select="Condition"/>
						</xsl:when>
						<xsl:otherwise>
							OTHERWISE
						</xsl:otherwise>
					</xsl:choose> 
				</td>
			</TR>
			<TR class="textoGrisTabla">
				<td/>
				<td>
					<xsl:call-template name="UsedIndex"/>
				</td>
			</TR>
		</xsl:for-each>
	</table>
	</TD>
</TR>
</table>
</xsl:if>
</xsl:template>

<xsl:template match="LevelType">
	<xsl:choose>
	<xsl:when test="text()[.='For First']">
		For First
	</xsl:when>
	<xsl:when test="text()[.='New']">
		New
	</xsl:when>
	<xsl:when test="text()[.='XNew']">
		XNew
	</xsl:when>
	<xsl:when test="text()[.='For Each']">
		For Each
	</xsl:when>
	<xsl:when test="text()[.='Break']">
		Break
	</xsl:when>
	<xsl:when test="text()[.='XFor Each']">
		XFor Each
	</xsl:when>
	<xsl:otherwise>
		Level
	</xsl:otherwise>
	</xsl:choose>
</xsl:template>


<xsl:template match="LevelBeginRow">
	<xsl:if test="$Product!='Deklarit'">
	 <xsl:text>  </xsl:text>(Line: <xsl:value-of select="."/>)
	</xsl:if>
</xsl:template>

<!-- LEVEL OR FOREACH -->

<xsl:template match="Level|ImplicitForEach">
<xsl:if test="BaseTable">
<DIV><xsl:attribute name="id">gx<xsl:value-of select="BaseTable/Table/TableId"/></xsl:attribute>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="id">gx<xsl:value-of select="BaseTable/Table/TableId"/></xsl:with-param>
		<xsl:with-param name="title">
			<xsl:apply-templates select="LevelType"/> 
			<xsl:apply-templates select="BaseTable"/> 
			<xsl:apply-templates select="LevelBeginRow"/> 
		</xsl:with-param>
	</xsl:call-template>
<DIV id="content">
<table  align="right" cellspacing="0" cellpadding="0"  width="98%" class="textoGrisTabla">
	<TR class="textoGrisTabla"><TD></TD></TR>
	<xsl:if test="SubordinateTo">
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="SubordinateTo"/></TD></TR>
	</xsl:if>
	<xsl:if test="LevelType[text()!='New']">
	<xsl:if test="Order">
		<TR class="textoGrisTabla"><TD>
			<table width="100%">
			<TR class="textoGrisTabla">
				<TD width="15%" valign="top" align="left">
					<FONT class="gxnavigfil">Order:</FONT>
				</TD>
				<TD width="85%">
				<table cellpadding="0" cellspacing="0">
					<TR class="textoGrisTabla">
						<td colspan="2">
							<xsl:apply-templates select="Order"/><xsl:text> </xsl:text>
						</td>
					</TR>
					<TR class="textoGrisTabla">
						<td/>
						<td>
							<xsl:call-template name="UsedIndex"/>
						</td>
					</TR>
				</table>
				</TD>
			</TR>
			</table>
		</TD></TR>
	</xsl:if>
	</xsl:if>
	<xsl:if test="LevelOptions/*">
		<TR class="textoGrisTabla"><TD><xsl:apply-templates select="LevelOptions"/></TD></TR>
	</xsl:if>
	<xsl:if test="ConditionalOrders">
		<TR class="textoGrisTabla"><TD><xsl:apply-templates select="ConditionalOrders"/></TD></TR>
	</xsl:if>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="OptimizedWhere"/></TD></TR>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="NonOptimizedWhere"/></TD></TR>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="Optimizations"/></TD></TR>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="StandAloneRules"/></TD></TR>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="BaseTableRule"/></TD></TR>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="StandAloneWithModeRules"/></TD></TR>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="//RedundantFormulas"/></TD></TR>
	<xsl:if test="AfterAttributeRules/Action">
		<xsl:call-template name="TitledRules">
			<xsl:with-param name="Title">After Attribute Rules</xsl:with-param>
			<xsl:with-param name="Node">AfterAttributeRules</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="Rules"/></TD></TR>
	<xsl:if test="AfterConfirmRules/Action">
		<xsl:call-template name="TitledRules">
			<xsl:with-param name="Title">After Confirm Rules</xsl:with-param>
			<xsl:with-param name="Node">AfterConfirmRules</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="NavigationTree"/></TD></TR>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="TablesToUpdate"/></TD></TR>
	<TR class="textoGrisTabla"><TD><table><TR><TD><xsl:apply-templates select="Formulas"/></TD></TR></table></TD></TR>
	<xsl:if test="AfterInsertRules/Action">
		<xsl:call-template name="TitledRules">
			<xsl:with-param name="Title">After Insert Rules</xsl:with-param>
			<xsl:with-param name="Node">AfterInsertRules</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
	<xsl:if test="AfterUpdateRules/Action">
		<xsl:call-template name="TitledRules">
			<xsl:with-param name="Title">After Update Rules</xsl:with-param>
			<xsl:with-param name="Node">AfterUpdateRules</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
	<xsl:if test="AfterDeleteRules/Action">
		<xsl:call-template name="TitledRules">
			<xsl:with-param name="Title">After Delete Rules</xsl:with-param>
			<xsl:with-param name="Node">AfterDeleteRules</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="TablesToControlOnDelete"/></TD></TR>
	<TR class="textoGrisTabla"><TD><xsl:apply-templates select="Levels"/></TD></TR>
	<TR class="textoGrisTabla"><TD>
		<table align="right" width="100%" cellspacing="0" cellpadding="0">
			<xsl:for-each select="Level">
				<tr><td><xsl:apply-templates select="."/></td></tr>
			</xsl:for-each>
		</table>
	</TD></TR>
	<xsl:if test="AfterTrnRules/Action">
		<xsl:call-template name="TitledRules">
			<xsl:with-param name="Title">After Transaction Rules</xsl:with-param>
			<xsl:with-param name="Node">AfterTrnRules</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
	<xsl:if test="AfterLevelRules/Action">
		<xsl:call-template name="TitledRules">
			<xsl:with-param name="Title">After Level Rules</xsl:with-param>
			<xsl:with-param name="Node">AfterLevelRules</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
	<xsl:if test="Event">
		<TR class="textoGrisTabla"><TD><xsl:apply-templates select="Event"/></TD></TR>
	</xsl:if>
</table>
</DIV>
</DIV>
</xsl:if>
<xsl:if test="LevelType[text()='Undefined']">
	<xsl:apply-templates select="Event"/>
</xsl:if>
</xsl:template>

<xsl:template match="IndexName">
	Index: <xsl:value-of select="."/><br/>
</xsl:template>
<!-- END LEVEL TEMPLATE -->


<!-- tableS TO UPDATE ON DELETE -->

<xsl:template match="TablesToControlOnDelete">
<xsl:if test="Table/TableName">

Referential integrity controls on delete: <BR/>
<xsl:for-each select="Table">
	<li><xsl:apply-templates select="."/> <xsl:apply-templates select="KeyAttributes"/></li>
</xsl:for-each>

</xsl:if>
</xsl:template>

<!-- END TALBES TO UPDATE ON DELETE -->

<xsl:template name="UsedIndex">
	<!-- Display index information only when an order has been specified -->
	<xsl:if test="Order/*">
		<xsl:choose>
			<xsl:when test="IndexName">
				<xsl:apply-templates select="IndexName"/>
			</xsl:when>
			<xsl:otherwise>
				<font class="gxwarn">! No index</font>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:if>
</xsl:template>



<xsl:template match="InputParameters|OutputParameters">
	<xsl:for-each select="Attribute|Variable">
		<xsl:if test="position() != 1">, </xsl:if>
		<xsl:apply-templates select="."/>
	</xsl:for-each>
</xsl:template>

<xsl:template match="AttriType|AttriOldType" mode="Properties">
	<xsl:for-each select="Properties/Property">
		<xsl:apply-templates select="." mode="Attribute"/>
	</xsl:for-each>
</xsl:template>

<xsl:template match="Property" mode="Attribute">
	<xsl:if test="Value[.='Yes']">
		<xsl:value-of select="Name"/> = <xsl:value-of select="Value"/>
	</xsl:if>
</xsl:template>

<xsl:template match="AttriType|AttriOldType">
	<xsl:call-template name="PrintType">
		<xsl:with-param name="Type"><xsl:value-of select="DataType"/></xsl:with-param>
		<xsl:with-param name="Length"><xsl:value-of select="Presicion"/></xsl:with-param>
		<xsl:with-param name="Decimals"><xsl:value-of select="Scale"/></xsl:with-param>
	</xsl:call-template>
</xsl:template>

<xsl:template match="FormulaName">
	<xsl:call-template name="ProcessList"/><xsl:if test="Attribute">=</xsl:if>
</xsl:template>

<xsl:template match="Expression">
	<xsl:call-template name="ProcessList"/>
</xsl:template>

<xsl:template match="FormulaExpression">
	<xsl:call-template name="ProcessList"/>
</xsl:template>

<xsl:template match="Parameters|RedundantAttris|RedundantAttrisToUpdate|FromAttrisToUpdate|AttrisToUpdate">
	<xsl:call-template name="ProcessList">
		<xsl:with-param name="Sep">,</xsl:with-param>
	</xsl:call-template>
</xsl:template>

<xsl:template match="Parameter">
	<xsl:if test="not($Product='Deklarit')">
		<xsl:apply-templates select="IO"/>
	</xsl:if>
	<xsl:apply-templates select="Token"/>
	<xsl:apply-templates select="Attribute"/>
	<xsl:apply-templates select="Variable"/>
</xsl:template>

<xsl:template match="IO">
	<xsl:if test="text()[.!='inout']">
		[<xsl:value-of select="."/>]
	</xsl:if>
</xsl:template>

<!-- END PROMPTS -->

<!-- FOR EACH -->



<!-- STATUS TEMPLATE -->
<xsl:template match="Result">
<xsl:choose>
	<xsl:when test="text()[.='genreq']">
		<B><FONT class="titulosRosa"> Generation is required </FONT></B>
	</xsl:when>
	<xsl:when test="text()[.='specfailed']">
		<b><FONT class="titulosRosa"> Specification Failed </FONT></b>
	</xsl:when>
	<xsl:when test="text()[.='nogenreq']">
		<b><FONT class="titulosRosa"> No Generation Required </FONT></b>
	</xsl:when>
	<xsl:when test="text()[.='nogenspc']">
		<b><FONT class="titulosRosa"> No Specification Required</FONT></b>
	</xsl:when>
	<xsl:when test="text()[.='nospcreq']">
		<b><FONT class="titulosRosa"> No Specification Required </FONT></b>
	</xsl:when>
	<xsl:otherwise>
		<xsl:value-of select="."/>
	</xsl:otherwise>
</xsl:choose>
</xsl:template>



<!-- END STATUS TEMPLATE -->
<xsl:template match="OptimizedWhere">
	<table width="100%" cellpadding="0" cellspacing="0">
		<TR class="textoGrisTabla" valign="top" align="left">
			<TD width="15%" >
				<FONT class="gxnavigfil">Navigation filters:</FONT>
			</TD>
			<TD width="85%">
				<table width="100%" cellpadding="0" cellspacing="0">
					<xsl:if test="../LevelType != 'Break'">
						<TR class="textoGrisTabla" valign="top" align="left">
							<TD width="15%">Start from:</TD>
							<TD width="85%"><xsl:apply-templates select="StartFrom"/></TD>
						</TR>
					</xsl:if>
					<TR class="textoGrisTabla" valign="top" align="left">
						<TD width="15%">Loop while:</TD>
						<TD width="85%"><xsl:apply-templates select="LoopWhile"/></TD>
					</TR>
				</table>
			</TD>
		</TR>
	</table>
</xsl:template>

<!-- ENDFOREACH -->

<xsl:template match="NonOptimizedWhere">
<xsl:if test="Condition or ConditionalConstraint">
	<table width="100%" cellpadding="0" cellspacing="0">
		<TR class="textoGrisTabla" valign="top" align="left">
			<TD width="15%" >
				<FONT class="gxnavigfil">Constraints:</FONT>
			</TD>
			<TD width="85%">
				<table width="100%" cellpadding="0" cellspacing="0">
					<xsl:for-each select="Condition">
						<TR class="textoGrisTabla" valign="top" align="left">
							<xsl:call-template name="ProcessList"/>
						</TR>
					</xsl:for-each>
					<xsl:for-each select="ConditionalConstraint">
						<TR class="textoGrisTabla" valign="top" align="left">
							<xsl:apply-templates select="Constraint"/>
							<font class="gxwarn"><xsl:text> WHEN </xsl:text></font>
							<xsl:apply-templates select="Condition"/>
						</TR>
					</xsl:for-each>
				</table>
			</TD>
		</TR>
	</table>
</xsl:if>
</xsl:template>

<xsl:template match="StartFrom">
	<table width="100%" cellpadding="0" cellspacing="0">
		<xsl:for-each select="Condition">
			<TR class="textoGrisTabla" valign="top" align="left">
			<xsl:call-template name="ProcessList"/>
			</TR>
		</xsl:for-each>
	</table>
</xsl:template>

<xsl:template match="LoopWhile">
	<table width="100%" cellpadding="0" cellspacing="0">
		<xsl:for-each select="Condition">
			<TR class="textoGrisTabla" valign="top" align="left">
			<xsl:call-template name="ProcessList"/>
			</TR>
		</xsl:for-each>
	</table>
</xsl:template>

<xsl:template match="IF">
	<xsl:call-template name="ProcessList"/>
</xsl:template>
<xsl:template match="Condition">
	<xsl:call-template name="ProcessList"/>
</xsl:template>

<xsl:template match="Constraint">
	<xsl:call-template name="ProcessList"/>
</xsl:template>

<xsl:template match="FormulaGivenAttris"><xsl:apply-templates/></xsl:template>

<xsl:template match="Formulas"><xsl:apply-templates/></xsl:template>

<xsl:template match="Tables"><UL><xsl:apply-templates select="Table" mode="Tree"/></UL></xsl:template>

<!-- tableSTOUPDATE TEMPLATE -->
<xsl:template match="TablesToUpdate">
	<table width="100%" class="textoGrisTabla">
		<xsl:for-each select="TableToUpdate">
			<TR class="textoGrisTabla"><TD><xsl:apply-templates select="."/></TD></TR>
		</xsl:for-each>
	</table>
</xsl:template>

<xsl:template match="TableToUpdate">
	<table class="textoGrisTabla">
	<tr>
		<td>
			<xsl:apply-templates select="TableAction"/>
			<xsl:apply-templates select="Table"/>
			<xsl:if test="AttrisToUpdate/Attribute">
			<xsl:text> </xsl:text>(
				<xsl:apply-templates select="AttrisToUpdate"/>
				)
			</xsl:if>
		</td>
	</tr>
	<xsl:if test="UpdateRedundancyCall">
		<tr><td><xsl:text>  </xsl:text>Update Redundancy: 
			<xsl:apply-templates select="UpdateRedundancyCall"/>
		</td></tr>
	</xsl:if>
	</table>
</xsl:template>

<xsl:template match="UpdateRedundancyCall">
	<FONT class="gxsource">CALL</FONT>
	(
	<xsl:value-of select="ProgramName"/>,<xsl:value-of select="Parameters"/>
	)
</xsl:template>

<xsl:template match="TableAction">
	<xsl:choose>
		<xsl:when test="text()[.='insert']">Insert into </xsl:when>
		<xsl:when test="text()[.='update']">Update </xsl:when>
		<xsl:when test="text()[.='delete']">Delete from </xsl:when>
		<xsl:otherwise></xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--
<xsl:template match="AttrisToUpdate">
	<xsl:for-each select="Attribute">
		<xsl:if test="position() != 1">, </xsl:if>
		<xsl:apply-templates select="."/>
	</xsl:for-each>
</xsl:template>
-->
<xsl:template match="TableName"><xsl:apply-templates/></xsl:template>

<!-- EVENT TEMPLATE -->
<xsl:template match="Event">
<xsl:if test="CALL|Level">
<table  cellspacing="0" cellpadding="0" width="100%" >
<TR class="textoGrisTabla" ><TD nowrap="yes"><xsl:apply-templates select="EventName"/></TD></TR>
	<xsl:for-each select="CALL">
		<TR class="textoGrisTabla"><TD>
			<xsl:text>   </xsl:text>
			<xsl:apply-templates select="."/>
		</TD></TR>	
	</xsl:for-each>
<xsl:if test="Level">
	<xsl:for-each select="Level">
		<TR><TD> <xsl:apply-templates select="."/> </TD></TR>
	</xsl:for-each>
</xsl:if>
<TR class="textoGrisTabla" ><TD>
	<FONT class="gxevent">End 
		<xsl:choose>
		<xsl:when test="EventType">Sub </xsl:when>
		<xsl:otherwise>Event </xsl:otherwise>
		</xsl:choose>
	</FONT>
</TD></TR>
</table>
</xsl:if>
</xsl:template>
<!-- END EVENT TEMPLATE -->

<xsl:template match="EventName">
<FONT class="gxevent">
<xsl:choose>
	<xsl:when test="../EventType">Sub </xsl:when>
	<xsl:otherwise>Event </xsl:otherwise>
</xsl:choose>
</FONT>
<xsl:value-of select="."/>
</xsl:template>


<!-- CALL TEMPLATE -->
<xsl:template match="CALL">
	<FONT class="gxsource">CALL</FONT><xsl:text> </xsl:text>
	<xsl:choose>
		<xsl:when test="ProgramName">
			<xsl:value-of select="ProgramName"/>
		</xsl:when>
		<xsl:when test="Object">
			<xsl:apply-templates select="Object"/>
		</xsl:when>
	</xsl:choose>
	(<xsl:apply-templates select="Parameters"/>)
	<xsl:if test="IF/*">
		IF
		<xsl:apply-templates select="IF"/>
	</xsl:if>
</xsl:template>
<!-- END CALL TEMPLATE -->

<xsl:template name="VAttributes">
	<table cellspacing="0" cellpadding="0">
		<TR class="textoGrisTabla">
			<TD>
				<table cellspacing="0" cellpadding="0">
					<xsl:for-each select="Attributes/Attribute">
						<TR class="textoGrisTabla">
							<TD>
								<xsl:apply-templates select="."/><xsl:text> </xsl:text>
							</TD>
						</TR>
					</xsl:for-each>
				</table>
			</TD>
		</TR>
	</table>
</xsl:template>

<xsl:template match="Table" mode="Tree">
	<LI style="LIST-STYLE: none">
		<IMG>
		<xsl:attribute name="src">
			<xsl:value-of select="/ObjSpecs/gxpath"/>
			<xsl:text>gxxml/</xsl:text>
			<xsl:choose>
				<xsl:when test="Tables/Table">
					<xsl:text>images/tablemin.bmp</xsl:text>
				</xsl:when>
				<xsl:otherwise>
					<xsl:choose>
						<xsl:when test="Updated[.='Yes']">
							<xsl:text>images/tbledited.bmp</xsl:text>
						</xsl:when>
						<xsl:otherwise>
							<xsl:text>images/table.ico</xsl:text>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:attribute>
		<xsl:if test="Tables/Table">
			<xsl:attribute name="style">CURSOR: hand</xsl:attribute>
			<xsl:attribute name="onclick">toggleView()</xsl:attribute>
		</xsl:if>
		</IMG>
		<span>
			<xsl:call-template name="GxOpen">
				<xsl:with-param name="Class">7</xsl:with-param>
				<xsl:with-param name="Id" select="TableId"/>
			</xsl:call-template>
			<xsl:apply-templates select="TableName"/>
		</span>
		<xsl:apply-templates select="KeyAttributes"/>
		<xsl:if test="Into/Attribute">
			INTO <xsl:apply-templates select="Into"/>
		</xsl:if>
		<xsl:apply-templates select="Tables"/>
	</LI>
</xsl:template>


</xsl:stylesheet>
