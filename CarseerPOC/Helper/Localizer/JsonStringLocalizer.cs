using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace CarseerPOC.Helper
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly IDictionary<string, string> _resources;
        private readonly JsonSerializer _serlizer = new JsonSerializer();
        public JsonStringLocalizer(IDictionary<string, string> resources)
        {
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
        }

        public LocalizedString this[string name] => new LocalizedString(name, GetString(name));

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                string format = GetString(name);
                return new LocalizedString(name, string.Format(format, arguments));
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private string GetValueFromJson(string value, string filepath)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(filepath))
                return string.Empty;
            using FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader reader = new StreamReader(stream);
            using JsonTextReader textReader = new JsonTextReader(reader);
            while (textReader.Read())
            {
                if (textReader.TokenType == JsonToken.PropertyName && textReader.Value as string == value)
                {
                    textReader.Read();
                    return _serlizer.Deserialize<string>(textReader);
                }
            }
            return string.Empty;

        }
        private string GetString(string name)
        {
            var fullFilePath = Path.GetFullPath($"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json");
            if (File.Exists(fullFilePath))
            {
                var result = GetValueFromJson(name, fullFilePath);
                return result;
            }
            return string.Empty;
        }
    }

}