<?xml version='1.0'?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxml="urn:schemas-microsoft-com:xslt"
	xmlns:gx="urn:shemas-artech-com:gx"
	exclude-result-prefixes="msxml gx">

<xsl:variable name="gxpath" select="/ObjSpecs/gxpath"/>


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
//</xsl:comment></script>
		
			
<FORM>
	<TABLE class="staticTable"  width="100%" cellSpacing="0">
		<TR>
			<TH width="20%"  class="gxheadercolumn" ><B><FONT class="titleSmall">Status</FONT></B></TH>
			<TH width="8%"  class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Transaction </xsl:text></FONT></TH>
			<TH width="8%"  class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Procedure </xsl:text></FONT></TH>
			<TH width="8%" class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Report </xsl:text></FONT></TH>
			<TH width="8%"  class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Work Panel </xsl:text></FONT></TH>
			<TH width="8%" class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Web Panel </xsl:text></FONT></TH>
			<TH width="8%"  class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Table,
			DataView </xsl:text></FONT></TH>
			<TH width="8%" class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Att,Grp,Dom,Frm </xsl:text></FONT></TH>
			<TH width="8%" class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Folder </xsl:text></FONT></TH>
			<TH width="8%" class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Menu, 
			MenuBar </xsl:text></FONT></TH>
			<TH width="8%" class="gxheadercolumn" ><FONT class="titleSmall"><xsl:text> Styles </xsl:text></FONT></TH>
		</TR>
		<TBODY>
			<xsl:call-template name="Totals">
				<xsl:with-param name="Status">Failed</xsl:with-param>
				<xsl:with-param name="iStatus">0</xsl:with-param>
				<xsl:with-param name="Warnings">1</xsl:with-param>
				<xsl:with-param name="bmp">fail.bmp</xsl:with-param>
				<xsl:with-param name="color">gxcolumnpar</xsl:with-param>
			</xsl:call-template>
			

			<xsl:call-template name="Totals">
				<xsl:with-param name="Status">New (with warning)</xsl:with-param>
				<xsl:with-param name="iStatus">1</xsl:with-param>
				<xsl:with-param name="Warnings">1</xsl:with-param>
				<xsl:with-param name="bmp">ok_w.bmp</xsl:with-param>
				<xsl:with-param name="color">gxcolumninpar</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="Totals">
				<xsl:with-param name="Status">New</xsl:with-param>
				<xsl:with-param name="iStatus">1</xsl:with-param>
				<xsl:with-param name="Warnings">0</xsl:with-param>
				<xsl:with-param name="bmp">ok.bmp</xsl:with-param>
				<xsl:with-param name="color">gxcolumnpar</xsl:with-param>
			</xsl:call-template>

			<xsl:call-template name="Totals">
				<xsl:with-param name="Status">Updated (with warning)</xsl:with-param>
				<xsl:with-param name="iStatus">2</xsl:with-param>
				<xsl:with-param name="Warnings">1</xsl:with-param>
				<xsl:with-param name="bmp">updobj_w.bmp</xsl:with-param>
				<xsl:with-param name="color">gxcolumninpar</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="Totals">
				<xsl:with-param name="Status">Updated</xsl:with-param>
				<xsl:with-param name="iStatus">2</xsl:with-param>
				<xsl:with-param name="Warnings">0</xsl:with-param>
				<xsl:with-param name="bmp">updobj.bmp</xsl:with-param>
				<xsl:with-param name="color">gxcolumnpar</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="Totals">
				<xsl:with-param name="Status">No Changes (with warning)</xsl:with-param>
				<xsl:with-param name="iStatus">3</xsl:with-param>
				<xsl:with-param name="Warnings">1</xsl:with-param>
				<xsl:with-param name="bmp">equal_w.bmp</xsl:with-param>
				<xsl:with-param name="color">gxcolumninpar</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="Totals">
				<xsl:with-param name="Status">No Changes</xsl:with-param>
				<xsl:with-param name="iStatus">3</xsl:with-param>
				<xsl:with-param name="Warnings">0</xsl:with-param>
				<xsl:with-param name="bmp">equal.bmp</xsl:with-param>
				<xsl:with-param name="color">gxcolumnpar</xsl:with-param>
			</xsl:call-template>
			<xsl:call-template name="GlobalTotals">
				<xsl:with-param name="color">gxcolumninpar</xsl:with-param>
			</xsl:call-template>


		</TBODY>
	</TABLE>
</FORM>			
	
	
</body>
</html>
</xsl:template>

<xsl:template name="GlobalTotals">
	<xsl:param name="color"/>
	<TR>
		<TD nowrap="yes"  align="right" valign="top" width="20%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport)"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute> 
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class = 0])"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class = 1])"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class = 2])"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class = 4])"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class = 13])"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class = 7 or Class = 6])"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class = 5 or Class = -5 or Class = 10 or Class = 11])"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class = 8])"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class = 3 or Class = 17])"/></FONT>
		</TD>
		<TD><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
			<FONT class="titleBig">Total: <xsl:value-of select="count(//RowImport[Class >= 18])"/></FONT>
		</TD>
	</TR>
