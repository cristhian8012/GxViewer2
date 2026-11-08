<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:gx="urn:shemas-artech-com:gx"
	exclude-result-prefixes="msxsl gx">
<xsl:param name="GXPBasedImagePath" select="/GxTheme/BasedImagePath[text()]"/>

<xsl:output method="text"/>

<xsl:template match="/">
	<xsl:apply-templates select="/GxTheme/Classes/*"/>
	<xsl:apply-templates select="/GxTheme/HTMLNodes/*" mode="html"/>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	match=HTMLTag mode=html
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
 
<xsl:template match="HTMLTag" mode="html">
<xsl:param name="vParentName"/>

<xsl:variable name="vNodeName">
	<xsl:if test="$vParentName and string-length($vParentName) > 0">
		<xsl:value-of select="$vParentName"/><xsl:text> </xsl:text>
	</xsl:if>
	<xsl:value-of select="Name"/>
</xsl:variable>

<xsl:call-template name="HTMLNode">
	<xsl:with-param name="vClassName" select="$vNodeName"/>
</xsl:call-template>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	match=HTMLClass mode=html
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->

<xsl:template match="HTMLClass" mode="html">
<xsl:param name="vParentName"/>

<xsl:variable name="vNodeName">
	<xsl:if test="$vParentName and string-length($vParentName) > 0">
		<xsl:value-of select="$vParentName"/>
	</xsl:if>.<xsl:value-of select="Name"/>
</xsl:variable>

<xsl:call-template name="HTMLNode">
	<xsl:with-param name="vClassName" select="$vNodeName"/>
</xsl:call-template>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	match=HTMLId mode=html
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->

<xsl:template match="HTMLId" mode="html">
<xsl:param name="vParentName"/>

<xsl:variable name="vNodeName">
	<xsl:if test="$vParentName and string-length($vParentName) > 0">
		<xsl:value-of select="$vParentName"/>
	</xsl:if><xsl:value-of select="Name"/>
</xsl:variable>

<xsl:call-template name="HTMLNode">
	<xsl:with-param name="vClassName" select="$vNodeName"/>
</xsl:call-template>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	name=HTMLNode
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->

<xsl:template name="HTMLNode">
<xsl:param name="vClassName"/>

<xsl:if test="count(Properties/Property) > 0">
<xsl:value-of select="$vClassName"/>
{
	<xsl:apply-templates select="Properties"/>
}
</xsl:if>

<xsl:if test="ContextualTags">
	<xsl:apply-templates select="ContextualTags/*" mode="html">
		<xsl:with-param name="vParentName" select="$vClassName"/>
	</xsl:apply-templates>
</xsl:if>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	match=Class
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->

<xsl:template match="Class">
.<xsl:value-of select="Name"/>
{
	<xsl:choose>
		<xsl:when test="ApplyTo[@idref='FreeStyleGrid' or @idref='Grid']">
			<xsl:apply-templates select="Properties" mode="NotSpecified">
				<xsl:with-param name="SpecifiedProps">AllBackColor,BackColorEven,BackColorOdd,LinesBackColor,LinesBackColor2,LinesColor,LinesFont,TitleBackColor,TitleColor,TitleFont,TitleForeColor</xsl:with-param>
			</xsl:apply-templates>
		</xsl:when>
		<xsl:otherwise>
			<xsl:apply-templates select="Properties/Property"/>
		</xsl:otherwise>
	</xsl:choose>
}
<xsl:if test="ApplyTo[@idref='FreeStyleGrid' or @idref='Grid']">
.<xsl:value-of select="Name"/>Uniform
{
	border-width: 0px;
	<xsl:apply-templates select="Properties" mode="OnlySpecified">
		<xsl:with-param name="SpecifiedProps">AllBackColor,LinesColor,LinesFont</xsl:with-param>
	</xsl:apply-templates>
}

.<xsl:value-of select="Name"/>Odd
{
	border-width: 0px;
	<xsl:apply-templates select="Properties" mode="OnlySpecified">
		<xsl:with-param name="SpecifiedProps">BackColorOdd,LinesBackColor,LinesColor,LinesFont</xsl:with-param>
	</xsl:apply-templates>
}

.<xsl:value-of select="Name"/>Even
{
	border-width: 0px;
	<xsl:apply-templates select="Properties" mode="OnlySpecified">
		<xsl:with-param name="SpecifiedProps">BackColorEven,LinesBackColor2,LinesColor,LinesFont</xsl:with-param>
	</xsl:apply-templates>
}
<xsl:if test="ApplyTo[@idref='Grid']">
.<xsl:value-of select="Name"/>UniformTitle
{
	<xsl:apply-templates select="Properties" mode="OnlySpecified">
		<xsl:with-param name="SpecifiedProps">AllBackColor,TitleColor,TitleFont</xsl:with-param>
	</xsl:apply-templates>
}

