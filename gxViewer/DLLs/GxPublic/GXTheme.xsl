<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:gx="urn:shemas-artech-com:gx"
	exclude-result-prefixes="msxsl gx">
<xsl:param name="GXPBasedImagePath" select="/GxTheme/BasedImagePath[text()]"/>

<xsl:output method="text"/>

<xsl:template match="/">
	<xsl:apply-templates select="//Class"/>
	<xsl:apply-templates select="/GxTheme/HTMLTags/HTMLTag"/>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	HTMLTags
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="/GxTheme/HTMLTags/HTMLTag">
	<xsl:variable name="vTagName" select="Name" />

<xsl:value-of select="$vTagName" />
{
	<xsl:apply-templates select="Properties"/>
}

<xsl:if test="ContextualTags != ''">
	<xsl:for-each select="ContextualTags/HTMLTag">
		<xsl:call-template name="HTMLTagContextual">
			<xsl:with-param name="vParentName">
				<xsl:value-of select="$vTagName"/>
			</xsl:with-param>
		</xsl:call-template>
	</xsl:for-each>
</xsl:if>

</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	HTMLTagContextual
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template name="HTMLTagContextual">
	<xsl:param name="vParentName"/>
	<xsl:variable name="vTagName" select="concat($vParentName, ' ', Name)" />

<xsl:value-of select="$vTagName" />
{
	<xsl:apply-templates select="Properties"/>
}

	<xsl:if test="ContextualTags != ''">
		<xsl:for-each select="ContextualTags/HTMLTag">
			<xsl:call-template name="HTMLTagContextual">
				<xsl:with-param name="vParentName">
					<xsl:value-of select="$vTagName" />
				</xsl:with-param>
			</xsl:call-template>
		</xsl:for-each>
	</xsl:if>
</xsl:template>


<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	Class
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
-->
<xsl:template match="Class">
<xsl:choose>
<!--
	Procesar objeto FREESTYLEGRID : crear clase particular para
	líneas impares y pares.
-->
	<xsl:when test="ApplyTo/Type[@ID = 'FreeStyleGrid']">
.<xsl:value-of select="Name"/>
{
	<xsl:apply-templates select="Properties" mode="FreeStyleGrid"/>
}
.<xsl:value-of select="Name"/>Uniform
{
	border-width: 0;
	<xsl:apply-templates select="Properties" mode="Uniform"/>
}
.<xsl:value-of select="Name"/>Odd
{
	border-width: 0;
	<xsl:apply-templates select="Properties" mode="Odd"/>
}
.<xsl:value-of select="Name"/>Even
{
	border-width: 0;
	<xsl:apply-templates select="Properties" mode="Even"/>
}
	</xsl:when>
	
<!--
	 Procesar objeto GRID : crear clase particular para el
	 título, líneas impares y pares.
 -->
	<xsl:when test="ApplyTo/Type[@ID = 'Grid']">
.<xsl:value-of select="Name"/>
{
	<xsl:apply-templates select="Properties" mode="Grid"/>
}
.<xsl:value-of select="Name"/>Uniform
{
	border-width: 0;
	<xsl:apply-templates select="Properties" mode="Uniform"/>
}
.<xsl:value-of select="Name"/>UniformTitle
{
	<xsl:apply-templates select="Properties" mode="UniformTitle"/>
}
.<xsl:value-of select="Name"/>Title
{
	<xsl:apply-templates select="Properties" mode="Titulo"/>
}
.<xsl:value-of select="Name"/>Odd
{
	border-width: 0;
	<xsl:apply-templates select="Properties" mode="Odd"/>
}
.<xsl:value-of select="Name"/>Even
{
	border-width: 0;
	<xsl:apply-templates select="Properties" mode="Even"/>
}
	</xsl:when>
	<xsl:otherwise>
.<xsl:value-of select="Name"/>
{
	<xsl:apply-templates select="Properties"/>
}
</xsl:otherwise>
</xsl:choose>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES : FREESTYLEGRID
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Properties" mode="FreeStyleGrid">
<!--
	<xsl:for-each select="Property[@ID='align']
	|Property[@ID='bgColor']
	|Property[@ID='borderColor']
	|Property[@ID='border']
	|Property[@ID='idBOXBORDERSTYLE']
	|Property[@ID='height']
	|Property[@ID='width']">
		<xsl:apply-templates select="."/>
	</xsl:for-each>
