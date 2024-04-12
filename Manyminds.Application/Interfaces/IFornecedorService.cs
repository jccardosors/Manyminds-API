using Manyminds.Application.ViewModels.Response.Fornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manyminds.Application.Interfaces
{
    public interface IFornecedorService: IDisposable
    {
        Task<RetornarLista_Response> RetornarLista();
    }
}
