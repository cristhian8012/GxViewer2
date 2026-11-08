<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl" xmlns="http://www.w3.org/TR/REC-html40">
<!-- <xsl:preserve-space elements="*" />  -->
<xsl:template match="/">
<html>
<head>
<!-- Include Cascade Style Sheet -->
<LINK REL="stylesheet" TYPE="text/css">
<xsl:attribute name="HREF"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/spec.css</xsl:attribute>
</LINK>
</head>
<body>
<!-- Include Script Code -->
<script><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/include.js</xsl:attribute><xsl:comment/></script>

<!-- Set GX path OnLoad Event -->
<script for="window" event="onload">
<xsl:comment>
	SetGXPath("<xsl:value-of select="/ObjSpecs/gxpath"/>")
//</xsl:comment></script>


<form action="" id="FORM1" method="post" name="FORM1">

	<xsl:if test="//RowImport[Status=0 or Status=32768]">
		<DIV id="Failed">
			<DIV id="wholeNugget">
				<TABLE width="80%" cellspacing="0">
					<TR>
						<TH id="disp" align="left" colspan="3">
							<IMG style="CURSOR: hand">
								<xsl:attribute name="onclick">displayType(Failed)</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/minus.bmp</xsl:attribute>
							</IMG>
							<TD width="45%"><B>Consolidation Failed</B></TD>
							<TD><B>Total : <xsl:eval>GetCantOf(this,"//RowImport[Status=0 or Status=32768]")</xsl:eval>  </B>
								<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/fail.bmp</xsl:attribute></IMG>
							</TD>
							<HR color="#800000"/>
						</TH>
					</TR>
				</TABLE>
			</DIV>
			<DIV id="content">
				<TABLE width="80%">
					<TR>
						<TH align="left"><B> Name </B></TH>
						<TH align="left"> <B> Description </B> </TH>
					</TR>
					<TBODY>
						<xsl:for-each select="//RowImport[Status=0 or Status=32768]">
							<xsl:apply-templates select="."/>
						</xsl:for-each>
					</TBODY>
				</TABLE>
			</DIV>
		</DIV>
	</xsl:if>

	<xsl:if test="//RowImport[(Status=1 or Status=32769) and Errors/Error]">
		<DIV id="NewWarn">
			<DIV id="wholeNugget">
				<TABLE width="80%" cellspacing="0">
					<TR>
						<TH id="disp" align="left" colspan="3">
							<IMG style="CURSOR: hand">
								<xsl:attribute name="onclick">displayType(NewWarn)</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
							</IMG>
							<TD width="45%"><B>New Objects (with warning)</B></TD>
							<TD><B>Total : <xsl:eval>GetCantOf(this,"//RowImport[(Status=1 or Status=32769) and Errors/Error]")</xsl:eval>  </B>
								<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/ok_W.bmp</xsl:attribute></IMG>
							</TD>
							<HR color="#800000"/>
						</TH>
					</TR>
				</TABLE>
			</DIV>
			<DIV id="content" style="display: none">
				<TABLE width="80%">
					<TR>
						<TH align="left"><B> Name </B></TH>
						<TH align="left"> <B> Description </B> </TH>
					</TR>
					<TBODY>
						<xsl:for-each select="//RowImport[(Status=1 or Status=32769) and Errors/Error]">
							<xsl:apply-templates select="."/>
						</xsl:for-each>
					</TBODY>
				</TABLE>
			</DIV>
		</DIV>
	</xsl:if>

	<xsl:if test="//RowImport[(Status=1 or Status=32769) and not(Errors/Error)]">
		<DIV id="New">
			<DIV id="wholeNugget">
				<TABLE width="80%" cellspacing="0">
					<TR>
						<TH id="disp" align="left" colspan="3">
							<IMG style="CURSOR: hand">
								<xsl:attribute name="onclick">displayType(New)</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
							</IMG>
							<TD width="45%"><B>New Objects Consolidated </B></TD>
							<TD><B>Total : <xsl:eval>GetCantOf(this,"//RowImport[(Status=1 or Status=32769) and not(Errors/Error)]")</xsl:eval>  </B>
								<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/ok.bmp</xsl:attribute></IMG>
							</TD>
							<HR color="#800000"/>
						</TH>
					</TR>
				</TABLE>
			</DIV>
			<DIV id="content" style="display: none">
				<TABLE width="80%">
					<TR>
						<TH align="left"><B> Name </B></TH>
						<TH align="left"> <B> Description </B> </TH>
					</TR>
					<TBODY>
						<xsl:for-each select="//RowImport[(Status=1 or Status=32769) and not(Errors/Error)]">
							<xsl:apply-templates select="."/>
						</xsl:for-each>
					</TBODY>
				</TABLE>
			</DIV>
		</DIV>
	</xsl:if>

	<xsl:if test="//RowImport[(Status=2 or Status=32770) and Errors/Error]">
		<DIV id="UpdWarn">
			<DIV id="wholeNugget">
				<TABLE width="80%" cellspacing="0">
					<TR>
						<TH id="disp" align="left" colspan="3">
							<IMG style="CURSOR: hand">
								<xsl:attribute name="onclick">displayType(UpdWarn)</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
							</IMG>
							<TD width="45%"><B>Objects Updated (with warning)</B></TD>
							<TD><B>Total : <xsl:eval>GetCantOf(this,"//RowImport[(Status=2 or Status=32770) and Errors/Error]")</xsl:eval>  </B>
								<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/updobj_W.bmp</xsl:attribute></IMG>
							</TD>
							<HR color="#800000"/>
						</TH>
					</TR>
				</TABLE>
			</DIV>
			<DIV id="content" style="display: none">
				<TABLE width="80%">
					<TR>
						<TH align="left"><B> Name </B></TH>
						<TH align="left"> <B> Description </B> </TH>
					</TR>
					<TBODY>
						<xsl:for-each select="//RowImport[(Status=2 or Status=32770) and Errors/Error]">
							<xsl:apply-templates select="."/>
						</xsl:for-each>
					</TBODY>
				</TABLE>
			</DIV>
		</DIV>
	</xsl:if>

	<xsl:if test="//RowImport[(Status=2 or Status=32770) and not(Errors/Error)]">
		<DIV id="Upd">
			<DIV id="wholeNugget">
				<TABLE width="80%" cellspacing="0">
					<TR>
						<TH id="disp" align="left" colspan="3">
							<IMG style="CURSOR: hand">
								<xsl:attribute name="onclick">displayType(Upd)</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
							</IMG>
							 <TD width="45%"><B>Objects Updated</B></TD>
							 <TD><B>Total : <xsl:eval>GetCantOf(this,"//RowImport[(Status=2 or Status=32770) and not(Errors/Error)]")</xsl:eval>  </B>
							 	<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/updobj.bmp</xsl:attribute></IMG>
							 </TD>
							<HR color="#800000"/>
						</TH>
					</TR>
				</TABLE>
			</DIV>
			<DIV id="content" style="display: none">
				<TABLE width="80%">
					<TR>
						<TH align="left"><B> Name </B></TH>
						<TH align="left"> <B> Description </B> </TH>
					</TR>
					<TBODY>
						<xsl:for-each select="//RowImport[(Status=2 or Status=32770) and not(Errors/Error)]">
							<xsl:apply-templates select="."/>
						</xsl:for-each>
					</TBODY>
				</TABLE>
			</DIV>
		</DIV>
	</xsl:if>

	<xsl:if test="//RowImport[(Status=3 or Status=32771) and Errors/Error]">
		<DIV id="NoCWarn">
			<DIV id="wholeNugget">
				<TABLE width="80%" cellspacing="0">
					<TR>
						<TH id="disp" align="left" colspan="3">
							<IMG style="CURSOR: hand">
								<xsl:attribute name="onclick">displayType(NoCWarn)</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
							</IMG>
							<TD width="45%"><B>Objects Did Not Change (with warning)</B></TD>
							<TD><B>Total : <xsl:eval>GetCantOf(this,"//RowImport[(Status=3 or Status=32771) and Errors/Error]")</xsl:eval>  </B>
								<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/equal_W.bmp</xsl:attribute></IMG>
							</TD>
							<HR color="#800000"/>
						</TH>
					</TR>
				</TABLE>
			</DIV>
			<DIV id="content" style="display: none">
				<TABLE width="80%">
					<TR>
						<TH align="left"><B> Name </B></TH>
						<TH align="left"> <B> Description </B> </TH>
					</TR>
					<TBODY>
						<xsl:for-each select="//RowImport[(Status=3 or Status=32771) and Errors/Error]">
							<xsl:apply-templates select="."/>
						</xsl:for-each>
					</TBODY>
				</TABLE>
			</DIV>
		</DIV>
	</xsl:if>

	<xsl:if test="//RowImport[(Status=3 or Status=32771) and not(Errors/Error)]">
		<DIV id="NoC">
			<DIV id="wholeNugget">
				<TABLE width="80%" cellspacing="0">
					<TR>
						<TH id="disp" align="left" colspan="3">
							<IMG style="CURSOR: hand">
								<xsl:attribute name="onclick">displayType(NoC)</xsl:attribute>
								<xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/plus.bmp</xsl:attribute>
							</IMG>
							<TD width="45%"><B>Objects Did Not Change </B></TD>
							<TD><B>Total : <xsl:eval>GetCantOf(this,"//RowImport[(Status=3 or Status=32771) and not(Errors/Error)]")</xsl:eval>  </B>
								<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/equal.bmp</xsl:attribute></IMG>
							</TD>
							<HR color="#800000"/>
						</TH>
					</TR>
				</TABLE>
			</DIV>
			<DIV id="content" style="display: none">
				<TABLE width="80%">
					<TR>
						<TH align="left"><B> Name </B></TH>
						<TH align="left"> <B> Description </B> </TH>
					</TR>
					<TBODY>
						<xsl:for-each select="//RowImport[(Status=3 or Status=32771) and not(Errors/Error)]">
							<xsl:apply-templates select="."/>
						</xsl:for-each>
					</TBODY>
				</TABLE>
			</DIV>
		</DIV>
	</xsl:if>

