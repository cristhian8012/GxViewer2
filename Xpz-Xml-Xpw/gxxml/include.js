// Script functions for XML listing
//
// $Log: include.js,v $
// Revision 1.15  2003/07/01 15:00:38  ABM
// displayNugget(), do not use "disp" and "content" constants
// 	to get the specified nodes because it does not support
// 	recursion (used in structured data type list).
//
// Revision 1.14  2003/03/05 20:06:40  jjm
// 	Mar 5th  3 - jjm
// 		Missing some rename of icons.
//
// Revision 1.13  2003/02/06 20:03:41  GMilano
// Se cambian nombres de iconos
//
// Revision 1.12  2002/12/13 23:11:25  jjm
// 	Dic 13th 2 - jjm
// 		Se arregla el bitamp de plus/minus por cerrar/abrir nodo.gif.
//
// Revision 1.11  2002/12/13 17:52:04  GMilano
// Nuevo manejo de imagenes, se tiene un archivo images.xml con las imagenes y
// nuevos templates RenderImage, RelPathImage y PathImage para manejar
// imagenes
//
// Revision 1.10  2002/12/09 19:31:07  GMilano
// Nuevo disenio para los listados.
//
// Revision 1.9  2002/09/13 17:30:07  builder
// Cambios varios, para ponerse al día con lo que está en el Q:
//
// Revision 1.8  2002/01/08 20:37:42  GMilano
// GM - Se agrega la funcion SetRowColor para setear los colores de las filas
// 	de una tabla.
//
//
//   Rev 1.7   Jan 08 2002 20:37:42   GMilano
//GM - Se agrega la funcion SetRowColor para setear los colores de las filas
//	de una tabla.
//
//   Rev 1.6   Dec 18 2001 15:46:14   GMilano
//	GMilano - Implementacion de los eventos Hide/Show de los listados.
//		Se corrige la displayNugget() sacando el parámetro.
//
//   Rev 1.5   Sep 11 2001 21:44:40   jjm
//	Sep 10th 1 - jjm
//		gxopen() & gxcontextmenu(): Convert class -5 (formula) to 5 (att).
//
//   Rev 1.4   Sep 08 2001 03:46:20   jjm
//	Sep  8th 1 - jjm
//		Do not convert class 11 to 5 for the caso of gxopen().
//
//   Rev 1.3   Sep 07 2001 20:53:26   jjm
//	Set 7th  1 - jjm/GM
//		gxopen() & gxcontextmenu(): Convert class 11 (domain) to 5 (att).
//
//   Rev 1.2   Aug 29 2001 16:50:14   CI
//	Aug 29th 1 - CI
//		Se agregó la función unescape().
//
//   Rev 1.1   03 May 2001 17:40:58   jjm
//	May 3rd 1 - jjm/GM
//		gxopen() & gxcontextmenu(): Convert class 12 (promtp) to 4 (wkp).
//

