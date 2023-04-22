using SE_PoliceInspectorate.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_PoliceInspectorate.DataAccess.EF
{
    // Search service class
    public class CriminalFilesSearchService
    {
        private readonly CriminalFilesRepository _repository;

        public CriminalFilesSearchService(CriminalFilesRepository repository)
        {
            _repository = repository;
        }

        public List<Criminal> Search(string searchString)
        {
            // Call the repository's search method to get a queryable of all criminal files that match the search string
            var queryable = _repository.Search(searchString);

            // Convert the queryable to a list
            var matchingFiles = queryable.ToList();

            return matchingFiles;
        }
    }

}
