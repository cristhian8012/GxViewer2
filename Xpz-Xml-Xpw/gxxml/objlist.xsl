<?xml version='1.0' ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxml="urn:schemas-microsoft-com:xslt"
	xmlns:gx="urn:schemas-artech-com:gx"
	exclude-result-prefixes="msxml gx">

<xsl:include href="include.xsl"/>

<xsl:template match="/">
<html>
	<xsl:call-template name="HtmlHeader"/>
	<xsl:call-template name="HtmlCSS"/>
<body>
	<xsl:call-template name="HtmlScript"/>
	<form action="" id="FORM1" method="post" name="FORM1">
		<table width="100%">
			<xsl:for-each select="ObjSpecs/Object">
				<tr><td><xsl:apply-templates select="." mode="AllInfo"/></td></tr>
			</xsl:for-each>
		</table>
	</form>
</body>
</html>
</xsl:template>

<xsl:template name="HtmlCSS">
	<xsl:for-each select="ObjSpecs/Object">
		<xsl:if test="not(Filters/NoHtmlForm)">
			<xsl:if test="HtmlPart/CSS">
				<link rel="stylesheet" type="text/css">
					<xsl:attribute name="href"><xsl:value-of select="HtmlPart/CSS"/></xsl:attribute>
				</link>
			</xsl:if>
		</xsl:if>
	</xsl:for-each>
</xsl:template>

<xsl:template match="Object" mode="AllInfo">
	<table width="100%" height="100%" border="1" cellspacing="0" cellpadding="0" bordercolor="#CDAAAD" style="border-collapse:collapse">
	<tr><td vAlign="top">
	<div>
		<xsl:call-template name="TableHeaderMain">
			<xsl:with-param name="title">
				<xsl:value-of select="InfoPart/ClassName"/>
				<xsl:text> </xsl:text>
				<xsl:value-of select="InfoPart/ObjName"/>
			</xsl:with-param>
		</xsl:call-template>
		<div id="content">
		<table width="100%" class="textoGrisTabla" border="0" cellpadding="0" cellspacing="5">
		<TBODY><tr><td>
		<table class="textoGrisTabla" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td width="25%">Name</td>
				<td><xsl:apply-templates select="InfoPart"/></td>
			</tr>
			<xsl:call-template name="EndRow"/>
			<tr>
				<td width="25%">Description</td>
				<td><em><xsl:value-of select="InfoPart/ObjTitle"/></em></td>
			</tr>
			<xsl:call-template name="EndRow"/>
			<xsl:choose>
			<xsl:when test="PrivateObject">
				<tr>
					<td width="25%"><b>Error</b></td>
					<td><b>Private Object, cannot be listed.</b></td>
				</tr>
				<xsl:call-template name="EndRow"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:if test="not(Filters/NoExtraInfo)">
					<xsl:apply-templates select="InfoPart" mode="AllInfo"/>
				</xsl:if>
				<xsl:if test="not(Filters/NoStruct)">
					<xsl:choose>
						<xsl:when test="StructPart">
							<tr><td colspan="2"><xsl:apply-templates select="StructPart"/></td></tr>
						</xsl:when>
						<xsl:when test="StructDataType">
							<tr><td colspan="2"><xsl:call-template name="StructDataType"/></td></tr>
						</xsl:when>
						<xsl:when test="DataViewPart">
							<tr><td colspan="2"><xsl:apply-templates select="DataViewPart"/></td></tr>
						</xsl:when>
					</xsl:choose>
				</xsl:if>
				<xsl:if test="not(Filters/NoExtraInfo) and PromptPart">
					<tr><td colspan="2"><xsl:apply-templates select="PromptPart"/></td></tr>
				</xsl:if>
				<xsl:if test="Content">
					<tr><td colspan="2"><xsl:apply-templates select="Content"/></td></tr>
				</xsl:if>
				<xsl:if test="not(Filters/NoForm)">
					<xsl:if test="FormPart and InfoPart/ObjCls[. != '13']"> <!-- 13: Web Panel -->
						<tr><td colspan="2"><xsl:apply-templates select="FormPart"/></td></tr>
					</xsl:if>
					<xsl:if test="LayoutPart">
						<tr><td colspan="2"><xsl:apply-templates select="LayoutPart"/></td></tr>
					</xsl:if>
				</xsl:if>
				<xsl:if test="not(Filters/NoMenu) and MenuBarPart">
					<tr><td colspan="2"><xsl:apply-templates select="MenuBarPart"/></td></tr>
				</xsl:if>
				<xsl:if test="not(Filters/NoHtmlForm) and HtmlPart">
					<tr><td colspan="2"><xsl:apply-templates select="HtmlPart"/></td></tr>
				</xsl:if>
				<xsl:if test="EventPart">
					<tr><td colspan="2"><xsl:apply-templates select="EventPart"/></td></tr>
				</xsl:if>
				<xsl:if test="not(Filters/NoVariables) and VarsPart">
					<tr><td colspan="2"><xsl:apply-templates select="VarsPart"/></td></tr>
				</xsl:if>
				<xsl:if test="not(Filters/NoProperties) and PropPart/OneProp">
					<tr><td colspan="2"><xsl:apply-templates select="PropPart"/></td></tr>
				</xsl:if>
				<xsl:if test="not(Filters/NoCrossRef) and CrossReference/Called/XRefObject|CrossReference/CallBy/XRefObject">
					<tr><td colspan="2"><xsl:apply-templates select="CrossReference"/></td></tr>
				</xsl:if>
				<xsl:if test="not(Filters/NoHelp) and Help">
					<tr><td colspan="2"><xsl:apply-templates select="Help"/></td></tr>
				</xsl:if>
				<xsl:if test="not(Filters/NoDocum) and Documentation">
					<tr><td colspan="2"><xsl:apply-templates select="Documentation"/></td></tr>
				</xsl:if>
			</xsl:otherwise>
			</xsl:choose>
		</table>
		</td></tr></TBODY>
		</table>
		</div>
	</div>	
	</td></tr></table>
