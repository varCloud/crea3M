﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace CREA3M.Models
{
    public class Utils
    {
        public static string ToXML(Object o)
        {
            try
            {
                var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                var serializer = new XmlSerializer(o.GetType());
                var settings = new XmlWriterSettings();
                settings.Indent = false;
                settings.OmitXmlDeclaration = true;
                using (var stream = new StringWriter())
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    serializer.Serialize(writer, o, emptyNamepsaces);
                    return stream.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Result NotificacionPedidoEnviado(string usuario, string email, string guia, int idUsuarioOrdenCompra)
        {
            Result result = new Result();
            try
            {
                string cuerpo = Cabecera();
                cuerpo += CuerpoSendInicioSesion(usuario, guia, idUsuarioOrdenCompra);
                cuerpo += PiePagina();
                EnviarCorreNotificacionInicioSesion("Tu pedido de CREA ha sido enviado", cuerpo, email);
                result.mensaje = "NOTIFICACION ENVIADA";
                result.status = true;
            }
            catch (Exception ex)
            {
                result.status = false;
                result.mensaje = "Error al enviar la notificacion: " + ex.Message;
            }
            return result;
        }


        public static string Cabecera()
        {
            return @"<html><body><!DOCTYPE html>
            <html>
            <head>
	            <title></title>
            </head>
            <body style='background-color: white;'>";
        }

        private static string CuerpoSendInicioSesion(string usuario, string guia, int idUsuarioOrdenCompra)
        {
            StringBuilder cuerpo = new StringBuilder();
            cuerpo.Append(@"<table border='0' cellpadding='0' cellspacing='0' width='100%'>	
	            <tr>
		            <td style='padding: 10px 0 30px 0;'>
			            <table align='center' border='0' cellpadding='0' cellspacing='0' width='700' style='border: 1px solid #cccccc; border-collapse: collapse;'>
				            <tr>
					            <td bgcolor='#ffffff' style='padding: 40px 30px 40px 30px;'>
						            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                         <tr>
                                            <td align='center' bgcolor='#ffffff' style='padding: 40px 0 30px 0;'>
                                                <img src = 'https://tienda.crea3m.com/assets/img/crealogo2.png'  width='30%' height='50' style='display: block;' />
                                            </td>
                                         </tr>
							            <tr>
								            <td style='color: #153643; font-family: Arial, sans-serif; font-size: 24px;'>
									            <b>Hola " + usuario + @"</b>
								            </td>
							            </tr>
							            <tr>
								            <td style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
									            Te informamos que hemos enviado tus productos.
								            </td>
							            </tr>
							            <tr>
								            <td>
									            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
										            <tr>
											            <td width='260' valign='top'>
												            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
													            <tr>
														            <td style='padding: 25px 0 0 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
															            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
															            <tr>
																            <td>
																	            <b>Fecha Envio: </b>" + DateTime.Now.ToString("dd/MM/yyyy") + @"
																            </td>
															            </tr>
                                                                         <tr>
																            <td>
																	            <b>Orden: </b>" + idUsuarioOrdenCompra + @"
																            </td>
															            </tr>
                                                                        <tr>
																            <td>
																	            <b>Guia: </b>" + guia + @"
																            </td>
															            </tr>
																      
															            
                                                                         <tr>
																            <td style='padding: 20px 0 30px 0; color: #153643; font-family: Arial, sans-serif; font-size: 16px; line-height: 20px;'>
									                                            Saludos cordiales.
								                                            </td>
															            </tr>
	                                                    	            </table>
														            </td>
													            </tr>
												            </table>
											            </td>
											            <td style='font-size: 0; line-height: 0;' width='20'>
												            &nbsp;
											            </td>
										            </tr>
									            </table>
								            </td>
							            </tr>
						            </table>
					            </td>
				            </tr>
                            <tr>
					            <td bgcolor='red' style='padding: 30px 30px 30px 30px;'>
						            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
							            <tr>
								            <td style='color: #ffffff; font-family: Arial, sans-serif; font-size: 16px; width='75%'>
									             
								            </td>
							            </tr>
						            </table>
					            </td>
				            </tr>
				            <tr>
					            <td bgcolor='#191919' style='padding: 30px 30px 30px 30px;'>
						            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
							            <tr>
								            <td style='color: #ffffff; font-family: Arial, sans-serif; font-size: 14px; width='75%'>
									          
								            </td>
							            </tr>
						            </table>
					            </td>
				            </tr>
                            
                        </table>
		            </td>
	            </tr>
            </table>");
            return cuerpo.ToString();
        }

        private static String PiePagina()
        {
            StringBuilder pie = new StringBuilder();
            pie.Append("<br /><br />");
            pie.Append("</body>");
            pie.Append("</html>");
            return pie.ToString();
        }

        private static void EnviarCorreNotificacionInicioSesion(string asunto, string cuerpo, string email)
        {
            try
            {
                string correoProveedor = WebConfigurationManager.AppSettings["correoProveedor"].ToString();
                string contrasenaProveedor = WebConfigurationManager.AppSettings["contrasenaProveedor"].ToString();

                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
                mmsg.To.Add(email); // cuenta Email a la cual sera dirigido el correo
                mmsg.Subject = asunto; //Asunto del correo
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8; //cambiamos el tipo de texto a UTF8
                mmsg.Body = cuerpo; //Cuerpo del mensaje
                mmsg.BodyEncoding = System.Text.Encoding.UTF8; // tambien encodear a utf8
                mmsg.IsBodyHtml = true; // indicamos que dentro del body viene codigo HTML
                mmsg.From = new System.Net.Mail.MailAddress(correoProveedor); // el email que enviara el correo (proveedor)

                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient(); // se realiza el cliente correo

                cliente.Port = 587;
                cliente.EnableSsl = true;
                cliente.UseDefaultCredentials = false;
                cliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                cliente.Host = "smtp.gmail.com"; //mail.dominio.com
                cliente.Credentials = new System.Net.NetworkCredential(correoProveedor, contrasenaProveedor);  // Credenciales del correo emisor
                
                
                cliente.Send(mmsg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }


}