-->
	<xsl:apply-templates select="." mode="Common_Grids"/>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES : GRID
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Properties" mode="Grid">
	<xsl:apply-templates select="." mode="Common_Grids"/>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES : UNIFORMTITLE
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Properties" mode="UniformTitle">
	<xsl:for-each select="Property[@ID='TitleFont']
	|Property[@ID='TitleColor']
	|Property[@ID='AllBackColor']">
		<xsl:apply-templates select="."/>
	</xsl:for-each>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES : UNIFORM
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Properties" mode="Uniform">
	<xsl:for-each select="Property[@ID='LinesFont']
	|Property[@ID='LinesColor']
	|Property[@ID='AllBackColor']">
		<xsl:apply-templates select="."/>
	</xsl:for-each>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES : TITULO
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Properties" mode="Titulo">
	<xsl:for-each select="Property[@ID='TitleFont']
	|Property[@ID='TitleColor']
	|Property[@ID='TitleBackColor']
	|Property[@ID='TitleForeColor']">
		<xsl:apply-templates select="."/>
	</xsl:for-each>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES : ODD
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Properties" mode="Odd">
	<xsl:for-each select="Property[@ID='LinesFont']
	|Property[@ID='LinesColor']
	|Property[@ID='LinesBackColor']
	|Property[@ID='BackColorOdd']">
		<xsl:apply-templates select="."/>
	</xsl:for-each>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES : EVEN
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Properties" mode="Even">
	<xsl:for-each select="Property[@ID='LinesFont']
	|Property[@ID='LinesColor']
	|Property[@ID='LinesBackColor2']
	|Property[@ID='BackColorEven']">
		<xsl:apply-templates select="."/>
	</xsl:for-each>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES : COMMON_GRIDS
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Properties" mode="Common_Grids">
	<xsl:for-each select="Property[@ID != 'AllBackColor'
		and @ID != 'BackColorEven'
		and @ID != 'BackColorOdd'
		and @ID != 'LinesBackColor'
		and @ID != 'LinesBackColor2'
		and @ID != 'LinesColor'
		and @ID != 'LinesFont'
		and @ID != 'TitleBackColor'
		and @ID != 'TitleColor'
		and @ID != 'TitleFont'
		and @ID != 'TitleForeColor']">
		<xsl:apply-templates select="."/>
	</xsl:for-each>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Properties">
	<xsl:if test="../DerivedFrom[text() != '']">
		<xsl:variable name="IdParent" select="../DerivedFrom" />
		<xsl:apply-templates select="//Class[@name=$IdParent]/Properties"/>
	</xsl:if>
	<xsl:for-each select="Property">
		<xsl:apply-templates select="."/>
	</xsl:for-each>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	PROPERTIES : PROPERTY
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template match="Property">
	<xsl:choose>
		<xsl:when test="@ID='Font' or @ID='TitleFont' or @ID='LinesFont'">
<xsl:call-template name="PrintProperty"><xsl:with-param name="pPropName">font-family</xsl:with-param><xsl:with-param name="pPropValue" select="Value/fontname"/></xsl:call-template>
<xsl:call-template name="PrintProperty"><xsl:with-param name="pPropName">font-size</xsl:with-param><xsl:with-param name="pPropValue" select="Value/fontsize"/><xsl:with-param name="pUnit">pt</xsl:with-param></xsl:call-template>
<xsl:call-template name="PrintProperty"><xsl:with-param name="pPropName">font-style</xsl:with-param><xsl:with-param name="pPropValue" select="Value/fontstyle"/></xsl:call-template>
<xsl:call-template name="PrintProperty"><xsl:with-param name="pPropName">font-weight</xsl:with-param><xsl:with-param name="pPropValue" select="Value/fontweight"/></xsl:call-template>
<xsl:call-template name="PrintProperty"><xsl:with-param name="pPropName">text-decoration</xsl:with-param><xsl:with-param name="pPropValue" select="Value/fontdecoration"/></xsl:call-template>
		</xsl:when>
		<xsl:when test="@ID='Margin' or @ID='Padding'">
			<xsl:for-each select="Value/*">
<xsl:call-template name="PrintProperty"><xsl:with-param name="pPropName" select="name()"/><xsl:with-param name="pPropValue" select="."/></xsl:call-template>
			</xsl:for-each>
		</xsl:when>
		<xsl:when test="@ID='background-image' or @ID='list-style-image'">
<xsl:call-template name="PrintProperty"><xsl:with-param name="pPropName" select="@ID"/><xsl:with-param name="pPropValue" select="gx:GetURL( string($GXPBasedImagePath), string(Value) )"/></xsl:call-template>
		</xsl:when>
		<xsl:when test="@ID='backgroundGX'">
