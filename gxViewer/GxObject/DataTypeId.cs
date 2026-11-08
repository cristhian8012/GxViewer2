using GXBaseKB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static log4net.Appender.ColoredConsoleAppender;

namespace gxViewer.GxObject
{

    public static class HelperDataTypes
    {
        public static string GetNameDataType(string pNmeType )
        {
            try
            {
                int pNumber;
                int.TryParse(pNmeType, out pNumber);
                if (pNumber > 0)
                {
                    return Enum.GetName(typeof(DataTypeId), pNumber);
                }
                else
                {
                    return pNmeType;
                }
                
            }
            catch (Exception ex)
            {
                return pNmeType;
            }
        }

        public static DataTypeId GetDataType(string pNmeType)
        {
            try
            {
                DataTypeId _dataType = DataTypeId.Character;
                int pNumber;
                int.TryParse(pNmeType, out pNumber);
                if (pNumber > 0)
                {
                    _dataType = (DataTypeId)pNumber;
                }
                return _dataType;
            }
            catch (Exception ex)
            {
                return DataTypeId.NULL;
            }
        }

    }


    public enum DataTypeId 
    {
        Numeric = 4,
        Character = 5,
        Date = 6,
        Bitmap = 7,
        LongVarChar = 8,
        DateTime = 12,
        VarChar = 13,
        Blob = 14,
        Boolean = 15,
        WorkflowEvent = 43,
        WorkflowStateChange = 48,
        WorkflowDataChange = 49,
        WorkflowServer = 50,
        WorkflowFilter = 51,
        WorkflowError = 52,
        WorkflowProcessDefinition = 53,
        WorkflowActivity = 54,
        WorkflowWorkitem = 55,
        WorkflowProcessInstance = 56,
        WorkflowCollection = 57,
        WorkflowApplicationData = 58,
        WorkflowWebApplication = 59,
        WorkflowUser = 60,
        WorkflowRole = 61,
        WorkflowOrganizationalModel = 62,
        WorkflowRestriction = 63,
        WorkflowRestrictionDefinition = 64,
        File = 65,
        Directory = 67,
        LdapClient = 69,
        QueueMessage = 71,
        Queue = 72,
        WorkflowAssignmentChangeEvent = 76,
        WorkflowPriorityChangeEvent = 77,
        WorkflowEventRepository = 78,
        WorkflowObject = 79,
        WorkflowAttribute = 80,
        WorkflowDocumentRepository = 81,
        WorkflowDocumentDefinition = 82,
        WorkflowDocumentInstance = 83,
        WorkflowActionPerformedEvent = 84,
        SDT = 254,
        UserDefinedType = byte.MaxValue,
        NULL = 0

    }

    public enum WebFormEditType
    {
        ComboBox = 0,
        RadioButton = 1,
        EditBox = 2,
        CheckBox = 3,
        ListBox = 6
    }

}
