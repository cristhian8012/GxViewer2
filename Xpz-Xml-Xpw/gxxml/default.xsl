<?xml version='1.0'?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<xsl:template match="/">
<html>
<body>
<PRE><xsl:value-of select="ObjSpecs/text()"/></PRE>
</body>
</html>
</xsl:template>
</xsl:stylesheet>