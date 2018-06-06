using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MortgageCalculator.Api.Services;
using MortgageCalculator.Api.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace MortgageCalculator.Api.Controllers
{
    public class MortgageController : ApiController
    {

        private readonly IMortgageService mortgageService;

        
        [ExcludeFromCodeCoverage]
        public MortgageController()
        {
            mortgageService = new MortgageService();
        }

        public MortgageController(IMortgageService mortgageServiceParam)
        {
            mortgageService = mortgageServiceParam;
        }

        // GET: api/Mortgage
        [WebApiOutputCache(Duration = 1440)] 
        public IEnumerable<Dto.Mortgage> Get()
        {
           var mortgageService = new MortgageService();
            return mortgageService.GetAllMortgages();
        }

        // GET: api/Mortgage/5
     
      
        //public Dto.Mortgage Get(int id)
        //{
        //    var mortgageService = new MortgageService();
        //    return mortgageService.GetAllMortgages().FirstOrDefault(x => x.MortgageId == id);
        //}
    }
}