.<xsl:value-of select="Name"/>Title
{
	<xsl:apply-templates select="Properties" mode="OnlySpecified">
		<xsl:with-param name="SpecifiedProps">TitleBackColor,TitleColor,TitleFont,TitleForeColor</xsl:with-param>
	</xsl:apply-templates>
}
</xsl:if>
</xsl:if>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	match=Properties mode=OnlySpecified
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->

<xsl:template match="Properties" mode="OnlySpecified">
<xsl:param name="SpecifiedProps"/>
	<xsl:for-each select="Property">
		<xsl:if test="(@parent and gx:IsDefined(string($SpecifiedProps), string(@parent)))
					or gx:IsDefined(string($SpecifiedProps), string(@idref))">
			<xsl:apply-templates select="."/>
		</xsl:if>
	</xsl:for-each>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	match=Properties mode="NotSpecified"
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->

<xsl:template match="Properties" mode="NotSpecified">
<xsl:param name="SpecifiedProps"/>
	<xsl:for-each select="Property">
		<xsl:if test="(not(@parent) or not(gx:IsDefined(string($SpecifiedProps), string(@parent))))
					and not(gx:IsDefined(string($SpecifiedProps), string(@idref)))">
			<xsl:apply-templates select="."/>
		</xsl:if>
	</xsl:for-each>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	match=Property
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
 
<xsl:template match="Property">

<xsl:variable name="vParentName" select="@parent"/>
<xsl:variable name="ParentNode" select="preceding-sibling[@idref=$vParentName] or following-sibling[@idref=$vParentName]"/>

	<!-- Test if the property had not been defined, to not overwrite it -->
	<xsl:if test="string-length(normalize-space(Value)) > 0 and (not(@parent) or string-length(normalize-space(parent::Property[@idref=$vParentName]/Value)) = 0)">

	<!-- Test if the value is empty -->				
		<xsl:call-template name="GetPropName">
			<xsl:with-param name="PropId"><xsl:value-of select="@idref"/></xsl:with-param>
		</xsl:call-template>: <xsl:choose>
			<xsl:when test="@idref='background-image' or @idref='list-style-image' or @idref='backgroundGX'">
				<xsl:variable name="vValue" select="Value[text()]" />
				<xsl:value-of select="gx:GetURL( string($GXPBasedImagePath), string($vValue) )"/>
			</xsl:when>
			<xsl:otherwise><xsl:value-of select="Value"/></xsl:otherwise>
		</xsl:choose>;
	</xsl:if>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	name=GetPropName
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->
 
<xsl:template name="GetPropName">
	<xsl:param name="PropId"/>
	<xsl:choose>
		<xsl:when test="$PropId='AllBackColor'
					or $PropId='BackColor'
					or $PropId='BackColorEven'
					or $PropId='BackColorOdd'
					or $PropId='bgColor'
					or $PropId='LinesBackColor'
					or $PropId='LinesBackColor2'
					or $PropId='TitleBackColor'">background-color</xsl:when>
		<xsl:when test="$PropId='backgroundGX'">background-image</xsl:when>
		<xsl:when test="$PropId='borderColor'
					or $PropId='BorderColor'">border-color</xsl:when>
		<xsl:when test="$PropId='idBOXBORDERSTYLE'">border-style</xsl:when>
		<xsl:when test="$PropId='border'
					or $PropId='Border'
					or $PropId='BorderWidth'">border-width</xsl:when>
		<xsl:when test="$PropId='ForeColor'
					or $PropId='LinesColor'
					or $PropId='text'
					or $PropId='TitleColor'">color</xsl:when>
		<xsl:when test="$PropId='Font'
					or $PropId='LinesFont'
					or $PropId='TitleFont'">font</xsl:when>
		<xsl:when test="$PropId='GxHeight'">height</xsl:when>
		<xsl:when test="$PropId='idIMEMODE'">ime-mode</xsl:when>
		<xsl:when test="$PropId='align'">text-align</xsl:when>
		<xsl:when test="$PropId='vAlign'">vertical-align</xsl:when>
		<xsl:when test="$PropId='GxWidth'">width</xsl:when>
		<xsl:otherwise><xsl:value-of select="$PropId"/></xsl:otherwise>
	</xsl:choose>
</xsl:template>

<!--
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
	SCRIPTS
	~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 -->

<msxsl:script language="JScript" implements-prefix="gx">
<![CDATA[
	function IsDefined(sPropertiesList, sPropertyName)
	{
		if (sPropertiesList == null || sPropertyName == null)
			return false;

		try
		{
			if (sPropertiesList.length == 0 || sPropertyName.length == 0)
				return false;
			
			var nPos = sPropertiesList.indexOf(sPropertyName);
			if (nPos == -1)
				return false;
				
			var nEndPos = nPos + sPropertyName.length;
			if (nPos != 0 && sPropertiesList.charAt(nPos-1) != ','
				|| nEndPos < sPropertiesList.length && sPropertiesList.charAt(nEndPos) != ',')
			{
				return false;
			}

			return true;
		}
		catch (e)
		{
			throw (e);
		}

		return false;
	}

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