</form>
</body>
</html>
</xsl:template>

<xsl:template match="Name">
<SPAN>
	<xsl:if test="../Id">
		<xsl:if test="../Class[.!=10]">
			<xsl:attribute name="class">gxobj</xsl:attribute>
			<xsl:attribute name="STYLE">CURSOR: hand</xsl:attribute>
			<xsl:attribute name="onclick">try{gxopen(<xsl:value-of select="../Class"/>,<xsl:value-of select="../Id"/>)}
											catch(Exception){}</xsl:attribute>
			<xsl:attribute name="oncontextmenu">try{gxcontextmenu(<xsl:value-of select="../Class"/>,<xsl:value-of select="../Id"/>)}catch(Exception){}</xsl:attribute>
		</xsl:if>
	</xsl:if>
	<xsl:value-of />
</SPAN>
</xsl:template>

<xsl:template match="Status">
	<FONT class="gxstatus"><xsl:eval>GetStatusDesc(this.nodeTypedValue)</xsl:eval></FONT>
</xsl:template>

<xsl:template match="RowImport">
		<TR><TD>
			<xsl:apply-templates select="Class"/> <xsl:apply-templates select="Name"/>  </TD>
			<TD><xsl:value-of select="Description"/></TD>
			<xsl:if test="Warnings/Folder">
			<TD> 
				<xsl:apply-templates select="Warnings/Folder"/>
			</TD>
			</xsl:if>
		 </TR>
