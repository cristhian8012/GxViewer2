<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 	xmlns:gx="urn:shemas-artech-com:gx">

<xsl:include href="include.xsl"/>


<xsl:template match="/">

<html>
	<xsl:call-template name="HtmlHeader"/>
<body>
	<xsl:call-template name="HtmlScript"/>

	<form action="" id="FORM1" method="post" name="FORM1">
		<DIV><TABLE width="100%">
		<xsl:for-each select="ObjSpecs/RowImport">
			<TR><TD><xsl:apply-templates select="."/></TD></TR>
		</xsl:for-each>
		</TABLE></DIV>
	</form>
</body>
</html>
</xsl:template>

<xsl:template match="Name">
<SPAN class="gxobj">
	<xsl:choose>
		<xsl:when test="../Id and ../Id != 0">
			<xsl:attribute name="title">
				<xsl:value-of select="."/> : <xsl:value-of select="../Description"/>
			</xsl:attribute>
			<xsl:attribute name="onclick">
				try{gxopen(<xsl:value-of select="../Class"/>,<xsl:value-of select="../Id"/>)}
				catch(Exception){}
			</xsl:attribute>
			<xsl:attribute name="oncontextmenu">
				try{gxcontextmenu(<xsl:value-of select="../Class"/>,<xsl:value-of select="../Id"/>)}
				catch(Exception){}
			</xsl:attribute>
		</xsl:when>
		<xsl:when test="../Class and ../Class = 9">
			<xsl:attribute name="class">gxobjnotexist</xsl:attribute>
			<xsl:attribute name="title">Model : <xsl:value-of select="."/>
			</xsl:attribute>
		</xsl:when>
		<xsl:otherwise>
			<xsl:attribute name="class">gxobjnotexist</xsl:attribute>
			<xsl:attribute name="title">Invalid Object</xsl:attribute>
		</xsl:otherwise>
	</xsl:choose>
	<xsl:value-of select="."/>
</SPAN>
</xsl:template>

<xsl:template match="Status">
	<FONT class="gxstatus">
		<xsl:call-template name="GetStatusDesc">
			<xsl:with-param name="Status"><xsl:value-of select="."/></xsl:with-param>
		</xsl:call-template>
	</FONT>
</xsl:template>

<xsl:template match="RowImport">
	<DIV>
		<xsl:call-template name="TableHeaderMain">
			<xsl:with-param name="title">
				<xsl:call-template name="GetClsName">
					<xsl:with-param name="Class">
						<xsl:value-of select="Class[position()=1]"/>
					</xsl:with-param>
				</xsl:call-template>
				<xsl:text> </xsl:text>
				<xsl:apply-templates select="Name[position()=1]"/>
				<xsl:text> </xsl:text>
				<xsl:apply-templates select="Status"/>
			</xsl:with-param>
		</xsl:call-template>
		<DIV id="content">
		  <TABLE border="0" cellPadding="0" cellSpacing="0" width="100%" class="textoGrisTabla">
			<TBODY>
			  <TR>
				<TD width="25%">Name</TD>
				<TD>
				  <EM>
					<xsl:apply-templates select="Class[position()=1]"/>
					<xsl:text> </xsl:text>
					<xsl:apply-templates select="Name[position()=1]"/>  
					<xsl:if test="Infos/OrigTblName">
						<B> Original Name : <xsl:value-of select="Infos/OrigTblName"/> </B>
					</xsl:if>
				  </EM>
				</TD>
			  </TR>
			  <xsl:call-template name="EndRow"/>

			  <TR>
				<TD>Description</TD>
				<TD>
				  <EM>
					<xsl:value-of select="Description"/>
				  </EM>
				</TD>
			  </TR>
			  <xsl:call-template name="EndRow"/>

			  <TR>
				<TD width="25%">ID</TD>
				<TD>
				  <EM>
				  <xsl:value-of select="Id"/>
				  </EM>
				</TD>
			  </TR>
			  <xsl:call-template name="EndRow"/>

			  <xsl:if test="Infos/Folder">
				<TR>
					<TD width="25%">Folder</TD>
					<TD>
					<EM>
						<xsl:apply-templates select="Infos/Folder"/>
					</EM>
					</TD>
				</TR>
				<xsl:call-template name="EndRow"/>
			  </xsl:if>
			  </TBODY>
		  </TABLE>
			<xsl:if test="Warnings/Warning|Infos/Message">
				<DIV>
					<xsl:call-template name="TableHeader">
						<xsl:with-param name="title">Info</xsl:with-param>
					</xsl:call-template> 
				
					<DIV id="content">
						<TABLE class="textoGrisTabla" border="0" cellPadding="0" cellSpacing="0" width="100%" style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
							<xsl:for-each select=".//Warning|.//Message">
								<TR><TD><xsl:apply-templates select="."/></TD></TR>
							</xsl:for-each>
						</TABLE>
					</DIV>
				</DIV>
			</xsl:if>
			<xsl:if test="Errors/Error">
				<DIV>
					<xsl:call-template name="TableHeader">
						<xsl:with-param name="title">Errors</xsl:with-param>
					</xsl:call-template> 
					<DIV id="content">
						<TABLE class="textoGrisTabla" border="0" cellPadding="0" cellSpacing="0" width="100%" style="BORDER-BOTTOM: thin inset; BORDER-RIGHT: thin inset">
							<xsl:for-each select=".//Error">
							<TR><TD><xsl:apply-templates select="."/></TD></TR>
							</xsl:for-each>
						</TABLE> 
					</DIV>
				</DIV>
			</xsl:if>
		</DIV>
	</DIV>