</xsl:template>
			

<xsl:template name="Totals">
	<xsl:param name="Status"/>
	<xsl:param name="iStatus"/>
	<xsl:param name="Warnings"/>
	<xsl:param name="bmp"/>
	<xsl:param name="color"/>
	<xsl:if test="(count(//RowImport[(Status= $iStatus or Status= (32768 + $iStatus))  and (Errors/Error or Warnings/Warning)]) > 0 and $Warnings = 1) or
				(count(//RowImport[(Status= $iStatus) and not(Errors/Error or Warnings/Warning)]) > 0 and $Warnings = 0)">
		<TR>
			<TD nowrap="yes"  valign="top" width="20%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute>
				<TABLE width="100%">
				<TR><TD nowrap="yes" width="10%"><IMG title="Show All" style="cursor: hand"><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
													  <xsl:attribute name="onclick">HideShowAll()</xsl:attribute></IMG> 	
												<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/<xsl:value-of select="$bmp"/></xsl:attribute></IMG>
				<xsl:text>  </xsl:text></TD>
				<TD  width="55%"><xsl:value-of select="$Status"/></TD><TD  width="35%"><STRONG>Total : <xsl:if test="$Warnings = 1">
													<xsl:value-of select="count(//RowImport[(Status= $iStatus or Status= (32768 + $iStatus))  and (Errors/Error or Warnings/Warning)])"/>
												  </xsl:if>
												  <xsl:if test="$Warnings = 0">
													<xsl:value-of select="count(//RowImport[(Status= $iStatus) and not(Errors/Error or Warnings/Warning)])"/>
												  </xsl:if></STRONG></TD></TR></TABLE>
												   
			</TD>
			<TD valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs"><xsl:with-param name="Warnings" select="$Warnings"/><xsl:with-param name="Status" select="$iStatus"/><xsl:with-param name="Type">0</xsl:with-param></xsl:call-template></TD>
			<TD valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs"><xsl:with-param name="Warnings" select="$Warnings"/><xsl:with-param name="Status" select="$iStatus"/><xsl:with-param name="Type">1</xsl:with-param></xsl:call-template></TD>
			<TD valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs"><xsl:with-param name="Warnings" select="$Warnings"/><xsl:with-param name="Status" select="$iStatus"/><xsl:with-param name="Type">2</xsl:with-param></xsl:call-template></TD>
			<TD valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs"><xsl:with-param name="Warnings" select="$Warnings"/><xsl:with-param name="Status" select="$iStatus"/><xsl:with-param name="Type">4</xsl:with-param></xsl:call-template></TD>
			<TD valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs"><xsl:with-param name="Warnings" select="$Warnings"/><xsl:with-param name="Status" select="$iStatus"/><xsl:with-param name="Type">13</xsl:with-param></xsl:call-template></TD>
			<TD valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs">
											<xsl:with-param name="Status" select="$iStatus"/>
											<xsl:with-param name="Type">6</xsl:with-param>
											<xsl:with-param name="TypeMin">7</xsl:with-param>
											<xsl:with-param name="Warnings" select="$Warnings"/>
										</xsl:call-template>
			</TD>
			<TD valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs">
											<xsl:with-param name="Status" select="$iStatus"/>
											<xsl:with-param name="Type">5</xsl:with-param>
											<xsl:with-param name="TypeMin">10</xsl:with-param>
											<xsl:with-param name="TypeMax">11</xsl:with-param>
											<xsl:with-param name="Warnings" select="$Warnings"/>
										</xsl:call-template>
			</TD>
			<TD  valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs"><xsl:with-param name="Warnings" select="$Warnings"/><xsl:with-param name="Status" select="$iStatus"/><xsl:with-param name="Type">8</xsl:with-param></xsl:call-template></TD>
			<TD  valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs">
											<xsl:with-param name="Type">3</xsl:with-param>
											<xsl:with-param name="TypeMin">17</xsl:with-param>
											<xsl:with-param name="Status" select="$iStatus"/>
											<xsl:with-param name="Warnings" select="$Warnings"/>
										</xsl:call-template>
			</TD>
			<TD valign="top" width="8%"><xsl:attribute name="class"><xsl:value-of select="$color"/></xsl:attribute><xsl:call-template name="Objs">
											<xsl:with-param name="Type">18</xsl:with-param>
											<xsl:with-param name="TypeMax">24</xsl:with-param>
											<xsl:with-param name="Status" select="$iStatus"/>
											<xsl:with-param name="Warnings" select="$Warnings"/>
										</xsl:call-template>
			</TD>
		</TR>
	</xsl:if>
</xsl:template>

<xsl:template match="Name">
<SPAN>
	<xsl:if test="../Id">
		<xsl:if test="../Class[.!=10]">
			<xsl:attribute name="title"><xsl:value-of select="../Description"/></xsl:attribute>
			<xsl:attribute name="class">gxobj</xsl:attribute>
			<xsl:attribute name="STYLE">CURSOR: hand</xsl:attribute>
			<xsl:attribute name="onclick">try{gxopen(<xsl:value-of select="../Class"/>,<xsl:value-of select="../Id"/>)}
											catch(Exception){}</xsl:attribute>
			<xsl:attribute name="oncontextmenu">try{gxcontextmenu(<xsl:value-of select="../Class"/>,<xsl:value-of select="../Id"/>)}catch(Exception){}</xsl:attribute>
		</xsl:if>
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
		<TABLE>
			<TR><TD noWrap="yes" valign="top">
						<xsl:if test="$Warnings = 0">
							<xsl:variable name="Cantidad"><xsl:value-of select="count(//RowImport[(Status=$Status) and (Class=-$Type or Class = $Type or (Class >= $TypeMin and $TypeMax >= Class)) and not(Errors/Error or Warnings/Warning) ])"/></xsl:variable>
							<xsl:if test="$Cantidad > 0"><IMG title="Show" style="CURSOR: hand">
								<xsl:attribute name="onclick">HideAndShow()</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
							</IMG>
								<STRONG> <xsl:value-of select="$Cantidad"/> </STRONG>
							</xsl:if>
						</xsl:if>
						<xsl:if test="$Warnings = 1">
							<xsl:variable name="Cantidad"><xsl:value-of select="count(//RowImport[(Status=$Status or Status =-$Status or Status=(32768 + $Status))  and (Class=-$Type or Class = $Type or (Class >= $TypeMin and $TypeMax >= Class)) and (Errors/Error or Warnings/Warning) ])"/></xsl:variable>
							<xsl:if test="$Cantidad > 0"><IMG title="Show" style="CURSOR: hand">
								<xsl:attribute name="onclick">HideAndShow()</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
							</IMG>
								<STRONG> <xsl:value-of select="$Cantidad"/> </STRONG>
							</xsl:if>
						</xsl:if>
				</TD>
		</TR>
		</TABLE>
		<TABLE style="display: none;">	
			<xsl:if test="$Warnings = 0">
				<xsl:for-each select="//RowImport[(Status=$Status or Status =-$Status) and (Class=-$Type or Class = $Type or (Class >= $TypeMin and $TypeMax >= Class)) and not(Errors/Error or Warnings/Warning) ]">
					<xsl:sort select="Name"/>
					<xsl:apply-templates select="."/>
				</xsl:for-each>
			</xsl:if>
			<xsl:if test="$Warnings = 1">
				<xsl:for-each select="//RowImport[(Status=$Status or Status =-$Status or Status=(32768 + $Status))  and (Class=-$Type or Class = $Type or (Class >= $TypeMin and $TypeMax >= Class)) and (Errors/Error or Warnings/Warning) ]">
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
			<TABLE width="100%">
				<TR><TD><IMG title="Show" style="CURSOR: hand">
							<xsl:attribute name="onclick">HideAndShow()</xsl:attribute>
							<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
						</IMG>
						<xsl:apply-templates select="Class"/><xsl:apply-templates select="Name"/></TD></TR>
			</TABLE>
				<TABLE width="100%" class="info" style="display:none;">
					
					<TR><TD nowrap="yes"><xsl:text>Description: </xsl:text><xsl:apply-templates select="Description"/></TD></TR>
					<xsl:if test="Infos/Folder">
						<TR><TD nowrap="yes"><xsl:text>Folder: </xsl:text><xsl:apply-templates select="Infos/Folder"/></TD></TR>
					</xsl:if>
					<xsl:if test="Errors/Error">
						<TR><TD nowrap="yes"><xsl:apply-templates select="Errors"/></TD></TR>
					</xsl:if>
					<xsl:if test="Warnings/Warning">
						<TR><TD nowrap="yes"><xsl:apply-templates select="Warnings"/></TD></TR>
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
	<IMG><xsl:attribute name="src"><xsl:value-of select="$gxpath"/>gxxml/<xsl:call-template name="GxClass"><xsl:with-param name="Type"><xsl:value-of select="."/></xsl:with-param></xsl:call-template>.bmp</xsl:attribute></IMG>  
</xsl:template>


<xsl:template match="Folder">
	<xsl:choose>
		<xsl:when test="text()[.!='']">
			<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/fld.bmp</xsl:attribute></IMG><xsl:text> </xsl:text><xsl:value-of select="."/>  
		</xsl:when>
		<xsl:when test="text()[.!='']">
			<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/fld.bmp</xsl:attribute></IMG><xsl:text> </xsl:text> Main  
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
