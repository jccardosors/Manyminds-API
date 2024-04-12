using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manyminds.Application.ViewModels.Response.Fornecedor
{
    public class RetornarLista_Response: ResultBase
    {
        public IEnumerable<FornecedorVM> Data { get; set; }
    }
}
