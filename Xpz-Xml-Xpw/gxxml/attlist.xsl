<?xml version='1.0' ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:gx="urn:schemas-artech-com:gx">

<xsl:output method="html" encoding="iso-8859-1"/>
<xsl:template match="/">

<html>
	<xsl:call-template name="HtmlHeader"/>
<body>
	<xsl:call-template name="HtmlScript"/>
	<form action="" id="FORM1" method="post" name="FORM1">

	<TABLE width="100%">
	<xsl:for-each select="ObjSpecs/AttriList">
		<xsl:if test="position() != 1">
			<TR><TD><xsl:text> </xsl:text></TD></TR>
		</xsl:if>
		<TR><TD><xsl:apply-templates select="."/></TD></TR>
	</xsl:for-each>
	</TABLE>
	</form>
</body>
</html>
</xsl:template>

<xsl:include href="include.xsl"/>


<xsl:template match="AttriList">
	<xsl:choose>
		<xsl:when test="Attribute">
			<xsl:variable name="currAtt" select="Attribute/AttriId"/>

			<TABLE width="100%" height="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CDAAAD" style="border-collapse:collapse" >
				<TR><TD vAlign="top">
					<xsl:apply-templates select="Attribute" mode="Info"/>
				</TD></TR>
			</TABLE>
		</xsl:when>
		<xsl:otherwise>
			<B> Attribute does not exist </B>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template match="Attribute" mode="Info">
<!-- Attribute HEADER -->
<DIV>
	<xsl:call-template name="TableHeaderMain">
		<xsl:with-param name="title">
			<xsl:choose>
				<xsl:when test="AttriIsDomain">Domain </xsl:when>
				<xsl:otherwise>Attribute </xsl:otherwise>
			</xsl:choose>
			<xsl:apply-templates select="."/>
		</xsl:with-param>
	</xsl:call-template>
	<DIV id="content">
		<TABLE class="textoGrisTabla" cellpadding="0" cellspacing="5" width="100%">
			<TBODY>
				<TR><TD>
				<TABLE class="textoGrisTabla" cellpadding="0" cellspacing="0" width="100%">
				<TR>
					<TD width="25%">Name</TD>
					<TD><EM><xsl:apply-templates select="."/></EM></TD>
				</TR>
				<xsl:call-template name="EndRow"/>
				<TR>
					<TD width="25%">Description</TD>
					<TD><EM><xsl:value-of select="AttriDesc"/></EM></TD>
				</TR>
				<xsl:call-template name="EndRow"/>
				<TR>
					<TD width="25%">ID</TD>
					<TD><EM><xsl:value-of select="AttriId"/></EM></TD>
				</TR>
				<xsl:call-template name="EndRow"/>
				<TR>
					<TD>Type</TD>
					<TD noWrap="yes"><EM><xsl:apply-templates select="Type"/></EM></TD>
				</TR>
				<xsl:call-template name="EndRow"/>
				<xsl:if test="Picture[.!='']">
					<TR>
						<TD>Picture</TD>
						<TD><EM><xsl:value-of select="Picture"/></EM></TD>
					</TR>
					<xsl:call-template name="EndRow"/>
				</xsl:if>
				<xsl:if test="Domain">
					<TR>
						<TD>Domain</TD>
						<TD><EM><xsl:apply-templates select="Domain/Attribute"/></EM></TD>
					</TR>
					<xsl:call-template name="EndRow"/>
				</xsl:if>
				<xsl:if test="ValueRange">
					<TR>
						<TD>Value Range</TD>
						<TD><EM><xsl:value-of select="ValueRange"/></EM></TD>
					</TR>
					<xsl:call-template name="EndRow"/>
				</xsl:if>
				<xsl:if test="AttriSuper">
					<TR>
						<TD>Subtype of</TD>
						<TD><EM><xsl:apply-templates select="AttriSuper"/></EM></TD>
					</TR>
					<xsl:call-template name="EndRow"/>
				</xsl:if>
				<xsl:if test="FormulaDef">
					<TR>
						<TD valign="top">Formula</TD>
						<TD><EM><xsl:apply-templates select="FormulaDef"/></EM></TD>
					</TR>
					<xsl:call-template name="EndRow"/>
				</xsl:if>
				</TABLE>
				</TD>
				</TR>

				<xsl:if test="../AttriWarnings">
					<TR>
						<TD colspan="2">
							<xsl:apply-templates select="../AttriWarnings"/>
						</TD>
					</TR>
				</xsl:if>
				<xsl:if test="../AttriXRef/Objects/ObjXRef|../AttriXRef/Formulas/Attribute/FormulaDef">
					<TR><TD colspan="2"><xsl:apply-templates select="../AttriXRef"/></TD></TR>
				</xsl:if>
				<TR><TD colspan="2"><xsl:apply-templates select="../ControlInfo"/></TD></TR>
				<xsl:if test="../PropPart/OneProp">
					<TR><TD colspan="2"><xsl:apply-templates select="../PropPart"/></TD></TR>
				</xsl:if>
				<xsl:if test="../Help">
					<TR><TD colspan="2"><xsl:apply-templates select="../Help"/></TD></TR>
				</xsl:if>
				<xsl:if test="../Documentation">
					<TR><TD colspan="2"><xsl:apply-templates select="../Documentation"/></TD></TR>
				</xsl:if>
			</TBODY>
		</TABLE>
		</DIV>
	</DIV>
