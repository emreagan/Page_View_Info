using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageViewInfo.Models
{
    public class PageViewInfoModel
    {
        public PageViewInfoModel()
        {
            DataList = new List<Data>();
        }

        public List<Data> DataList { get; set; }
        public List<TableData> CountryViewList { get; set; }
        public List<TableDateData> DateViewList { get; set; }
        public string MostViewedCountry { get; set; }
        public int TotalView { get; set; }

    }
    public class Data
    {
        public string Url { get; set; }
        public string Browser { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
    }

    public class TableData
    {
        public int Count { get; set; }
        public string Label { get; set; }
    }

    public class TableDateData
    {
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }
}