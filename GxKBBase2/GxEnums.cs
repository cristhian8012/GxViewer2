
namespace GxKBBase18
{
    public enum AttributeClass
    {
        Primary = 1,
        Secondary = 2,
        Inferred = 3,
        FormulaVertical = 4,
        FormulaHorizontal = 5
    }

    public enum ControlType
    {
        ComboBox = 0,
        RadioButton = 1,
        Edit = 2,
        CheckBox = 3,
        DynamicComboBox = 4,
        ListBox = 6,
        DynamicListBox = 7
    }

    public enum DataType
    {
        Numeric = 4,
        Character = 5,
        Date = 6,
        LongVarchar = 8,
        DateTime = 12,
        Varchar = 13,
        Blob = 14
    }


    public enum EditInputType
    {
        Values,
        Descriptions
    }

    public enum EditSuggestType
    {
        None,
        OnRequest,
        Incremental
    }

    public enum GXClass
    {
        Attribute = 20,
        Table = 34,
        DataView = 24,
        Domain = 26,
        Language = 30,
        Procedure = 31,
        Report = 32,
        SDT = 33,
        Theme = 35,
        Transaction = 36,
        Menu = 37,
        Menubar = 38,
        WebPanel = 40,
        WorkPanel = 43,
        API = 48,
        WebForm = 95
    }

    //Transaction = 0,
    //Procedure = 1,
    //Report = 2,
    //Menu = 3,
    //WorkPanel = 4,
    //Attribute = 5,
    //DataView = 6,
    //Table = 7,
    //Folder = 8,
    //Model = 9,
    //SubtypeGroup = 10,
    //Domain = 11,
    //Prompt = 12,
    //WebPanel = 13,
    //ExternalProgram = 14,
    //Module = 15,
    //MenuBar = 0x11,
    //StyleTransaction = 0x12,
    //StyleProcedure = 0x13,
    //StyleReport = 20,
    //StyleWorkPanel = 0x15,
    //StylePrompt = 0x16,
    //StyleWebPanel = 0x17,
    //StyleMenuBar = 0x18,
    //Theme = 0x19,
    //SDT = 0x1a,
    //Language = 0x1c

    internal enum KnowledgeBaseType
    {
        Full = 0,
        Trial = 1,
        Unspecified = -1
    }


}
