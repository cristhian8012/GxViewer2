<?xml version='1.0'?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxml="urn:schemas-microsoft-com:xslt"
	xmlns:gx="urn:shemas-artech-com:gx"
	exclude-result-prefixes="msxml gx">

<xsl:variable name="gxpath" select="/ObjSpecs/gxpath"/>

<xsl:include href="include.xsl"/>

<xsl:template match="/">
<html>
<head>
<!-- Include Cascade Style Sheet -->
<LINK REL="stylesheet" TYPE="text/css">
<xsl:attribute name="HREF"><xsl:value-of select="$gxpath"/>gxxml/spec.css</xsl:attribute>
</LINK>
</head>
<body>
<!-- Include Script Code -->
<script><xsl:attribute name="src"><xsl:value-of select="$gxpath"/>gxxml/include.js</xsl:attribute><xsl:comment/></script>

<!-- Set GX path OnLoad Event -->
<script for="window" event="onload">
<xsl:comment>
	SetGXPath("<xsl:value-of select="$gxpath"/>")
	SetRowsColor("GxResult");
//</xsl:comment></script>
		
			
<FORM>
	<TABLE id="GxResult" class="titulosRosa"  width="100%" cellSpacing="0" border="0" cellpadding="0">
		<TR class="gxheadercolumn">
			<TH width="5%" class="gxheadercolumn"><B><xsl:text>Status</xsl:text></B></TH>
			<TH width="8%" class="gxheadercolumn"><B><xsl:text> Failed </xsl:text></B></TH>
			<TH width="8%" class="gxheadercolumn"><xsl:text> New (warning) </xsl:text></TH>
			<TH width="8%" class="gxheadercolumn"><xsl:text> New </xsl:text></TH>
			<TH width="8%" class="gxheadercolumn"><xsl:text> Updated (warning) </xsl:text></TH>
			<TH width="8%" class="gxheadercolumn"><xsl:text> Updated </xsl:text></TH>
			<TH width="8%" class="gxheadercolumn"><xsl:text> No Changes (warning) </xsl:text></TH>
			<TH width="8%" class="gxheadercolumn"><xsl:text> No Changes </xsl:text></TH>
			<TH width="8%" class="gxheadercolumn"><xsl:text> Totals </xsl:text></TH>
		</TR>
		<TBODY>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Transaction</xsl:with-param>
				<xsl:with-param name="Type">0</xsl:with-param>
			</xsl:call-template>
			
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Procedure</xsl:with-param>
				<xsl:with-param name="Type">1</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Report</xsl:with-param>
				<xsl:with-param name="Type">2</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Menu</xsl:with-param>
				<xsl:with-param name="Type">3</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Work Panel</xsl:with-param>
				<xsl:with-param name="Type">4</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Data View</xsl:with-param>
				<xsl:with-param name="Type">6</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Table</xsl:with-param>
				<xsl:with-param name="Type">7</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Prompt</xsl:with-param>
				<xsl:with-param name="Type">12</xsl:with-param>
			</xsl:call-template>
	
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Attribute</xsl:with-param>
				<xsl:with-param name="Type">5</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Formula</xsl:with-param>
				<xsl:with-param name="Type">-5</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Folder</xsl:with-param>
				<xsl:with-param name="Type">8</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Model</xsl:with-param>
				<xsl:with-param name="Type">9</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Group</xsl:with-param>
				<xsl:with-param name="Type">10</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Domain</xsl:with-param>
				<xsl:with-param name="Type">11</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Web Panel</xsl:with-param>
				<xsl:with-param name="Type">13</xsl:with-param>
			</xsl:call-template>
			<!-- 14 - external progra, 15-unused, 16-form class -->
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Menu Bar</xsl:with-param>
				<xsl:with-param name="Type">17</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Themes</xsl:with-param>
				<xsl:with-param name="Type">25</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Structure Data Type</xsl:with-param>
				<xsl:with-param name="Type">26</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Trn Style</xsl:with-param>
				<xsl:with-param name="Type">18</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Prc Style</xsl:with-param>
				<xsl:with-param name="Type">19</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Rpt Style</xsl:with-param>
				<xsl:with-param name="Type">20</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Wkp Style</xsl:with-param>
				<xsl:with-param name="Type">21</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Prompt Style</xsl:with-param>
				<xsl:with-param name="Type">22</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Wbp Style</xsl:with-param>
				<xsl:with-param name="Type">23</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="ObjTotals">
				<xsl:with-param name="Name">Mbr Style</xsl:with-param>
				<xsl:with-param name="Type">24</xsl:with-param>
			</xsl:call-template>
	<!--		<xsl:call-template name="GlobalTotals">
				<xsl:with-param name="color">gxcolumninpar</xsl:with-param>
			</xsl:call-template>
	-->	</TBODY>
	</TABLE>
