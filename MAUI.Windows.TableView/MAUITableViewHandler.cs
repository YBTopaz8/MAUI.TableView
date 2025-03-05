using Microsoft.Maui.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YB.MAUITableView;


public static class TableViewHandler
{
    public static void ConfigureTableViewHandler(IMauiHandlersCollection handlers)
    {
#if WINDOWS
        //handlers.AddHandler(typeof(MyTableView), typeof(MyTableViewHandler));
#endif
    }
}