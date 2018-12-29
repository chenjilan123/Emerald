using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Pets.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Formatter
{
    public class VcardOutputFormatter : TextOutputFormatter
    {
        public VcardOutputFormatter()
        {
            //Add support type
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        /// <summary>
        /// only be able to create vCard text from a Contact type and vice versa.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected override bool CanWriteType(Type type)
        {
            if (typeof(Contact).IsAssignableFrom(type) ||
                typeof(IEnumerable<Contact>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            //DI
            //get services from the dependency injection container (you can't get them from constructor parameters).
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            var logger = serviceProvider.GetService(typeof(ILogger<VcardOutputFormatter>)) as ILogger;
            var response = context.HttpContext.Response;

            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<Contact> contracts)
            {
                foreach (var contract in contracts)
                {
                    FormatVcard(buffer, contract, logger);
                }
            }
            else
            {
                //CanWriteType override ensure the object is Contract of subclass of Contract.
                var contract = context.Object as Contact;
                FormatVcard(buffer, contract, logger);
            }
            return response.WriteAsync(buffer.ToString());
        }

        private static void FormatVcard(StringBuilder buffer, Contact contract, ILogger logger)
        {
            buffer.AppendLine("BEGIN:VCARD");
            buffer.AppendLine("VERSION:2.1");
            //为什么不直接用Append?
            buffer.AppendFormat($"N:{contract.LastName};{contract.FirstName}\r\n");
            buffer.AppendFormat($"FN:{contract.FirstName} {contract.LastName}\r\n");
            buffer.AppendFormat($"UID:{contract.ID}\r\n");
            buffer.AppendLine("END:VCARD");

            logger.LogInformation($"Writing {contract.FirstName} {contract.LastName}");
        }
    }
}