</xsl:template>

<!-- InfoPart TEMPLATE -->
<xsl:template match="InfoPart" mode="AllInfo">
	<xsl:if test="ObjCls[.!= 26]">	<!-- SDTs -->
		<tr>
			<td width="25%">Folder</td>
			<td><EM>
				<xsl:apply-templates select="Folder"/>
			</EM></td>
		</tr>
		<xsl:call-template name="EndRow"/>
	</xsl:if>
	<tr>
		<td width="25%">Id</td>
		<td><EM>
			<xsl:value-of select="ObjId"/>
		</EM></td>
	</tr>
	<xsl:call-template name="EndRow"/>
	<tr>
		<td width="25%">Created</td>
		<td><EM>
			<xsl:value-of select="Created"/>
		</EM></td>
	</tr>
	<xsl:call-template name="EndRow"/>
	<tr>
		<td width="25%">Updated</td>
		<td><EM>
			<xsl:value-of select="Updated"/>
		</EM></td>
	</tr>
	<xsl:call-template name="EndRow"/>
	<xsl:if test="Imported">
	<tr>
		<td width="25%">Consolidated</td>
		<td><EM>
			<xsl:value-of select="Imported"/>
		</EM></td>
	</tr>
	<xsl:call-template name="EndRow"/>
	</xsl:if>
	<xsl:if test="DatawarehouseTrn">
	<tr>
		<td width="25%">Data Warehousing Transaction</td>
		<td><EM>
			<xsl:value-of select="DatawarehouseTrn"/>
		</EM></td>
	</tr>
	<xsl:call-template name="EndRow"/>
	</xsl:if>
	<xsl:if test="AssocStyle">
	<tr>
		<td width="25%">Associated to Style</td>
		<td><EM>
			<xsl:apply-templates select="AssocStyle"/>
		</EM></td>
	</tr>
	<xsl:call-template name="EndRow"/>
	</xsl:if>
	<xsl:if test="MainObject">
	<xsl:apply-templates select="MainObject"/>
	<xsl:call-template name="EndRow"/>
	</xsl:if>
	<xsl:if test="Generators">
	<tr>
		<td width="25%" valign="top">Generators</td>
		<td><EM>
			<xsl:for-each select="Generators/Generator">
				<xsl:apply-templates select="GenId"/>
				<xsl:value-of select="GenName"/><BR/>
			</xsl:for-each>
		</EM></td>
	</tr>
	<xsl:call-template name="EndRow"/>
	</xsl:if>
