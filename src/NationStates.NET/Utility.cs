namespace NationStates.NET
{
    using HtmlAgilityPack;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Xml;

    /// <summary>
    /// A class containing useful methods to parse the API. It cannot be used in other assemblies (for obvious reasons).
    /// </summary>
    internal class Utility
    {
        private static readonly Dictionary<char, Authority> AuthorityDict = new()
        {
            { 'X', Authority.Executive },
            { 'W', Authority.World_Assembly },
            { 'A', Authority.Appearance },
            { 'B', Authority.Border_Control },
            { 'C', Authority.Communications },
            { 'E', Authority.Embassies },
            { 'P', Authority.Polls },
        };

        private static readonly Dictionary<string, string> Zeroes = new()
        {
            { "thousand", string.Empty },
            { "million", "000" },
            { "billion", "000000" },
            { "trillion", "000000000" },
        };

        private static readonly List<char> Commands = new()
        {
            '+',
            '-',
            '/',
        };

        /// <summary>
        /// Add "0" to the front of a number if it is single digit.
        /// </summary>
        /// <param name="number">The number to manipulate.</param>
        /// <returns>A string of the manipulated number.</returns>
        public static string AddZeroIfSingleDigitNumber(int number)
        {
            if (number.ToString().Length == 1)
            {
                return $"0{number}";
            }

            return number.ToString();
        }

        /// <summary>
        /// Capitalises a string.
        /// </summary>
        /// <param name="text">The string to capitalise.</param>
        /// <returns>The capitalised string.</returns>
        public static string Capitalise(string text)
        {
            string firstChar = char.ToUpper(text[0]) + text.ToLower()[1..];

            StringBuilder sb = new(firstChar);

            for (int i = 0; i < sb.Length - 1; i++)
            {
                if (sb[i] == '-' || sb[i] == ' ')
                {
                    sb[i + 1] = char.ToUpper(sb[i + 1]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Check if the sub-category matches the main category.
        /// </summary>
        /// <param name="category">The category to compare to.</param>
        /// <param name="subCategory">The sub-category to compare.</param>
        public static void CheckCategoryAndSubCategory(DispatchCategory category, Enum subCategory)
        {
            switch (category)
            {
                case DispatchCategory.Account:
                    if (!Enum.IsDefined(typeof(DispatchAccount), subCategory))
                    {
                        throw new NSError("Sub-category type must be DispatchAccount.");
                    }

                    break;

                case DispatchCategory.Bulletin:
                    if (!Enum.IsDefined(typeof(DispatchBulletin), subCategory))
                    {
                        throw new NSError("Sub-category type must be DispatchBulletin.");
                    }

                    break;

                case DispatchCategory.Factbook:
                    if (!Enum.IsDefined(typeof(DispatchFactbook), subCategory))
                    {
                        throw new NSError("Sub-category type must be DispatchFactbook.");
                    }

                    break;

                case DispatchCategory.Meta:
                    if (!Enum.IsDefined(typeof(DispatchMeta), subCategory))
                    {
                        throw new NSError("Sub-category type must be DispatchMeta.");
                    }

                    break;
            }
        }

        /// <summary>
        /// Converts a <see cref="DateTime"/> object to its UNIX timestamp.
        /// </summary>
        /// <param name="dateTime">The object to convert.</param>
        /// <returns>The <see cref="DateTime"/> as a UNIX timestamp.</returns>
        public static long ConvertToUnix(DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }

        /// <summary>
        /// Formats a string for enum parsing.
        /// </summary>
        /// <param name="text">The string to format.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatForEnum(string text)
        {
            return text.Replace(" - ", string.Empty).Replace("-", string.Empty).Replace(": ", string.Empty).Trim().Replace(" ", "_");
        }

        /// <summary>
        /// Gets the sub-category's main category.
        /// </summary>
        /// <param name="subCategory">The sub-category to get the main category from.</param>
        /// <returns>The type of the main category.</returns>
        public static Type GetDispatchCategoryFromSubCategory(Enum subCategory)
        {
            List<Type> cases = new()
            {
                typeof(DispatchAccount),
                typeof(DispatchBulletin),
                typeof(DispatchFactbook),
                typeof(DispatchMeta),
            };

            foreach (Type t in cases)
            {
                try
                {
                    if (Enum.IsDefined(t, subCategory))
                    {
                        return t;
                    }
                }
                catch
                {
                }
            }

            throw new NSError("Unexpected sub-category");
        }

        /// <summary>
        /// Gets a WebClient request.
        /// </summary>
        /// <param name="headers">A list of optional additional headers.</param>
        /// <param name="delay">The number of milliseconds to sleep. By default it is 600 milliseconds to comply with the 50 requests/30 seconds rule.</param>
        /// <returns>The WebClient.</returns>
        public static WebClient GetClient(Dictionary<string, string>? headers = null, int delay = 600)
        {
            using WebClient client = new();
            client.Headers.Add("user-agent", "NationStates.NET (https://github.com/asdia0/NationStates.NET)");

            if (headers != null)
            {
                foreach (string key in headers.Keys)
                {
                    client.Headers.Add(key, headers[key]);
                }
            }

            Thread.Sleep(delay);
            return client;
        }

        /// <summary>
        /// Downloads a webpage.
        /// </summary>
        /// <param name="path">The path to the webpage.</param>
        /// <param name="headers">A list of optional additional headers.</param>
        /// <param name="delay">The number of milliseconds to sleep. By default it is 600 milliseconds to comply with the 50 requests/30 seconds rule.</param>
        /// <returns>The webpage contents.</returns>
        public static string DownloadPage(string path, Dictionary<string, string>? headers = null, int delay = 600)
        {
            return GetClient(headers, delay).DownloadString(path);
        }

        /// <summary>
        /// Gets a collection of headers from a request response.
        /// </summary>
        /// <param name="path">The path to the webpage.</param>
        /// <param name="headers">A list of optional additional headers.</param>
        /// <param name="delay">The number of milliseconds to sleep. By default it is 600 milliseconds to comply with the 50 requests/30 seconds rule.</param>
        /// <returns>The response headers.</returns>
        public static WebHeaderCollection GetResponseHeaders(string path, Dictionary<string, string>? headers = null, int delay = 600)
        {
            WebClient client = GetClient(headers, delay);
            client.OpenRead("https://www.nationstates.net/cgi-bin/api.cgi?" + path + "&v=11");
            return client.ResponseHeaders;
        }

        /// <summary>
        /// Gets the nation's name from its DBID.
        /// </summary>
        /// <param name="id">The nation's ID.</param>
        /// <returns>The nation's name.</returns>
        public static string NationNameFromID(long id)
        {
            return ParseXMLDocument($"q=cards+info;nationid={id}")
                .SelectSingleNode("/CARDS/INFO/NAME")
                .InnerText;
        }

        /// <summary>
        /// Parses a string for <see cref="Authority"/>s.
        /// </summary>
        /// <param name="authorities">The string to parse.</param>
        /// <returns>A collection of <see cref="Authority"/>s.</returns>
        public static HashSet<Authority> ParseAuthority(string authorities)
        {
            HashSet<Authority> res = new();

            foreach (char c in authorities)
            {
                res.Add(AuthorityDict[c]);
            }

            return res;
        }

        /// <summary>
        /// Parses an enum from text.
        /// </summary>
        /// <param name="type">The type of enum to parse to.</param>
        /// <param name="text">The enum in words.</param>
        /// <returns>An enum.</returns>
        public static dynamic ParseEnum(Type type, string text)
        {
            if (text.StartsWith("FT"))
            {
                return Enum.Parse(type, FormatForEnum(text));
            }

            return Enum.Parse(type, FormatForEnum(Capitalise(text)));
        }

        /// <summary>
        /// Parses <see cref="Event"/>s from a <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="events">The parent <see cref="XmlNode"/>.</param>
        /// <returns>A collection of <see cref="Event"/>s.</returns>
        public static HashSet<Event> ParseEvents(XmlNode events)
        {
            HashSet<Event> res = new();

            foreach (XmlNode ev in events.ChildNodes)
            {
                DateTime timestamp = ParseUnix(ev.SelectSingleNode("TIMESTAMP").InnerText);
                string text = ev.SelectSingleNode("TEXT").InnerText;

                res.Add(new Event(timestamp, text));
            }

            return res;
        }

        /// <summary>
        /// Parses an HTML document from a webpage.
        /// </summary>
        /// <param name="path">The webpage to parse the HTML document from.</param>
        /// <param name="pin">The nation's PIN.</param>
        /// <returns>An <see cref="HtmlNode"/>.</returns>
        public static HtmlNode ParseHTMLDocument(string path, string pin = null)
        {
            Dictionary<string, string> headers = new();
            if (pin != null)
            {
                headers.Add("X-Pin", pin);
            }

            string html = DownloadPage(path, headers);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc.DocumentNode;
        }

        /// <summary>
        /// Parses a string for <see cref="DateTime"/>.
        /// </summary>
        /// <param name="unix">The string to parse.</param>
        /// <returns>A <see cref="DateTime"/>.</returns>
        public static DateTime ParseUnix(string unix)
        {
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(unix)).DateTime;
        }

        /// <summary>
        /// Parses <see cref="UNCategory"/>.
        /// </summary>
        /// <param name="option">The option node.</param>
        /// <param name="category">The category of the proposal/resolution.</param>
        /// <returns>A sub-category.</returns>
        public static dynamic ParseUNSubCategory(HtmlNode option, dynamic category)
        {
            string formatted = option.InnerText.Replace(option.SelectSingleNode(".//span[@class='WA_leader']").InnerText, string.Empty);

            if (category is UNCategory)
            {
                return category switch
                {
                    UNCategory.Repeal => long.Parse(formatted.Trim().Replace("UN#", string.Empty)),
                    UNCategory.Bookkeeping => GABookeeping.Sweeping,
                    UNCategory.Regulation => (GARegulation)ParseEnum(typeof(GARegulation), formatted),
                    UNCategory.Health => (GAHealth)ParseEnum(typeof(GAHealth), formatted),
                    UNCategory.Environmental => (GAEnvironmental)ParseEnum(typeof(GAEnvironmental), formatted),
                    UNCategory.Education_And_Creativity => (GAEducationAndCreativity)ParseEnum(typeof(GAEducationAndCreativity), formatted),
                    UNCategory.Advancement_Of_Industry => (GAAdvancementOfIndustry)ParseEnum(typeof(GAAdvancementOfIndustry), formatted),
                    UNCategory.Recreational_Drug_Use => (UNRecreationalDrugUse)ParseEnum(typeof(UNRecreationalDrugUse), formatted),
                    _ => (GAStrength)ParseEnum(typeof(GAStrength), formatted),
                };
            }
            else
            {
                throw new NSError("Invalid category type.");
            }
        }

        /// <summary>
        /// Parses <see cref="GACategory"/> or <see cref="SCCategory"/>.
        /// </summary>
        /// <param name="option">The option node.</param>
        /// <param name="category">The category of the proposal/resolution.</param>
        /// <returns>A sub-category.</returns>
        public static dynamic ParseWASubCategory(XmlNode option, dynamic category)
        {
            if (category is GACategory)
            {
                return category switch
                {
                    GACategory.Repeal => long.Parse(option.InnerText),
                    GACategory.Bookkeeping => GABookeeping.Sweeping,
                    GACategory.Regulation => (GARegulation)ParseEnum(typeof(GARegulation), option.InnerText),
                    GACategory.Health => (GAHealth)ParseEnum(typeof(GAHealth), option.InnerText),
                    GACategory.Environmental => (GAEnvironmental)ParseEnum(typeof(GAEnvironmental), option.InnerText),
                    GACategory.Education_And_Creativity => (GAEducationAndCreativity)ParseEnum(typeof(GAEducationAndCreativity), option.InnerText),
                    GACategory.Advancement_Of_Industry => (GAAdvancementOfIndustry)ParseEnum(typeof(GAAdvancementOfIndustry), option.InnerText),
                    _ => (GAStrength)ParseEnum(typeof(GAStrength), option.InnerText),
                };
            }
            else if (category is SCCategory)
            {
                if (option.InnerText.StartsWith("N:"))
                {
                    return (Entity.Nation, option.InnerText.Replace("N:", string.Empty));
                }

                if (option.InnerText.StartsWith("R:"))
                {
                    return (Entity.Region, option.InnerText.Replace("R:", string.Empty));
                }
                else
                {
                    // Identical to RepealedID
                    return long.Parse(option.InnerText);
                }
            }
            else
            {
                throw new NSError("Invalid category type.");
            }
        }

        /// <summary>
        /// Parses a string of a written standard notation (e.g. "1.39 trillion").
        /// </summary>
        /// <param name="number">The written standard notation.</param>
        /// <returns>A <see cref="long"/>.</returns>
        public static long ParseWrittenStandardNotation(string number)
        {
            double numberPart = double.Parse(number.Split(" ")[0]);
            string zeroes = Zeroes[number.Split(" ")[1]];

            return long.Parse((numberPart * 1000).ToString() + zeroes);
        }

        /// <summary>
        /// Parses an XML document from a webpage.
        /// </summary>
        /// <param name="path">The webpage to parse the XML document from.</param>
        /// <param name="pin">The nation's PIN.</param>
        /// <returns>An <see cref="XmlDocument"/>.</returns>
        public static XmlElement ParseXMLDocument(string path, string pin = null)
        {
            Dictionary<string, string> headers = new();
            if (pin != null)
            {
                headers.Add("X-Pin", pin);
            }

            XmlDocument doc = new();
            doc.LoadXml(DownloadPage("https://www.nationstates.net/cgi-bin/api.cgi?" + path + "&v=11", headers));
            return doc.DocumentElement;
        }

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>A JSON string of the object.</returns>
        public static string Serialize(object? value)
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            jsonSerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            return JsonConvert.SerializeObject(value, jsonSerializerSettings);
        }

        /// <summary>
        /// Parses a primitive in a <see href="https://github.com/auralia/node-nstg/blob/master/trl.md">Telegram Recipient Language</see> string.
        /// </summary>
        /// <param name="trl">The TRL primitive string to parse.</param>
        /// <returns>A list of the names of the nations in the TRL primitive.</returns>
        public static HashSet<string> ParseTRLPrimitive(string trl)
        {
            HashSet<string> nations = new();

            string primitive = trl.Split("[")[0].Trim();

            if (Commands.Contains(primitive[0]))
            {
                primitive = primitive.Substring(1);
            }

            int openIndex = trl.IndexOf("[");
            List<string> arguments = trl[(openIndex + 1)..trl.LastIndexOf("]")].Split(",").ToList();

            switch (primitive)
            {
                case "nations":
                    foreach (string nation in arguments)
                    {
                        Nation n = new(nation);
                        nations.Add(NationNameFromID(n.DBID));
                    }

                    break;
                case "regions":
                    foreach (string region in arguments)
                    {
                        Region r = new(region);
                        nations.UnionWith(r.Nations);
                    }

                    break;
                case "tags":
                    foreach (string tag in arguments)
                    {
                        foreach (string regionTag in World.RegionsByTags(arguments.Select(i => (RegionTag)ParseEnum(typeof(RegionTag), i)).ToHashSet(), null))
                        {
                            Region rTag = new(regionTag);
                            nations.UnionWith(rTag.Nations);
                        }
                    }

                    break;
                case "wa":
                    if (arguments[0] == "members")
                    {
                        nations.UnionWith(WorldAssembly.Delegates);
                        nations.UnionWith(WorldAssembly.Members);
                    }
                    else if (arguments[0] == "delegates")
                    {
                        nations.UnionWith(WorldAssembly.Delegates);
                    }
                    else
                    {
                        throw new NSError("Invalid WA type in TRL primitive.");
                    }

                    break;
                case "new":
                    int newLimit = int.Parse(arguments[0]);
                    nations.UnionWith(World.NewNations(newLimit));
                    break;
                case "refounded":
                    int reLimit = int.Parse(arguments[0]);
                    nations.UnionWith(World.NewRefoundedNations(reLimit));
                    break;
            }

            return nations;
        }

        /// <summary>
        /// Parses a group in a <see href="https://github.com/auralia/node-nstg/blob/master/trl.md">Telegram Recipient Language</see> string.
        /// </summary>
        /// <param name="trl">The TRL group string to parse.</param>
        /// <returns>A list of the names of the nations in the TRL group.</returns>
        public static HashSet<string> ParseTRLGroup(string trl)
        {
            HashSet<string> nations = new();

            // Go through trl and get sequence.
            List<string> trlSequence = new();

            int indent = 0;

            int matchingBracket = 0;

            List<char> charArr = new();

            char cmd = ' ';

            for (int i = 0; i < trl.Length; i++)
            {
                char c = trl[i];

                if (c == '(')
                {
                    indent++;

                    if (indent == 2)
                    {
                        matchingBracket = i;
                    }
                }
                else if (c == ')')
                {
                    if (charArr.Count == 1)
                    {
                        cmd = charArr.First();
                        charArr.Clear();
                    }
                    else if (charArr.Count > 0)
                    {
                        trlSequence.AddRange(new string(charArr.ToArray()).Split("; "));
                        charArr.Clear();
                    }

                    indent--;

                    if (indent == 1)
                    {
                        trlSequence.Add($"{(cmd != ' ' ? cmd : string.Empty)}" + trl.Substring(matchingBracket, i - matchingBracket + 1) + ";");
                        cmd = ' ';

                        // Compensate for the extra ";".
                        i++;
                    }
                }
                else if (indent == 1)
                {
                    charArr.Add(c);
                }
            }

            trlSequence.RemoveAll(i => i == string.Empty);

            foreach (string sequence in trlSequence)
            {
                char command = sequence[0];

                if (!Commands.Contains(command))
                {
                    command = '+';
                }

                if (sequence.StartsWith("("))
                {
                    nations = ParseTRLCommand(command, nations, ParseTRLGroup(sequence));
                }
                else
                {
                    nations = ParseTRLCommand(command, nations, ParseTRLPrimitive(sequence));
                }
            }

            return nations.ToHashSet();
        }

        /// <summary>
        /// Parses a command in a <see href="https://github.com/auralia/node-nstg/blob/master/trl.md">Telegram Recipient Language</see> string.
        /// </summary>
        /// <param name="command">The command (`+`, `-`, `/`).</param>
        /// <param name="prim1">The first primitive.</param>
        /// <param name="prim2">The second primitive.</param>
        /// <returns>A list of the names of the nations from the TRL command.</returns>
        public static HashSet<string> ParseTRLCommand(char command, HashSet<string> prim1, HashSet<string> prim2)
        {
            switch (command)
            {
                case '+':
                    return prim1.Union(prim2).ToHashSet();
                case '-':
                    return prim1.Except(prim2).ToHashSet();
                case '/':
                    return prim1.Intersect(prim2).ToHashSet();
                default:
                    throw new NSError("Invalid TRL command.");
            }
        }
    }
}