</FORM>			
</body>
</html>
</xsl:template>


<xsl:template name="GlobalTotals">
	<xsl:param name="color"/>
	<TR class="text">
		<TD  class="gxcolumninpar" nowrap="yes"  valign="top">
			<B>Totals</B>
		</TD>
		<TD class="gxcolumninpar"> 
			<B><xsl:value-of select="count(//RowImport[(Status= 0 or Status= 32768)])"/></B>
		</TD>
		<TD class="gxcolumninpar">
			<B><xsl:value-of select="count(//RowImport[(Status= 1 or Status= 32769) and (Errors/Error or Warnings/Warning)])"/></B>
		</TD>
		<TD class="gxcolumninpar">
			<B><xsl:value-of select="count(//RowImport[(Status= 1 or Status= 32769) and not(Errors/Error or Warnings/Warning)])"/></B>
		</TD>
		<TD class="gxcolumninpar">
			<B><xsl:value-of select="count(//RowImport[(Status= 2 or Status= 32770) and (Errors/Error or Warnings/Warning)])"/></B>
		</TD>
		<TD class="gxcolumninpar">
			<B><xsl:value-of select="count(//RowImport[(Status= 2 or Status= 32770) and not(Errors/Error or Warnings/Warning)])"/></B>
		</TD>
		<TD class="gxcolumninpar">
			<B><xsl:value-of select="count(//RowImport[(Status= 3 or Status= 32771) and (Errors/Error or Warnings/Warning)])"/></B>
		</TD>
		<TD class="gxcolumninpar">
			<B><xsl:value-of select="count(//RowImport[(Status= 3 or Status= 32771) and not(Errors/Error or Warnings/Warning)])"/></B>
		</TD>
		<TD class="gxcolumninpar">
			<B><xsl:value-of select="count(//RowImport)"/></B>
		</TD>
	</TR>
</xsl:template>
			