</xsl:template>

<xsl:template match="MainObject">
	<tr>
		<td width="25%" valign="top">Main Object</td>
		<td><table class="textoGrisTabla" cellspacing="0" cellpadding="0" style="padding-left: 0px; padding-right: 5px">
			<tr>
				<td>Generator</td>
				<td><EM>
					<xsl:apply-templates select="Generator/GenId"/>
					<xsl:value-of select="Generator/GenName"/>
				</EM></td>
			</tr>
			<xsl:if test="WinTitle">
				<tr>
					<td>Windows Title</td>
					<td><EM>
						<xsl:value-of select="WinTitle"/>
					</EM></td>
				</tr>
			</xsl:if>
			<xsl:if test="AppIcon">
				<tr>
					<td>Aplication Icon</td>
					<td><EM>
						<xsl:value-of select="AppIcon"/>
					</EM></td>
				</tr>
			</xsl:if>
			<xsl:if test="Location">
				<tr>
					<td>Location</td>
					<td><EM>
						<xsl:value-of select="Location"/>
					</EM></td>
				</tr>
			</xsl:if>
		</table></td>
	</tr>
</xsl:template>

<!-- Content TEMPLATE -->
<xsl:template match="Content">
	<xsl:if test="Object">
		<xsl:call-template name="GenerateTable">
			<xsl:with-param name="ID">ObjectList</xsl:with-param>
			<xsl:with-param name="Title">Content</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
</xsl:template>

<xsl:template match="Object" mode="TableRow">
	<td><xsl:call-template name="LinkObjectStd"/></td>
	<td><xsl:value-of select="ObjTitle"/></td>
	<td><xsl:value-of select="ClassName"/></td>
	<td><xsl:value-of select="Modified"/></td>
</xsl:template>

<!-- PromptPart TEMPLATE -->
<xsl:template match="PromptPart">
<div>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Prompt</xsl:with-param>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<tr class="textoGrisTabla"><td width="25%">
			<xsl:text>Associated Table</xsl:text>
		</td><td>
			<xsl:for-each select="AssocTable">
				<xsl:call-template name="LinkObjectStd"/>
			</xsl:for-each>
		</td></tr>
	 </table>
	</div>
</div>
</xsl:template>

