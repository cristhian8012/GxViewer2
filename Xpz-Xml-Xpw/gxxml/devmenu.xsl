<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/TR/WD-xsl">

<xsl:template match="/">
<html>
<head>
        <meta http-equiv="Content-Type">
<xsl:attribute name="content">text/html; charset=<xsl:value-of select="/Objects/Charset"/>
</xsl:attribute></meta>
</head>
<body>
        <TABLE cellSpacing="1" cellPadding="1" width="75%" border="0">
  
	  <TR>
	    <TD><STRONG><FONT color="darkred" size="6"><EM>GeneXus 
              Programs</EM></FONT></STRONG></TD></TR>
	  <TR>
	    <TD>
	    	<UL>
	    		<xsl:apply-templates select="/Objects"/>
	    	</UL>
	    </TD>
	  </TR>
	</TABLE>
	
</body>
</html>
</xsl:template>

<xsl:template match="Objects">
	<xsl:apply-templates select="//Object"/>
		
	
</xsl:template>

<xsl:template match="Object">
	<LI>
		<TABLE><TR><TD><IMG style="CURSOR: hand" height="13" width="17">
<xsl:attribute name="src">
<xsl:value-of select="/Objects/Path"/>gxxml/images/
<xsl:eval>GetClsName(this)</xsl:eval>.ico</xsl:attribute></IMG></TD><TD>
<A><xsl:attribute name="href"><xsl:value-of select="ObjLink"/></xsl:attribute><xsl:apply-templates select="ObjDesc"/></A></TD></TR></TABLE>
	</LI>
</xsl:template>

<xsl:template match="ObjDesc">
	<xsl:value-of/>
</xsl:template>

<xsl:script><![CDATA[

function GetName(oNode)
{
	try
	{
		var sName;
		sName = oNode.text;
		return sName.substr(1,sNamel);
	}
	catch (exception)
	{
		return "";
	}
}



function GetClsName(oNode)
{

	try
	{
		var sVal = oNode.selectSingleNode("ObjCls").text;
		var iVal;
		iVal = parseInt(sVal);
		switch (iVal)
		{
			case 0: return "Transaction";
			case 1: return "Procedure";
			case 2: return "Report";
			case 12: return "Prompt";
			case 13: return "WebPanel";
			case 17: return "mbr";
			
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