var sGXPath = "";

	function unescape(s)
	{
		s = s.replace(/&gt;/g,">");
		s = s.replace(/&lt;/g,"<");
		s = s.replace(/&amp;/g,"&");
		return s;
	}
	
	function SetRowsColor(tableName)
	{
		tbl = document.all(tableName);
		for (i = 0; i < tbl.rows.length ; i++)
		{
			dr = tbl.rows(i);
			if (i % 2 == 0)
			{
				dr.className = "clsGrayFont";
			}
			else
			{
				dr.className = "text";
			}
		}
			
	}

	function HideShowAll()
	{
		var currel = window.event.srcElement;
		var el=currel.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement;
		var i;
		for (i = 1; i < el.children.length ; i++)
		{
			try
			{
				HideShow(el.children(i),el.children(i).children(0).children(0).children(0).children(0).children(0).children(0));
			}
			catch (e)
			{
			}
		}
		if (currel.title == "Show All")
		{
			currel.title = "Hide All";
			currel.src = sGXPath + "images/closeNode.gif";
		}
		else
		{
			currel.title = "Show All";
			currel.src = sGXPath + "images/openNode.gif";
		}
		
		
	}

	
	function HideShow(element,currel)
	{

			var el = element.children(0);
			if (el.children.length > 0)
			{
				var f = el.children(1);
				if (f.style.display != "none")
				{
					if (currel)
					{
						currel.title = "Show";
						currel.src = sGXPath + "images/openNode.gif";
					}
					f.style.display = "none";
				}
				else
				{
					if (currel)
					{
						currel.title = "Hide";
						currel.src = sGXPath + "images/closeNode.gif";
					}
					f.style.display = "block";
				}
			}

		
	}
	
	function HideAndShow()
	{
			var currel = window.event.srcElement;
			var el=currel.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement;
			HideShow(el,currel);

		
	}

	function displayType(oNug)
	{
		try
		{
		var e = oNug.all("disp");
		var f = oNug.all("content");

		ContentRegKey = "DD_CONTENT_" + document.title + "_" + oNug.id;

		if (f.length != null)
		{
			if (f(0).style.display == "none")
			{
				e(0).title = "Hide";
				e(0).children(0).src = sGXPath + "images/openNode.gif";
				f(0).style.display = "block";
			}
			else
			{
				e(0).title = "Show";
				e(0).children(0).src = sGXPath + "images/closeNode.gif";
				f(0).style.display = "none";
			}
		}
		else
		{
			//  If contents are hidden, show them
			if (f.style.display == "none")
			{
				e.title = "Hide";
				e.children(0).src = sGXPath + "images/openNode.gif";
				f.style.display = "block";
				display = "0";
				// if the page is not runin the correct security context then
				try { window.external.SetPref(ContentRegKey,"display");}
				catch (exception) {}

			}
			//  If contents are showing, hide them
			else
			{
				e.title = "Show";
				e.children(0).src = sGXPath + "images/closeNode.gif";
				f.style.display = "none";
				display = "1";
				try { window.external.SetPref(ContentRegKey,"hide");}
				catch (exception) {}
			}
		 }
		}
		catch (exception)
		{
		}
	}

	function displayNugget()
	{
	
		try
		{
		var currel = window.event.srcElement;
		oNug = currel.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement;
		var e = currel.parentElement;	// oNug.all("disp");
		var f = oNug.children(1);		// oNug.all("content");
		

		ContentRegKey = "DD_CONTENT_" + document.title + "_" + oNug.id;
		

		if (f.length != null)
		{
			if (f(0).style.display == "none")
			{
				e(0).title = "Hide";
				e(0).children(0).src = sGXPath + "images/closeNugget.gif";
				f(0).style.display = "block";
			}
			else
			{
				e(0).title = "Show";
				e(0).children(0).src = sGXPath + "images/openNugget.gif";
				f(0).style.display = "none";
			}
		}
		else
		{
			//  If contents are hidden, show them
			if (f.style.display == "none")
			{
				e.title = "Hide";
				e.children(0).src = sGXPath + "images/closeNugget.gif";
				f.style.display = "block";
				display = "0";
				// if the page is not runin the correct security context then
				try { window.external.SetPref(ContentRegKey,"display");}
				catch (exception) {}

			}
			//  If contents are showing, hide them
			else
			{
				e.title = "Show";
				e.children(0).src = sGXPath + "images/openNugget.gif";
				f.style.display = "none";
				display = "1";
				try { window.external.SetPref(ContentRegKey,"hide");}
				catch (exception) {}
			}
		 }
		}
		catch (exception)
		{
		}
	}

	function GetPath()
	{
		return sGXPath;
	}

	function GetDoc()
	{

	}

	function SetGXPath(val)
	{
		sGXPath = val +  "gxxml/";
	}

	function toggleView()
	{
		try
		{
		var currel = window.event.srcElement;
		var el=currel.parentElement;
		if (el.nodeName=="LI"){
			var uls=el.children;
			if (uls.length > 1){
				var ul=uls(3);
				if (ul.style.display=="none"){
					ul.style.display="block";
					currel.src = sGXPath + "images/tablemin.bmp";
				}
				else {
					ul.style.display="none";
					currel.src = sGXPath + "images/tableplus.bmp";
				}
			}
		}
		}
		catch (exception)
		{
		}
		window.event.cancelBubble=true;
	}

	function gxopen(nCls,nId)
	{
		try
		{
			if (nCls == 12)		// Prompt
				nCls = 4;			// Work Panel
			else if (nCls == -5)	// Formula
				nCls = 5;			// Attribute
			window.external.gxopen(nCls,nId);
		}
		catch (exception)
		{
		}
	}

	function gxcontextmenu(nCls,nId)
	{
		try
		{
			var el=window.event;
			if (nCls == 12)		// Prompt
				nCls = 4;			// Work Panel
			else if (nCls == 11)	// Domain
				nCls = 5;			// Attribute
			else if (nCls == -5)	// Formula
				nCls = 5;			// Attribute
			window.external.gxcontextmenu(nCls,nId,el.screenX,el.screenY);
		}
		catch (exception)
		{
		}
		window.event.returnValue=false;
	}

	function gxopenhelp(sHelpStr)
	{
		try
		{
			window.external.gxopenhelp(sHelpStr);
		}
		catch (exception)
		{
		}
		window.event.returnValue=false;
	}

	function getGxClass(nVal)
	{
		return "Work Panel";
	}

	function displayVirtual()
	{
		try
		{
			var currel = window.event.srcElement;
			var tagTD = currel.parentElement;
			var tagTR = tagTD.parentElement;
			var tagTABLE = tagTR.parentElement;
			var tagToHideShow = tagTABLE.all("virtual");
			if (tagToHideShow.style.display == "none")
			{
				tagToHideShow.style.display = "block";
				currel.title = "Hide";
				currel.src = sGXPath + "images/closeNode.gif";
			}
			else
			{
				tagToHideShow.style.display="none";
				currel.title = "Show";
				currel.src = sGXPath + "images/openNode.gif";
			}
		}
		catch (exception)
		{
		}
	}