<!-- DataViewPart TEMPLATE -->
<xsl:template match="DataViewPart">
<div>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Structure</xsl:with-param>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<xsl:if test="AssocTable">
			<tr class="textoGrisTabla"><td width="25%">
				<xsl:text>Associated Table</xsl:text>
			</td><td>
				<xsl:for-each select="AssocTable">
					<xsl:call-template name="LinkObjectStd"/>
				</xsl:for-each>
			</td></tr>
			<xsl:call-template name="EndRow"/>
		</xsl:if>
		<tr class="textoGrisTabla"><td width="25%">
			<xsl:text>Associated Data Store</xsl:text>
		</td><td><EM>
			<xsl:value-of select="AssocDataStore"/>
		</EM></td></tr>
		<xsl:call-template name="EndRow"/>
		<tr class="textoGrisTabla"><td width="25%" valign="top">
			<xsl:text>Composition</xsl:text>
		</td><td>
			<xsl:choose>
				<xsl:when test="Composition/Attribute">
					<table width="100%" cellspacing="0" cellpadding="0">
					<tr class="subtitulosTabla">
						<td width="25%"><b>Name</b></td>
						<td width="25%"><b>External Name</b></td>
						<td width="10%"><b>Type</b></td>
						<td width="40%"><b>Description</b></td>
					</tr>
					<xsl:for-each select="Composition/Attribute">
						<tr class="textoGrisTabla">
							<td>
								<xsl:call-template name="LinkObject">
									<xsl:with-param name="ObjCls">5</xsl:with-param>
									<xsl:with-param name="ObjId" select="Id"/>
									<xsl:with-param name="ObjName" select="Name"/>
								</xsl:call-template>
							</td>
							<td><xsl:value-of select="Text"/></td>
							<td><xsl:value-of select="Type"/></td>
							<td><xsl:value-of select="Header"/></td>
						</tr>
						<xsl:call-template name="SeparatorRow"/>
					</xsl:for-each>
					</table>
				</xsl:when>
				<xsl:otherwise><EM>
					<xsl:choose><xsl:when test="AssocTable">
						<xsl:text>Dynamic</xsl:text>
					</xsl:when><xsl:otherwise>
						<xsl:text>Empty</xsl:text>
					</xsl:otherwise></xsl:choose>
				</EM></xsl:otherwise>
			</xsl:choose>
		</td></tr>
		<xsl:call-template name="EndRow"/>
		<xsl:apply-templates select="PlatformInfo"/>
		<xsl:if test="Indices">
		<tr class="textoGrisTabla"><td width="25%" valign="top">
			<xsl:text>Indices</xsl:text>
		</td><td>
			<table width="100%" cellspacing="0" cellpadding="0">
				<xsl:for-each select="Indices/Indice">
				<tr class="textoGrisTabla"><td width="25%" valign="top">
					<b><xsl:value-of select="Name"/></b>
				</td><td>
					<table width="100%" cellspacing="0" cellpadding="0">
						<tr class="textoGrisTabla"><td>Type</td><td>
							<EM><xsl:value-of select="Type"/></EM>
						</td></tr>
						<xsl:call-template name="EndRow"/>
						<tr class="textoGrisTabla"><td>Composition</td><td>
							<table width="100%" cellspacing="0" cellpadding="0">
							<xsl:for-each select="Composition/Attribute">
								<tr><td>
								<xsl:apply-templates select="."/>
								</td></tr>
							</xsl:for-each>
							</table>
						</td></tr>
						<xsl:call-template name="EndRow"/>
						<xsl:apply-templates select="PlatformInfo"/>
					</table>
				</td></tr>
				<xsl:call-template name="EndRow"/>
				</xsl:for-each>
			</table>
		</td></tr>
		<xsl:call-template name="EndRow"/>
		</xsl:if>
	</table>
	</div>
</div>
</xsl:template>

<xsl:template match="PlatformInfo">
	<tr class="textoGrisTabla"><td width="25%" valign="top">
		<xsl:text>Platform Information</xsl:text>
	</td><td>
		<table width="100%" cellspacing="0" cellpadding="0">
			<xsl:for-each select="Platform">
				<tr class="textoGrisTabla"><td width="25%" valign="top">
					<b><xsl:value-of select="Name"/></b>
				</td>
					<xsl:if test="Property"><td>
					<table width="100%" cellspacing="0" cellpadding="0">
					<tr class="subtitulosTabla">
						<td width="40%"><b>Name</b></td>
						<td width="60%"><b>Value</b></td>
					</tr>
					<xsl:for-each select="Property">
						<tr class="textoGrisTabla"><td>
							<xsl:value-of select="Name"/>
						</td><td>
							<xsl:value-of select="Value"/>
						</td></tr>
					</xsl:for-each>
					</table>
					</td></xsl:if>
				</tr>
				<xsl:call-template name="SeparatorRow"/>
			</xsl:for-each>
		</table>
	</td></tr>
	<xsl:call-template name="EndRow"/>
</xsl:template>

