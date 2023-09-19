using System.Collections.Generic;

namespace CarseerPOC.DomainClasses
{
    public class CarsModelsResponse
    {
        public string Make { get; set; }
        public long Year { get; set; }
        public List<Result> Models { get; set; }
    }
    public class Result
    {
        public string Make_Name { get; set; }
        public string Model_Name { get; set; }
    }
    public class CarMake
    {
        public string make_name { get; set; }
        public long make_id { get; set; }
    }
    public class CarsModels
    {
        public long Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public List<Result> Results { get; set; }
    }

}
