<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<xsl:template match="/">
	<xsl:value-of/>
</xsl:template>

<xsl:template match="text()|@*"><xsl:value-of select="."/></xsl:template>
</xsl:stylesheet>