</xsl:template>

<xsl:template match="Folder">
	<xsl:choose>
		<xsl:when test="text()[.!='']">
			<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/fld.bmp</xsl:attribute></IMG> <xsl:value-of/>  
		</xsl:when>
		<xsl:when test="text()[.!='']">
			<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/fld.bmp</xsl:attribute></IMG> Main  
		</xsl:when>
	</xsl:choose>
</xsl:template>

<xsl:template match="Errors">
<xsl:if test="Error">
<DIV><xsl:attribute name="id"><xsl:eval>GetNuggetName()</xsl:eval></xsl:attribute>
<DIV id="wholeNugget">
	<TABLE style="BACKGROUND-COLOR: #d5d0b7" bgColor="#c0c0c0" border="0" cellPadding="1" cellSpacing="0" width="100%">
		<TBODY>
			<TR style="FONT: bold 10pt arial; HEIGHT: 17px" title="Errors">
				<TD style="BORDER-BOTTOM: 1px solid; BORDER-LEFT: 1px solid; BORDER-TOP: 1px solid; PADDING-LEFT: 5px" class="NuggetBar" id="title" noWrap="title">
						<B><FONT class="gxerror">Errors</FONT></B>
				</TD>
				<TD style="BORDER-BOTTOM: 1px solid; BORDER-TOP: 1px solid" class="NuggetBar" id="drag" noWrap="drag"></TD>
				<TD style="BORDER-BOTTOM: 1px solid; BORDER-LEFT: 2px solid; BORDER-RIGHT: 2px solid; BORDER-TOP: 1px solid; WIDTH: 16px" class="NuggetBar" id="disp" noWrap="disp" title="hide">
						<xsl:attribute name="onclick">try{displayNugget(<xsl:eval>GetCurrentNugget()</xsl:eval>)}catch(Exception){}</xsl:attribute>
						<IMG style="CURSOR: hand" height="13" width="17"><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/close.gif</xsl:attribute></IMG>
				</TD>
			</TR>
		</TBODY>
	</TABLE>
	</DIV>
	<DIV id="content">
		<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%" style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
				<xsl:apply-templates select="Error"/>
		</TABLE>
	</DIV>
