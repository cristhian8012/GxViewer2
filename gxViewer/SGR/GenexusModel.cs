using System.Collections.Generic;

namespace Genexus_Definition_HtmlBuilder
{
    /// <summary>Representa el objeto WebUI completo.</summary>
    public class WebUiDefinition
    {
        public string Title { get; set; }
        public string ClassName { get; set; }
        public bool GenerateStart { get; set; }
        public bool GenerateInit { get; set; }
        public bool GenerateMsgReport { get; set; }
        public string GenerateCall { get; set; }
        public FormDefinition Form { get; set; }
        public HiddenData HiddenData { get; set; }
        public string RawXml { get; set; } 

    }

    /*
    public class FormDefinition
    {
        public List<GridDefinition> Grids { get; set; } = new List<GridDefinition>();
    }
    */

    // NUEVO: FormDefinition ahora contiene categorías (y mantiene Grids por compatibilidad)
    public class FormDefinition
    {
        public List<CategoryDefinition> Categories { get; set; } = new List<CategoryDefinition>();
        public List<GridDefinition> Grids { get; set; } = new List<GridDefinition>();
    }

    // NUEVO: Representa cada <category> del XML
    public class CategoryDefinition
    {
        public string Title { get; set; }
        public FormDefinition Form { get; set; }              // El <form> interno de la categoría
        public List<OperationDefinition> Operations { get; set; } = new List<OperationDefinition>();
    }


    public class GridDefinition
    {
        public string Name { get; set; }
        public string OrderBy { get; set; }
        public List<ConditionDefinition> Conditions { get; set; } = new List<ConditionDefinition>();
        public List<FilterDefinition> Filters { get; set; } = new List<FilterDefinition>();
        public List<ColumnDefinition> Columns { get; set; } = new List<ColumnDefinition>();
        public List<ColumnDefinition> HiddenColumns { get; set; } = new List<ColumnDefinition>();
        public List<OperationDefinition> Operations { get; set; } = new List<OperationDefinition>();
    }

    public class ConditionDefinition
    {
        public string Expression { get; set; }
    }

    public class FilterDefinition
    {
        public string Type { get; set; } // "dependency" | "field" | "line-break"
        public string Caption { get; set; }
        public string Determinants { get; set; }
        public string Dependents { get; set; }
        public string Variable { get; set; }
        public PromptDefinition Prompt { get; set; }
    }

    public class PromptDefinition
    {
        public string Object { get; set; }
        public string Params { get; set; }
        public bool AutoRefresh { get; set; }
    }

    public class ColumnDefinition
    {
        public string Title { get; set; }
        public string Data { get; set; }
        public bool IsHidden { get; set; }
    }

    public class OperationDefinition
    {
        public string Caption { get; set; }
        public string Selection { get; set; } // "single" | "none"
        public string VisibleExpression { get; set; } // null = visible
    }

    public class HiddenData
    {
        public List<string> DataNames { get; set; } = new List<string>();
        public bool Auto { get; set; }
    }
}