using CarseerPOC.DomainClasses;
using CarseerPOC.Helper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace CarseerPOC.Repo
{
    public class CarsDetailsRepo : ICarsDetailsRepo
    {
        #region Const
        private readonly IConfiguration _IConfiguration;
        public CarsDetailsRepo(IConfiguration configuration)
        {
            _IConfiguration = configuration;
        }
        #endregion

        #region GetMakeId
        public long GetMakeId(string MakeName)
        {
            if (!string.IsNullOrEmpty(MakeName))
            {

                var csvFilePath = AppDomain.CurrentDomain.BaseDirectory + _IConfiguration["csvFilePath"];
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var records = csv.GetRecords<CarMake>();
                    foreach (var record in records)
                    {
                        if (string.Equals(record.make_name, MakeName, StringComparison.OrdinalIgnoreCase))
                            return record.make_id;
                    }
                }
            }
            return 0;
        }
        #endregion

        #region GetCarModels
        public async Task<CarsModels> GetCarModels(long MakeId, long ModelYear)
        {
            if (MakeId != 0 && ModelYear != 0)
            {
                string _GetModelsForMakeIdYearURL = string.Format(_IConfiguration["GetModelsForMakeIdYearURL"], MakeId, ModelYear);
                var _GetModelsTasks = await ManageAPI.GetURI(new Uri(_GetModelsForMakeIdYearURL));
                if (!string.IsNullOrEmpty(_GetModelsTasks))
                {
                    var _GetModelsDeserialize = JsonConvert.DeserializeObject<CarsModels>(_GetModelsTasks);
                    return _GetModelsDeserialize;
                }
            }
            return null;
        }
        #endregion
    }
}
