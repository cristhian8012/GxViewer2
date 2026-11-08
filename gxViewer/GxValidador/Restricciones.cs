using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace gxViewer.GxValidador
{
    class Restricciones
    {
    internal static void ValidaRestricciones(ref ListBox Lista, string pLinea, ref string pResultado) 
    {
        //string lPalabra;
        //int indice; // Indice para recorrer listado de palabras
        //int indice2; // Indice para recorrer la cadena
        //int longitud; // Longitud de la cadena
        //char Caracter; // Caracter actual
        //bool flagIniPalabra;
        //string PalabraPrueba;
 
        //longitud = pLinea.Length;
        //flagIniPalabra = true; // Flag que indica si se está iniciando una palabra.
        //pResultado = "";
 
        //for(indice2 = 0; indice2 < longitud; indice2++){
        //    Caracter = pLinea.Substring(indice2, 1);
 
        //    // se busca el primer caracter que sea una letra
        //    if( (System.Text.RegularExpressions.Regex.IsMatch(Caracter, "^[A-Za-z]")) ){
        //        // Si estamos al inicio de la palabra entonces procesamos, de lo contrario se ignora.
        //        if( flagIniPalabra == true ){
        //            flagIniPalabra = false; // actualizamos el flag para indicar que ya hemos revisado esta palabra
        //            // Se copia la porción del renglon desde el inicio de la palabra a la variable PalabraPrueba
        //            PalabraPrueba = pLinea.Substring(indice2).ToString().ToUpper().Trim();
 
        //            int indexEs = PalabraPrueba.IndexOf(" ");
        //            int indexIg = PalabraPrueba.IndexOf("=");
        //            int indexComent = 0;
        //            indexComent = pLinea.IndexOf("//");
 
        //            //Dim a As String = PalabraPrueba.Substring(indexComent)
        //            if( (indexEs != -1) ){
        //                PalabraPrueba = PalabraPrueba.Substring(0, PalabraPrueba.IndexOf(" "));
        //            }else if( (indexIg != -1) ){
        //                PalabraPrueba = PalabraPrueba.Substring(0, PalabraPrueba.IndexOf("="));
        //            }else if( (indexComent != -1) ){
 
        //                PalabraPrueba = pLinea.Substring(indexComent);
        //            }
 
        //            // Recorremos la lista de palabras restrMOingidas
        //            for(indice = 0; indice < Lista.Items.Count(); indice++){
        //                lPalabra = Lista.Items[indice].ToString().Trim().ToUpper();
 
        //                // Preguntamos si alguna de las palabras restringidas está coincide con el inicio del renglón.
 
        //                if( PalabraPrueba.Replace("'", " ").Trim().Equals(Trim(lPalabra)) == true ){
        //                    //If PalabraPrueba.StartsWith(Trim(lPalabra)) = True Then
        //                    pResultado = lPalabra;
        //                    return;
        //                }
 
        //            }
        //        }
        //    }else{
        //        flagIniPalabra = true;
        //    }
        //}
    }  
 
    internal static void ValidaFormatoConstante(string pLinea, ref string pResultado1) 
    {
    
//        int indice2; // Indice para recorrer la cadena
//        int longitud; // Longitud de la cadena
//        char Caracter; // Caracter actual
//        string PalabraPrueba;
//        string Estado;
//        int Posicion1;
//        int posicion2;
//        int numero;
 
//        longitud = pLinea.Length();
 
//        Estado = "INICIAL";
//        PalabraPrueba = "";
//        pResultado1 = "";
 
//        for(indice2 = 0; indice2 < longitud; indice2++){
//            Caracter = pLinea.Substring(indice2, 1).ToUpper();
//            if( (System.Text.RegularExpressions.Regex.IsMatch(Caracter, "^[A-Za-z]")) ){
 
//                if( Estado == "INICIAL" ){
//                    if( Caracter == "O" | Caracter == "U" | Caracter == "D" | Caracter == "C" | Caracter == "G" ){
//                        Estado = "POSUSER";
//                        PalabraPrueba = PalabraPrueba + Caracter;
//                    }else{
//                        Estado = "POSMAILNOM";
//                        PalabraPrueba = PalabraPrueba + Caracter;
//                    }
//                }else if( Estado == "POSUSER" ){
//                    Estado = "POSMAILNOM";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAILNOM" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSNUMERO" ){
//                    Estado = "POSMAILNOM";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAIL" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }
//            }else if( (System.Text.RegularExpressions.Regex.IsMatch(Caracter, "^[0-9]")) ){
//                if( Estado == "INICIAL" ){
//                    Estado = "POSNUMERO";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSUSER" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSNUMERO" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAILNOM" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAIL" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else{
//                    Estado = "INICIAL";
//                    PalabraPrueba = "";
//                }
//            }else if( (Caracter == "@") ){
//                if( Estado == "INICIAL" ){
//                    Estado = "POSMAIL";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSUSER" ){
//                    Estado = "POSMAIL";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSNUMERO" ){
//                    Estado = "POSMAIL";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAILNOM" ){
//                    Estado = "POSMAIL";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAIL" ){
//                    Estado = "INICIAL";
//                    PalabraPrueba = "";
//                }else{
//                    Estado = "INICIAL";
//                    PalabraPrueba = "";
//                }
//            }else if( (Caracter == "_") ){
//                if( Estado == "INICIAL" ){
//                    Estado = "POSMAILNOM";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSUSER" ){
//                    Estado = "POSMAILNOM";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSNUMERO" ){
//                    Estado = "POSMAILNOM";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAIL" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAILNOM" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else{
//                    Estado = "INICIAL";
//                    PalabraPrueba = "";
//                }
//            }else if( (Caracter == ".") ){
//                if( Estado == "INICIAL" ){
//                    Estado = "POSMAILNOM";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSUSER" ){
//                    Estado = "POSMAILNOM";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSNUMERO" ){
//                    Estado = "POSMAILNOM";
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAIL" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else if( Estado == "POSMAILNOM" ){
//                    PalabraPrueba = PalabraPrueba + Caracter;
//                }else{
//                    Estado = "INICIAL";
//                    PalabraPrueba = "";
//                }
//            }else{
//                if( Estado == "POSUSER" ){
//                    if( PalabraPrueba.Length() == 5 | PalabraPrueba.Length() == 6 ){
//                        pResultado1 = "Posible código de usuario : " + PalabraPrueba;
//                        return;
//                    }else{
//                        Estado = "INICIAL";
//                        PalabraPrueba = "";
//                    }
//                }else if( Estado == "POSNUMERO" ){
//                    if( PalabraPrueba.Length() == 16 ){
//                        pResultado1 = "Posible Tarjeta de Crédito : " + PalabraPrueba;
//                        return;
//                    }else if( PalabraPrueba.Length() == 11 ){
//                        pResultado1 = "Posible número de RUC : " + PalabraPrueba;
//                        return;
//                    }else if( PalabraPrueba.Length() == 8 ){
//System.Globalization.NumberFormatInfo nfi_g0 = new  System.Globalization.NumberFormatInfo();
//nfi_g0.CurrencyGroupSeparator = ".";
//                        numero = Convert.ToDouble(PalabraPrueba.Trim(, nfi_g0).Substring(0, 4));
//                        if( numero < 1900 ){
//                            pResultado1 = "Posible número de DNI : " + PalabraPrueba;
//                            return;
//                        }
//                    }else if( PalabraPrueba.Length() == 14 ){
//                        pResultado1 = "Posible Rubro : " + PalabraPrueba;
//                        return;
//                    }else{
//                        Estado = "INICIAL";
//                        PalabraPrueba = "";
//                    }
//                }else if( Estado == "POSMAIL" ){
 
//                    Posicion1 = PalabraPrueba.IndexOf("@");
//                    posicion2 = PalabraPrueba.LastIndexOf(".");
 
//                    if( PalabraPrueba.EndsWith(".") == false ){
//                        if( posicion2 > Posicion1 ){
//                            if( PalabraPrueba.StartsWith("@") ){
//                                pResultado1 = "Posible Dominio : " + PalabraPrueba;
//                                return;
//                            }else{
//                                pResultado1 = "Posible Correo Electrónico : " + PalabraPrueba;
//                                return;
//                            }
//                        }else{
//                            Estado = "INICIAL";
//                            PalabraPrueba = "";
//                        }
//                    }
//                }else{
//                    Estado = "INICIAL";
//                    PalabraPrueba = "";
//                }
//            }
//        }
 
//        if( Estado == "POSNUMERO" ){
//            if( PalabraPrueba.Length() == 16 ){
//                pResultado1 = "Posible Tarjeta de Crédito : " + PalabraPrueba;
//            }else if( PalabraPrueba.Length() == 11 ){
//                pResultado1 = "Posible número de RUC : " + PalabraPrueba;
 
//            }else if( PalabraPrueba.Length() == 8 ){
//System.Globalization.NumberFormatInfo nfi_g1 = new  System.Globalization.NumberFormatInfo();
//nfi_g1.CurrencyGroupSeparator = ".";
//                numero = Convert.ToDouble(PalabraPrueba.Trim(, nfi_g1).Substring(0, 4));
//                if( numero < 1900 ){
//                    pResultado1 = "Posible número de DNI : " + PalabraPrueba;
//                }
 
//            }else if( PalabraPrueba.Length() == 14 ){
//                pResultado1 = "Posible Rubro : " + PalabraPrueba;
//            }
//        }else if( Estado == "POSUSER" ){
//            if( PalabraPrueba.Length() == 5 | PalabraPrueba.Length() == 6 ){
//                pResultado1 = "Posible código de usuario : " + PalabraPrueba;
//            }
//        }else if( Estado == "POSMAIL" ){
 
//            Posicion1 = PalabraPrueba.IndexOf("@");
//            posicion2 = PalabraPrueba.LastIndexOf(".");
 
//            if( PalabraPrueba.EndsWith(".") == false ){
//                if( posicion2 > Posicion1 ){
//                    if( PalabraPrueba.StartsWith("@") ){
//                        pResultado1 = "Posible Dominio : " + PalabraPrueba;
//                    }else{
//                        pResultado1 = "Posible Correo Electrónico : " + PalabraPrueba;
//                    }
//                }
//            }
//        }
 
//        if( pResultado1 == "" ){
//            // validar división entre cero
 
//            string linea2;
 
//            linea2 = pLinea;
//            if( linea2.Contains("/*") ){
//                Posicion1 = linea2.IndexOf("/*");
 
              
 
//                if( linea2.Contains("*/") ){
//                    posicion2 = linea2.IndexOf("*/");
//                    linea2 = linea2.Remove(Posicion1, posicion2 - Posicion1 + 2);
//                }else{
//                    // no hay cierre de comentario. Se borra hasta el final de la linea
//                    linea2 = linea2.Remove(Posicion1);
//                }
//            }
 
//            if( linea2.Contains("//") ){
//                Posicion1 = linea2.IndexOf("//");
//                linea2 = linea2.Remove(Posicion1);
//            }
 
//            while( linea2.Contains("'")){
//                Posicion1 = 0;
//                posicion2 = 0;
//                Posicion1 = linea2.IndexOf("'");
//                posicion2 = linea2.IndexOf("'", Posicion1 + 1);
//                if( posicion2 > 0 ){
//                    linea2 = linea2.Remove(Posicion1, posicion2 - Posicion1 + 1);
//                }else{
//                    break;
//                }
 
//            }
 
//            if( linea2.Contains("/") ){
//                Posicion1 = linea2.IndexOf("/");
//                if( linea2.Length() > 2 ){
//                    if( linea2.Substring(Posicion1 - 1, 2) != "</" ){
//                        linea2 = linea2.Substring(Posicion1 + 1).Trim();
//                        if( linea2.Length() > 0 ){
 
//                            Caracter = linea2.Substring(0, 1).Trim();
 
//                            if( (System.Text.RegularExpressions.Regex.IsMatch(Caracter, "^[0-9]")) == false ){
//                                pResultado1 = "Posible división entre cero : " + pLinea.Trim();
//                            }
//                        }
 
//                    }
//                }
                //ElseIf linea2.Contains("&") Then
                //    ' validando si se usa un vector
                //    Posicion1 = linea2.LastIndexOf("&") + 1
                //    longitud = linea2.Length()
                //    Estado = "VARIABLE"
                //    Do While Posicion1 < longitud - 1
                //        Caracter = linea2.Substring(Posicion1, 1)
                //        If (System.Text.RegularExpressions.Regex.IsMatch(Caracter, "^[0-9]")) Then
 
                //        ElseIf (System.Text.RegularExpressions.Regex.IsMatch(Caracter, "^[A-Za-z]")) Then
                //        ElseIf Caracter = "_" Then
                //            If Estado = "VARIABLE" Then
 
                //            End If
                //        ElseIf Caracter = "(" Then
 
                //        ElseIf Caracter = ")" Then
 
                //        End If
                //        Posicion1 = Posicion1 + 1
                //    Loop
        //    }
        //}
    }  
 
 
    }


}