<!-- END ATTRIBUTE HEADER -->
</xsl:template>


<!-- Type template -->
<xsl:template match="Type">
	<xsl:value-of select="."/>
	<xsl:if test="../Length">
		(
		<xsl:value-of select="../Length"/>
		<xsl:if test="../Decimals">.<xsl:value-of select="../Decimals"/></xsl:if>
		)
	</xsl:if>
	<xsl:if test="../MaxLen">
		(
		<xsl:value-of select="../MaxLen"/>
		)
	</xsl:if>
	<xsl:if test="../DateLen">
		(
		<xsl:value-of select="../DateLen"/>.<xsl:value-of select="../TimeLen"/>
		)
	</xsl:if>
	<xsl:if test="../Sign">-</xsl:if>
</xsl:template>



<!--AttriXRef TEMPLATE -->
<xsl:template match="AttriXRef">
<DIV>
  <xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Cross Reference</xsl:with-param>
  </xsl:call-template>
  <DIV id="content">
  <TABLE width="100%" cellspacing="5"  style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
	<!--TABLES -->

	<xsl:for-each select="Objects/GroupList">
		<TR><TD>
			<xsl:apply-templates select="."/>
		</TD></TR>
	</xsl:for-each>
	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">7</xsl:with-param>
		<xsl:with-param name="Title">Tables</xsl:with-param>
	</xsl:apply-templates>
	
	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">0</xsl:with-param>
		<xsl:with-param name="Title">Transactions</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">6</xsl:with-param>
		<xsl:with-param name="Title">Data Views</xsl:with-param>
	</xsl:apply-templates>
	
	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">1</xsl:with-param>
		<xsl:with-param name="Title">Procedures</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">2</xsl:with-param>
		<xsl:with-param name="Title">Reports</xsl:with-param>
	</xsl:apply-templates>
	
	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">3</xsl:with-param>
		<xsl:with-param name="Title">Menus</xsl:with-param>
	</xsl:apply-templates>
	
	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">4</xsl:with-param>
		<xsl:with-param name="Title">Work Panels</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">12</xsl:with-param>
		<xsl:with-param name="Title">Prompts</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">13</xsl:with-param>
		<xsl:with-param name="Title">Web Panels</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">17</xsl:with-param>
		<xsl:with-param name="Title">Menu Bars</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">18</xsl:with-param>
		<xsl:with-param name="Title">Transactions Style</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">19</xsl:with-param>
		<xsl:with-param name="Title">Procedures Style</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">20</xsl:with-param>
		<xsl:with-param name="Title">Reports Style</xsl:with-param>
	</xsl:apply-templates>


	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">21</xsl:with-param>
		<xsl:with-param name="Title">Workpanels Style</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">22</xsl:with-param>
		<xsl:with-param name="Title">Prompts Style</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">23</xsl:with-param>
		<xsl:with-param name="Title">Web Panels Style</xsl:with-param>
	</xsl:apply-templates>

	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">24</xsl:with-param>
		<xsl:with-param name="Title">MenuBars Style</xsl:with-param>
	</xsl:apply-templates>
	
	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">25</xsl:with-param>
		<xsl:with-param name="Title">Themes</xsl:with-param>
	</xsl:apply-templates>
	
	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">26</xsl:with-param>
		<xsl:with-param name="Title">Structure Data Types</xsl:with-param>
	</xsl:apply-templates>


	<xsl:apply-templates select="Objects">
		<xsl:with-param name="ClassId">11</xsl:with-param>
		<xsl:with-param name="Title">Domains</xsl:with-param>
	</xsl:apply-templates>
	

	<xsl:for-each select="Objects">
		<xsl:if test="ObjXRef/Attribute">
			<xsl:call-template name="Attributes"/>
		</xsl:if>
	</xsl:for-each>

	<xsl:for-each select="Formulas">
		<xsl:if test="Attribute">
			<TR><TD>
				<TABLE class="textoGrisTabla" width="90%">
					<TR><TH align="left" colspan="3"> <B> Formulas</B><HR color="#800000"/> </TH></TR>
					<TBODY>
						<xsl:for-each select="Attribute">
							<TR><TD>
							<xsl:apply-templates select="."/>
							=
							<xsl:apply-templates select="FormulaDef"/>
							</TD></TR>
						</xsl:for-each>
					</TBODY>
				</TABLE>
			</TD></TR>
		</xsl:if>
	</xsl:for-each>
  </TABLE>
  </DIV>
