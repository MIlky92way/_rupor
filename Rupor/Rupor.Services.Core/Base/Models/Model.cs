using System.Web;

namespace Rupor.Services.Core.Base.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            CountOnPage = 30;
            Page = 1;
            FieldOrderBy = "Id";
            IsAscending = true;
            IsDelete = false;
            IsActive = true;
            InitSortingData();
        }

        public int Total
        {
            get; set;
        }

        public int? Id { get; set; }

        public bool IsDelete { get; set; }

        public bool IsActive { get; set; }

        public int Items { get; set; }

        public int Page { get; set; }

        public string FieldOrderBy { get; set; }

        public bool IsAscending { get; set; }

        public int CountOnPage { get; set; }


        #region DataTable

        public string[] сolumnName { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        public int draw { get; set; }


        private void InitSortingData()
        {
            var httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                if (httpContext.Request.Params["order[0][column]"] != null && httpContext.Request.Params["order[0][dir]"] != null)
                {
                    int indexColumn = -1;
                    int.TryParse(httpContext.Request.Params["order[0][column]"], out indexColumn);
                    if (indexColumn > -1 && сolumnName != null && сolumnName.Length > indexColumn
                        && !string.IsNullOrEmpty(сolumnName[indexColumn]))
                    {
                        FieldOrderBy = сolumnName[indexColumn];
                        IsAscending = httpContext.Request.Params["order[0][dir]"] != null && httpContext.Request.Params["order[0][dir]"] == "asc" ? true : false;
                    }

                }

                if (length != 0)
                {
                    Page = (length + start) / length;
                    CountOnPage = length;
                }
                else
                {
                    Page = 1;
                    CountOnPage = 100;
                }

            }
        }

        #endregion

    }
}
