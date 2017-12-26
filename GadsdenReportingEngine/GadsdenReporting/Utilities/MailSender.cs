/*Author: Cameron Block*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace GadsdenReporting.Utilities {
    /// <summary>
    /// Interface for the mail sender class. 
    /// </summary>
    public interface IMailSender {
        void Send(String toAddress, String subject, String body, params Stream[] attachments);
    }//end class

    /// <summary>
    /// Abstract class for the mail sender class. Contains some implementation. 
    /// </summary>
    public abstract class AbstractMailSender : IMailSender {
        protected const String FROM_ADDRESS = "cblock.pti-nps.com";
        protected String password;

        /// <summary>
        /// Constructor with password argument. 
        /// </summary>
        /// <param name="password"></param>
        public AbstractMailSender(String password) {
            this.password = password;
        }//end method

        /// <summary>
        /// The Send method overloaded to send mail. Good for smart mail type setups. 
        /// </summary>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        public abstract void Send(String toAddress, String subject, String body, params Stream[] attachments);
    }//end class

    /// <summary>
    /// The MailSender class concrete implementation. 
    /// </summary>
    public class MailSender : AbstractMailSender {
        /// <summary>
        /// Send password to the base class implementation. 
        /// </summary>
        /// <param name="password"></param>
        public MailSender(String password) : base(password) { }

        /// <summary>
        /// Send regular smtp email to email server. 
        /// </summary>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        public override void Send(String toAddress, String subject, String body, params Stream[] attachments) {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(FROM_ADDRESS);
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.Body = body;
            foreach (var stream in attachments) {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(stream, new System.Net.Mime.ContentType("application / vnd.openxmlformats - officedocument.spreadsheetml.sheet"));
                mail.Attachments.Add(attachment);
            }//end loop
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential(FROM_ADDRESS, password);
            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);
        }//end method

    }//end class

    /// <summary>
    /// The MailSender Testing concrete implementation. 
    /// </summary>
    public class TestingMailSender : AbstractMailSender {
        /// <summary>
        /// Send password to the base class implementation. 
        /// </summary>
        /// <param name="password"></param>
        public TestingMailSender(String password) : base(password) { }

        /// <summary>
        /// Send regular smtp email to the email server. 
        /// </summary>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        public override void Send(String toAddress, String subject, String body, params Stream[] attachments) {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(FROM_ADDRESS);
            mail.To.Add("cnblock@cox.net");
            mail.Subject = subject;
            mail.Body = body;
            foreach (var stream in attachments) {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(stream, new System.Net.Mime.ContentType("application / vnd.openxmlformats - officedocument.spreadsheetml.sheet"));
                mail.Attachments.Add(attachment);
            }//end loop
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential(FROM_ADDRESS, password);
            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);
        }//end method
    }//end class

}//end namespace
