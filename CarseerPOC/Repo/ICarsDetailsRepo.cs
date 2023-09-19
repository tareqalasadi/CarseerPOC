using CarseerPOC.DomainClasses;
using System.Threading.Tasks;

namespace CarseerPOC.Repo
{
    public interface ICarsDetailsRepo
    {
        long GetMakeId(string make);
        Task<CarsModels> GetCarModels(long makeId, long modelyear);

    }
}