</DIV>
</xsl:if>
</xsl:template>

<xsl:template match= "Error">
	<TR><TD><B> <FONT class="gxerror"><xsl:value-of/></FONT></B></TD></TR>
</xsl:template>
<xsl:template match="Warnings">
<xsl:if test="Warning">
<DIV><xsl:attribute name="id"><xsl:eval>GetNuggetName()</xsl:eval></xsl:attribute>
<DIV id="wholeNugget">
	<TABLE style="BACKGROUND-COLOR: #d5d0b7" bgColor="#c0c0c0" border="0" cellPadding="1" cellSpacing="0" width="100%">
		<TBODY>
		<TR style="FONT: bold 10pt arial; HEIGHT: 17px" title="Info">
			<TD style="BORDER-BOTTOM: 1px solid; BORDER-LEFT: 1px solid; BORDER-TOP: 1px solid; PADDING-LEFT: 5px" class="NuggetBar" id="title" noWrap="title">
					<B><FONT class="gxwarn">Info</FONT></B>
			</TD>
			<TD style="BORDER-BOTTOM: 1px solid; BORDER-TOP: 1px solid" class="NuggetBar" id="drag" noWrap="drag"></TD>
			<TD style="BORDER-BOTTOM: 1px solid; BORDER-LEFT: 2px solid; BORDER-RIGHT: 2px solid; BORDER-TOP: 1px solid; WIDTH: 16px" class="NuggetBar" id="disp" noWrap="disp" title="hide">
					<xsl:attribute name="onclick">try{displayNugget(<xsl:eval>GetCurrentNugget()</xsl:eval>)}catch(Exception){}</xsl:attribute>
					<IMG style="CURSOR: hand" height="13" width="17"><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/close.gif</xsl:attribute></IMG>
			</TD>
		</TR>
		</TBODY>
	</TABLE>
	</DIV>
	<DIV id="content">
		<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%" style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
			<xsl:for-each select="Warning">
				<TR><TD><B> <FONT class="gxwarn"><xsl:value-of/></FONT></B></TD></TR>
			</xsl:for-each>
		</TABLE>
	</DIV>
</DIV>
</xsl:if>
</xsl:template>

<xsl:template match="Class">
	<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/<xsl:eval>GetClsName(this.nodeTypedValue)</xsl:eval>.bmp</xsl:attribute></IMG>  
</xsl:template>