</DIV>
</xsl:template>

<xsl:template match="ObjXRef" mode="WhereIs">
	<xsl:param name="ObjId">-1</xsl:param>
	<xsl:for-each select="../ObjXRef[Object[ObjCls=0 and ObjId=$ObjId] and ObjXRefParts[ObjPart!='Structure']]">
		<xsl:for-each select="ObjXRefParts/ObjPart">
			<xsl:text>, </xsl:text><xsl:value-of select="."/>
		</xsl:for-each>
	</xsl:for-each>
</xsl:template>

<xsl:template name="XRefOneObj">
	<xsl:param name="IsStruct">0</xsl:param>
	<TR>
		<TD noWrap="yes" valign="top" width="20%">
			<xsl:apply-templates select="." mode="xRef"/>
		</TD>
		<TD noWrap="yes" width="30%"><xsl:value-of select="ObjTitle"/></TD>
		<TD width="30%" valign="top">
			<xsl:apply-templates select="../ObjXRefParts"/>
			<xsl:if test="$IsStruct = 1">
				<xsl:if test="ObjCls[.=0]">
					<xsl:apply-templates select=".." mode="WhereIs">
						<xsl:with-param name="ObjId">
							<xsl:value-of select="ObjId"/>
						</xsl:with-param>
					</xsl:apply-templates>	
				</xsl:if>
			</xsl:if>
		</TD>
	</TR>
</xsl:template>
      
<!-- OBJXREF TEMPLATE -->
<xsl:template match="ObjXRef">
	<xsl:for-each select="Object">
		<xsl:choose>
			<xsl:when test="ObjCls=0 and ../ObjXRefParts[ObjPart!='Structure']">
				<xsl:variable name="ObjId" select="ObjId"/>
				<xsl:choose>
					<xsl:when test="../../ObjXRef[ObjXRefParts/ObjPart='Structure']/Object[ObjId = $ObjId]"/>
					<xsl:otherwise>
						<xsl:call-template name="XRefOneObj"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="XRefOneObj">
					<xsl:with-param name="IsStruct">1</xsl:with-param>
				</xsl:call-template>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:for-each>
	<xsl:for-each select="Attribute">
		<TR>
			<TD noWrap="yes" valign="top" width="20%">
				<xsl:apply-templates select="."/>
			</TD>
			<TD width="20%"><xsl:value-of select="AttriDesc"/></TD>
			<TD>Domain</TD>
		</TR>
	</xsl:for-each>
