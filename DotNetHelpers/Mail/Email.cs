using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetHelpers.Mail
{

    public class Email
    {
        /// <summary>
        /// Regex validation expression to validate string if valid email
        /// </summary>
        private static Regex EmailValidationExpression = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*)$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        /// <summary>
        /// Sender address
        /// </summary>
        public string From { get; }

        /// <summary>
        /// <see cref="List{string}"/> of recipients
        /// </summary>
        public List<string> To { get; set; } = new List<string>();

        /// <summary>
        /// <see cref="List{string}"/> of recipients
        /// </summary>
        public List<string> Bcc { get; set; } = new List<string>();

        /// <summary>
        /// <see cref="List{string}"/> of recipients
        /// </summary>
        public List<string> Cc { get; set; } = new List<string>();

        /// <summary>
        /// Is email body written in HTML or plain text
        /// </summary>
        public bool IsHTMLBody { get; set; } = false;

        /// <summary>
        /// SMTP server address
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// SMTP server port
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Enable SSL when sending email
        /// </summary>
        public bool EnableSSL { get; set; } = false;

        /// <summary>
        /// Sender credentials
        /// </summary>
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
            if (!Email.IsValidEmail(_From))
                throw new System.ArgumentException("From is not valid");
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

            using (SmtpClient mClient = new SmtpClient())
            {
                MimeKit.MimeMessage mMessage = new MimeKit.MimeMessage();
                mMessage.From.Add(new MailboxAddress("", this.From));
                mMessage.Body = new TextPart(this.IsHTMLBody ? "HTML" : "plain") { Text = Content };
                mMessage.Subject = Subject;

                foreach (var item in this.To)
                {
                    if (!Email.IsValidEmail(item))
                        throw new System.ArgumentException($"{item} is not valid email");
                    mMessage.To.Add(new MailboxAddress("", item));
                }

                foreach (var item in this.Cc)
                {
                    if (!Email.IsValidEmail(item))
                        throw new System.ArgumentException($"{item} is not valid email");
                    mMessage.Cc.Add(new MailboxAddress("", item));
                }

                foreach (var item in this.Bcc)
                {
                    if (!Email.IsValidEmail(item))
                        throw new System.ArgumentException($"{item} is not valid email");
                    mMessage.Bcc.Add(new MailboxAddress("", item));
                }
                mClient.Connect(this.Host, this.Port, false);
                mClient.AuthenticationMechanisms.Remove("XOAUTH2");
                mClient.Authenticate(this.Credentials.UserName, this.Credentials.Password);
                mClient.Send(mMessage);
                mClient.Disconnect(true);
            }
        }

        /// <summary>
        /// Validates if email is valid
        /// </summary>
        /// <param name="InMail">Email to validate</param>
        /// <returns></returns>
        public static bool IsValidEmail(string InMail)
        {
            var isMatch = EmailValidationExpression.Match(InMail);
            return isMatch.Success;
        }
    }
}
