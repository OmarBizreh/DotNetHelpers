using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace DotNetHelpers.Mail
{
    public class Email
    {
        public string From { get; }
        public List<string> To { get; set; } = new List<string>();
        public List<string> Bcc { get; set; } = new List<string>();
        public List<string> Cc { get; set; } = new List<string>();
        public bool IsHTMLBody { get; set; } = false;
        public string Host { get; }
        public int Port { get; }
        public bool EnableSSL { get; set; } = false;
        public NetworkCredential Credentials { get; }

        /// <summary>
        /// Create a new instance of <see cref="Email"/> class.
        /// </summary>
        /// <param name="_Host">SMTP Host</param>
        /// <param name="_Port">SMTP Port</param>
        /// <param name="_From">From Address</param>
        /// <param name="_Credentials">Authentication for From Address</param>
        public Email(string _Host, int _Port, string _From, NetworkCredential _Credentials)
        {
            this.Host = _Host;
            this.Port = _Port;
            this.From = _From;
            this.Credentials = _Credentials;
        }
        /// <summary>
        /// Send Email without Attachments
        /// </summary>
        /// <param name="Content">Content of email</param>
        /// <param name="Subject">Subject of email</param>
        public void SendEmail(string Content, string Subject)
        {
            // To must not be empty
            if (this.To.Count == 0)
                throw new System.ArgumentException("To is empty");

            using (SmtpClient mClient = new SmtpClient(this.Host, this.Port))
            using (MailMessage mMessage = new MailMessage())
            {
                mMessage.From = new MailAddress(this.From);
                mMessage.IsBodyHtml = this.IsHTMLBody;
                mMessage.Body = Content;
                mMessage.Subject = Subject;

                foreach (var item in this.To)
                {
                    mMessage.To.Add(item);
                }

                mClient.EnableSsl = this.EnableSSL;
                mClient.UseDefaultCredentials = false;
                mClient.Credentials = this.Credentials;

                mClient.Send(mMessage);
            }
        }
        /// <summary>
        /// Send email with Attachments
        /// </summary>
        /// <param name="Content">Content of email</param>
        /// <param name="Subject">Subject of email</param>
        /// <param name="AttachmentsPath">List of string containing attachments path</param>
        public void SendEmail(string Content, string Subject, List<string> AttachmentsPath)
        {
            using (SmtpClient mClient = new SmtpClient(this.Host, this.Port))
            using (MailMessage mMessage = new MailMessage())
            {
                mMessage.From = new MailAddress(this.From);
                mMessage.IsBodyHtml = this.IsHTMLBody;
                mMessage.Body = Content;
                mMessage.Subject = Subject;

                foreach (var item in this.To)
                {
                    mMessage.To.Add(item);
                }

                foreach (var item in AttachmentsPath)
                    mMessage.Attachments.Add(new Attachment(item));


                mClient.EnableSsl = this.EnableSSL;
                mClient.UseDefaultCredentials = false;
                mClient.Credentials = this.Credentials;

                mClient.Send(mMessage);
            }
        }
    }
}