<!-- MenuBarPart TEMPLATE -->
<xsl:template match="MenuBarPart">
<div>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Menu</xsl:with-param>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<tr class="subtitulosTabla">
			<td width="30%"><b>Name</b></td>
			<td width="20%"><b>Id</b></td>
			<td width="50%"><b>Shortcut</b></td>
		</tr>
		<xsl:apply-templates select="Menu"/>
	</table>
	<xsl:if test="Toolbar/Button">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<tr class="subtitulosTabla">
			<td width="100%"><b>Toolbar</b></td>
		</tr>
		<tr><td>
			<table bgColor="silver" cellspacing="0" style="margin: 3px;border: silver thin solid"><tr>
			<xsl:for-each select="Toolbar/Button|Toolbar/ButtonSeparator">
				<td>
					<xsl:choose><xsl:when test="name()='Button'">
						<img style="border: thin outset">
							<xsl:attribute name="src">
								<xsl:value-of select="Path"/>
							</xsl:attribute>
						</img>
					</xsl:when><xsl:otherwise>
						<xsl:attribute name="width">2</xsl:attribute>
					</xsl:otherwise></xsl:choose>
				</td>
			</xsl:for-each>
			</tr></table>
		</td></tr>
	</table>
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<tr class="subtitulosTabla">
			<td width="15%"><b>Image</b></td>
			<td width="15%"><b>Tooltip</b></td>
			<td width="20%"><b>Id</b></td>
			<td width="50%"><b>Bitmap</b></td>
		</tr>
		<xsl:for-each select="Toolbar/Button">
		<tr class="textoGrisTabla"><td>
			<img>
				<xsl:attribute name="src">
					<xsl:value-of select="Path"/>
				</xsl:attribute>
			</img>
		</td><td>
			<xsl:value-of select="Tooltip"/>
		</td><td>
			<xsl:value-of select="Id"/>
		</td><td>
			<xsl:value-of select="Bitmap"/>
		</td></tr>
		<xsl:call-template name="EndRow"/>
		</xsl:for-each>
	</table>
	</xsl:if>
	</div>
</div>
</xsl:template>

<xsl:template match="Menu">
	<xsl:param name="Indentation">0</xsl:param>
	<xsl:if test="@id">
		<tr class="textoGrisTabla"><td>
			<xsl:attribute name="style">
				<xsl:text>padding-left: </xsl:text>
				<xsl:value-of select="$Indentation"/>
			</xsl:attribute>
			<xsl:value-of select="@name" disable-output-escaping="yes"/>
		</td><td>
			<xsl:value-of select="@id"/>
		</td><td>
			<xsl:value-of select="@shortcut"/>
		</td></tr>
		<xsl:call-template name="EndRow"/>
	</xsl:if>
	<xsl:for-each select="MenuSeparator|Menu">
		<xsl:apply-templates select=".">
			<xsl:with-param name="Indentation">
				<xsl:choose><xsl:when test="../@id">
					<xsl:value-of select="$Indentation+10"/>
				</xsl:when><xsl:otherwise>
					<xsl:value-of select="$Indentation"/>
				</xsl:otherwise></xsl:choose>
			</xsl:with-param>
		</xsl:apply-templates>
	</xsl:for-each>
</xsl:template>

<xsl:template match="MenuSeparator">
	<xsl:param name="Indentation">0</xsl:param>
	<tr>
		<td>
			<xsl:attribute name="style">
				<xsl:text>padding-left: </xsl:text>
				<xsl:value-of select="$Indentation"/>
				<xsl:text>; padding-top: 2px</xsl:text>
				<xsl:text>; padding-bottom: 2px</xsl:text>
			</xsl:attribute>
			<table width="60%" cellspacing="0">
				<tr><td bgcolor="gray" height="1"/></tr>
			</table>
		</td>
		<td colspan="2"/>
	</tr>
	<xsl:call-template name="EndRow"/>
</xsl:template>

<!-- FormPart TEMPLATE -->
<xsl:template match="FormPart">
<div>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Form</xsl:with-param>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<xsl:for-each select="Screens/Screen">
			<tr><td>
				<img>
				<xsl:attribute name="src"><xsl:value-of select="ImagePath"/></xsl:attribute>
				</img>
			</td></tr>
		</xsl:for-each>
	 </table>
	</div>
</div>
</xsl:template>

<!-- EventPart TEMPLATE -->
<xsl:template match="EventPart">
	<xsl:if test="not(../Filters/NoRules) and Rules">
		<tr><td colspan="2"><xsl:apply-templates select="Rules"/></td></tr>
	</xsl:if>
	<xsl:if test="not(../Filters/NoConditions) and Conditions">
		<tr><td colspan="2"><xsl:apply-templates select="Conditions"/></td></tr>
	</xsl:if>
	<xsl:if test="not(../Filters/NoSource) and Events">
		<tr><td colspan="2"><xsl:apply-templates select="Events"/></td></tr>
	</xsl:if>
	<xsl:if test="not(../Filters/NoSource) and Subroutines">
		<tr><td colspan="2"><xsl:apply-templates select="Subroutines"/></td></tr>
	</xsl:if>