<xsl:template name="ObjTotals">
	<xsl:param name="Name"></xsl:param>
	<xsl:param name="Type"></xsl:param>
	<xsl:param name="bmp">none.bmp</xsl:param>
	
	<xsl:if test="//RowImport[Class=$Type]">
	<TR>
		<TD class="gxcolumnpar" valign="top" nowrap="yes">
				<TABLE width="100%" cellSpacing="0" cellpadding="0">
				<TR class="text"><TD nowrap="yes" width="10%">
				<IMG title="Show All" style="cursor: hand">
					<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/images/openNode.gif</xsl:attribute>
					<xsl:attribute name="onclick">HideShowAll()</xsl:attribute></IMG>
				<IMG>
					<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/images/
					<xsl:call-template name="GetClsName">
						<xsl:with-param name="Class"><xsl:value-of select="$Type"/></xsl:with-param>
					</xsl:call-template>.ico
					</xsl:attribute>
				</IMG>
				<xsl:text>  </xsl:text></TD>
				<TD  width="55%"><xsl:value-of select="$Name"/></TD></TR></TABLE>
		</TD>
		<TD class="gxcolumnpar"> <!-- Failed -->
			<xsl:call-template name="Objs">
				<xsl:with-param name="Status">0</xsl:with-param>
				<xsl:with-param name="Type" select="$Type"/>
				<xsl:with-param name="Warnings">1</xsl:with-param>
			</xsl:call-template>
		</TD>
		<TD class="gxcolumnpar"> <!-- New (W)-->
			<xsl:call-template name="Objs">
				<xsl:with-param name="Status">1</xsl:with-param>
				<xsl:with-param name="Type" select="$Type"/>
				<xsl:with-param name="Warnings">1</xsl:with-param>
			</xsl:call-template>

		</TD>
		<TD class="gxcolumnpar"> <!-- New -->
			<xsl:call-template name="Objs">
				<xsl:with-param name="Status">1</xsl:with-param>
				<xsl:with-param name="Type" select="$Type"/>
				<xsl:with-param name="Warnings">0</xsl:with-param>
			</xsl:call-template>
		</TD>
		<TD class="gxcolumnpar"> <!-- Update (W)-->
			<xsl:call-template name="Objs">
				<xsl:with-param name="Status">2</xsl:with-param>
				<xsl:with-param name="Type" select="$Type"/>
				<xsl:with-param name="Warnings">1</xsl:with-param>
			</xsl:call-template>
		</TD>
		<TD class="gxcolumnpar"> <!-- Update -->
			<xsl:call-template name="Objs">
				<xsl:with-param name="Status">2</xsl:with-param>
				<xsl:with-param name="Type" select="$Type"/>
				<xsl:with-param name="Warnings">0</xsl:with-param>
			</xsl:call-template>
		</TD>
		<TD class="gxcolumnpar"> <!-- No Changes (W) -->
			<xsl:call-template name="Objs">
				<xsl:with-param name="Status">3</xsl:with-param>
				<xsl:with-param name="Type" select="$Type"/>
				<xsl:with-param name="Warnings">1</xsl:with-param>
			</xsl:call-template>
		</TD>
		<TD class="gxcolumnpar"> <!-- No Changes  -->
			<xsl:call-template name="Objs">
				<xsl:with-param name="Status">3</xsl:with-param>
				<xsl:with-param name="Type" select="$Type"/>
				<xsl:with-param name="Warnings">0</xsl:with-param>
			</xsl:call-template>
		</TD>
		<TD valign="top">
			<B><xsl:value-of select="count(//RowImport[Class = $Type])"/></B>
		</TD>
	</TR>
	</xsl:if>
</xsl:template>



<xsl:template match="Name">
<SPAN>
	<xsl:if test="../Id">
		<xsl:attribute name="title">
			<xsl:value-of select="."/> : <xsl:value-of select="../Description"/>
		</xsl:attribute>
		<xsl:attribute name="class">gxobj</xsl:attribute>
		<xsl:attribute name="STYLE">CURSOR: hand</xsl:attribute>
		<xsl:attribute name="onclick">
			try{gxopen(<xsl:value-of select="../Class"/>,<xsl:value-of select="../Id"/>)}
			catch(Exception){}
		</xsl:attribute>
		<xsl:attribute name="oncontextmenu">
			try{gxcontextmenu(<xsl:value-of select="../Class"/>,<xsl:value-of select="../Id"/>)}
			catch(Exception){}
		</xsl:attribute>
	</xsl:if>
	<xsl:value-of select="."/>
</SPAN>
</xsl:template>



