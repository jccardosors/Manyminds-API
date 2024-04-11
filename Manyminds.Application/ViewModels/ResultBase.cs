using System.Net;

namespace Manyminds.Application.ViewModels
{
    public class ResultBase
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public bool Success
        {
            get { return this.Status == (int)HttpStatusCode.OK ? true : false; }
        }
    }
}
