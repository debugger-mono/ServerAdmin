﻿using System;
using System.Web.Mvc;

namespace Tbl.ServerAdmin.Web
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}