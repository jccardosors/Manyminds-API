namespace Manyminds.Application.ViewModels.Response.RegistroLogs
{
    public class RetornarListaLogsResponse : ResultBase
    {
        public IEnumerable<RegistroLogsVM> Data { get; set; }
    }
}