<xsl:template name="Objs">
	<xsl:param name="Type"/>
	<xsl:param name="TypeMin" select="$Type"/>
	<xsl:param name="TypeMax" select="$TypeMin"/>
	<xsl:param name="Status"/>
	<xsl:param name="Warnings"/>

	<DIV>
		<TABLE cellSpacing="0" cellpadding="0">
		  <TR class="text">
			<TD noWrap="yes" valign="top">
				<xsl:if test="$Warnings = 0">
					<xsl:variable name="Cantidad">
						<xsl:value-of select="count(//RowImport[(Status=$Status) and (Class = $Type or (Class >= $TypeMin and $TypeMax >= Class)) and not(Errors/Error or Warnings/Warning) ])"/>
					</xsl:variable>
					<xsl:if test="$Cantidad > 0">
					  <IMG title="Show" style="CURSOR: hand">
						<xsl:attribute name="onclick">HideAndShow()</xsl:attribute>
						<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/images/openNode.gif</xsl:attribute>
					  </IMG>
					  <STRONG> <xsl:value-of select="$Cantidad"/> </STRONG>
					</xsl:if>
				</xsl:if>
				<xsl:if test="$Warnings = 1">
					<xsl:variable name="Cantidad">
						<xsl:value-of select="count(//RowImport[(Status=$Status or Status =-$Status or Status=(32768 + $Status))  and (Class = $Type or (Class >= $TypeMin and $TypeMax >= Class)) and (Errors/Error or Warnings/Warning) ])"/>
					</xsl:variable>
					<xsl:if test="$Cantidad > 0">
					  <IMG title="Show" style="CURSOR: hand">
						<xsl:attribute name="onclick">HideAndShow()</xsl:attribute>
						<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/images/openNode.gif</xsl:attribute>
					  </IMG>
					  <STRONG> <xsl:value-of select="$Cantidad"/> </STRONG>
					</xsl:if>
				</xsl:if>
			</TD>
		  </TR>
		</TABLE>
		<TABLE style="display: none;" cellSpacing="0" cellpadding="0">	
			<xsl:if test="$Warnings = 0">
				<xsl:for-each select="//RowImport[(Status=$Status or Status =-$Status) and (Class = $Type or (Class >= $TypeMin and $TypeMax >= Class)) and not(Errors/Error or Warnings/Warning) ]">
					<xsl:sort select="Name"/>
					<xsl:apply-templates select="."/>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="$Warnings = 1">
				<xsl:for-each select="//RowImport[(Status=$Status or Status =-$Status or Status=(32768 + $Status))  and (Class = $Type or (Class >= $TypeMin and $TypeMax >= Class)) and (Errors/Error or Warnings/Warning) ]">
					<xsl:sort select="Name"/>
					<xsl:apply-templates select="."/>
				</xsl:for-each>
			</xsl:if>
		</TABLE>
	</DIV>
</xsl:template>

<xsl:template match="RowImport">
		<TR><TD>
			<DIV>
			<TABLE width="100%" cellSpacing="0" cellpadding="0">
				<TR class="text"><TD><IMG title="Show" style="CURSOR: hand">
							<xsl:attribute name="onclick">HideAndShow()</xsl:attribute>
							<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/images/openNode.gif</xsl:attribute>
						</IMG>
						<xsl:apply-templates select="Name"/></TD></TR>
			</TABLE>
				<TABLE width="100%" class="info" style="display:none;" cellSpacing="0" cellpadding="0">
					
					<TR class="text">
					<TD nowrap="yes">
						<xsl:text>Description: </xsl:text>
						<xsl:apply-templates select="Description"/>
					</TD>
					</TR>
					<xsl:if test="Infos/Folder">
						<TR class="text">
						<TD nowrap="yes">
							<xsl:text>Folder: </xsl:text>
							<xsl:apply-templates select="Infos/Folder"/>
						</TD>
						</TR>
					</xsl:if>
					<xsl:if test="Errors/Error">
						<TR class="text">
						<TD nowrap="yes"><xsl:apply-templates select="Errors"/></TD>
						</TR>
					</xsl:if>
					<xsl:if test="Warnings/Warning">
						<TR class="text">
						<TD nowrap="yes">
							<xsl:apply-templates select="Warnings"/>
						</TD>
						</TR>
					</xsl:if>
				</TABLE>
			</DIV>
			</TD>
		</TR>
</xsl:template>

<xsl:template match="Errors">
	<UL>
		<xsl:apply-templates select="Error"/>
	</UL>
</xsl:template>

