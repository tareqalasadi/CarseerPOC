using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;

namespace CarseerPOC.Helper
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IDictionary<string, string> _resources;

        public JsonStringLocalizerFactory(IDictionary<string, string> resources)
        {
            _resources = resources;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer(_resources);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalizer(_resources);
        }
    }

}