using PageViewInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PageViewInfo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(DateTime? beginDate, DateTime? endDate)
        {
            PageViewInfoModel model = new PageViewInfoModel();
            List<string> lines = new List<string>();
            string filePath = @"C:\Users\emrea\OneDrive\Masaüstü\documents\data.txt";

            lines = System.IO.File.ReadAllLines(filePath).ToList();
            foreach (var line in lines)
            {
                Data newData = new Data();
                string[] entries = line.Split(new Char[] { ' ', '\n', '\t' });
                newData.Url = entries[0];
                newData.Browser = entries[1];
                newData.Country = entries[2];
                newData.Date = Convert.ToDateTime(entries[3]);
                model.DataList.Add(newData);
            }

            model.TotalView = model.DataList.Count();
            var groupByUrl= from Data in model.DataList orderby Data.Url group Data by Data.Url into groupedData select new TableData{ Label = groupedData.Key, Count = groupedData.Count() };
            model.CountryViewList = groupByUrl.ToList();

            var groupByCountry = from Data in model.DataList orderby Data.Country group Data by Data.Country into groupedData select new TableData { Label = groupedData.Key, Count = groupedData.Count() };
            var mostViewedCountryData = groupByCountry.OrderByDescending(x => x.Count).FirstOrDefault();
            model.MostViewedCountry = mostViewedCountryData.Label;


            var groupByDate = from Data in model.DataList orderby Data.Date group Data by Data.Date into date select new TableDateData { Date = date.Key, Count = date.Count() };
            if (beginDate.HasValue && !endDate.HasValue)
            {
                groupByDate.Where(x => x.Date > beginDate);
            }
            else if (!beginDate.HasValue && endDate.HasValue)
            {
                groupByDate.Where(x => x.Date < endDate);
            }
            else if (beginDate.HasValue && endDate.HasValue)
            {
                groupByDate.Where(x => x.Date < endDate && x.Date > beginDate);
            }
            model.DateViewList = groupByDate.ToList();  
            return View(model);
        }

    }
}