using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace GxKBBase18
{
    public static class GXDB
    {
        public static DBOFactory dboFactory;
        public static int ChekModels()
        {
            string _sql = "SELECT Count(*) FROM UdmModel ";
            object _ret = DBOQuerys.ReadScalar(dboFactory.GetCurrentConnection(), _sql);
            return Convert.ToInt32(_ret);
        }

        public static DataTable getModels()
        {
            string _sql = @"
                        select um.ModelId, um.ModelName, mm.model_name, mm.model_path, mm.kbase_version
                          from UdmModel um
                          join MODEL    mm on (mm.model_id = um.ModelId)
                          order by um.ModelId  ";

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);
            return _ret;
        }

        public static DataTable getFolders()
        {
            string _sql = @"
                       	SELECT E.EntityId as FolderId, E.EntityGuid, EV.EntityVersionName as FolderName, EV.EntityVersionDescription 
	                      FROM Entity E 
	                      JOIN EntityVersion EV on (EV.EntityId=E.EntityId AND EV.EntityTypeId=E.EntityTypeId AND EV.EntityVersionId=E.EntityLastVersionId)
	                     WHERE E.EntityTypeId=8
                         ORDER BY EV.EntityVersionName  ";

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);
            return _ret;
        }

        public static DataTable FindObjects(int pModelId=1, int pObjectTyoeId = 0, int pFolderId=0, string pSearchPattern="")
        {
            string _sql = $@"
                            WITH t_folders as
	                            (
	                              SELECT MEP.EntityTypeId, MEP.EntityId, MEP.ModelId, EV.EntityVersionId, 
	                                     E.EntityGuid AS FolderGuid, E.EntityId as FolderId,
			                             EV.EntityVersionName as FolderName
	                                FROM ModelEntityProperty MEP 
	                                JOIN Entity                E ON (E.EntityTypeId=8  AND E.EntityId=MEP.ModelEntityPropertyValue)
	                                JOIN EntityVersion        EV ON (EV.EntityTypeId=E.EntityTypeId AND EV.EntityId=E.EntityId AND EV.EntityVersionId=E.EntityLastVersionId)
	                               WHERE MEP.ModelEntityPropertyId=11 
	                                 AND MEP.ModelId={pModelId}
	                            )
	                            SELECT E.EntityGuid ,
	                                   EV.EntityVersionName AS ObjectName, 
		                               EV.EntityVersionDescription AS ObjectDecription,
		                               et.EntityTypeName AS ObjectType,
		                               FL.FolderName,
                                       E.EntityId, E.EntityTypeId
	                              FROM Entity          E 
	                              JOIN EntityVersion  EV ON (EV.EntityId=E.EntityId AND EV.EntityTypeId=E.EntityTypeId AND EV.EntityVersionId=E.EntityLastVersionId)
	                              JOIN EntityType     ET ON (ET.EntityTypeId=E.EntityTypeId)
	                              LEFT JOIN t_folders FL ON (FL.EntityTypeId=E.EntityTypeId AND FL.EntityId=E.EntityId AND FL.EntityVersionId=EV.EntityVersionId)
	                            WHERE E.EntityTypeId IN ( 24,	26,	30,	31,	32,	33,	35,	36,	37,	38,	40,	43,	48,	88,	95,	108, 109)
                                  AND EV.EntityVersionName LIKE '%{pSearchPattern}%'
	                              AND (E.EntityTypeId = {pObjectTyoeId} OR {pObjectTyoeId} <= 0 )
	                              AND (FL.FolderId = {pFolderId} OR {pFolderId} <=0 )
                                ORDER BY EV.EntityVersionName";


            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);
            return _ret;

        }



        public static DataTable getTables(int IdModel, string pSearchPatterns = "")
        {
            string _sql = $@"  SELECT o.obj_id AS table_id,  O.obj_name AS table_name , O.obj_title, 'TABLE' as ObjectType,
                                      o.obj_id AS EntityId, 34 AS EntityTypeId
                                 FROM OBJECT  O
                                 WHERE O.obj_class = 7
                                   AND O.model_id = {IdModel}
                                   AND (O.obj_name LIKE '%{pSearchPatterns}%')
                                ORDER BY O.obj_name";

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);
            return _ret;
        }

        public static DataTable getObjects(int IdModel)
        {
            string _sql = $@"SELECT o.obj_id AS table_id,  O.obj_name AS table_name , O.obj_title 
                                 FROM OBJECT  O
                                 WHERE O.obj_class = 7
                                   AND O.model_id = {IdModel}
                                ORDER BY O.obj_name";

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);
            return _ret;
        }


        public static DataTable getColumns(int pIdModel, int pTableId)
        {
            string _sql = $@"
                    WITH t_fields AS
                    (
                        SELECT iaa.model_id AS ModelId, iaa.table_id AS TableId, iaa.index_position AS Position,
                               SUBSTR(aa.attri_name, 1, 10) AS attri_name, aa.attri_type, aa.length, aa.decimals, aa.header
                        FROM  [INDEX] i 
                        JOIN  [IDX_ATRI] iaa ON (iaa.model_id = i.model_id AND iaa.table_id = i.table_id AND iaa.index_id = i.index_id)
                        JOIN  [ATTRIBUTE] aa ON (aa.model_id = iaa.model_id AND aa.attri_num = iaa.attri_num)
                        WHERE i.model_id = {pIdModel} AND i.table_id = {pTableId} AND i.index_type = 1
                        UNION ALL
                        SELECT ta.model_id, ta.table_id, 100 + ta.PositionRcfd AS Position,
                               SUBSTR(aa.attri_name, 1, 10) AS attri_name, aa.attri_type, aa.length, aa.decimals, aa.header
                        FROM [TBL_ATRI] ta
                        JOIN [ATTRIBUTE] aa ON (aa.model_id = ta.model_id AND aa.attri_num = ta.TBAattri_num)
                        WHERE ta.model_id = {pIdModel} AND ta.table_id = {pTableId}
                    )
                    SELECT t.* , 
                            CASE t.attri_type
                                WHEN 4 THEN 'NUMBER(' || t.length || ',' || t.decimals || ')'  
                                WHEN 5 THEN 'CHAR(' || length || ')'
                                WHEN 6 THEN 'DATE'
                                ELSE 'VARCHAR2(50)'
                            END AS tipo
                    FROM t_fields t ORDER BY Position";

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);
            return _ret;
        }

        public static DataTable getIndexes(int pIdModel, int pTableId)
        {
            string _sql = $@"
                      SELECT i.index_id, i.index_name, i.index_type
                        FROM [INDEX] i
                       WHERE i.model_id={pIdModel} AND i.table_id={pTableId}
                       ORDER BY i.index_name asc";

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);
            return _ret;
        }

        public static DataTable getColumnsIndex(int pIdModel, int pTableId, int pIndexId)
        {
            string _sql = $@"
                      SELECT SUBSTR(aa.attri_name, 1, 10) AS attri_name
                        FROM [INDEX] i
                        JOIN [IDX_ATRI] iaa ON iaa.model_id=i.model_id AND iaa.table_id=i.table_id AND iaa.index_id=i.index_id
                        JOIN [ATTRIBUTE] aa ON aa.model_id=iaa.model_id AND aa.attri_num=iaa.attri_num
                       WHERE i.model_id={pIdModel} AND i.table_id={pTableId} AND i.index_id={pIndexId}
                       ORDER BY iaa.index_position";

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);
            return _ret;
        }


        public static DataTable getTablesByAttrib(int pModelId, string pTextSearch)
        {
            string _sql = $@"
                    SELECT  ta.model_id,
                            ta.table_id,
		                    t.obj_name as table_name,
		                    t.obj_title as table_title,
		                    SUBSTR(a.attri_name, 1, 10) AS attri_name,
		                    a.header AS attri_title,
		                    a.attri_type,
		                    a.length,
		                    a.decimals,
                            CASE attri_type
                                    WHEN 4 THEN 'NUMBER(' || length || ',' || decimals || ')'   -- Numérico
                                    WHEN 5 THEN 'CHAR(' || length || ')'                        -- Carácter
                                    WHEN 6 THEN 'DATE'                                         -- Fecha
                                    ELSE 'VARCHAR2(50)'                                        -- Default
                                END AS tipo
	                    FROM [TBL_ATRI] ta
	                    JOIN [ATTRIBUTE] a ON a.model_id = ta.model_id AND a.attri_num = ta.TBAattri_num
	                    JOIN [OBJECT] t  ON t.model_id = ta.model_id  AND t.obj_id = ta.table_id
                        WHERE ta.model_id = {pModelId}
                          AND  (
                                UPPER(a.attri_name) LIKE UPPER('%{pTextSearch}%')
                                OR UPPER(a.header) LIKE UPPER('%{pTextSearch}%')
                              )
                        ORDER BY t.obj_name, a.attri_name
                        ";

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);
            return _ret;
        }


        /* para Generar XML - Deistributed */
        public static List<GxObjectProperty> getProperties(int pIdModel, int pEntityTypeId, int pEntityId)
        {
            string _sql = $@"
                    SELECT E.EntityTypeId            ,
	                       MEP.ModelEntityPropertyId as PropertyId,
	                       E.EntityGuid AS PropertyName, 
	                       MEP.ModelEntityPropertyValue As PropertyValue,
	                       mep.ModelEntityPropertyValueShort As PropertyValueShort

                      FROM ModelEntityProperty MEP 
                      JOIN Entity                E ON (E.EntityTypeId=3 AND E.EntityId=MEP.ModelEntityPropertyId)
                      JOIN EntityVersion        EV ON (EV.EntityTypeId=E.EntityTypeId AND EV.EntityId=E.EntityId AND EV.EntityVersionId=E.EntityLastVersionId)
                     WHERE mep.EntityTypeId = {pEntityTypeId}
                       AND MEP.EntityId = {pEntityId} 
                       AND MEP.ModelId  = {pIdModel} "
                       ;

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);

            return _ret.AsEnumerable().Select(dr => new GxObjectProperty
            {
                EntityTypeId = Convert.ToInt32(dr["EntityTypeId"]),
                PropertyId = Convert.ToInt32(dr["PropertyId"]),
                Name = dr["PropertyName"].ToString(),
                Value = dr["PropertyValue"] == DBNull.Value
                        ? null
                        : dr["PropertyValue"].ToString(),
                ShortValue = dr["PropertyValueShort"] == DBNull.Value
                        ? null
                        : dr["PropertyValueShort"].ToString()
            }).ToList();

        }

        public static List<GxObjectPart> getComposition(int pIdModel, int pEntityTypeId, int pEntityId)
        {
            string _sql = $@"
                     SELECT E2.EntityGuid, EV2.EntityTypeId, 
                            EV2.EntityId, EV2.EntityVersionId, EV2.EntityVersionName, EV2.EntityVersionProperties,
		                    EV2.EntityVersionData 
                       FROM Entity         E
                       JOIN EntityVersion EV ON (EV.EntityTypeId=E.EntityTypeId AND EV.EntityId=E.EntityId AND EV.EntityVersionId=E.EntityLastVersionId)
                       JOIN EntityVersionComposition EVC ON (EVC.CompoundEntityTypeId=EV.EntityTypeId AND EVC.CompoundEntityId=EV.EntityId AND EVC.CompoundEntityVersionId=EV.EntityVersionId)
                       LEFT JOIN Entity    E2 ON (E2.EntityTypeId=EVC.ComponentEntityTypeId AND E2.EntityId=EVC.ComponentEntityId ) 
                       LEFT JOIN EntityVersion EV2 ON (EV2.EntityTypeId=E2.EntityTypeId AND EV2.EntityId=E2.EntityId AND EV2.EntityVersionId=E2.EntityLastVersionId)
                     WHERE  E.EntityTypeId = {pEntityTypeId} AND E.EntityId={pEntityId}"
                     ;

            DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);

            return _ret.AsEnumerable().Select(dr => new GxObjectPart
            {
                EntityGuid = Guid.Parse(dr["EntityGuid"].ToString()),
                EntityTypeId = Convert.ToInt32(dr["EntityTypeId"]),
                EntityId = Convert.ToInt32(dr["EntityId"]),
                EntityVersionId = Convert.ToInt32(dr["EntityVersionId"]),
                Name = dr["EntityVersionName"].ToString(),
                RawData = dr["EntityVersionData"] == DBNull.Value  ? null    : (byte[])dr["EntityVersionData"]
            }).ToList();

            //return _ret;
        }

        public static GxObjectMetadata getMetadata(int pIdModel, int pEntityTypeId, int pEntityId)
        {
            string _sql = $@"
                     WITH t_folders as
                    (
                        SELECT MEP.EntityTypeId, MEP.EntityId, MEP.ModelId, EV.EntityVersionId, 
                                E.EntityGuid AS FolderGuid, E.EntityId as FolderId,
                                EV.EntityVersionName as FolderName
                        FROM ModelEntityProperty MEP 
                        JOIN Entity                E ON (E.EntityTypeId=8  AND E.EntityId=MEP.ModelEntityPropertyValue)
                        JOIN EntityVersion        EV ON (EV.EntityTypeId=E.EntityTypeId AND EV.EntityId=E.EntityId AND EV.EntityVersionId=E.EntityLastVersionId)
                        WHERE MEP.ModelEntityPropertyId=11 
	                        AND MEP.EntityTypeId = {pEntityTypeId}
	                        AND MEP.EntityId = {pEntityId}
                            AND MEP.ModelId= {pIdModel}
                    )
                    SELECT E.EntityGuid ,
                            EV.EntityVersionName AS ObjectName, 
                            EV.EntityVersionDescription AS ObjectDecription,
                            et.EntityTypeName AS ObjectType,
                            FL.FolderGuid, FL.FolderName,
                            E.EntityId, E.EntityTypeId,
                            'GuidType' as Type,
                            'parentGuid' as parentGuid,
                            '2026-01-20' as versionDate,
                            '2026-01-20' as lastUpdate,
                            'checksum' as checksum
                        FROM Entity          E 
                        JOIN EntityVersion  EV ON (EV.EntityId=E.EntityId AND EV.EntityTypeId=E.EntityTypeId AND EV.EntityVersionId=E.EntityLastVersionId)
                        JOIN EntityType     ET ON (ET.EntityTypeId=E.EntityTypeId)
                        LEFT JOIN t_folders FL ON (FL.EntityTypeId=E.EntityTypeId AND FL.EntityId=E.EntityId AND FL.EntityVersionId=EV.EntityVersionId)
                    WHERE E.EntityTypeId = {pEntityTypeId}
	                    AND E.EntityId = {pEntityId} "
                     ;
                     
            DataTable dt = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];

            var obj = new GxObjectMetadata
            {
                Guid = row.Field<string>("EntityGuid"),
                Name = row.Field<string>("ObjectName"),
                FullyQualifiedName = row.Field<string>("ObjectName"),
                Description = row.Field<string>("ObjectDecription"),
                TypeGuid = row.Field<string>("type"),
                ParentGuid = row.Field<string>("parentGuid"),
                ParentName = row.Field<string>("FolderName"),
                ParentTypeGuid = "",//row.Field<string>("parentType"),
                VersionDate = row.Field<string>("versionDate"),
                LastUpdate = row.Field<string>("lastUpdate"),
                Checksum = row.Field<string>("checksum"),
                User = $"{Environment.UserDomainName}\\{Environment.UserName}"
            };

            return obj;

        }


        public static GxDependencyReference getDependency(int pIdModel, int pEntityTypeId, int pEntityId)
        {
            return new GxDependencyReference() { PackageGuId="PackageId", Id= pEntityId.ToString(), Type= pEntityTypeId.ToString(), Properties= new GxDependencyProperties() { Name=pEntityId.ToString(), PackageName="GenexusBL" } };
            //string _sql = $@"
            //         SELECT E2.EntityGuid, EV2.EntityTypeId, 
            //                EV2.EntityId, EV2.EntityVersionId, EV2.EntityVersionName, EV2.EntityVersionProperties,
            //          EV2.EntityVersionData 
            //           FROM Entity         E
            //           JOIN EntityVersion EV ON (EV.EntityTypeId=E.EntityTypeId AND EV.EntityId=E.EntityId AND EV.EntityVersionId=E.EntityLastVersionId)
            //           JOIN EntityVersionComposition EVC ON (EVC.CompoundEntityTypeId=EV.EntityTypeId AND EVC.CompoundEntityId=EV.EntityId AND EVC.CompoundEntityVersionId=EV.EntityVersionId)
            //           LEFT JOIN Entity    E2 ON (E2.EntityTypeId=EVC.ComponentEntityTypeId AND E2.EntityId=EVC.ComponentEntityId ) 
            //           LEFT JOIN EntityVersion EV2 ON (EV2.EntityTypeId=E2.EntityTypeId AND EV2.EntityId=E2.EntityId AND EV2.EntityVersionId=E2.EntityLastVersionId)
            //         WHERE  E.EntityTypeId = {pEntityTypeId} AND E.EntityId={pEntityId}"
            //         ;

            //DataTable _ret = DBOQuerys.ReadQuery(dboFactory.GetFactory(), dboFactory.GetCurrentConnection(), _sql);

            //return _ret.AsEnumerable().Select(dr => new GxDependencyReference
            //{
            //    EntityGuid = Guid.Parse(dr["EntityGuid"].ToString()),
            //    EntityTypeId = Convert.ToInt32(dr["EntityTypeId"]),
            //    EntityId = Convert.ToInt32(dr["EntityId"]),
            //    EntityVersionId = Convert.ToInt32(dr["EntityVersionId"]),
            //    Name = dr["EntityVersionName"].ToString(),
            //    RawData = dr["EntityVersionData"] == DBNull.Value ? null : (byte[])dr["EntityVersionData"]
            //}).ToList()[0];

            //return _ret;
        }


        //


    }
}
