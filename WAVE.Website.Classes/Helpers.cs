using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ZXing;
using ZXing.Common;
using BarcodeWriter = ZXing.BarcodeWriter;

namespace WAVE.Website.Classes
{
    public static class Helper
    {
        /// <summary>
        ///     Trim the front of a string and replace with "..." if it's longer than a certain length.
        /// </summary>
        /// <param name="value">String this extends.</param>
        /// <param name="maxLength">Maximum length.</param>
        /// <returns>Ellipsis shortened string.</returns>
        public static string TrimFrontIfLongerThan(this string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                return "..." + value.Substring(value.Length - (maxLength - 3));
            }

            return value;
        }

        /// <summary>
        ///     Trim the front of a string and replace with "..." if it's longer than a certain length.
        /// </summary>
        /// <param name="value">String this extends.</param>
        /// <param name="maxLength">Maximum length.</param>
        /// <returns>Ellipsis shortened string.</returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (value == null)
            {
                return "";
            }
            if (value.Length > maxLength)
            {
                return value.Substring(0, maxLength) + "...";
            }

            return value;
        }

        public static string HtmlStripTags(this string htmlContent, bool replaceNamedEntities,
            bool replaceNumberedEntities)
        {
            if (htmlContent == null)
                return null;
            htmlContent = htmlContent.Trim();
            if (htmlContent == string.Empty)
                return string.Empty;

            int bodyStartTagIdx = htmlContent.IndexOf("<body", StringComparison.CurrentCultureIgnoreCase);
            int bodyEndTagIdx = htmlContent.IndexOf("</body>", StringComparison.CurrentCultureIgnoreCase);

            int startIdx = 0, endIdx = htmlContent.Length - 1;
            if (bodyStartTagIdx >= 0)
                startIdx = bodyStartTagIdx;
            if (bodyEndTagIdx >= 0)
                endIdx = bodyEndTagIdx;

            bool insideTag = false,
                insideAttributeValue = false,
                insideHtmlComment = false,
                insideScriptBlock = false,
                insideNoScriptBlock = false,
                insideStyleBlock = false;
            char attributeValueDelimiter = '"';

            var sb = new StringBuilder(htmlContent.Length);
            for (int i = startIdx; i <= endIdx; i++)
            {
                // html comment block
                if (!insideHtmlComment)
                {
                    if (i + 3 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        htmlContent[i + 1] == '!' &&
                        htmlContent[i + 2] == '-' &&
                        htmlContent[i + 3] == '-')
                    {
                        i += 3;
                        insideHtmlComment = true;
                        continue;
                    }
                }
                else // inside html comment
                {
                    if (i + 2 < htmlContent.Length &&
                        htmlContent[i] == '-' &&
                        htmlContent[i + 1] == '-' &&
                        htmlContent[i + 2] == '>')
                    {
                        i += 2;
                        insideHtmlComment = false;
                    }
                    continue;
                }

                // noscript block
                if (!insideNoScriptBlock)
                {
                    if (i + 9 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        (htmlContent[i + 1] == 'n' || htmlContent[i + 1] == 'N') &&
                        (htmlContent[i + 2] == 'o' || htmlContent[i + 2] == 'O') &&
                        (htmlContent[i + 3] == 's' || htmlContent[i + 3] == 'S') &&
                        (htmlContent[i + 4] == 'c' || htmlContent[i + 4] == 'C') &&
                        (htmlContent[i + 5] == 'r' || htmlContent[i + 5] == 'R') &&
                        (htmlContent[i + 6] == 'i' || htmlContent[i + 6] == 'I') &&
                        (htmlContent[i + 7] == 'p' || htmlContent[i + 7] == 'P') &&
                        (htmlContent[i + 8] == 't' || htmlContent[i + 8] == 'T') &&
                        (char.IsWhiteSpace(htmlContent[i + 9]) || htmlContent[i + 9] == '>'))
                    {
                        i += 9;
                        insideNoScriptBlock = true;
                        continue;
                    }
                }
                else // inside noscript block
                {
                    if (i + 10 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        htmlContent[i + 1] == '/' &&
                        (htmlContent[i + 2] == 'n' || htmlContent[i + 2] == 'N') &&
                        (htmlContent[i + 3] == 'o' || htmlContent[i + 3] == 'O') &&
                        (htmlContent[i + 4] == 's' || htmlContent[i + 4] == 'S') &&
                        (htmlContent[i + 5] == 'c' || htmlContent[i + 5] == 'C') &&
                        (htmlContent[i + 6] == 'r' || htmlContent[i + 6] == 'R') &&
                        (htmlContent[i + 7] == 'i' || htmlContent[i + 7] == 'I') &&
                        (htmlContent[i + 8] == 'p' || htmlContent[i + 8] == 'P') &&
                        (htmlContent[i + 9] == 't' || htmlContent[i + 9] == 'T') &&
                        (char.IsWhiteSpace(htmlContent[i + 10]) || htmlContent[i + 10] == '>'))
                    {
                        if (htmlContent[i + 10] != '>')
                        {
                            i += 9;
                            while (i < htmlContent.Length && htmlContent[i] != '>')
                                i++;
                        }
                        else
                            i += 10;
                        insideNoScriptBlock = false;
                    }
                    continue;
                }

                // script block
                if (!insideScriptBlock)
                {
                    if (i + 7 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        (htmlContent[i + 1] == 's' || htmlContent[i + 1] == 'S') &&
                        (htmlContent[i + 2] == 'c' || htmlContent[i + 2] == 'C') &&
                        (htmlContent[i + 3] == 'r' || htmlContent[i + 3] == 'R') &&
                        (htmlContent[i + 4] == 'i' || htmlContent[i + 4] == 'I') &&
                        (htmlContent[i + 5] == 'p' || htmlContent[i + 5] == 'P') &&
                        (htmlContent[i + 6] == 't' || htmlContent[i + 6] == 'T') &&
                        (char.IsWhiteSpace(htmlContent[i + 7]) || htmlContent[i + 7] == '>'))
                    {
                        i += 6;
                        insideScriptBlock = true;
                        continue;
                    }
                }
                else // inside script block
                {
                    if (i + 8 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        htmlContent[i + 1] == '/' &&
                        (htmlContent[i + 2] == 's' || htmlContent[i + 2] == 'S') &&
                        (htmlContent[i + 3] == 'c' || htmlContent[i + 3] == 'C') &&
                        (htmlContent[i + 4] == 'r' || htmlContent[i + 4] == 'R') &&
                        (htmlContent[i + 5] == 'i' || htmlContent[i + 5] == 'I') &&
                        (htmlContent[i + 6] == 'p' || htmlContent[i + 6] == 'P') &&
                        (htmlContent[i + 7] == 't' || htmlContent[i + 7] == 'T') &&
                        (char.IsWhiteSpace(htmlContent[i + 8]) || htmlContent[i + 8] == '>'))
                    {
                        if (htmlContent[i + 8] != '>')
                        {
                            i += 7;
                            while (i < htmlContent.Length && htmlContent[i] != '>')
                                i++;
                        }
                        else
                            i += 8;
                        insideScriptBlock = false;
                    }
                    continue;
                }

                // style block
                if (!insideStyleBlock)
                {
                    if (i + 7 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        (htmlContent[i + 1] == 's' || htmlContent[i + 1] == 'S') &&
                        (htmlContent[i + 2] == 't' || htmlContent[i + 2] == 'T') &&
                        (htmlContent[i + 3] == 'y' || htmlContent[i + 3] == 'Y') &&
                        (htmlContent[i + 4] == 'l' || htmlContent[i + 4] == 'L') &&
                        (htmlContent[i + 5] == 'e' || htmlContent[i + 5] == 'E') &&
                        (char.IsWhiteSpace(htmlContent[i + 6]) || htmlContent[i + 6] == '>'))
                    {
                        i += 5;
                        insideStyleBlock = true;
                        continue;
                    }
                }
                else // inside script block
                {
                    if (i + 8 < htmlContent.Length &&
                        htmlContent[i] == '<' &&
                        htmlContent[i + 1] == '/' &&
                        (htmlContent[i + 2] == 's' || htmlContent[i + 2] == 'S') &&
                        (htmlContent[i + 3] == 't' || htmlContent[i + 3] == 'C') &&
                        (htmlContent[i + 4] == 'y' || htmlContent[i + 4] == 'R') &&
                        (htmlContent[i + 5] == 'l' || htmlContent[i + 5] == 'I') &&
                        (htmlContent[i + 6] == 'e' || htmlContent[i + 6] == 'P') &&
                        (char.IsWhiteSpace(htmlContent[i + 7]) || htmlContent[i + 7] == '>'))
                    {
                        if (htmlContent[i + 7] != '>')
                        {
                            i += 7;
                            while (i < htmlContent.Length && htmlContent[i] != '>')
                                i++;
                        }
                        else
                            i += 7;
                        insideStyleBlock = false;
                    }
                    continue;
                }

                if (!insideTag)
                {
                    if (i < htmlContent.Length &&
                        htmlContent[i] == '<')
                    {
                        insideTag = true;
                        continue;
                    }
                }
                else // inside tag
                {
                    if (!insideAttributeValue)
                    {
                        if (htmlContent[i] == '"' || htmlContent[i] == '\'')
                        {
                            attributeValueDelimiter = htmlContent[i];
                            insideAttributeValue = true;
                            continue;
                        }
                        if (htmlContent[i] == '>')
                        {
                            insideTag = false;
                            sb.Append(' ');
                                // prevent words from different tags (<td>s for example) from joining together
                        }
                    }
                    else // inside tag and inside attribute value
                    {
                        if (htmlContent[i] == attributeValueDelimiter)
                        {
                            insideAttributeValue = false;
                        }
                    }
                    continue;
                }

                sb.Append(htmlContent[i]);
            }

            if (replaceNamedEntities)
                foreach (var htmlNamedEntity in HtmlNamedEntities)
                    sb.Replace(htmlNamedEntity[0], htmlNamedEntity[1]);

            if (replaceNumberedEntities)
                for (int i = 0; i < 512; i++)
                    sb.Replace("&#" + i + ";", ((char) i).ToString(CultureInfo.InvariantCulture));

            return sb.ToString();
        }

        public static string ResolveUrl(string originalUrl)
        {
            if (originalUrl == null)
                return null;

            // *** Absolute path - just return
            if (originalUrl.IndexOf("://", StringComparison.Ordinal) != -1)
                return originalUrl;

            // *** Fix up image path for ~ root app dir directory
            if (originalUrl.StartsWith("~"))
            {
                string newUrl;
                if (HttpContext.Current != null)
                    newUrl = HttpContext.Current.Request.ApplicationPath +
                             originalUrl.Substring(1).Replace("//", "/");
                else
                    // *** Not context: assume current directory is the base directory
                    throw new ArgumentException("Invalid URL: Relative URL not allowed.");

                // *** Just to be sure fix up any double slashes
                return newUrl;
            }

            return originalUrl;
        }


        public static bool IsValidImage(this Stream image)
        {
            ImageFormat[] validFormats = {ImageFormat.Jpeg, ImageFormat.Png};
            try
            {
                using (Image img = Image.FromStream(image))
                {
                    return validFormats.Contains(img.RawFormat);
                }
            }
            catch
            {
                return false;
            }
        }

        #region variables

        private static readonly string[][] HtmlNamedEntities =
        {
            new[] {"&quot;", "\""},
            new[] {"&lt;", "<"},
            new[] {"&gt;", ">"},
            new[] {"&nbsp;", " "},
            new[] {"&iexcl;", "¡"},
            new[] {"&cent;", "¢"},
            new[] {"&pound;", "£"},
            new[] {"&curren;", "¤"},
            new[] {"&yen;", "¥"},
            new[] {"&brvbar;", "¦"},
            new[] {"&sect;", "§"},
            new[] {"&uml;", "¨"},
            new[] {"&copy;", "©"},
            new[] {"&ordf;", "ª"},
            new[] {"&laquo;", "«"},
            new[] {"&not;", "¬"},
            new[] {"&shy;", "­"},
            new[] {"&reg;", "®"},
            new[] {"&macr;", "¯"},
            new[] {"&deg;", "°"},
            new[] {"&plusmn;", "±"},
            new[] {"&sup2;", "²"},
            new[] {"&sup3;", "³"},
            new[] {"&acute;", "´"},
            new[] {"&micro;", "µ"},
            new[] {"&para;", "¶"},
            new[] {"&middot;", "·"},
            new[] {"&cedil;", "¸"},
            new[] {"&sup1;", "¹"},
            new[] {"&ordm;", "º"},
            new[] {"&raquo;", " »"},
            new[] {"&frac14;", "¼"},
            new[] {"&frac12;", "½"},
            new[] {"&frac34;", "¾"},
            new[] {"&iquest;", "¿"},
            new[] {"&Agrave;", "À"},
            new[] {"&Aacute;", "Á"},
            new[] {"&Acirc;", "Â"},
            new[] {"&Atilde;", "Ã"},
            new[] {"&Auml;", "Ä"},
            new[] {"&Aring;", "Å"},
            new[] {"&AElig;", "Æ"},
            new[] {"&Ccedil;", "Ç"},
            new[] {"&Egrave;", "È"},
            new[] {"&Eacute;", "É"},
            new[] {"&Ecirc;", "Ê"},
            new[] {"&Euml;", "Ë"},
            new[] {"&Igrave;", "Ì"},
            new[] {"&Iacute;", "Í"},
            new[] {"&Icirc;", "Î"},
            new[] {"&Iuml;", "Ï"},
            new[] {"&ETH;", "Ð"},
            new[] {"&Ntilde;", "Ñ"},
            new[] {"&Ograve;", "Ò"},
            new[] {"&Oacute;", "Ó"},
            new[] {"&Ocirc;", "Ô"},
            new[] {"&Otilde;", "Õ"},
            new[] {"&Ouml;", "Ö"},
            new[] {"&times;", "×"},
            new[] {"&Oslash;", "Ø"},
            new[] {"&Ugrave;", "Ù"},
            new[] {"&Uacute;", "Ú"},
            new[] {"&Ucirc;", "Û"},
            new[] {"&Uuml;", "Ü"},
            new[] {"&Yacute;", "Ý"},
            new[] {"&THORN;", "Þ"},
            new[] {"&szlig;", "ß"},
            new[] {"&agrave;", "à"},
            new[] {"&aacute;", "á"},
            new[] {"&acirc;", "â"},
            new[] {"&atilde;", "ã"},
            new[] {"&auml;", "ä"},
            new[] {"&aring;", "å"},
            new[] {"&aelig;", "æ"},
            new[] {"&ccedil;", "ç"},
            new[] {"&egrave;", "è"},
            new[] {"&eacute;", "é"},
            new[] {"&ecirc;", "ê"},
            new[] {"&euml;", "ë"},
            new[] {"&igrave;", "ì"},
            new[] {"&iacute;", "í"},
            new[] {"&icirc;", "î"},
            new[] {"&iuml;", "ï"},
            new[] {"&eth;", "ð"},
            new[] {"&ntilde;", "ñ"},
            new[] {"&ograve;", "ò"},
            new[] {"&oacute;", "ó"},
            new[] {"&ocirc;", "ô"},
            new[] {"&otilde;", "õ"},
            new[] {"&ouml;", "ö"},
            new[] {"&divide;", "÷"},
            new[] {"&oslash;", "ø"},
            new[] {"&ugrave;", "ù"},
            new[] {"&uacute;", "ú"},
            new[] {"&ucirc;", "û"},
            new[] {"&uuml;", "ü"},
            new[] {"&yacute;", "ý"},
            new[] {"&thorn;", "þ"},
            new[] {"&yuml;", "ÿ"},
            new[] {"&OElig;", "Œ"},
            new[] {"&oelig;", "œ"},
            new[] {"&Scaron;", "Š"},
            new[] {"&scaron;", "š"},
            new[] {"&Yuml;", "Ÿ"},
            new[] {"&fnof;", "ƒ"},
            new[] {"&circ;", "ˆ"},
            new[] {"&tilde;", "˜"},
            new[] {"&Alpha;", "Α"},
            new[] {"&Beta;", "Β"},
            new[] {"&Gamma;", "Γ"},
            new[] {"&Delta;", "Δ"},
            new[] {"&Epsilon;", "Ε"},
            new[] {"&Zeta;", "Ζ"},
            new[] {"&Eta;", "Η"},
            new[] {"&Theta;", "Θ"},
            new[] {"&Iota;", "Ι"},
            new[] {"&Kappa;", "Κ"},
            new[] {"&Lambda;", "Λ"},
            new[] {"&Mu;", "Μ"},
            new[] {"&Nu;", "Ν"},
            new[] {"&Xi;", "Ξ"},
            new[] {"&Omicron;", "Ο"},
            new[] {"&Pi;", "Π"},
            new[] {"&Rho;", "Ρ"},
            new[] {"&Sigma;", "Σ"},
            new[] {"&Tau;", "Τ"},
            new[] {"&Upsilon;", "Υ"},
            new[] {"&Phi;", "Φ"},
            new[] {"&Chi;", "Χ"},
            new[] {"&Psi;", "Ψ"},
            new[] {"&Omega;", "Ω"},
            new[] {"&alpha;", "α"},
            new[] {"&beta;", "β"},
            new[] {"&gamma;", "γ"},
            new[] {"&delta;", "δ"},
            new[] {"&epsilon;", "ε"},
            new[] {"&zeta;", "ζ"},
            new[] {"&eta;", "η"},
            new[] {"&theta;", "θ"},
            new[] {"&iota;", "ι"},
            new[] {"&kappa;", "κ"},
            new[] {"&lambda;", "λ"},
            new[] {"&mu;", "μ"},
            new[] {"&nu;", "ν"},
            new[] {"&xi;", "ξ"},
            new[] {"&omicron;", "ο"},
            new[] {"&pi;", "π"},
            new[] {"&rho;", "ρ"},
            new[] {"&sigmaf;", "ς"},
            new[] {"&sigma;", "σ"},
            new[] {"&tau;", "τ"},
            new[] {"&upsilon;", "υ"},
            new[] {"&phi;", "φ"},
            new[] {"&chi;", "χ"},
            new[] {"&psi;", "ψ"},
            new[] {"&omega;", "ω"},
            new[] {"&thetasym;", "ϑ"},
            new[] {"&upsih;", "ϒ"},
            new[] {"&piv;", "ϖ"},
            new[] {"&ensp;", " "},
            new[] {"&emsp;", " "},
            new[] {"&thinsp;", " "},
            new[] {"&zwnj;", "‌"},
            new[] {"&zwj;", "‍"},
            new[] {"&lrm;", "‎"},
            new[] {"&rlm;", "‏"},
            new[] {"&ndash;", "–"},
            new[] {"&mdash;", "—"},
            new[] {"&lsquo;", "‘"},
            new[] {"&rsquo;", "’"},
            new[] {"&sbquo;", "‚"},
            new[] {"&ldquo;", "“"},
            new[] {"&rdquo;", "”"},
            new[] {"&bdquo;", "„"},
            new[] {"&dagger;", "†"},
            new[] {"&Dagger;", "‡"},
            new[] {"&bull;", "•"},
            new[] {"&hellip;", "…"},
            new[] {"&permil;", "‰"},
            new[] {"&prime;", "′"},
            new[] {"&Prime;", "″"},
            new[] {"&lsaquo;", "‹"},
            new[] {"&rsaquo;", "›"},
            new[] {"&oline;", "‾"},
            new[] {"&frasl;", "⁄"},
            new[] {"&euro;", "€"},
            new[] {"&image;", "ℑ"},
            new[] {"&weierp;", "℘"},
            new[] {"&real;", "ℜ"},
            new[] {"&trade;", "™"},
            new[] {"&alefsym;", "ℵ"},
            new[] {"&larr;", "←"},
            new[] {"&uarr;", "↑"},
            new[] {"&rarr;", "→"},
            new[] {"&darr;", "↓"},
            new[] {"&harr;", "↔"},
            new[] {"&crarr;", "↵"},
            new[] {"&lArr;", "⇐"},
            new[] {"&uArr;", "⇑"},
            new[] {"&rArr;", "⇒"},
            new[] {"&dArr;", "⇓"},
            new[] {"&hArr;", "⇔"},
            new[] {"&forall;", "∀"},
            new[] {"&part;", "∂"},
            new[] {"&exist;", "∃"},
            new[] {"&empty;", "∅"},
            new[] {"&nabla;", "∇"},
            new[] {"&isin;", "∈"},
            new[] {"&notin;", "∉"},
            new[] {"&ni;", "∋"},
            new[] {"&prod;", "∏"},
            new[] {"&sum;", "∑"},
            new[] {"&minus;", "−"},
            new[] {"&lowast;", "∗"},
            new[] {"&radic;", "√"},
            new[] {"&prop;", "∝"},
            new[] {"&infin;", "∞"},
            new[] {"&ang;", "∠"},
            new[] {"&and;", "∧"},
            new[] {"&or;", "∨"},
            new[] {"&cap;", "∩"},
            new[] {"&cup;", "∪"},
            new[] {"&int;", "∫"},
            new[] {"&there4;", "∴"},
            new[] {"&sim;", "∼"},
            new[] {"&cong;", "≅"},
            new[] {"&asymp;", "≈"},
            new[] {"&ne;", "≠"},
            new[] {"&equiv;", "≡"},
            new[] {"&le;", "≤"},
            new[] {"&ge;", "≥"},
            new[] {"&sub;", "⊂"},
            new[] {"&sup;", "⊃"},
            new[] {"&nsub;", "⊄"},
            new[] {"&sube;", "⊆"},
            new[] {"&supe;", "⊇"},
            new[] {"&oplus;", "⊕"},
            new[] {"&otimes;", "⊗"},
            new[] {"&perp;", "⊥"},
            new[] {"&sdot;", "⋅"},
            new[] {"&lceil;", "⌈"},
            new[] {"&rceil;", "⌉"},
            new[] {"&lfloor;", "⌊"},
            new[] {"&rfloor;", "⌋"},
            new[] {"&lang;", "〈"},
            new[] {"&rang;", "〉"},
            new[] {"&loz;", "◊"},
            new[] {"&spades;", "♠"},
            new[] {"&clubs;", "♣"},
            new[] {"&hearts;", "♥"},
            new[] {"&diams;", "♦"},
            new[] {"&amp;", "&"}
        };

        #endregion
    }


    public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
    {
        public RequireRequestValueAttribute(string valueName)
        {
            ValueName = valueName;
        }

        public string ValueName { get; private set; }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return (controllerContext.HttpContext.Request[ValueName] != null);
        }
    }


    public static class QrCodeHtmlHelper
    {
        /// <summary>
        /// Produces the markup for an image element that displays a QR Code image, as provided by Google's chart API.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="data">The data to be encoded, as a string.</param>
        /// <param name="size">The square length of the resulting image, in pixels.</param>
        /// <param name="margin">The width of the border that surrounds the image, measured in rows (not pixels).</param>
        /// <param name="errorCorrectionLevel">The amount of error correction to build into the image.  Higher error correction comes at the expense of reduced space for data.</param>
        /// <param name="htmlAttributes">Optional HTML attributes to include on the image element.</param>
        /// <returns></returns>
        public static MvcHtmlString QrCode(this HtmlHelper htmlHelper, string data, int size = 80, int margin = 4, QrCodeErrorCorrectionLevel errorCorrectionLevel = QrCodeErrorCorrectionLevel.Low, object htmlAttributes = null)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (size < 1)
                throw new ArgumentOutOfRangeException("size", size, "Must be greater than zero.");
            if (margin < 0)
                throw new ArgumentOutOfRangeException("margin", margin, "Must be greater than or equal to zero.");
            if (!Enum.IsDefined(typeof(QrCodeErrorCorrectionLevel), errorCorrectionLevel))
                throw new InvalidEnumArgumentException("errorCorrectionLevel", (int)errorCorrectionLevel, typeof(QrCodeErrorCorrectionLevel));

            var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chld={2}|{3}&chs={0}x{0}&chl={1}", size, HttpUtility.UrlEncode(data), errorCorrectionLevel.ToString()[0], margin);

            var tag = new TagBuilder("img");
            if (htmlAttributes != null)
                tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tag.Attributes.Add("src", url);
            tag.Attributes.Add("width", size.ToString(CultureInfo.InvariantCulture));
            tag.Attributes.Add("height", size.ToString(CultureInfo.InvariantCulture));

            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }
    }

    public enum QrCodeErrorCorrectionLevel
    {
        /// <summary>Recovers from up to 7% erroneous data.</summary>
        Low,
        /// <summary>Recovers from up to 15% erroneous data.</summary>
        Medium,
        /// <summary>Recovers from up to 25% erroneous data.</summary>
        QuiteGood,
        /// <summary>Recovers from up to 30% erroneous data.</summary>
        High
    }

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString GenerateRelayQrCode(this HtmlHelper html, string data, int height = 250, int width = 250, int margin = 0)
        {
            
            var barcodeWriter = new BarcodeWriter
            {
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin
                },
                Format = BarcodeFormat.QR_CODE
            };

            using (var bitmap = barcodeWriter.Write(data))
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Gif);

                var img = new TagBuilder("img");
                img.MergeAttribute("alt", "your alt tag");
                img.Attributes.Add("src", String.Format("data:image/gif;base64,{0}",
                    Convert.ToBase64String(stream.ToArray())));

                return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
            }
        }
    }
}