</xsl:template>

<!-- VarsPart TEMPLATE -->
<xsl:template match="VarsPart">
	<xsl:if test="Variable">
		<xsl:call-template name="GenerateTable">
			<xsl:with-param name="ID">Variables</xsl:with-param>
			<xsl:with-param name="Title">Variables</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
</xsl:template>

<xsl:template match="Variable" mode="TableRow">
	<td><xsl:value-of select='Name'/></td>
	<td><xsl:value-of select='Title'/></td>
	<td>
		<xsl:choose>
			<xsl:when test='UserDefType'>
				<xsl:value-of select='UserDefType'/>
			</xsl:when>
			<xsl:when test='Type'>
				<xsl:value-of select='Type'/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:text>Number</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
		<xsl:if test="Rows">
			<xsl:text>[</xsl:text>
			<xsl:value-of select="Rows"/>
			<xsl:if test="Columns">
				<xsl:text>,</xsl:text>
				<xsl:value-of select="Columns"/>
			</xsl:if>
			<xsl:text>]</xsl:text>
		</xsl:if>
	</td>
	<td>
		<xsl:if test="Length!='0'">
			<xsl:value-of select="Length"/>
		</xsl:if>
	</td>
	<td>
		<xsl:if test="Decimals!='0'">
			<xsl:value-of select="Decimals"/>
		</xsl:if>
	</td>
	<td><xsl:value-of select='Picture'/></td>
</xsl:template>


<!-- LayoutPart TEMPLATE -->
<xsl:template match="LayoutPart">
<div>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Layout</xsl:with-param>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<xsl:for-each select="CodeBlock|PrintBlock">
		  	<tr class="textoGrisTabla">
			  	<td><xsl:apply-templates select="."/></td>
		  	</tr>
		</xsl:for-each>
	</table>
	</div>
</div>
</xsl:template>

<!-- CodeBlock TEMPLATE -->
<xsl:template match="CodeBlock">
	<PRE><xsl:value-of select="."/></PRE>
</xsl:template>

<!-- PrintBlock TEMPLATE -->
<xsl:template match="PrintBlock">
	<img><xsl:attribute name="src">
		<xsl:value-of select="."/>
	</xsl:attribute></img>
</xsl:template>

<!-- Source TEMPLATE -->
<xsl:template match="Source">
	<PRE> <xsl:value-of select="."/> </PRE>
</xsl:template>

<!-- Rules TEMPLATE -->
<xsl:template match="Rules">
	<xsl:for-each select="..">
	<xsl:call-template name="GenerateTable">
		<xsl:with-param name="ID">Rules</xsl:with-param>
		<xsl:with-param name="Title">Rules</xsl:with-param>
		<xsl:with-param name="Subtitulo">false</xsl:with-param>
	</xsl:call-template>
	</xsl:for-each>
</xsl:template>

<xsl:template match="Rules" mode="TableRow">
	<td><PRE><xsl:value-of select="."/></PRE></td>
</xsl:template>


<!-- Events TEMPLATE -->
<xsl:template match="Events">
	<xsl:for-each select="..">
	<xsl:call-template name="GenerateTable">
		<xsl:with-param name="ID">Events</xsl:with-param>
		<xsl:with-param name="Title">Source</xsl:with-param>
		<xsl:with-param name="Subtitulo">false</xsl:with-param>
	</xsl:call-template>
	</xsl:for-each>
</xsl:template>

<xsl:template match="Events" mode="TableRow">
	<td><PRE><xsl:value-of select="."/></PRE></td>
</xsl:template>


<!-- Conditions TEMPLATE -->
<xsl:template match="Conditions">
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Conditions</xsl:with-param>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<tr class="textoGrisTabla">
			<td><PRE><xsl:value-of select="."/></PRE></td>
		</tr>
	</table>
	</div>
</xsl:template>

<!-- Subroutines TEMPLATE -->
<xsl:template match="Subroutines">
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Source</xsl:with-param>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<tr class="textoGrisTabla">
			<td><PRE><xsl:value-of select="."/></PRE></td>
		</tr>
	</table>
	</div>
</xsl:template>

