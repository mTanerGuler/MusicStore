using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlakDukkani.BLL.Concrete.ResultServiceBLL
{
    public class ResultService<T>
    {
        public ResultService()
        {
            Errors = new List<ErrorItem>();
        }
        public bool HasError { get; set; } //Hata var mı?
        public List<ErrorItem> Errors { get; set; } //Error listesi
        public T Data { get; set; } //T'ye göre veriler. //Örn. T=List<SingleAlbumsVM>

        public void AddError(string errorType, string errorMessage) //Gelen hataya göre ErrorItem nesnesi oluşturup propertylerini dolduran ve bunu Hata listesine(Errors) atan metot
        {
            Errors.Add(new ErrorItem { ErrorType = errorType, ErrorMessage = errorMessage });
            HasError = true;
        }
    }

    public class ErrorItem //Hatanın tipini ve mesajını tek bir classta topladık
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}