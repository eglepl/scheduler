using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Helpers
{
    public class EnumExporterHelper
    {
        public EnumExporterHelper()
        {
            
        }

        //public void ExportEnum<T>()
        //{
        //    var type = typeof(T);
        //    var values = Enum.GetValues(type).Cast<T>();
        //    var dict = values.ToDictionary(e => e.ToString(), e => Convert.ToInt32(e));
        //    var json = new JavaScriptSerializer().Serialize(dict);
        //    var script = string.Format("{0}={1};", type.Name, json);
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "CloseLightbox", script, true);
        //}
    }
}