<!-- StructPart TEMPLATE -->
<xsl:template match="StructPart">
<div>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Structure</xsl:with-param>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<tr><td>
		<table width="100%" cellspacing="0" cellpadding="0">
		<tr class="subtitulosTabla">
			<td width="25%"><b>Name</b></td>
			<td width="7%"><b>Flags</b></td>
			<td width="10%"><b>Type</b></td>
			<td><b>Description</b></td>
			<td><b>Formula</b></td>
		</tr>
		<xsl:for-each select="Lines/Line">
			<tr class="textoGrisTabla">
				<td>
					<span>
						<xsl:call-template name="GxOpen">
							<xsl:with-param name="Class">5</xsl:with-param>
							<xsl:with-param name="Id" select="Attribute/AttriId"/>
						</xsl:call-template>
						<xsl:value-of select="translate(Text,'*','')"/>
					</span>
					<xsl:if test="Flags/PrimaryKey">
						<xsl:call-template name="RenderImage">
							<xsl:with-param name="Id">Key</xsl:with-param>
						</xsl:call-template>
					</xsl:if>
				</td>
				<td><xsl:call-template name="Flags"/></td>
				<td><xsl:value-of select="Attribute/TypeStr"/></td>
				<td><xsl:value-of select="Attribute/AttriDesc"/></td>
				<td><xsl:call-template name="Formula"/></td>
			</tr>
			<xsl:call-template name="EndRow"/>
		</xsl:for-each>
		</table>
		<xsl:if test="Tables/Table">
			<P></P>
			<table class="textoGrisTabla" cellspacing="0" cellpadding="0">
				<tr><td><b>Directly Associated Tables for each Level</b></td></tr>
				<xsl:for-each select="Tables/Table">
					<tr><td>
						<xsl:call-template name="LinkObject">
							<xsl:with-param name="ObjCls">7</xsl:with-param>
							<xsl:with-param name="ObjId" select="Id"/>
							<xsl:with-param name="ObjName" select="Name"/>
						</xsl:call-template>
					</td></tr>
				</xsl:for-each>
			</table>
		</xsl:if>
		</td></tr>
	</table>
	</div>
</div>
</xsl:template>

<xsl:template name="Flags">
	<xsl:if test="Attribute/FormulaDef">
		<xsl:call-template name="RenderImage">
			<xsl:with-param name="Id">Frml</xsl:with-param>
		</xsl:call-template>
	</xsl:if>
	<xsl:if test="Attribute/AttriSuper">
		<span title="Subtype">S</span>
	</xsl:if>
</xsl:template>

<xsl:template name="Formula">
	<xsl:apply-templates select="Attribute/FormulaDef"/>
	<xsl:if test="Attribute/AttriSuper">
		<xsl:text>Subtyle of </xsl:text>
		<xsl:apply-templates select="Attribute/AttriSuper"/>
	</xsl:if>
</xsl:template>

<!-- PropPart TEMPLATE -->
<xsl:template match="PropPart">
	<xsl:call-template name="GenerateTable">
		<xsl:with-param name="ID">Properties</xsl:with-param>
		<xsl:with-param name="Title">Properties</xsl:with-param>
	</xsl:call-template>
</xsl:template>

<xsl:template match="OneProp" mode="TableRow">
	<td><xsl:value-of select="Name"/></td>
	<td><xsl:value-of select="Value"/></td>
</xsl:template>


<!-- CrossReference TEMPLATE -->
<xsl:template match="CrossReference">
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Cross Reference</xsl:with-param>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
	<tr>
		<td width="50%">
		<table width="100%" class="textoGrisTabla" cellspacing="0" cellpadding="0" height="100%">
			<tr class="subTitulosTabla">
				<td><b>References</b></td>
			</tr>
			<xsl:for-each select="Called/XRefObject">
				<tr><td><xsl:apply-templates select="."/></td></tr>
				<xsl:call-template name="EndRow"/>
			</xsl:for-each>
		</table>
		</td>
		<td width="50%">
		<table width="100%" class="textoGrisTabla" cellspacing="0" cellpadding="0" height="100%">
			<tr class="subtitulosTabla">
				<td><b>Referenced by</b></td>
			</tr>
			<xsl:for-each select="CallBy/XRefObject">
				<tr><td><xsl:apply-templates select="."/></td></tr>
				<xsl:call-template name="EndRow"/>
			</xsl:for-each>
		</table></td>
	</tr>
	</table>
	</div>
