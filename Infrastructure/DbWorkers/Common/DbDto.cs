using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbWorkers.Common
{
    public class DbDto
    {
        private List<List<string>> response { get; set; } = new List<List<string>>();

        public void Add(List<string> inputList)
        {
            response.Add(inputList);
        }

        public List<List<string>> Get()
        {
            return response;
        }
    }
}