<xsl:script><![CDATA[

var sNuggPrefix = "gxNugg";
var sCurrNugg="";
var iNuggNumber = 0;

var iStatus = -1;

function SetStatus(iVal)
{
	iStatus = iVal;
}

function GetNuggetName()
{
	iNuggNumber++;
	sCurrNugg = sNuggPrefix + iNuggNumber;
	return sCurrNugg;
}

function GetCurrentNugget()
{
	return sCurrNugg;
}


function MoreThanOne(oNode)
{
	if (oNode.childNodes.length > 1)
		return true;
	return false;
}

// For TblAtt Nodes
function IsVirtual(oNode)
{
	try
	{
		var bAttriRedundant = (oNode.selectSingleNode("TblAttRed") != null);
		var bAttriFormula = (oNode.selectSingleNode("Attribute/FormulaDef") != null);
		var bAttriInfered = (oNode.selectSingleNode("TblAttInf") != null);
		return bAttriInfered || (bAttriFormula && !bAttriRedundant);
	}
	catch (Exception)
	{
		return true;
	}
}

function GetBmpAtt(oNode)
{
	var bIsKey = (oNode.selectSingleNode("TblAttIsKey") != null);
	var bAttriRedundant = (oNode.selectSingleNode("TblAttRed") != null);
	var bAttriFormula = (oNode.selectSingleNode("Attribute/FormulaDef") != null);
	var bAttriInfered = (oNode.selectSingleNode("TblAttInf") != null);
	if (bIsKey)
		return "key.gif";
	if (bAttriFormula)
	{
		if (bAttriRedundant)
			return "fred.bmp";
		return "formula.bmp";
	}
	else
	if (bAttriRedundant)
		return "red.bmp";
	if (bAttriInfered)
		return "inf.bmp";
	return "";
}

function IsFormula(oNode)
{
	try
	{
		var bAttriFormula = (oNode.selectSingleNode("Attribute/FormulaDef") != null);
		return (bAttriFormula);
	}
	catch (Exception)
	{
		return false;
	}
}

var bFirstVirtual = true;
var bSomeVirtual = false;
function IsFirstVirtual()
{
	return bFirstVirtual;
}

function SetFirstVirtual()
{
	bFirstVirtual = false;
}

function SetSomeVirtual()
{
	bSomeVirtual = true;
}

function GetSomeVirtual()
{
	return bSomeVirtual;
}

function GetCantOf(oNode,sQuery)
{
	try
	{
		var oChild = oNode.selectNodes(sQuery);
		return (oChild.length);
	}
	catch (exception)
	{
		return 0;
	}
}

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

function GetType(oNode)
{
	var sVal;
	try
	{
		var sQuery = "Class";
		var oChild = oNode.selectSingleNode(sQuery);
		sVal = oChild.nodeTypedValue;
	}
	catch (exception)
	{
		return "Exception getting type";
	}
	try
	{
		var iVal;
		iVal = parseInt(sVal);
		switch (iVal)
		{
			case 0: return "Transaction";
			case 1: return "Procedure";
			case 2: return "Report";
			case 3: return "Menu";
			case 4: return "Work Panel";
			case 5: return "Attribute";
			case -5:return "Formula";
			case 6: return "DataView";
			case 7: return "Table";
			case 8: return "Folder";
			case 10: return "Group";
			case 11: return "Domain";
			case 12: return "Prompt";
			case 13: return "Web Panel";
			case 17: return "Menu Bar";
			case 18: return "Style Transaction";
			case 19: return "Style Procedure";
			case 20: return "Style Report";
			case 21: return "Style Workpanel";
			case 22: return "Style Prompt";
			case 23: return "Style Web Panel";
			case 24: return "Style Menu Bar";
			default: return "";
		}
	}
	catch (exception)
	{
		return "";
	}
}

function GetClsName(sVal)
{
	try
	{
		var iVal;
		iVal = parseInt(sVal);
		switch (iVal)
		{
			case 0: return "Transaction";
			case 1: return "Procedure";
			case 2: return "Report";
			case 3: return "Menu";
			case 4: return "Work Panel";
			case 5: return "Att";
			case -5:return "Formula";
			case 6: return "DataView";
			case 7: return "Table";
			case 8: return "fld";
			case 10: return "Grp";
			case 11: return "dom";
			case 12: return "Prompt";
			case 13: return "Web Panel";
			case 17: return "mbr";
			case 18: return "strn";
			case 19: return "sprc";
			case 20: return "srpt";
			case 21: return "swkp";
			case 22: return "spmt";
			case 23: return "swbp";
			case 24: return "smbr";
			
			default: return "";
		}
	}
	catch (exception)
	{
		return "";
	}
}

]]></xsl:script>


</xsl:stylesheet>
