namespace gxViewer.GxDebugger.Helpers
{
    using System;
    using System.Collections.Generic;

    public static class MockDb
    {
        public static List<Dictionary<string, object>> SimularConsulta(string sql)
        {
            Console.WriteLine($"[Simulador SQL] → {sql}");
            return new List<Dictionary<string, object>>
            {
                //new() { ["Pgcod"] = "001", ["Pgfape"] = "2025-01-01", ["Pgidio"] = "ES", ["Pgnom"] = "Nom A" },
                //new() { ["Pgcod"] = "002", ["Pgfape"] = "2025-06-01", ["Pgidio"] = "EN", ["Pgnom"] = "Nom B" }
            };
        }
    }
}