<xsl:template match="Error">
	<LI><FONT class="gxerr"><xsl:value-of select="."/></FONT></LI>
</xsl:template>


<xsl:template match="Warnings">
	<UL>
		<xsl:apply-templates select="Warning"/>
	</UL>
</xsl:template>

<xsl:template match="Warning">
	<LI><FONT class="gxwarn"><xsl:value-of select="."/></FONT></LI>
</xsl:template>



<xsl:template match="Class">
	<IMG>
	<xsl:attribute name="src"><xsl:value-of select="$gxpath"/>gxxml/images/
		<xsl:call-template name="GetClsName">
			<xsl:with-param name="Class"><xsl:value-of select="."/></xsl:with-param>
		</xsl:call-template>.bmp
	</xsl:attribute>
	</IMG>
</xsl:template>


<xsl:template match="Folder">
	<xsl:choose>
		<xsl:when test="text()[.!='']">
			<IMG>
				<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/images/fld.bmp</xsl:attribute>
			</IMG>
			<xsl:text> </xsl:text><xsl:value-of select="."/>
		</xsl:when>
		<xsl:when test="text()[.!='']">
			<IMG>
				<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/images/fld.bmp</xsl:attribute>
			</IMG>
			<xsl:text> </xsl:text> Main
		</xsl:when>
	</xsl:choose>
</xsl:template>


<!-- Standars -->

<xsl:template name="GxClass">
	<xsl:param name="Type"/>
	<xsl:choose>
		<xsl:when test="$Type = 0">
			Transaction
		</xsl:when>
		<xsl:when test="$Type = 1">
			Procedure
		</xsl:when>
		<xsl:when test="$Type = 2">
			Report
		</xsl:when>
		<xsl:when test="$Type = 3">
			Menu
		</xsl:when>
		<xsl:when test="$Type = 4">
			Work Panel
		</xsl:when>
		<xsl:when test="$Type = 5">
			Att
		</xsl:when>
		<xsl:when test="$Type = -5">
			Formula
		</xsl:when>
		<xsl:when test="$Type = 6">
			DataView
		</xsl:when>
		<xsl:when test="$Type = 7">
			Table
		</xsl:when>
		<xsl:when test="$Type = 8">
			fld
		</xsl:when>
		<xsl:when test="$Type = 10">
			Grp
		</xsl:when>
		<xsl:when test="$Type = 11">
			dom
		</xsl:when>
		<xsl:when test="$Type = 12">
			Prompt
		</xsl:when>
		<xsl:when test="$Type = 13">
			Web Panel
		</xsl:when>
		<xsl:when test="$Type = 17">
			mbr
		</xsl:when>
		<xsl:when test="$Type = 18">
			strn
		</xsl:when>
		<xsl:when test="$Type = 19">
			sprc
		</xsl:when>
		<xsl:when test="$Type = 20">
			srpt
		</xsl:when>
		<xsl:when test="$Type = 21">
			swkp
		</xsl:when>
		<xsl:when test="$Type = 22">
			spmt
		</xsl:when>
		<xsl:when test="$Type = 23">
			swbp
		</xsl:when>
		<xsl:when test="$Type = 24">
			smbr
		</xsl:when>
		<xsl:when test="$Type = 25">
			Themes
		</xsl:when>
		<xsl:when test="$Type = 26">
			SDT
		</xsl:when>
	</xsl:choose>
</xsl:template>




<msxml:script implements-prefix="gx"><![CDATA[


function GetStatusDesc(sVal)
{
	try
	{
		var iVal;
		iVal = parseInt(sVal);
		switch (iVal)
		{
			case 0: case 0+32768: return " not consolidated";
			case 1: case 1+32768: return " is new";
			case 2: case 2+32768: return " was updated";
			case 3: case 3+32768: return " did not changed";
			default: return " Unknow Status";
		}
	}
	catch (exception)
	{
		return "Unknow Status";
	}
}



]]></msxml:script>


</xsl:stylesheet>
