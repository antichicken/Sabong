using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sabong.Repository.Repo;

public partial class Calendar : System.Web.UI.Page
{
    private CalendarRepository _repository = new CalendarRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public string CalendarData
    {
        get
        {
            var data = _repository.GetAll();
            var x =Newtonsoft.Json.JsonConvert.SerializeObject(data.Select(i => new
            {
                title = i.description,
                start = i.date.ToString("yyyy-MM-dd")
            }));
            return x;
        }
    }
}