using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Pets.Data.Model;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Formatter
{
    /// <summary>
    /// Vcard 是一种联系人文档格式.vcf
    /// </summary>
    public class VcardInputFormatter : TextInputFormatter
    {
        public VcardInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            if (type == typeof(Contact))
            {
                return base.CanReadType(type);
            }
            return false;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding effectiveEncoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            //The encoding will be ensure as encodings added by constructor?
            if (effectiveEncoding == null)
            {
                throw new ArgumentNullException(nameof(effectiveEncoding));
            }
            var request = context.HttpContext.Request;

            using (var reader = new StreamReader(request.Body, effectiveEncoding))
            {
                try
                {
                    //Ensure it is a valid vcard, or else throw a exception with a error message in ModelState context.
                    await ReadLineAsync("BEGIN:VCARD", reader, context);
                    //Ensure it is a valid vcard
                    await ReadLineAsync("VERSION:2.1", reader, context);

                    var nameLine = await ReadLineAsync("N:", reader, context);
                    var split = nameLine.Split(";".ToCharArray());
                    var contact = new Contact() { LastName = split[0].Substring(2), FirstName = split[1] };

                    //Ensure it is a valid vcard
                    await ReadLineAsync("FN:", reader, context);

                    var idLine = await ReadLineAsync("UID", reader, context);
                    contact.ID = idLine.Substring(4);
                    //Ensure it is a valid vcard
                    await ReadLineAsync("END:VCARD", reader, context);
                    return await InputFormatterResult.SuccessAsync(contact);
                }
                catch 
                {
                    return await InputFormatterResult.FailureAsync();
                }
            }
        }

        private async Task<string> ReadLineAsync(string expectedText, StreamReader reader, InputFormatterContext context)
        {
            var line = await reader.ReadLineAsync();
            if (!line.StartsWith(expectedText))
            {
                //Add Model Error.
                var errorMessage = $"Looked for '{expectedText}' and got '{line}'";
                context.ModelState.TryAddModelError(context.ModelName, errorMessage);
                throw new Exception(errorMessage);
            }
            return line;
        }
    }
}