</xsl:template>

<!-- Subtypes TEMPLATE -->
<xsl:template match="GroupList">
	<xsl:if test="Composition/GroupElement">
		<xsl:for-each select="Composition">
			<xsl:call-template name="GenerateTable">
				<xsl:with-param name="ID">Subtypes</xsl:with-param>
				<xsl:with-param name="Title">Subtypes<xsl:text> </xsl:text>
					<xsl:apply-templates select="../Object/ObjName"/>
				</xsl:with-param>
				<xsl:with-param name="image">
					<xsl:call-template name="GetClsName">
						<xsl:with-param name="Class">10</xsl:with-param>
					</xsl:call-template>
					.ico
				</xsl:with-param>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:if>
</xsl:template>

<xsl:template match="GroupElement" mode="TableRow">
	<TD><xsl:apply-templates select="SubType"/></TD>
	<TD ><xsl:apply-templates select="ElementType"/></TD>
	<TD><xsl:apply-templates select="SubType/Attribute/AttriSuper/Group"/>
		<xsl:if test="SubType/Attribute/AttriSuper/Group">.</xsl:if>
		<xsl:apply-templates select="SuperType"/>
	</TD>
</xsl:template>

<!-- OBJXRefPARTS TEMPLATE -->
<xsl:template match="ObjXRefParts">
	<xsl:for-each select="ObjPart"><xsl:if test="position() != 1">, </xsl:if><xsl:value-of select="."/></xsl:for-each>
</xsl:template>

<!-- ControlInfo TEMPLATE -->
<xsl:template match="ControlInfo">
<DIV>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">
			<xsl:text>Control Info</xsl:text>
			<xsl:if test="ControlInfoFromDomain">
				<xsl:text> (from domain)</xsl:text>
			</xsl:if>
		</xsl:with-param>
	</xsl:call-template>
	<DIV id="content">
	<TABLE width="100%" cellspacing="0" cellpadding="0" style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
		<TR class="subTitulosTabla"> 
			<TD>Name</TD>
			<TD>Value</TD>
		 </TR>
		<xsl:for-each select="Properties/Property">
			<TR class="textoGrisTabla">
				<TD><xsl:value-of select="PropertyName"/></TD>
				<TD><xsl:apply-templates select="PropertyValue"/></TD>
			</TR>
			<xsl:call-template name="EndRow"/>
		</xsl:for-each>
		<xsl:if test="Properties/Contents">
			<TR class="textoGrisTabla">
				<TD colspan="2">
					<xsl:apply-templates select="Properties/Contents" mode="ControlInfo"/>
				</TD>
			</TR>
			<xsl:call-template name="EndRow"/>
		</xsl:if>
	</TABLE>
	</DIV>
</DIV>
</xsl:template>

<!-- PROPPART TEMPLATE -->
<xsl:template match="PropPart">
	<xsl:call-template name="GenerateTable">
		<xsl:with-param name="ID">Properties</xsl:with-param>
		<xsl:with-param name="Title">Properties</xsl:with-param>
	</xsl:call-template>
</xsl:template>

<xsl:template match="OneProp" mode="TableRow">
	<TD valign="top"><xsl:value-of select="Name"/></TD>
	<TD valign="top"><xsl:apply-templates select="Value"/></TD>
</xsl:template>

<xsl:template match="Value">
	<xsl:choose>
		<xsl:when test="Enum">
			<xsl:apply-templates select="Enum"/>
		</xsl:when>
		<xsl:otherwise> 
			<xsl:value-of select="."/>
		</xsl:otherwise> 
	</xsl:choose>