<xsl:call-template name="PrintPropertyById"><xsl:with-param name="pPropId" select="@ID"/><xsl:with-param name="pPropValue" select="gx:GetURL( string($GXPBasedImagePath), string(Value) )"/></xsl:call-template>
		</xsl:when>
		<xsl:otherwise>
<xsl:call-template name="PrintPropertyById"><xsl:with-param name="pPropId" select="@ID"/><xsl:with-param name="pPropValue" select="Value"/></xsl:call-template>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<xsl:template name="PrintProperty">
<xsl:param name="pPropName"/>
<xsl:param name="pPropValue"/>
<xsl:param name="pUnit"/>
	<xsl:if test="string-length($pPropValue) > 0">
	<xsl:value-of select="$pPropName"/> : <xsl:value-of select="$pPropValue"/><xsl:value-of select="$pUnit"/>;
	</xsl:if>
</xsl:template>

<xsl:template name="PrintPropertyById">
<xsl:param name="pPropId"/>
<xsl:param name="pPropValue"/>
	<xsl:if test="string-length($pPropValue) > 0">
	<xsl:call-template name="GetPropName"><xsl:with-param name="PropId" select="$pPropId"/></xsl:call-template> : <xsl:value-of select="$pPropValue"/>;
	</xsl:if>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	GETPROPNAME
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
<xsl:template name="GetPropName">
	<xsl:param name="PropId"/>
	<xsl:choose>
		<xsl:when test="$PropId='BackColor'">background-color</xsl:when>
		<xsl:when test="$PropId='BackColorOdd'">background-color</xsl:when>
		<xsl:when test="$PropId='BackColorEven'">background-color</xsl:when>
		<xsl:when test="$PropId='LinesBackColor'">background-color</xsl:when>
		<xsl:when test="$PropId='LinesBackColor2'">background-color</xsl:when>
		<xsl:when test="$PropId='TitleBackColor'">background-color</xsl:when>
		<xsl:when test="$PropId='bgColor'">background-color</xsl:when>
		<xsl:when test="$PropId='AllBackColor'">background-color</xsl:when>
		<xsl:when test="$PropId='backgroundGX'">background-image</xsl:when>
		<xsl:when test="$PropId='ForeColor'">color</xsl:when>
		<xsl:when test="$PropId='TitleColor'">color</xsl:when>
		<xsl:when test="$PropId='LinesColor'">color</xsl:when>
		<xsl:when test="$PropId='text'">color</xsl:when>
		<xsl:when test="$PropId='border'">border-width</xsl:when>
		<xsl:when test="$PropId='Border'">border-width</xsl:when>
		<xsl:when test="$PropId='BorderWidth'">border-width</xsl:when>
		<xsl:when test="$PropId='borderColor'">border-color</xsl:when>
		<xsl:when test="$PropId='BorderColor'">border-color</xsl:when>
		<xsl:when test="$PropId='align'">text-align</xsl:when>
		<xsl:when test="$PropId='vAlign'">vertical-align</xsl:when>
		<xsl:when test="$PropId='idBOXBORDERSTYLE'">border-style</xsl:when>
		<xsl:when test="$PropId='GxHeight'">height</xsl:when>
		<xsl:when test="$PropId='GxWidth'">width</xsl:when>
		<xsl:when test="$PropId='idIMEMODE'">ime-mode</xsl:when>
		<xsl:otherwise>
	<xsl:value-of select="$PropId"/>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>

<msxsl:script language="JScript" implements-prefix="gx">
<![CDATA[

	function GetURL(sImgPath, sUrl)
	{
		var sReturn = sUrl;
		if (sImgPath && sUrl)
		{
			try
			{
				var nPos;
				var vRExp = new RegExp( "://", "i" );

				nPos = sUrl.search( vRExp );
				if (nPos < 0)
				{
					vRExp = new RegExp( ":/", "i" );
					nPos = sUrl.search( vRExp );
				}

				if (nPos < 0)
				{
					vRExp = new RegExp("url[(]", "i" );
					nPos = sUrl.search(vRExp);
					if (nPos == 0)
						sUrl = sUrl.substr(4);

					sReturn = "url(" + sImgPath;
					if (sUrl.charAt(0) != '/' && sImgPath.charAt(sImgPath.length-1) != '/')
						sReturn += "/";
					sReturn += sUrl;
					if (nPos != 0)
						sReturn += ")";
				}
			}
			catch (e)
			{
				sReturn = "XSL Script Error( " + e.description + " )";
			}
		}
		return sReturn;
	}

]]>
</msxsl:script>

</xsl:stylesheet>