</xsl:template>

<!-- HtmlSource TEMPLATE -->
<xsl:template name="HtmlSource">
<xsl:param name="Title"/>
<xsl:param name="SameFont">no</xsl:param>
<div>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title" select="$Title"/>
	</xsl:call-template>
	<div id="content">
	<table width="100%" class="gxcontent" cellspacing="0" cellpadding="0">
		<tr>
			<xsl:if test="$SameFont='no'">
				<xsl:attribute name="class">textoGrisTabla</xsl:attribute>
			</xsl:if>
			<td><xsl:value-of disable-output-escaping="yes" select="Source"/></td>
		</tr>
	</table>
	</div>
</div>
</xsl:template>

<!-- Help TEMPLATE -->
<xsl:template match="Help">
	<xsl:call-template name="HtmlSource">
		<xsl:with-param name="Title">Help</xsl:with-param>
	</xsl:call-template>
</xsl:template>

<!-- Documentation TEMPLATE -->
<xsl:template match="Documentation">
	<xsl:call-template name="HtmlSource">
		<xsl:with-param name="Title">Documentation</xsl:with-param>
	</xsl:call-template>
</xsl:template>

<!-- HtmlPart TEMPLATE -->
<xsl:template match="HtmlPart">
	<xsl:call-template name="HtmlSource">
		<xsl:with-param name="Title">Web Form</xsl:with-param>
		<xsl:with-param name="SameFont">yes</xsl:with-param>
	</xsl:call-template>
</xsl:template>


<!-- StructDataType TEMPLATE -->
<xsl:template name="StructDataType">
<div>
	<xsl:call-template name="TableHeader">
		<xsl:with-param name="title">Structure</xsl:with-param>
	</xsl:call-template>
	<div id="content">
		<xsl:apply-templates select="StructDataType"/>
	</div>
</div>
</xsl:template>

<!-- XRefObject, AssocStyle, Folder, InfoPart TEMPLATE -->
<xsl:template match="XRefObject|AssocStyle|Folder|InfoPart">
	<xsl:call-template name="LinkObjectStd"/>
</xsl:template>

<xsl:template match="StructDataType|SDTLine">
<div>
	<div id="wholeNugget">
		<table border="0" cellspacing="0" cellpadding="0" width="100%">
		<tbody>
			<xsl:variable name="ClassStyle">
				<xsl:choose>
					<xsl:when test="Expandable='True'">subTitulosTabla</xsl:when>
					<xsl:otherwise>textoGrisTabla</xsl:otherwise>
				</xsl:choose>
			</xsl:variable>
			<tr>
				<xsl:attribute name="class"><xsl:value-of select="$ClassStyle"/></xsl:attribute>
				<td width="35%">
					<xsl:attribute name="title"><xsl:value-of select="Name"/></xsl:attribute>
					<xsl:value-of select="Name"/>
					<xsl:if test="Collection='True'"> (Collection)</xsl:if>
				</td>
				<td>
				<xsl:if test="Type!=''">
					<xsl:call-template name="LinkObject">
						<xsl:with-param name="ObjName" select="Type"/>
						<xsl:with-param name="ObjCls" select="TypeClass"/>
						<xsl:with-param name="ObjId" select="TypeId"/>
					</xsl:call-template>
				</xsl:if>
				</td>
				<xsl:if test="Expandable='True'">
					<xsl:call-template name="closeNugget">
						<xsl:with-param name="tdClass" select="$ClassStyle"/>
					</xsl:call-template>
				</xsl:if>
			</tr>
		</tbody>
		</table>
	</div>
	<div id="content">
		<xsl:if test="Expandable='True'">
			<table align="right" width="98%" cellspacing="0" cellpadding="0">
				<xsl:for-each select="Lines/SDTLine">
					<tr><td colspan="3"><xsl:apply-templates select="."/></td></tr>
					<xsl:call-template name="EndRow"/>
				</xsl:for-each>
			</table>
		</xsl:if>
	</div>
</div>
</xsl:template>

</xsl:stylesheet>