</xsl:template>

<!-- Enum TEMPLATE -->
<xsl:template match="Enum">
	<TABLE cellspacing="0" cellpadding="0">
	<TR class="subtitulosTabla">
		<TD>Name</TD>
		<TD>Description</TD>
		<TD>Value</TD>
	</TR>
	<xsl:for-each select="EnumValue">
		<TR class="textoGrisTabla">
		<TD><xsl:value-of select="Name"/></TD>
		<TD><xsl:value-of select="Description"/></TD>
		<TD><xsl:value-of select="Value"/></TD>
		</TR>
		<xsl:call-template name="EndRow"/>
	</xsl:for-each>
</TABLE>
</xsl:template>

<!-- HELP TEMPLATE -->
<xsl:template match="Help">
<DIV>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Help</xsl:with-param>
	</xsl:call-template>
	<DIV id="content">
	<TABLE width="100%" style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
		<TR><TD><xsl:apply-templates select="Contents"/></TD></TR>
 	</TABLE>
	</DIV>
</DIV>
</xsl:template>


<!-- Enum CONTENTS TEMPLATE -->
<xsl:template match="Contents" mode="ControlInfo">
	<PRE> <xsl:value-of select="."/> </PRE>
</xsl:template>

<!-- Help & Doc CONTENTS TEMPLATE -->
<xsl:template match="Contents">
	<PRE> <xsl:value-of disable-output-escaping="yes" select="."/> </PRE>
</xsl:template>

<!-- DOCUMENTATION TEMPLATE -->
<!-- HELP TEMPLATE -->
<xsl:template match="Documentation">
<DIV>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Documentation</xsl:with-param>
	</xsl:call-template>
	<DIV id="content">
	<TABLE width="100%" style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
		<TR><TD><xsl:apply-templates select="Contents"/></TD></TR>
	</TABLE>
	</DIV>
</DIV>
</xsl:template>


<!-- PROPERTYVALUE TEMPLATE -->
<xsl:template match="PropertyValue">
	<xsl:choose>
		<xsl:when test="Color">
			<TABLE height="70%">
			<TR class="textoGrisTabla">
				<TD>
				<TABLE height="70%" border="0" borderColor="black" cellPadding="0" cellSpacing="0">
				  <TR><TD>
				  <LABEL>
					<xsl:attribute name="style">BACKGROUND-COLOR: 
						<xsl:value-of select="Color/ColorValue"/></xsl:attribute>
					<FONT class="textoGrisTabla">
						<xsl:attribute name="style">COLOR: 
							<xsl:value-of select="Color/ColorValue"/>
						</xsl:attribute>
						<xsl:text>    </xsl:text>
					</FONT>
				  </LABEL>
				  </TD></TR>
				</TABLE>
				</TD>
				<TD>
					<PR> <xsl:value-of select="Color/ColorName"/> </PR>
				</TD>
			</TR>
			</TABLE>
		</xsl:when>
		<xsl:otherwise>
			<xsl:value-of select="."/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!-- TABLE TEMPLATE -->


<xsl:template match="AttriWarnings">
<DIV id="warnings">
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Warnings/Errors</xsl:with-param>
	</xsl:call-template>
	<DIV id="content">
	<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%" style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
	<TR class="textoGrisTabla">
		<xsl:for-each select="Warning">
			<TR><TD><xsl:apply-templates select="."/></TD></TR>
		</xsl:for-each>
	</TR>
	</TABLE>
	</DIV>
</DIV>
</xsl:template>

