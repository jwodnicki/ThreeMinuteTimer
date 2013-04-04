using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ThreeMinuteTimer
{
	class Fortune
	{
		private List<string> fortunes;

		public Fortune()
		{
			string s = "";
			string t;
			fortunes = new List<string>();
			Assembly ass = this.GetType().Assembly;
			using (Stream file = ass.GetManifestResourceStream("ThreeMinuteTimer.Resources.platitudes"))
			{
				using (TextReader reader = new StreamReader(file))
				{
					while ((t = reader.ReadLine()) != null)
					{
						if (t == "%")
						{
							fortunes.Add(s);
							s = "";
						}
						else
						{
							s += t;
						}
					}
				}
			}
		}

		public string GetRandom()
		{
			return fortunes[new Random().Next(fortunes.Count)];
		}
	}
}