</xsl:template>

<xsl:template match="Folder">
	<xsl:call-template name="RenderImage">
		<xsl:with-param name="Id">Folder</xsl:with-param>
	</xsl:call-template>
	<xsl:text> </xsl:text>
	<xsl:choose>
		<xsl:when test="text()[.!='']">
			<xsl:value-of select="."/>
		</xsl:when>
		<xsl:otherwise>
			<xsl:text>Main</xsl:text>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template match="Warning">
	<table width="100%" class="textoWarn">
		<tr>
		<td valign="top">
			<xsl:call-template name="RenderImage">
				<xsl:with-param name="Id">Warning</xsl:with-param>
			</xsl:call-template>
		</td><td><PRE style="font-family: Verdana" canHaveHTML="true"><B> 
		<FONT class="textoWarn"><xsl:value-of select="."/></FONT>
		</B></PRE></td>
		</tr>
	</table>
</xsl:template>

<xsl:template match="Message">
	<table width="100%" class="textoMsg">
		<tr>
		<td valign="top">
			<xsl:call-template name="RenderImage">
				<xsl:with-param name="Id">Message</xsl:with-param>
			</xsl:call-template>
		</td><td><PRE style="font-family: Verdana" canHaveHTML="true"><B>
		<FONT class="textoMsg"><xsl:value-of select="."/></FONT>
		</B></PRE></td>
		</tr>
	</table>
</xsl:template>

<xsl:template match= "Error">
	<table width="100%" class="textoError"><tr>
		<td valign="top">
			<xsl:call-template name="RenderImage">
				<xsl:with-param name="Id">Error</xsl:with-param>
			</xsl:call-template>
		</td><td><PRE style="font-family: Verdana" canHaveHTML="true"><B>
		<FONT class="textoError"><xsl:value-of select="."/></FONT>
		</B></PRE></td>
		</tr>
	</table>
</xsl:template>

<xsl:template match="Class">
	<IMG><xsl:attribute name="src"><xsl:value-of select="/ObjSpecs/gxpath"/>gxxml/images/
		<xsl:call-template name="GetClsName">
			<xsl:with-param name="Class"><xsl:value-of select="."/></xsl:with-param>
		</xsl:call-template>.ico</xsl:attribute>
	</IMG>
</xsl:template>

<xsl:template name="GetStatusDesc">
	<xsl:param name="Status"/>
	<xsl:choose>
			<xsl:when test="$Status=0">not consolidated</xsl:when> 
			<xsl:when test="$Status=32768">not consolidated</xsl:when>
			<xsl:when test="$Status=1">is new</xsl:when> 
			<xsl:when test="$Status=32769">is new</xsl:when>
			<xsl:when test="$Status=2">was updated</xsl:when> 
			<xsl:when test="$Status=32770">was updated</xsl:when>
			<xsl:when test="$Status=3">did not change</xsl:when>
			<xsl:when test="$Status=3+32768">did not change</xsl:when>
			<xsl:otherwise>Unknow Status</xsl:otherwise>
	</xsl:choose>
</xsl:template>


</xsl:stylesheet>