<xsl:template match="Warning">
	<td class="textoError">
		<xsl:call-template name="RenderImage">
			<xsl:with-param name="Id">Warning</xsl:with-param>
		</xsl:call-template>
	</td>
	<td class="textoError">
	<xsl:choose>
		<xsl:when test="text()[.=1]">
			Error: Secondary attribute is in more than one table. Database should not be Created or Reorganized.
		</xsl:when>
		<xsl:when test="text()[.=2]">
			Error: attribute is in at least one Transaction but not in any Table.
		</xsl:when>
		<xsl:when test="text()[.=3]">
			Error: attribute is in at least one Table but not in any Transaction.
		</xsl:when>
		<xsl:when test="text()[.=4]">
			Warning: attribute is not in any table.
		</xsl:when>
		<xsl:when test="text()[.=5]">
			Warning: attribute is not being used.
		</xsl:when>
		<xsl:when test="text()[.=6]">
			Warning: attribute is in some table index but is not part of the table.
		</xsl:when>
	</xsl:choose>
	</td>
</xsl:template>

<xsl:template match="Objects">
		<xsl:param name="ClassId"/>
		<xsl:param name="Title"/>
  <xsl:if test="ObjXRef[Object[ObjCls=$ClassId]]">
	<TR><TD>
		<DIV>
			<xsl:call-template name="TableHeader">
				<xsl:with-param name="title" select="$Title"/>
				<xsl:with-param name="image">
					<xsl:call-template name="GetClsName">
						<xsl:with-param name="Class">
							<xsl:value-of select="$ClassId"/>
						</xsl:with-param>
					</xsl:call-template>
					<xsl:text>.ico</xsl:text>
				</xsl:with-param>
				<xsl:with-param name="width">90%</xsl:with-param>
			</xsl:call-template>
			<DIV id="content">
				<TABLE class="textoGrisTabla" width="90%" cellspacing="0" cellpadding="0">
					<TR class="subtitulosTabla">
						<TH align="left"> Name </TH>
						<TH align="left"> Description </TH>
						<xsl:choose>
							<xsl:when  test="$ClassId != 11">  <!-- 11 is domain -->
								<TH align="left"> In </TH>
							</xsl:when>
							<xsl:otherwise>
								<TH/>
							</xsl:otherwise>
						</xsl:choose>
					</TR>
					<TBODY>
						<xsl:for-each select="ObjXRef[Object[ObjCls=$ClassId]]">
							<xsl:apply-templates select="."/>
							<xsl:call-template name="EndRow"/>
						</xsl:for-each>
					</TBODY>
				</TABLE>
			</DIV>
		</DIV>
	</TD></TR>
  </xsl:if>		
</xsl:template>

<xsl:template name="Attributes">
	<TR><TD>
	<DIV>
		<xsl:call-template name="TableHeader">
			<xsl:with-param name="title">Attributes</xsl:with-param>
			<xsl:with-param name="image">
				<xsl:call-template name="GetClsName">
					<xsl:with-param name="Class">5</xsl:with-param>
				</xsl:call-template>
				.ico
			</xsl:with-param>
			<xsl:with-param name="width">90%</xsl:with-param>
		</xsl:call-template>
		<DIV id="content">
		<TABLE class="textoGrisTabla" width="90%" cellspacing="0" cellpadding="0">
			<TR class="subtitulosTabla"> 
				<TH align="left"> Name </TH>
				<TH align="left"> Description </TH>
				<TH align="left"> In </TH>
			</TR>
			<TBODY>
				<xsl:for-each select="ObjXRef[Attribute]">
					<TR class="textoGrisTabla">
						<TD><xsl:apply-templates select="Attribute"/></TD>
						<TD><xsl:value-of select="Attribute/AttriDesc"/></TD>
						<TD><xsl:for-each select="ObjXRefParts/ObjPart">
								<xsl:if test="position() != 1">, </xsl:if>
								<xsl:value-of select="."/>
							</xsl:for-each>
						</TD>
					</TR>
					<xsl:call-template name="EndRow"/>
				</xsl:for-each>
			</TBODY>
		</TABLE>
		</DIV>
	</DIV>
	</TD></TR>
</xsl:template>

<xsl:template match="text()|@*"><xsl:value-of select="."/></xsl:template>

</xsl:stylesheet>
